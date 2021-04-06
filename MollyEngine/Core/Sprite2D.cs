using MollyEngine.Core;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace MollyEngine.Core
{
    public class Sprite2D : GameObject , ICollider
    {
        public Bitmap Sprite = null;
        public string Directory = null;

        public bool isColliderDisabled { get; set; }

        public Sprite2D(Vector2 position, Scale scale, string directory, string tag, uint renderingPriority)
        {
            this.Position = position;
            this.Scale = scale;
            this.Directory = directory;
            this.Tag = tag;
            this.renderingPriority = renderingPriority;

            Image tmp = Image.FromFile(this.Directory);
            Sprite = new Bitmap(tmp, (int)this.Scale.Width, (int)this.Scale.Height);

            MollyEngine.RegisterGameObject(this);
        }

        public Sprite2D(Vector2 position, Scale scale, string directory, string tag)
        {
            this.Position = position;
            this.Scale = scale;
            this.Directory = directory;
            this.Tag = tag;

            Image tmp = Image.FromFile(this.Directory);
            Sprite = new Bitmap(tmp, (int)this.Scale.Width, (int)this.Scale.Height);

            MollyEngine.RegisterGameObject(this);
        }

        public Sprite2D(Vector2 position, Scale scale, string directory)
        {
            this.Position = position;
            this.Scale = scale;
            this.Directory = directory;

            Image tmp = Image.FromFile(this.Directory);
            Sprite = new Bitmap(tmp, (int)this.Scale.Width, (int)this.Scale.Height);

            MollyEngine.RegisterGameObject(this);
        }

        public void setImage(string directory)
        {
            this.Directory = directory;
            Image tmp = Image.FromFile(this.Directory);
            Sprite = new Bitmap(tmp, (int)this.Scale.Width, (int)this.Scale.Height);
        }

        public void setImage(string directory, Scale scale)
        {
            this.Directory = directory;
            this.Scale = scale;

            Image tmp = Image.FromFile(this.Directory);
            Sprite = new Bitmap(tmp, (int)this.Scale.Width, (int)this.Scale.Height);
        }

        public Bitmap getImage()
        {
            return Sprite;
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
                if ((this as GameObject) == gameObject)
                {
                    continue;
                }
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
