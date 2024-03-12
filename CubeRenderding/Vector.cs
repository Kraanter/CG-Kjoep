namespace CubeRenderding;

public class Vector(float[] values) : Matrix(values) {
    public Vector() : this(0, 0) { }

    public Vector(float x, float y, float z = 0, float w = 1) : this(new[] { x, y, z, w }) { }

    public float X { get => this[0]; set => this[0] = value; }
    public float Y { get => this[1]; set => this[1] = value; }
    public float Z { get => this[2]; set => this[2] = value; }
    public float W { get => this[3]; set => this[3] = value; }

    public float Length() => MathF.Sqrt(LengthSquared());

    public float LengthSquared() => X * X + Y * Y + Z * Z;

    public Vector Add(Vector v) {
        X += v.X;
        Y += v.Y;
        Z += v.Z;

        return this;
    }

    public static Vector operator +(Vector left, Vector right) =>
        left.Clone().Add(right);

    public Vector Sub(Vector v) {
        X -= v.X;
        Y -= v.Y;
        Z -= v.Z;

        return this;
    }

    public static Vector operator -(Vector left, Vector right) =>
        left.Clone().Sub(right);

    public Vector Multiply(float value) {
        X *= value;
        Y *= value;
        Z *= value;

        return this;
    }

    public static Vector operator *(Vector left, float right) =>
        left.Clone().Multiply(right);

    public Vector Divide(float value) {
        X /= value;
        Y /= value;
        Z /= value;

        return this;
    }

    public static Vector operator /(Vector left, float right) =>
        left.Clone().Divide(right);

    public Vector Normalize() {
        float length = Length();

        // check for zero length
        if (!(length > float.Epsilon)) return this;

        X /= length;
        Y /= length;
        Z /= length;

        return this;
    }

    public Vector Truncate(float max) {
        if (Length() > max) {
            Normalize();
            Multiply(max);
        }

        return this;
    }

    public Vector Perp() => new(-Y, X, Z);

    public Vector Clone() => new(X, Y, Z);

    public override string ToString() => $"({X}, {Y}, {Z})";

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

        return FloatEquals(a.X, b.X) && FloatEquals(a.Y, b.Y) && FloatEquals(a.Z, b.Z);
    }

    public static bool operator !=(Vector a, Vector b) => !(a == b);

    // public static Vector operator *(Vector v, Matrix m) => ((Matrix)v * m).ToVector();

    public float DistanceTo(Vector targetPos) => MathF.Sqrt(DistanceToSquared(targetPos));

    public float DistanceToSquared(Vector targetPos) {
        float x = targetPos.X - X;
        float y = targetPos.Y - Y;
        float z = targetPos.Z - Z;

        return x * x + y * y + z * z;
    }

    public Vector ApplyProjection(float distance) => Z == 0 ? Clone() : new(-distance * X / Z, -distance * Y / Z);

    public Vector ApplyViewport(float width, float height) => new(X + width / 2, -Y + height / 2);
}