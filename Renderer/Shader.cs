using OpenTK.Graphics.OpenGL4;
using System;

namespace Scene.Utilities
{
    public abstract class Shader
    {
        protected int id;
        public Shader(string shaderSourceCode) { }

        public int Id
        {
            get { return id; }
        }

        protected static int compileShader(ShaderType shaderType, string shaderSourceCode)
        {
            var shader = GL.CreateShader(shaderType);
            GL.ShaderSource(shader, shaderSourceCode);
            GL.CompileShader(shader);

            var shaderCompileLog = GL.GetShaderInfoLog(shader);
            if (shaderCompileLog != "")
                throw new Exception("Ошибка компиляции шейдера " + shaderCompileLog);

            return shader;
        }
    }

    public class VertexShader : Shader
    {
        public VertexShader(string shaderSourceCode) : base(shaderSourceCode)
        {
            id = compileShader(ShaderType.VertexShader, shaderSourceCode);
        }
    }

    public class FragmentShader : Shader
    {
        public FragmentShader(string shaderSourceCode) : base(shaderSourceCode)
        {
            id = compileShader(ShaderType.FragmentShader, shaderSourceCode);
        }
    }

    public class ShaderProgram
    {
        private int id;

        private VertexShader vertexShader;
        private FragmentShader fragmentShader;
        public ShaderProgram(VertexShader vertexShader, FragmentShader fragmentShader)
        {
            id = GL.CreateProgram();

            VertexShader = vertexShader;
            FragmentShader = fragmentShader;
        }

        public int Id
        {
            get { return id; }
        }

        public VertexShader VertexShader
        {
            get { return vertexShader; }
            set
            {
                vertexShader = value;
                GL.AttachShader(id, vertexShader.Id);
                GL.LinkProgram(id);
            }
        }

        public FragmentShader FragmentShader
        {
            get { return fragmentShader; }
            set
            {
                fragmentShader = value;
                GL.AttachShader(id, fragmentShader.Id);
                GL.LinkProgram(id);
            }
        }

        public void Use()
        {
            GL.UseProgram(id);
        }

        public int GetUniform(string name)
        {
            return GL.GetUniformLocation(id, name);
        }
    }
}
