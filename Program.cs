using System;
using Scene.SceneObject;

namespace Render
{
    class Player : SceneObject
    {
        public Player(string name = "new object") : base(name) { }

        public override string ToString()
        {
            string str = Name + " -> [";
            foreach (var child in Hierarchy.GetChildren())
            {
                str += child.ToString();
            }
            str += "]; ";

            return str;
        }
    }

    class MainClass
    {
        public static void Main()
        {
            var player1 = new Player("player1");

            Console.WriteLine(player1);
            Console.ReadKey();
        }
    }
}
