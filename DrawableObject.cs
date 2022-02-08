using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.IO;
using Scene.Utilities;

namespace Scene.Defaults
{
    public class DrawableObject
    {
        virtual public void Draw(Camera camera) { }
    }

    public class VoxelGrid : DrawableObject
    {
        private ShaderProgram shaderProgram;
        private int vao;

        public VoxelGrid()
        {
            float[] canvasVerticesPos =
            {
                -0.9f, -0.9f, 0,
                 0.9f, -0.9f, 0,
                 0.9f,  0.9f, 0,
                 0.9f,  0.9f, 0,
                -0.9f,  0.9f, 0,
                -0.9f, -0.9f, 0
            };

            int vbo = GL.GenBuffer();
            vao = GL.GenVertexArray();


            GL.BindVertexArray(vao);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                canvasVerticesPos.Length * sizeof(float),
                canvasVerticesPos,
                BufferUsageHint.StaticDraw);


            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.BindVertexArray(0);

            var vertexShader = new VertexShader(File.ReadAllText(@"Resources\Shaders\VertexShader.glsl"));
            var fragmentShader = new FragmentShader(File.ReadAllText(@"Resources\Shaders\FragmentShader.glsl"));

            shaderProgram = new ShaderProgram(vertexShader, fragmentShader);
        }

        override public void Draw(Camera camera) { 
            base.Draw(camera);

            shaderProgram.Use();

            GL.Uniform3(
                shaderProgram.GetUniform("cameraPosition"),
                camera.Transform.Position.GetAbsolutePosition());

            GL.BindVertexArray(vao);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.DisableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }
    }
}
