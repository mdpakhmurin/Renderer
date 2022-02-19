using OpenTK.Mathematics;
using System;

namespace Scene.SceneObject
{
    public partial class SceneObject
    {
        public partial class _Transform
        {
            /// <summary>
            /// Вращение объекта
            /// </summary>
            public class _Rotation
            {
                // Все трансформации объекта.
                private _Transform transform;

                // Вращение объекта.
                private Quaternion rotation;

                public _Rotation(_Transform transform)
                {
                    this.transform = transform;
                    rotation = new Quaternion(0, 0, 0, 1);
                }

                /// <summary>
                /// Все трансформации объекта.
                /// </summary>
                public _Transform Transform
                {
                    get { return transform; }
                }

                public Quaternion Local
                {
                    get { return rotation; }
                    set { rotation = value; }
                }

                public Quaternion Global
                {
                    get
                    {
                        var parent = transform.SceneObject.Hierarchy.Parent;
                        if (parent is null)
                        {
                            return Local;
                        }
                        else
                        {
                            return parent.transform.Rotation.Global * transform.Rotation.Local;
                        }
                    }
                }
            }
        }
    }
}