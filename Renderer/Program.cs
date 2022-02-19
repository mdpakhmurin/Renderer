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
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            voxelGrid = new VoxelGrid();
            camera = new Camera();
            camera.Transform.Position.Local = new Vector3(0, 0, 8);

            voxelGrid.Data.GenerateGrid(new Vector3i(5, 5, 1));
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    voxelGrid.Data.SetVoxel(new Vector3i(x, y, 0), new Vector3(1, 1, 0));
                }
            }
            voxelGrid.Data.SetVoxel(new Vector3i(1, 1, 0), new Vector3(0, 0, 0));
            voxelGrid.Data.SetVoxel(new Vector3i(3, 1, 0), new Vector3(0, 0, 0));
            voxelGrid.Data.SetVoxel(new Vector3i(1, 3, 0), new Vector3(0, 0, 0));
            voxelGrid.Data.SetVoxel(new Vector3i(2, 3, 0), new Vector3(0, 0, 0));
            voxelGrid.Data.SetVoxel(new Vector3i(3, 3, 0), new Vector3(0, 0, 0));

            voxelGridContainer = new SceneObject();
            voxelGridContainer.Hierarchy.AddChild(voxelGrid);
            voxelGrid.Transform.Position.Local = new Vector3(2.5f, 2.5f, 0.5f);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            voxelGrid.Draw(camera);
            //voxelGridContainer.Transform.Rotation.Local *= new Quaternion(0, 0, 0.1f);
            voxelGridContainer.Transform.Rotation.Local *= Quaternion.FromEulerAngles(0.01f, 0.01f, 0.01f);

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

            var vector = new Vector3(2, 2, 0);
            var quat = Quaternion.FromEulerAngles(0, 0, (float) (45.0f*Math.PI/180) );
            Console.WriteLine(quat*new Quaternion(vector,0)*quat.Inverted());
        }
    }
}
