using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scene.SceneObject
{
    /// <summary>
    /// Объект на сцене, хранит в себе таких же потомков (Компоновщик)
    /// </summary>
    abstract class SceneObject
    {
        // Имя объекта.
        private string name;

        // Родитель объекта.
        private SceneObject parent;

        // Трансформации объекта
        private Transform.Transform transform;

        // Потомки объекта
        private ChildrenList children;

        public SceneObject(string name = "new object")
        {
            this.name = name;

            parent = null;
            transform = new Transform.Transform();
            children = new ChildrenList(this);
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
        /// Родитель объекта.
        /// </summary>
        public SceneObject Parent
        {
            get { return parent; }
            set
            {
                value?.children.AttachChild(this);
            }
        }

        /// <summary>
        /// Трансформации объекта.
        /// </summary>
        public Transform.Transform Transform
        {
            get { return transform; }
        }

        /// <summary>
        /// Потомки объекта.
        /// </summary>
        public ChildrenList Children
        {
            get { return children; }
        }

        public abstract void Update();

        /// <summary>
        /// Управляет иерархией родитель/потомок.
        /// </summary>
        // Делегирует управление списком потомков и управляет полем - родитель объекта.
        public class ChildrenList
        {
            // Объект, содержащий в себе список потомков.
            private SceneObject parent;

            // Список детей.
            private List<SceneObject> children;

            public ChildrenList(SceneObject parent)
            {
                children = new List<SceneObject>();
                this.parent = parent;
            }

            /// <summary>
            /// Прикрепляет объект к новому родителю.
            /// </summary>
            /// <param name="sceneObject">Родительский объект.</param>
            public void AttachChild(SceneObject sceneObject)
            {
                // Проверка является ли прошлый родитель новым родителем.
                if (sceneObject.parent == parent)
                    return;

                // Проверка на попытку добавить объект в свои потомки.
                var parentObject = parent;
                while (parentObject != null){
                    if (parentObject == sceneObject)
                        throw new ArgumentException("Объект не может являться своим потомком");

                    parentObject = parentObject.Parent;
                }


                // Открепление прошлого родителя.
                sceneObject.Parent?.Children.DetachChild(sceneObject);
 
                // Прикрепление нового родителя.
                sceneObject.parent = parent;
                children.Add(sceneObject);
            }

            /// <summary>
            /// Открепляет объект от родителя.
            /// </summary>
            /// <param name="sceneObject">Объект, который необходимо открепить.</param>
            public void DetachChild(SceneObject sceneObject)
            {
                sceneObject.parent = null;
                children.Remove(sceneObject);
            }


            /// <summary>
            /// Возвращает всех потомков 1-го уровня.
            /// </summary>
            /// <returns>Список объектов, доступный только для чтения.</returns>
            public ReadOnlyCollection<SceneObject> GetChildren()
            {
                return children.AsReadOnly();
            }

            /// <summary>
            /// Получает всех потомков 1-го уровня, с заданным именем.
            /// </summary>
            /// <returns>Список объектов, доступный только для чтения.</returns>
            /// <param name="name">Имя объекта</param>
            public ReadOnlyCollection<SceneObject> GetChildrenByName(string name)
            {
                var found = new List<SceneObject>();

                foreach (var child in children)
                {
                    if (child.Name == name)
                        found.Add(child);
                }

                return found.AsReadOnly();
            }
        }
    }
}
