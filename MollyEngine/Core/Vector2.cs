namespace MollyEngine.Core
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Vector2()
        {
            this.X = Zero().X;
            this.Y = Zero().Y;
        }

        public static Vector2 Zero()
        {
            return new Vector2(0f, 0f);
        }

        public static bool isEqual(Vector2 vector1, Vector2 vector2)
        {
            if (vector1 == vector2)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 Multiply(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2 Divide(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }

        public static Vector2 operator+ (Vector2 a, Vector2 b)
        {
            return Vector2.Add(a, b);
        }

        public static Vector2 operator- (Vector2 a, Vector2 b)
        {
            return Vector2.Subtract(a, b);
        }

        public static Vector2 operator* (Vector2 a, Vector2 b)
        {
            return Vector2.Multiply(a, b);
        }

        public static Vector2 operator/ (Vector2 a, Vector2 b)
        {
            return Vector2.Divide(a, b);
        }

        public static bool operator == (Vector2 a, Vector2 b)
        {
            if (a.X == b.X && a.Y == b.Y)
            {
                return true;
            }
            return false;
        }

        public static bool operator != (Vector2 a, Vector2 b)
        {
            if (a.X != b.X || a.Y != b.Y)
            {
                return true;
            }
            return false;
        }
    }
}
