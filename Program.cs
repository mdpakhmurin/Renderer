using System;
using TFM;

namespace Render
{
    class MainClass
    {
        public static void Main()
        {
            Transform tfm = new Transform();
            Console.WriteLine(tfm.Scale.GetLocalScale());

            Console.WriteLine("Hello World!");
        }
    }
}
