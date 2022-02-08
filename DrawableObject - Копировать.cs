//using OpenTK.Graphics.OpenGL4;
//using OpenTK.Mathematics;
//using OpenTK.Windowing.Common;
//using OpenTK.Windowing.Desktop;
//using System;
//using System.IO;
//using Scene.Utilities;

//namespace Scene.Defaults
//{
//    public class DrawableObject
//    {
//        virtual public void Draw(Camera camera) { }
//    }

//    public class VoxelGrid : DrawableObject
//    {
//        private ShaderProgram shaderProgram;
//        private int vbo;
//        private int vao;


//        private void initVBO()
//        {
//            // Координаты углов "экрана" отрисовки
//            //
//            // Экран - 3х мерная плоскость (состояющая из двух треугольников)
//            // на которой будет происходить отрисовка
//            // 
//            // Каждая строчка - координаты x,y соответствующего угла
//            // (координата z устанавливается в 0 в шейдере)
//            float[] canvasVerticesPos =
//            {
//                -0.9f, -0.9f,
//                 0.9f, -0.9f,
//                 0.9f,  0.9f,
//                 0.9f,  0.9f,
//                -0.9f,  0.9f,
//                -0.9f, -0.9f
//            };


//            int vbo = GL.GenBuffer();

//            // Установка активного VBO для всей программы
//            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

//            // Загрузка данных в VBO
//            GL.BufferData(
//                BufferTarget.ArrayBuffer,
//                canvasVerticesPos.Length * sizeof(float),
//                canvasVerticesPos,
//                BufferUsageHint.StaticDraw);
//        }

//        private void initVAO()
//        {
//            vao = GL.GenVertexArray();

//            // Установка активного VBO для всей программы
//            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
//            GL.BindVertexArray(vao);
//            GL.EnableVertexAttribArray(0);
//            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
//            GL.BindVertexArray(0);
//        }

//        private void initShaderProgram()
//        {
//            var vertexShader = new VertexShader(File.ReadAllText(@"Resources\Shaders\VertexShader.glsl"));
//            var fragmentShader = new FragmentShader(File.ReadAllText(@"Resources\Shaders\FragmentShader.glsl"));

//            shaderProgram = new ShaderProgram(vertexShader, fragmentShader);
//        }

//        public VoxelGrid()
//        {
//            initVBO();
//            initVAO();
//            initShaderProgram();
//        }

//        override public void Draw(Camera camera)
//        {
//            base.Draw(camera);

//            shaderProgram.Use();

//            GL.Uniform3(
//                shaderProgram.GetUniform("cameraPosition"),
//                camera.Transform.Position.GetAbsolutePosition());

//            GL.BindVertexArray(vao);
//            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
//            GL.BindVertexArray(0);
//        }
//    }
//}
