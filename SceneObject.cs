namespace Scene.SceneObject
{
    /// <summary>
    /// Объект на сцене, хранит в себе таких же потомков.
    /// Каждый объект сцены должен расширяется единственной ответственностью 
    /// и представлять собой узел иерархии сцены.
    /// </summary>
    public class SceneObject
    {
        // Имя объекта.
        private string name;

        // Трансформации объекта
        private Transform transform;

        // Иерархия объекта (родитель/дочерние объекты).
        private Hierarchy.Hierarchy hierarchy;

        public SceneObject(string name = "new object")
        {
            this.name = name;
            hierarchy = new Hierarchy.Hierarchy(this);
            transform = new Transform(this);
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
        public Transform Transform
        {
            get { return transform; }
        }

        /// <summary>
        /// Иерархия объекта (родитель/дочерние объекты). 
        /// </summary>
        public Hierarchy.Hierarchy Hierarchy
        {
            get { return hierarchy; }
        }

        public virtual void Update() { }
    }
}
