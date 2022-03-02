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
        public VoxelGrid voxelGrid2;

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
            GL.ClearColor(Color4.Wheat);

            voxelGrid2 = new VoxelGrid();
            voxelGrid2.Data.GenerateGrid(new Vector3i(1, 1, 1));
            voxelGrid2.Data.SetVoxel(new Vector3i(0, 0, 0), new Vector3(1, 0, 0));
            voxelGrid2.Transform.Position.Local = new Vector3(-30, 0, 0);

            voxelGrid = new VoxelGrid();
            camera = new Camera();
            camera.Transform.Position.Local = new Vector3(0, 0, 40);
            camera.Transform.Rotation.Local = new Quaternion(0, MathF.PI, 0);
            Random rand = new Random();

            Vector3 r()
            {
                return new Vector3((float)rand.NextDouble() / 20);
            }

            //voxelGrid.Data.GenerateGrid(new Vector3i(3, 3, 4));
            //for (int x = 0; x < 5; x++)
            //    for (int y = 0; y < 5; y++)
            //        for (int z = 0; z < 5; z++)
            //            voxelGrid.Data.SetVoxel(new Vector3i(x, y, z), new Vector3(r().X, r().Y, r().Z));
            //voxelGrid.Data.SetVoxel(new Vector3i(0, 1, 1), new Vector3(1, 1, 1));


            // Дерево
            int mS = 16;
            voxelGrid.Data.GenerateGrid(new Vector3i(mS, mS, mS));
            for (int x = 0; x < mS; x++)
                for (int y = 0; y < mS; y++)
                    for (int z = 0; z < mS; z++)
                        voxelGrid.Data.SetVoxel(new Vector3i(x, y, z), new Vector3(0.6f, 0.4f, 0.1f) + r());

            for (int i = 0; i < voxelGrid.Data.Size.X; i++)
            {
                // Вертикали (по y)
                voxelGrid.Data.SetVoxel(new Vector3i(0, i, 0), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(mS - 1, i, 0), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(mS - 1, i, mS - 1), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(0, i, mS - 1), new Vector3(0.1f, 0.1f, 0.1f) + r());

                // Горизонтали (по x)
                voxelGrid.Data.SetVoxel(new Vector3i(i, 0, 0), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(i, mS - 1, 0), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(i, mS - 1, mS - 1), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(i, 0, mS - 1), new Vector3(0.1f, 0.1f, 0.1f) + r());
                // Линяя посередине
                voxelGrid.Data.SetVoxel(new Vector3i(i, mS - 5, 0), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(i, mS - 5, mS - 1), new Vector3(0.1f, 0.1f, 0.1f) + r());

                // Горизонтали (по z)
                voxelGrid.Data.SetVoxel(new Vector3i(0, 0, i), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(mS - 1, 0, i), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(mS - 1, mS - 1, i), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(0, mS - 1, i), new Vector3(0.1f, 0.1f, 0.1f) + r());
                // Линяя посередине
                voxelGrid.Data.SetVoxel(new Vector3i(mS - 1, mS - 5, i), new Vector3(0.1f, 0.1f, 0.1f) + r());
                voxelGrid.Data.SetVoxel(new Vector3i(0, mS - 5, i), new Vector3(0.1f, 0.1f, 0.1f) + r());
            }

            voxelGrid.Data.SetVoxel(new Vector3i(7, mS - 4, 15), new Vector3(0.8f, 0.8f, 0.8f) + r());
            voxelGrid.Data.SetVoxel(new Vector3i(8, mS - 4, 15), new Vector3(0.8f, 0.8f, 0.8f) + r());
            voxelGrid.Data.SetVoxel(new Vector3i(7, mS - 5, 15), new Vector3(0.8f, 0.8f, 0.8f) + r());
            voxelGrid.Data.SetVoxel(new Vector3i(8, mS - 5, 15), new Vector3(0.8f, 0.8f, 0.8f) + r());
            voxelGrid.Data.SetVoxel(new Vector3i(7, mS - 6, 15), new Vector3(0.8f, 0.8f, 0.8f) + r());
            voxelGrid.Data.SetVoxel(new Vector3i(8, mS - 6, 15), new Vector3(0.8f, 0.8f, 0.8f) + r());
            voxelGrid.Data.SetVoxel(new Vector3i(7, mS - 7, 15), new Vector3(0.8f, 0.8f, 0.8f) + r());
            voxelGrid.Data.SetVoxel(new Vector3i(8, mS - 7, 15), new Vector3(0.8f, 0.8f, 0.8f) + r());

            voxelGridContainer = new SceneObject();
            voxelGridContainer.Hierarchy.AddChild(voxelGrid);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            voxelGrid.Draw(camera);
            voxelGrid2.Draw(camera);
            //voxelGridContainer.Transform.Rotation.Local *= Quaternion.FromEulerAngles(0.005f, 0.005f, 0.005f);

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
