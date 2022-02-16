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
            Width = 10;
            Height = 10;

            Far = 1;
            Near = 100;

            Type = CameraType.Orthographic;
        }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Near { get; set; }

        public float Far { get; set; }

        public CameraType Type { get; set; }
    }
}
