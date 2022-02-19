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

                public Vector3 Local
                {
                    get { return position; }
                    set { position = value; }
                }

                public Vector3 Global
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
                            var parentScale = parent.Transform.Scale.Global;
                            var parentRotation = parent.Transform.Rotation.Global;
                            var parentPosition = parent.transform.Position.Global;

                            var positionQ = new Quaternion(
                                parentScale.X * Local.X, 
                                parentScale.Y * Local.Y,
                                parentScale.Z * Local.Z, 
                                0
                            );

                            var rotatedPos = (parentRotation * positionQ * parentRotation.Inverted()).Xyz;
                            return parentPosition + rotatedPos;
                        }
                    }
                }
            }
        }
    }
}