using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.IO;
using Scene.Utilities;

namespace Scene.Defaults
{
    public class DrawableObject : SceneObject.SceneObject
    {
        virtual public void Draw(Camera camera) { }
    }

    public partial class VoxelGrid : DrawableObject
    {
        private ShaderProgram shaderProgram;
        private _Drawer drawer;
        private _Data data;

        public VoxelGrid()
        {
            drawer = new _Drawer(this);
            data = new _Data(this);
        }

        override public void Draw(Camera camera)
        {
            base.Draw(camera);

            drawer.Draw(camera);
        }

        public _Data Data
        {
            get { return data; }
        }
    }

    public partial class VoxelGrid : DrawableObject
    {
        private class _Drawer
        {
            private VoxelGrid voxelGrid;
            private int vbo;
            private int vao;

            public _Drawer(VoxelGrid voxelGrid)
            {
                this.voxelGrid = voxelGrid;
                init();
            }

            /// <summary>
            /// Инициализирует VBO, занося в него данные
            /// описывающие экран отрисовки.
            /// </summary>
            private void initVBO()
            {
                // Координаты углов "экрана" отрисовки
                //
                // Экран - 3х мерная плоскость (состояющая из двух треугольников)
                // на которой будет происходить отрисовка
                // 
                // Каждая строчка - координаты x,y соответствующего угла
                // (координата z устанавливается в 0 в шейдере)
                float[] canvasVerticesPos =
                {
                    -1, -1f,
                     1, -1f,
                     1,  1f,
                     1,  1f,
                    -1,  1f,
                    -1, -1f
                };


                vbo = GL.GenBuffer();

                // Установка активного VBO для всей программы
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

                // Загрузка данных из canvasVerticesPos в VBO
                GL.BufferData(
                    BufferTarget.ArrayBuffer,
                    canvasVerticesPos.Length * sizeof(float),
                    canvasVerticesPos,
                    BufferUsageHint.StaticDraw);
            }

            /// <summary>
            /// Инициализирует VAO, используя данные из VBO
            /// </summary>
            private void initVAO()
            {
                // Установка активного VBO
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

                vao = GL.GenVertexArray();

                // Установка активного VAO
                GL.BindVertexArray(vao);

                // Указывает активный массив атрибутов вершин
                GL.EnableVertexAttribArray(0);
                // Связывает активный VBO с активным индексом VAO
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindVertexArray(0);
            }

            /// <summary>
            /// Загружает шейдеры и создает шейдерную программу
            /// </summary>
            private void initShaderProgram()
            {
                var vertexShader = new VertexShader(File.ReadAllText(@"Resources\Shaders\VertexShader.glsl"));
                var fragmentShader = new FragmentShader(File.ReadAllText(@"Resources\Shaders\FragmentShader.glsl"));

                voxelGrid.shaderProgram = new ShaderProgram(vertexShader, fragmentShader);
            }

            /// <summary>
            /// Начальная инициализация отрисовщика
            /// </summary>
            private void init()
            {
                initVBO();
                initVAO();
                initShaderProgram();
            }

            public void Draw(Camera camera)
            {
                voxelGrid.shaderProgram.Use();

                // Матрица вида
                var resultMat =
                    camera.Transform.Rotation.Matrix *
                    camera.Transform.Position.Matrix *
                    camera.Transform.Scale.Matrix *
                    voxelGrid.Transform.Position.Matrix.Inverted() *
                    voxelGrid.Transform.Rotation.Matrix.Inverted() *
                    voxelGrid.Transform.Scale.Matrix.Inverted();

                GL.UniformMatrix4(voxelGrid.shaderProgram.GetUniform("cameraView"),
                    false,
                    ref resultMat);

                // Параметры проекции камеры
                GL.Uniform2(voxelGrid.shaderProgram.GetUniform("cameraSize"), new Vector2(camera.Width, camera.Height));
                GL.Uniform2(voxelGrid.shaderProgram.GetUniform("cameraPlaneDist"), new Vector2(camera.Near, camera.Far));
                if (camera.Type == Camera.CameraType.Perspective)
                    GL.Uniform1(voxelGrid.shaderProgram.GetUniform("cameraIsPersp"), 1);
                else
                    GL.Uniform1(voxelGrid.shaderProgram.GetUniform("cameraIsPersp"), 0);

                // Использование VAO для отрисовки треугольников.
                // (2 треугольника образующих прямоугольник экрана отрисовки).
                GL.BindVertexArray(vao);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
                GL.BindVertexArray(0);
            }
        }
    }
    
    public partial class VoxelGrid : DrawableObject
    {
        public class _Data
        {
            // Примечание: В шейдерах 430 версии существует выравнивание блоков
            // данных: например для получения vec3 в шейдере, через ssbo,
            // в буфер необходимо передать vector4.
            private VoxelGrid voxelGrid;
            private int ssbo;

            public _Data(VoxelGrid voxelGrid)
            {
                this.voxelGrid = voxelGrid;
            }

            public void GenerateGrid(Vector3i size)
            {
                voxelGrid.shaderProgram.Use();

                // Сохранение размера воксельной сетки
                GL.Uniform3(voxelGrid.shaderProgram.GetUniform("voxelGridSize"), size.X, size.Y, size.Z);
                
                // Инициализация ssbo
                GL.DeleteBuffer(ssbo);
                ssbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, ssbo);
                GL.BufferData(BufferTarget.ShaderStorageBuffer, 16 * size.X * size.Y * size.Z, IntPtr.Zero, BufferUsageHint.DynamicDraw);
                GL.BindBuffersBase(BufferRangeTarget.ShaderStorageBuffer, 3, 1, ref ssbo);
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);
            }

            public void SetVoxel(Vector3i pos, Vector3 color)
            {
                // Получение размера сетки
                var size = new int[3];
                GL.GetUniform(voxelGrid.shaderProgram.Id, voxelGrid.shaderProgram.GetUniform("voxelGridSize"), size);

                // Проверка на выход за границы
                if (pos.X < 0 || pos.X >= size[0] || pos.Y < 0 || pos.Y >= size[1] || pos.Z < 0 || pos.Z >= size[2])
                    throw new IndexOutOfRangeException("Индекс выходит за границы сетки");

                // Установка данных
                var id = pos.X + pos.Y * size[0] + pos.Z * size[1] * size[2];
                GL.NamedBufferSubData(ssbo, new IntPtr(16 * id), 12, ref color);
            }

            public Vector3 GetVoxel(Vector3i pos)
            {
                // Получение размера сетки
                var size = new int[3];
                GL.GetUniform(voxelGrid.shaderProgram.Id, voxelGrid.shaderProgram.GetUniform("voxelGridSize"), size);

                // Проверка на выход за границы
                if (pos.X < 0 || pos.X >= size[0] || pos.Y < 0 || pos.Y >= size[1] || pos.Z < 0 || pos.Z >= size[2])
                    throw new IndexOutOfRangeException("Индекс выходит за границы сетки");

                // Получение данных
                Vector3 color = new Vector3(0, 0, 0);
                var id = pos.X + pos.Y * size[0] + pos.Z * size[1] * size[2];
                GL.GetNamedBufferSubData(ssbo, new IntPtr(16 * id), 12, ref color);

                return color;
            }

            public Vector3i Size
            {
                get
                {
                    var size = new int[3];
                    GL.GetUniform(voxelGrid.shaderProgram.Id, voxelGrid.shaderProgram.GetUniform("voxelGridSize"), size);

                    return new Vector3i(size[0], size[1], size[2]);
                }
            }
        }
    }
}
