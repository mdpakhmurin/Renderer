using OpenTK.Mathematics;
using System;

namespace Scene.SceneObject
{
    public partial class Transform
    {
        /// <summary>
        /// Вращение объекта
        /// </summary>
        public class _Rotation
        {
            // Все трансформации объекта.
            private Transform transform;

            // Последовательное вращение объекта, относительно родителя.
            private Vector3 rotation;

            public _Rotation(Transform transform)
            {
                this.transform = transform;
                rotation = new Vector3(0, 0, 0);
            }

            /// <summary>
            /// Все трансформации объекта.
            /// </summary>
            public Transform Transform
            {
                get { return transform; }
            }

            /// <summary>
            /// Получает абсолютное вращение объекта.
            /// ПРЕДУПРЕЖДЕНИЕ: Временно возвращает локальное вращение
            /// </summary>
            /// <returns>Последовательное вращение объекта вокруг осей (x, y, z).</returns>
            public Vector3 GetAbsoluteRotation()
            {
                return rotation;
            }

            /// <summary>
            /// Устанавливает абсолютное вращение объекта.
            /// </summary>
            /// <param name="rotation">Последовательное вращение объекта вокруг осей (x, y, z).</param>
            public void SetAbsoluteRotation(Vector3 rotation)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Получает вращение объекта, относительно его родителя.
            /// </summary>
            /// <returns>Последовательное вращение объекта вокруг осей (x, y, z).</returns>
            public Vector3 GetLocalRotation()
            {
                return rotation;
            }

            /// <summary>
            /// Последовательное вращение объекта вокруг осей(x, y, z).
            /// </summary>
            /// <param name="rotation">Последовательное вращение объекта вокруг осей (x, y, z).</param>
            public void SetLocalRotation(Vector3 rotation)
            {
                this.rotation = rotation;
            }
        }
    }
}