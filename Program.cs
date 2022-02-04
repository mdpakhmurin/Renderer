using System;
using Scene.Transform;

namespace Render
{
    class MainClass
    {
        public static void Main()
        {
            Transform tfm = new Transform();
            Console.WriteLine(tfm.Scale.GetLocalScale());
            //Position


            Console.WriteLine("Hello World!");
        }
    }
}
