using MollyEngine.MollyEngine.Core;
using System;
using System.Drawing;

namespace MollyEngine.MollyEngine
{
    public class Shape2D : GameObject, ICollider
    {
        public Color Color = Color.Red;

        public bool isColliderDisabled { get; set; }

        public Shape2D(Vector2 position, Scale scale, Color color, string tag, uint renderingPriority)
        {
            this.Position = position;
            this.Scale = scale;
            this.Tag = tag;
            this.Color = color;
            this.renderingPriority = renderingPriority;

            MollyEngine.RegisterGameObject(this);
        }

        public Shape2D(Vector2 position, Scale scale, string tag)
        {
            this.Position = position;
            this.Scale = scale;
            this.Tag = tag;

            MollyEngine.RegisterGameObject(this);
        }

        public Shape2D(Vector2 position, Scale scale, Color color)
        {
            this.Position = position;
            this.Scale = scale;
            this.Color = color;

            MollyEngine.RegisterGameObject(this);
        }

        public Shape2D(Vector2 position, Scale scale)
        {
            this.Position = position;
            this.Scale = scale;

            MollyEngine.RegisterGameObject(this);
        }

        public bool isColliding(out GameObject collide)
        {
            if (isColliderDisabled)
            {
                collide = null;
                return false;
            }
            
            foreach (GameObject gameObject in MollyEngine.getAllGameObjects())
            {
                if (this.Position.X < gameObject.Position.X + gameObject.Scale.Width &&
                    this.Position.X + this.Scale.Width > gameObject.Position.X &&
                    this.Position.Y < gameObject.Position.Y + gameObject.Scale.Height &&
                    this.Position.Y + this.Scale.Height > gameObject.Position.Y)
                {
                    collide = gameObject;
                    return true;
                }
            }

            collide = null;
            return false;
        }

        public bool isColliding()
        {
            if (isColliderDisabled)
            {
                return false;
            }
            GameObject collide;
            return isColliding(out collide);
        }

        public bool isCollidingWith(GameObject gameObject)
        {
            if ((this as GameObject) == gameObject)
            {
                throw new Exception("Cannot check whether or not a gameObject is colliding with itself");
            }
            if (this.Position.X < gameObject.Position.X + gameObject.Scale.Width &&
                this.Position.X + this.Scale.Width > gameObject.Position.X &&
                this.Position.Y < gameObject.Position.Y + gameObject.Scale.Height &&
                this.Position.Y + this.Scale.Height > gameObject.Position.Y)
            {
                return true;
            }
            return false;
        }
    }
}
