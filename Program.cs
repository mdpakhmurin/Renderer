﻿using System;
using GlmNet;

namespace Transform
{
    class Transform
    {
        private Position position;
        private Rotation rotation;
        private Scale scale;

        public Transform()
        {
            position = new Position(this);
            rotation = new Rotation(this);
            scale = new Scale(this);
        }

        public Position Position
        {
            get { return position; }
        }

        public Rotation Rotation
        {
            get { return rotation; }
        }

        public Scale Scale
        {
            get { return scale; }
        }
    }

    class Position
    {
        private Transform transform;
        vec3 position;

        public Position(Transform transform)
        {
            this.transform = transform;
            position = new vec3(0, 0, 0);
        }

        public Transform Transform
        {
            get { return transform; }
        }

        public vec3 GetAbsolutePosition()
        {
            throw new NotImplementedException();
        }

        public void SetAbsolutePosition(vec3 position)
        {
            throw new NotImplementedException();
        }

        public vec3 GetLocalPosition()
        {
            return position;
        }

        public void SetLocalPosition(vec3 position)
        {
            this.position = position;
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
            get { return transform; }
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
            return rotation;
        }

        public void SetLocalRotation(vec3 rotation)
        {
            this.rotation = rotation;
        }

        public void RotateByAngle()
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
            get { return transform; }
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
            return scale;
        }

        public void SetLocalScale(vec3 scale)
        {
            this.scale = scale;
        }
    }
}

namespace Render
{
    class MainClass
    {
        public static void Main()
        {
            Transform.Transform tfm = new Transform.Transform();
            vec3 aboba = new vec3(1, 2, 3);

            Console.WriteLine("Hello World!");
        }
    }
}
