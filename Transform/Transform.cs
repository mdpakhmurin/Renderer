namespace Scene.SceneObject
{
    /// <summary>
    /// Трансформации объетка.
    /// </summary>
    public partial class Transform
    {
        // текущий объект (к которому привязана трансформация).
        private SceneObject sceneObject;

        // Позиция объекта.
        private _Position position;

        // Вращение объекта.
        private _Rotation rotation;

        // Размер объекта.
        private _Scale scale;

        public Transform(SceneObject sceneObject)
        {
            this.sceneObject = sceneObject;

            position = new _Position(this);
            rotation = new _Rotation(this);
            scale = new _Scale(this);
        }

        /// <summary>
        /// Текущий объект на сцене
        /// </summary>
        public SceneObject SceneObject
        {
            get { return sceneObject; }
        }

        /// <summary>
        /// Позиция объекта.
        /// </summary>
        public _Position Position
        {
            get { return position; }
        }

        /// <summary>
        /// Вращение объекта.
        /// </summary>
        public _Rotation Rotation
        {
            get { return rotation; }
        }

        /// <summary>
        /// Размер объекта.
        /// </summary>
        public _Scale Scacle
        {
            get { return scale; }
        }
    }
}