namespace MollyEngine.Core
{
    public class Scale
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public Scale(float Width, float Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public Scale()
        {
            Width = Height = 0;
        }

        public static Scale Zero()
        {
            return new Scale(0, 0);
        }

        public static Scale operator+ (Scale a, Scale b)
        {
            return new Scale(a.Width + b.Width, a.Height + b.Height);
        }

        public static Scale operator -(Scale a, Scale b)
        {
            return new Scale(a.Width - b.Width, a.Height - b.Height);
        }

        public static Scale operator *(Scale a, Scale b)
        {
            return new Scale(a.Width * b.Width, a.Height * b.Height);
        }

        public static Scale operator /(Scale a, Scale b)
        {
            return new Scale(a.Width / b.Width, a.Height / b.Height);
        }

        public static bool operator ==(Scale a, Scale b)
        {
            if (a.Width == b.Width && a.Height == b.Height)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Scale a, Scale b)
        {
            if (a.Width != b.Width || a.Height != b.Height)
            {
                return true;
            }
            return false;
        }
    }
}
