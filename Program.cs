using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.IO;
using Scene.Utilities;
using Scene.Defaults;

namespace Render
{
    public class Window : GameWindow
    {
        public VoxelGrid voxelGrid;
        public Camera camera;
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            voxelGrid = new VoxelGrid();
            camera = new Camera();
            camera.Transform.Position.SetLocalPosition(new Vector3(0, -0.5f, -10));

            GL.ClearColor(1, 1, 1, 1);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            voxelGrid.Draw(camera);

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
