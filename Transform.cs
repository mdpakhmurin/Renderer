using System;
using GlmNet;

namespace Scene.Transform
{
    /// <summary>
    /// Трансформации объетка.
    /// </summary>
    public class Transform
    {
        // Позиция объекта.
        private Position position;

        // Вращение объекта.
        private Rotation rotation;

        // Размер объекта.
        private Scale scale;

        public Transform()
        {
            position = new Position(this);
            rotation = new Rotation(this);
            scale = new Scale(this);
        }

        /// <summary>
        /// Позиция объекта.
        /// </summary>
        public Position Position
        {
            get { return position; }
        }

        /// <summary>
        /// Вращение объекта.
        /// </summary>
        public Rotation Rotation
        {
            get { return rotation; }
        }

        /// <summary>
        /// Размер объекта.
        /// </summary>
        public Scale Scale
        {
            get { return scale; }
        }
    }

    /// <summary>
    /// Положение объекта на сцене.
    /// </summary>
    class Position
    {
        // Все трансформации объекта.
        private Transform transform;

        // Позиция объекта, относительно родителя
        vec3 position;

        public Position(Transform transform)
        {
            this.transform = transform;
            position = new vec3(0, 0, 0);
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
        /// </summary>
        /// <returns>Положение объекта (x, y, z).</returns>
        public vec3 GetAbsolutePosition()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Устанавливает абсолютное положение объекта.
        /// </summary>
        /// <param name="position">Положение объекта (x, y, z).</param>
        public void SetAbsolutePosition(vec3 position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получает положение объекта, относительно его родителя.
        /// </summary>
        /// <returns>Положение объекта (x, y, z).</returns>
        public vec3 GetLocalPosition()
        {
            return position;
        }

        /// <summary>
        /// Устанавливает получает положение объекта, относительно его родителя.
        /// </summary>
        /// <param name="position">Положение объекта (x, y, z).</param>
        public void SetLocalPosition(vec3 position)
        {
            this.position = position;
        }
    }

    /// <summary>
    /// Вращение объекта
    /// </summary>
    class Rotation
    {
        // Все трансформации объекта.
        private Transform transform;

        // Последовательное вращение объекта, относительно родителя.
        private vec3 rotation;

        public Rotation(Transform transform)
        {
            this.transform = transform;
            rotation = new vec3(0, 0, 0);
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
        /// </summary>
        /// <returns>Последовательное вращение объекта вокруг осей (x, y, z).</returns>
        public vec3 GetAbsoluteRotation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Устанавливает абсолютное вращение объекта.
        /// </summary>
        /// <param name="rotation">Последовательное вращение объекта вокруг осей (x, y, z).</param>
        public void SetAbsoluteRotation(vec3 rotation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получает вращение объекта, относительно его родителя.
        /// </summary>
        /// <returns>Последовательное вращение объекта вокруг осей (x, y, z).</returns>
        public vec3 GetLocalRotation()
        {
            return rotation;
        }

        /// <summary>
        /// Последовательное вращение объекта вокруг осей(x, y, z).
        /// </summary>
        /// <param name="rotation">Последовательное вращение объекта вокруг осей (x, y, z).</param>
        public void SetLocalRotation(vec3 rotation)
        {
            this.rotation = rotation;
        }

        public void RotateByAngle()
        {
            throw new NotImplementedException();
        }
    }

    class Scale
    {
        // Все трансформации объекта.
        Transform transform;

        // Размер объекта, относительно родителя.
        vec3 scale;

        public Scale(Transform transform)
        {
            this.transform = transform;
            scale = new vec3(1, 1, 1);
        }

        /// <summary>
        /// Все трансформации объекта.
        /// </summary>
        public Transform Transform
        {
            get { return transform; }
        }

        /// <summary>
        /// Получает абсолютный размер объекта.
        /// </summary>
        /// <returns>Размер объекта по каждой из осей (x, y, z).</returns>
        public vec3 GetAbsoluteScale()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Устанавливает абсолютный размер объекта.
        /// </summary>
        /// <param name="scale">Размер объекта по каждой из осей (x, y, z).</param>
        public void SetAbsoluteScale(vec3 scale)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получает размер объекта, относительно его родителя
        /// </summary>
        /// <returns>Размер объекта по каждой из осей (x, y, z).</returns>
        public vec3 GetLocalScale()
        {
            return scale;
        }

        /// <summary>
        /// Устанавливает размер объекта, относительно его родителя
        /// </summary>
        /// <param name="scale">Размер объекта по каждой из осей (x, y, z).</param>
        public void SetLocalScale(vec3 scale)
        {
            this.scale = scale;
        }
    }
}