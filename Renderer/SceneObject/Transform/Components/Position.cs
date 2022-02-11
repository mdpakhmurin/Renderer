using OpenTK.Mathematics;
using System;

namespace Scene.SceneObject
{
    public partial class SceneObject
    {
        public partial class _Transform
        {
            /// <summary>
            /// Положение объекта на сцене.
            /// </summary>
            public class _Position
            {
                // Все трансформации объекта.
                private _Transform transform;

                // Позиция объекта, относительно родителя
                Vector3 position;

                public _Position(_Transform transform)
                {
                    this.transform = transform;
                    position = new Vector3(0, 0, 0);
                }

                /// <summary>
                /// Все трансформации объекта.
                /// </summary>
                public _Transform Transform
                {
                    get { return transform; }
                }

                public Vector3 Position
                {
                    get { return position; }
                    set { position = value; }
                }
            }
        }
    }
}