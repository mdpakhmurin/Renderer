using System;
using GlmNet;

namespace Transform
{
    class Transform
    { 
        public Position Position
        {
            get { throw new NotImplementedException(); }
        }

        public Scale Scale
        {
            get { throw new NotImplementedException(); }
        }

        public Rotation Rotation
        {
            get { throw new NotImplementedException(); }
        }
    }

    class Rotation {
        private vec3 rotation;
        private Transform transform;

        public Rotation(Transform transform)
        {
            this.transform = transform;
            rotation = new vec3(0,0,0);
        }

        public Transform Transform
        {
            get { throw new NotImplementedException(); }
        }

        public vec3 GetAbsoluteRotation()
        {
            throw new NotImplementedException();
        }

        public void SetAbsoluteRotation(vec3 rotation)
        {
            throw new NotImplementedException();
        }

        public vec3 GetLocalRotation()
        {
            throw new NotImplementedException();
        }

        public vec3 SetLocalRotation(vec3 rotation)
        {
            throw new NotImplementedException();
        }

        public void RotateByAngle()
        {
            throw new NotImplementedException();
        }
    }

    class Position {
        private Transform transform;
        vec3 position;

        public Position(Transform transform)
        {
            this.transform = transform;
            position = new vec3(0, 0, 0);
        }

        public Transform Transform
        {
            get { throw new NotImplementedException(); }
        }

        public vec3 GetAbsolutePosition()
        {
            throw new NotImplementedException();
        }

        public void SetAbsolutePosition(vec3 position)
        {
            throw new NotImplementedException();
        }

        vec3 GetLocalPosition()
        {
            throw new NotImplementedException();
        }

        void SetLocalPosition(vec3 position)
        {
            throw new NotImplementedException();
        }
    }

    class Scale {
        Transform transform;
        vec3 scale;

        public Scale(Transform transform)
        {
            this.transform = transform;
            scale = new vec3(1, 1, 1);
        }

        public Transform Transform
        {
            get { throw new NotImplementedException(); }
        }

        public vec3 GetAbsoluteScale()
        {
            throw new NotImplementedException();
        }

        public void SetAbsoluteScale()
        {
            throw new NotImplementedException();
        }

        public vec3 GetLocalScale()
        {
            throw new NotImplementedException();
        }

        public void SetLocalScale(vec3 scale)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Render
{
    class MainClass
    {
        public static void Main(string[] args)
        {


            Console.WriteLine("Hello World!");
        }
    }
}
