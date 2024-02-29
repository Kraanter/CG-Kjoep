namespace CubeRenderding;

public class Vector(float[] values) : Matrix(values) {
    public Vector() : this(0, 0) { }

    public Vector(float x, float y, float w = 1) : this(new[] { x, y, w }) {
        // X = x;
        // Y = y;
        // W = w;
    }

    public float X { get => this[0]; set => this[0] = value; }
    public float Y { get => this[1]; set => this[1] = value; }
    public float W { get => this[2]; set => this[2] = value; }

    public float Length() => MathF.Sqrt(LengthSquared());

    public float LengthSquared() => X * X + Y * Y;

    public Vector Add(Vector v) {
        this[0, 0] += v.X;
        this[0, 1] += v.Y;

        return this;
    }

    public static Vector operator +(Vector left, Vector right) =>
        left.Clone().Add(right);

    public Vector Sub(Vector v) {
        this[0, 0] -= v.X;
        this[0, 1] -= v.Y;

        return this;
    }

    public static Vector operator -(Vector left, Vector right) =>
        left.Clone().Sub(right);

    public Vector Multiply(float value) {
        this[0, 0] *= value;
        this[0, 1] *= value;

        return this;
    }

    public static Vector operator *(Vector left, float right) =>
        left.Clone().Multiply(right);

    public Vector Divide(float value) {
        this[0, 0] /= value;
        this[0, 1] /= value;

        return this;
    }

    public static Vector operator /(Vector left, float right) =>
        left.Clone().Divide(right);

    public Vector Normalize() {
        float length = Length();

        // check for zero length
        if (!(length > float.Epsilon)) return this;

        this[0, 0] /= length;
        this[0, 1] /= length;

        return this;
    }

    public Vector Truncate(float max) {
        if (Length() > max) {
            Normalize();
            Multiply(max);
        }

        return this;
    }

    public Vector Perp() => new(-Y, X);

    public Vector Clone() => new(X, Y);

    public override string ToString() => $"({X}, {Y})";

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

        return obj.GetType() == GetType() && Equals((Vector)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    private bool Equals(Vector other) => this == other;

    private static bool FloatEquals(float a, float b) => MathF.Abs(a - b) < FloatingPointTolerance;

    public static bool operator ==(Vector a, Vector b) {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;

        return FloatEquals(a.X, b.X) && FloatEquals(a.Y, b.Y) && FloatEquals(a.W, b.W);
    }

    public static bool operator !=(Vector a, Vector b) => !(a == b);

    // Oke this is very nice new syntax booiiii DOTNET 8 babay :D
    public static Vector operator *(Vector v, Matrix m) =>
        (m.Rows, m.Cols) switch {
            _ when m.Rows != m.Cols =>
                throw new InvalidOperationException($"Matrix must be 2x2 or 3x3, not {m.Rows}x{m.Cols}"),
            (2, 2) => new(m[0, 0] * v.X + m[0, 1] * v.Y, m[1, 0] * v.X + m[1, 1] * v.Y),
            (3, 3) => new(m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.W, m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.W),
            _      => throw new InvalidOperationException("Matrix must be 2x2 or 3*3 " + $"not {m.Rows}x{m.Cols}"),
        };

    public float DistanceTo(Vector targetPos) => MathF.Sqrt(DistanceToSquared(targetPos));

    public float DistanceToSquared(Vector targetPos) {
        float x = targetPos.X - X;
        float y = targetPos.Y - Y;

        return x * x + y * y;
    }
}