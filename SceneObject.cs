using System;
using System.Collections.Generic;

namespace Scene.SceneObject
{
    public abstract class SceneObject
    {
        private string name;
        private SceneObject parent;
        private Transform.Transform transform;
        private Children children;

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

    public class Children
    {
        List<SceneObject> children;

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
