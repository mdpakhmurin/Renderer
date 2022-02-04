using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scene.SceneObject
{
    abstract class SceneObject
    {
        private string name;
        private SceneObject parent;
        private Transform.Transform transform;
        private ChildrenContainer children;

        public SceneObject(string name = "new object")
        {
            this.name = name;

            parent = null;
            transform = new Transform.Transform();
            children = new ChildrenContainer(this);
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public SceneObject Parent
        {
            get { return parent; }
            set
            {
                if (value == this)
                    throw new ArgumentException("Объект не может являться своим родителем");

                value.children.AttachChild(this);
            }
        }

        public Transform.Transform Transform
        {
            get { return transform; }
        }

        public ChildrenContainer Children
        {
            get { return children; }
        }

        public abstract void Update();

        public class ChildrenContainer
        {
            private SceneObject parent;
            private List<SceneObject> children;

            public ChildrenContainer(SceneObject parent)
            {
                children = new List<SceneObject>();
                this.parent = parent;
            }

            public void AttachChild(SceneObject sceneObject)
            {
                sceneObject.Parent?.Children.DetachChild(sceneObject);
 
                sceneObject.parent = parent;

                foreach (var child in children)
                {
                    if (child == sceneObject)
                        return;
                }
                children.Add(sceneObject);
            }

            public void DetachChild(SceneObject sceneObject)
            {
                children.Remove(sceneObject);
            }

            public ReadOnlyCollection<SceneObject> GetChildren()
            {
                return children.AsReadOnly();
            }

            public ReadOnlyCollection<SceneObject> GetChildrenByName(String name)
            {
                var found = new List<SceneObject>();

                foreach (var child in children)
                {
                    if (child.Name == name)
                    {
                        found.Add(child);
                    }
                }

                return found.AsReadOnly();
            }
        }
    }
}
