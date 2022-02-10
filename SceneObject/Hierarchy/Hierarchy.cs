using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scene.SceneObject
{
    partial class SceneObject
    {
        public class _Hierarchy
        {
            // Родительский объект.
            private SceneObject parent;

            // Текущий объект (к которому привязана иерархия).
            private SceneObject currentObject;

            // Потомки объекта.
            private List<SceneObject> children;

            /// <summary>
            /// Устанавливает родителя.
            /// (Открепляет от прошлого и добавляет к новому).
            /// </summary>
            /// <param name="newParent">Родительский объект.</param>
            /// <exception cref="ArgumentException">Объект не может являться своим потомком.</exception>
            private void SetParent(SceneObject newParent)
            {
                // Проверка является ли прошлый родитель новым родителем.
                if (parent == newParent)
                    return;

                // Проверка на попытку добавить объект в свои потомки.
                var newParentTemp = newParent;
                while (newParentTemp != null)
                {
                    if (currentObject == newParentTemp)
                        throw new ArgumentException("Объект не может являться своим потомком");

                    newParentTemp = newParentTemp.Hierarchy.Parent;
                }

                // Открепление от прошлого родителя.
                Parent?.Hierarchy.children.Remove(currentObject);

                // Прикрепление нового родителя.
                parent = newParent;
                newParent.Hierarchy.children.Add(currentObject);
            }

            public _Hierarchy(SceneObject currentObject)
            {
                this.currentObject = currentObject;
                children = new List<SceneObject>();
            }

            /// <summary>
            /// Родительсий объект.
            /// </summary>
            public SceneObject Parent
            {
                get { return parent; }
                set { SetParent(value); }
            }

            /// <summary>
            /// Текущий объект.
            /// </summary>
            public SceneObject CurrentObject
            {
                get { return currentObject; }
            }

            /// <summary>
            /// Добавляет дочерний объект к текущему.
            /// </summary>
            /// <param name="child"></param>
            public void AddChild(SceneObject child)
            {
                child.Hierarchy.Parent = currentObject;
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
