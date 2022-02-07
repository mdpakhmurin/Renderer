using System;
using Scene.SceneObject;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Render
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
    }

    public class FragmentShader : Shader
    {
        public FragmentShader(string shaderSourceCode) : base(shaderSourceCode)
        {
            id = compileShader(ShaderType.FragmentShader, shaderSourceCode);
        }
    }
    public class Window : GameWindow
    {
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
        }

        private float[] _vertices =
        {
            -0.9f, -0.9f, 0,
             0.9f, -0.9f, 0,
             0.9f,  0.9f, 0,
             0.9f,  0.9f, 0,
            -0.9f,  0.9f, 0,
            -0.9f, -0.9f, 0
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private ShaderProgram _program;

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(1, 1, 1, 1);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            var vertexShader = new VertexShader(File.ReadAllText(@"Resources\Shaders\VertexShader.glsl"));

            var fragmentShader = new FragmentShader(File.ReadAllText(@"Resources\Shaders\FragmentShader.glsl"));

            _program = new ShaderProgram(vertexShader, fragmentShader);
            _program.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            _program.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            SwapBuffers();
        }
    }
    class MainClass
    {
        public static void Main()
        {
            var windowSettings = new NativeWindowSettings();
            windowSettings.Size = new Vector2i(600, 600);
            var newWindow = new Window(GameWindowSettings.Default, windowSettings);
            newWindow.Run();
        }
    }
}
