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
                    rotation = new Quaternion(0, 0, 0);
                }

                /// <summary>
                /// Все трансформации объекта.
                /// </summary>
                public _Transform Transform
                {
                    get { return transform; }
                }

                public Quaternion Quaternion
                {
                    get { return rotation; }
                    set { rotation = value; }
                }

                public Matrix3 Matrix{
                    get { return Matrix3.CreateFromQuaternion(rotation); }
                    set { rotation = Quaternion.FromMatrix(value); }
                }

                public Vector3 EulerAngles
                {
                    get { return rotation.ToEulerAngles(); }
                    set { rotation = Quaternion.FromEulerAngles(value); }
                }

                public Vector4 AxisAngle
                {
                    get { return rotation.ToAxisAngle(); }
                    set { rotation = Quaternion.FromAxisAngle(value.Xyz, value.W); }
                }
            }
        }
    }
}