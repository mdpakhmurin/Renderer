using OpenTK.Mathematics;
using System;

namespace Scene.SceneObject
{
    public partial class Transform
    {
        /// <summary>
        /// Положение объекта на сцене.
        /// </summary>
        public class _Position
        {
            // Все трансформации объекта.
            private Transform transform;

            // Позиция объекта, относительно родителя
            Vector3 position;

            public _Position(Transform transform)
            {
                this.transform = transform;
                position = new Vector3(0, 0, 0);
            }

            /// <summary>
            /// Все трансформации объекта.
            /// </summary>
            public Transform Transform
            {
                get { return transform; }
            }

            /// <summary>
            /// Получает абсолютное положение объекта.
            /// ПРЕДУПРЕЖДЕНИЕ: Временно возвращает локальную позицию
            /// </summary>
            /// <returns>Положение объекта (x, y, z).</returns>
            public Vector3 GetAbsolutePosition()
            {
                return position;
            }

            /// <summary>
            /// Устанавливает абсолютное положение объекта.
            /// </summary>
            /// <param name="position">Положение объекта (x, y, z).</param>
            public void SetAbsolutePosition(Vector3 position)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Получает положение объекта, относительно его родителя.
            /// </summary>
            /// <returns>Положение объекта (x, y, z).</returns>
            public Vector3 GetLocalPosition()
            {
                return position;
            }

            /// <summary>
            /// Устанавливает получает положение объекта, относительно его родителя.
            /// </summary>
            /// <param name="position">Положение объекта (x, y, z).</param>
            public void SetLocalPosition(Vector3 position)
            {
                this.position = position;
            }
        }
    }
}