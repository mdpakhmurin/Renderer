using System;
using Scene.SceneObject;

namespace Render
{
    class Player : SceneObject
    {
        public Player(string name = "new object") : base(name) { }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }

    class MainClass
    {
        public static void Main()
        {
            var player = new Player("player#0000");
        }
    }
}
