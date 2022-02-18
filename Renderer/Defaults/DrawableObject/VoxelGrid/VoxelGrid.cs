using Scene.Utilities;

namespace Scene.Defaults
{
    public partial class VoxelGrid : DrawableObject
    {
        private ShaderProgram shaderProgram;
        private _Drawer drawer;
        private _Data data;

        public VoxelGrid()
        {
            drawer = new _Drawer(this);
            data = new _Data(this);
        }

        override public void Draw(Camera camera)
        {
            base.Draw(camera);

            drawer.Draw(camera);
        }

        public _Data Data
        {
            get { return data; }
        }
    }
}
