using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scene.SceneObject
{
    /// <summary>
    /// Объект на сцене, хранит в себе таких же потомков (Компоновщик)
    /// </summary>
    public abstract class SceneObject
    {
        // Имя объекта.
        private string name;

        // Трансформации объекта
        private Transform.Transform transform;

        // Иерархия объекта (родитель/дочерние объекты).
        private Hierarchy.Hierarchy hierarchy;

        public SceneObject(string name = "new object")
        {
            this.name = name;
            hierarchy = new Hierarchy.Hierarchy(this);
            transform = new Transform.Transform();
        }

        /// <summary>
        /// Имя объекта
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Трансформации объекта.
        /// </summary>
        public Transform.Transform Transform
        {
            get { return transform; }
        }

        /// <summary>
        /// Иерархия объекта (родитель/дочерние объекты). 
        /// </summary>
        public Hierarchy.Hierarchy Hierarchy{
            get { return hierarchy; }
        }

        public abstract void Update();
    }
}
