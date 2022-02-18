using OpenTK.Mathematics;
using Scene.SceneObject;

namespace Scene.Defaults
{
    public class Camera : SceneObject.SceneObject
    {
        public enum CameraType
        {
            Orthographic,
            Perspective
        }

        public Camera(string name = "newCamera") : base(name) {
            Width = 5;
            Height = 5;

            Far = 100;
            Near = 1;

            Type = CameraType.Perspective;
        }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Near { get; set; }

        public float Far { get; set; }

        public CameraType Type { get; set; }
    }
}
