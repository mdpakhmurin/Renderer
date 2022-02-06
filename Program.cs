using System;
using Scene.SceneObject;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Render
{
    public class Window : GameWindow
    {
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : 
            base(gameWindowSettings, nativeWindowSettings)
        {
        }
    }
    class MainClass
    {
        public static void Main()
        {
            var newWindow = new Window(GameWindowSettings.Default, NativeWindowSettings.Default);
            newWindow.Run();
        }
    }
}
