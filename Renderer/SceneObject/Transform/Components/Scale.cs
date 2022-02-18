using OpenTK.Mathematics;
using System;

namespace Scene.SceneObject
{
    public partial class SceneObject
    {
        public partial class _Transform
        {
            public class _Scale
            {
                // Все трансформации объекта.
                _Transform transform;

                // Размер объекта, относительно родителя.
                Vector3 scale;

                public _Scale(_Transform transform)
                {
                    this.transform = transform;
                    scale = new Vector3(1, 1, 1);
                }

                /// <summary>
                /// Все трансформации объекта.
                /// </summary>
                public _Transform Transform
                {
                    get { return transform; }
                }

                public Vector3 Scale
                {
                    get { return scale; }
                    set { scale = value; }
                }
            }
        }
    }
}