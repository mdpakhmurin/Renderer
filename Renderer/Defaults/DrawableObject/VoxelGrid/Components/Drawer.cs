using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.IO;
using Scene.Utilities;

namespace Scene.Defaults
{
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
                    Matrix4.CreateScale(camera.Transform.Scale.Global) *
                    Matrix4.CreateFromQuaternion(camera.Transform.Rotation.Global) *
                    Matrix4.CreateTranslation(camera.Transform.Position.Global) *
                    Matrix4.CreateTranslation(voxelGrid.Transform.Position.Global).Inverted() *
                    Matrix4.CreateFromQuaternion(voxelGrid.Transform.Rotation.Global).Inverted() *
                    Matrix4.CreateScale(voxelGrid.Transform.Scale.Global).Inverted();

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
}
