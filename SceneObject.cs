using System;
using System.Collections.Generic;

namespace Scene.SceneObject
{
    abstract class SceneObject
    {
        private string name;
        private SceneObject parent;
        private Transform.Transform transform;
        private Children children;

        public SceneObject(string name = "new object")
        {
            this.name = name;

            parent = null;
            transform = new Transform.Transform();
            children = new Children();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public SceneObject Parent
        {
            get { return parent; }
        }

        public Transform.Transform Transform
        {
            get { return transform; }
        }

        public Children Children{
            get { return children; }
        }

        public abstract void Update();
    }

    class Children
    {
        List<SceneObject> children;

        public Children()
        {
            children = new List<SceneObject>();
        }

        public void AttachChild(SceneObject sceneObject)
        {
            children.Add(sceneObject);
        }

        public void DetachChild(SceneObject sceneObject)
        {
            throw new NotImplementedException();
        }

        public List<SceneObject> GetChildrenByName(String name)
        {
            var found = new List<SceneObject>();

            foreach (var child in children)
            {
                if (child.Name == name)
                {
                    found.Add(child);
                }
            }

            return found;
        }
    }
}
