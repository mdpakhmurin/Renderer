using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.IO;
using Scene.Utilities;
using Scene.Defaults;
using Scene.SceneObject;

namespace Render
{
    public class Window : GameWindow
    {
        public VoxelGrid voxelGrid;
        public SceneObject voxelGridContainer;
        public Camera camera;
        bool move = false;
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            voxelGrid = new VoxelGrid();
            camera = new Camera();
            camera.Transform.Position.Local = new Vector3(0, 0, 10);
            Random rand = new Random();

            voxelGrid.Data.GenerateGrid(new Vector3i(3, 3, 3));
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        voxelGrid.Data.SetVoxel(new Vector3i(x, y, z), new Vector3((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble()));
                    }
                }
            }
            voxelGrid.Data.SetVoxel(new Vector3i(0, 0, 0), new Vector3(1, 1, 1));
            voxelGrid.Data.SetVoxel(new Vector3i(1, 0, 0), new Vector3(1, 0, 0));
            voxelGrid.Data.SetVoxel(new Vector3i(2, 0, 0), new Vector3(0, 1, 0));
            voxelGrid.Data.SetVoxel(new Vector3i(0, 1, 0), new Vector3(0, 0, 1));
            voxelGrid.Data.SetVoxel(new Vector3i(2, 2, 2), new Vector3(1, 1, 1));


            voxelGridContainer = new SceneObject();
            voxelGridContainer.Hierarchy.AddChild(voxelGrid);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            voxelGrid.Draw(camera);
            voxelGridContainer.Transform.Rotation.Local *= Quaternion.FromEulerAngles(0.005f, 0.005f, 0.005f);

            SwapBuffers();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.W)
            {
                var rot = camera.Transform.Rotation.Local;
                camera.Transform.Position.Local += (rot * new Quaternion(0, 0, 1, 0) * rot.Inverted()).Xyz;
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.S)
            {
                var rot = camera.Transform.Rotation.Local;
                camera.Transform.Position.Local += (rot * new Quaternion(0, 0, -1, 0) * rot.Inverted()).Xyz;
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.A)
            {
                var rot = camera.Transform.Rotation.Local;
                camera.Transform.Position.Local += (rot * new Quaternion(-1, 0, 0, 0) * rot.Inverted()).Xyz;
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.D)
            {
                var rot = camera.Transform.Rotation.Local;
                camera.Transform.Position.Local += (rot * new Quaternion(1, 0, 0, 0) * rot.Inverted()).Xyz;
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space)
            {
                camera.Transform.Position.Local += new Vector3(0, 1, 0);
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift)
            {
                camera.Transform.Position.Local += new Vector3(0, -1, 0);
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Left)
            {
                camera.Transform.Rotation.Local *= new Quaternion(0, -0.1f,0);
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Right)
            {
                camera.Transform.Rotation.Local *= new Quaternion(0, 0.1f, 0);
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Up)
            {
                camera.Transform.Rotation.Local *= new Quaternion(-0.1f, 0, 0);
            }
            if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Down)
            {
                camera.Transform.Rotation.Local *= new Quaternion(0.1f, 0, 0);
            }
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

            var vector = new Vector3(2, 2, 0);
            var quat = Quaternion.FromEulerAngles(0, 0, (float) (45.0f*Math.PI/180) );
            Console.WriteLine(quat*new Quaternion(vector,0)*quat.Inverted());
        }
    }
}
