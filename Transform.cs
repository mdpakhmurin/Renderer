﻿using OpenTK.Mathematics;
using System;

namespace Scene.Transform
{
    /// <summary>
    /// Трансформации объетка.
    /// </summary>
    public class Transform
    {
        // текущий объект (к которому привязана трансформация).
        private SceneObject.SceneObject sceneObject;

        // Позиция объекта.
        private Position position;

        // Вращение объекта.
        private Rotation rotation;

        // Размер объекта.
        private Scale scale;

        public Transform(SceneObject.SceneObject sceneObject)
        {
            this.sceneObject = sceneObject;
            position = new Position(this);
            rotation = new Rotation(this);
            scale = new Scale(this);
        }

        /// <summary>
        /// Текущий объект на сцене
        /// </summary>
        public SceneObject.SceneObject SceneObject
        {
            get { return sceneObject; }
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
    public class Position
    {
        // Все трансформации объекта.
        private Transform transform;

        // Позиция объекта, относительно родителя
        Vector3 position;

        public Position(Transform transform)
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

    /// <summary>
    /// Вращение объекта
    /// </summary>
    public class Rotation
    {
        // Все трансформации объекта.
        private Transform transform;

        // Последовательное вращение объекта, относительно родителя.
        private Vector3 rotation;

        public Rotation(Transform transform)
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

    public class Scale
    {
        // Все трансформации объекта.
        Transform transform;

        // Размер объекта, относительно родителя.
        Vector3 scale;

        public Scale(Transform transform)
        {
            this.transform = transform;
            scale = new Vector3(1, 1, 1);
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
        /// ПРЕДУПРЕЖДЕНИЕ: Временно возвращает локальный размер
        /// </summary>
        /// <returns>Размер объекта по каждой из осей (x, y, z).</returns>
        public Vector3 GetAbsoluteScale()
        {
            return scale;
        }

        /// <summary>
        /// Устанавливает абсолютный размер объекта.
        /// </summary>
        /// <param name="scale">Размер объекта по каждой из осей (x, y, z).</param>
        public void SetAbsoluteScale(Vector3 scale)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получает размер объекта, относительно его родителя
        /// </summary>
        /// <returns>Размер объекта по каждой из осей (x, y, z).</returns>
        public Vector3 GetLocalScale()
        {
            return scale;
        }

        /// <summary>
        /// Устанавливает размер объекта, относительно его родителя
        /// </summary>
        /// <param name="scale">Размер объекта по каждой из осей (x, y, z).</param>
        public void SetLocalScale(Vector3 scale)
        {
            this.scale = scale;
        }
    }
}