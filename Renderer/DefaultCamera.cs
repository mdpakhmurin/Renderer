using OpenTK.Mathematics;
using Scene.SceneObject;

namespace Scene.Defaults
{
    public class Camera: SceneObject.SceneObject
    {
        public Camera(string name = "newCamera") : base(name) { }

        public float DistanceToNearPlane { get; set; }
        public float DistanceToFarPlane { get; set; }

        public Vector3 AspectRatioNearPlane  { get; set; }
        public Vector3 AspectRatioFarPlane { get; set; }
    }
}
