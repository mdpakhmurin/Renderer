using GlmNet;
using Scene.SceneObject;

namespace Scene.Defaults
{
    class Camera: SceneObject.SceneObject
    {
        public Camera(string name = "newCamera") : base(name) { }

        public float DistanceToNearPlane { get; set; }
        public float DistanceToFarPlane { get; set; }

        public vec2 AspectRatioNearPlane  { get; set; }
        public vec2 AspectRatioFarPlane { get; set; }
    }
}
