using System.Text;

namespace CubeRenderding;

public enum Axis { X, Y, Z }

public class Matrix(float[,] elements) {
    protected const float FloatingPointTolerance = 0.0001f;
    public Matrix(int size) : this(size, size) { }

    public Matrix(int rows, int cols) : this(new float[rows, cols]) { }

    public Matrix(float[] elements) : this(new float[1, elements.Length]) {
        for (var i = 0; i < elements.Length; i++) this[i] = elements[i];
    }

    public Matrix(float e00, float e01, float e10, float e11) : this(new[,] { { e00, e01 }, { e10, e11 } }) { }

    public Matrix(float e00, float e01, float e02, float e10, float e11, float e12, float e20, float e21, float e22) :
        this(new[,] { { e00, e01, e02 }, { e10, e11, e12 }, { e20, e21, e22 } }) { }

    private float[,] Grid { get; } = elements;
    public  int      Rows => Grid.GetLength(0);
    public  int      Cols => Grid.GetLength(1);

    public float this[int row, int col] { get => Grid[row, col]; set => Grid[row, col] = value; }
    public float this[int index] { get => Grid[0, index]; set => Grid[0, index] = value; }

    public override string ToString() {
        var sb = new StringBuilder();
        for (var i = 0; i < Rows; i++) {
            for (var j = 0; j < Cols; j++) sb.Append($"{Grid[i, j]} ");
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private bool Equals(Matrix other) => this == other;

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

        return obj.GetType() == GetType() && Equals((Matrix)obj);
    }

    public override int GetHashCode() => Grid.GetHashCode();

    public static Matrix Identity(int size) {
        Matrix result = new(size, size);

        for (var i = 0; i < size; i++) result[i, i] = 1;

        return result;
    }

    public static Matrix operator +(Matrix a, Matrix b) {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            throw new InvalidOperationException("Matrices must have the same dimensions");

        var result = new Matrix(a.Rows, a.Cols);
        for (var i = 0; i < a.Rows; i++) {
            for (var j = 0; j < a.Cols; j++) result[i, j] = a[i, j] + b[i, j];
        }

        return result;
    }

    public static Matrix operator -(Matrix a, Matrix b) {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            throw new InvalidOperationException("Matrices must have the same dimensions");

        var result = new Matrix(a.Rows, a.Cols);
        for (var i = 0; i < a.Rows; i++) {
            for (var j = 0; j < a.Cols; j++) result[i, j] = a[i, j] - b[i, j];
        }

        return result;
    }

    public static Matrix operator *(Matrix a, Matrix b) {
        if (a.Cols != b.Rows) {
            throw new InvalidOperationException(
                "The number of columns in the first matrix must be equal to the number of rows in the second matrix"
            );
        }

        var result = new Matrix(a.Rows, b.Cols);
        for (var i = 0; i < a.Rows; i++) {
            for (var j = 0; j < b.Cols; j++) {
                for (var k = 0; k < a.Cols; k++) result[i, j] += a[i, k] * b[k, j];
            }
        }

        return result;
    }

    public static Matrix operator *(Matrix a, float b) {
        var result = new Matrix(a.Rows, a.Cols);
        for (var i = 0; i < a.Rows; i++) {
            for (var j = 0; j < a.Cols; j++) result[i, j] = a[i, j] * b;
        }

        return result;
    }

    public static Matrix operator /(Matrix a, float b) {
        var result = new Matrix(a.Rows, a.Cols);
        for (var i = 0; i < a.Rows; i++) {
            for (var j = 0; j < a.Cols; j++) result[i, j] = a[i, j] / b;
        }

        return result;
    }

    public static Matrix operator -(Matrix a) {
        var result = new Matrix(a.Rows, a.Cols);
        for (var i = 0; i < a.Rows; i++) {
            for (var j = 0; j < a.Cols; j++) result[i, j] = -a[i, j];
        }

        return result;
    }

    public static bool operator ==(Matrix a, Matrix b) {
        // If both are square matrices and not the same size resize them to the bigger size
        if (a.Rows == a.Cols && b.Rows == b.Cols) {
            int size = Math.Max(a.Rows, b.Rows);
            a = a.IdentityResized(size);
            b = b.IdentityResized(size);
        } else if (a.Rows != b.Rows || a.Cols != b.Cols) return false;

        for (var i = 0; i < a.Rows; i++) {
            for (var j = 0; j < a.Cols; j++)
                if (MathF.Abs(a[i, j] - b[i, j]) > FloatingPointTolerance)
                    return false;
        }

        return true;
    }

    public Matrix IdentityResized(int size) {
        Matrix result                                               = Identity(size);
        for (var i = 0; i < Math.Min(Rows, Cols); i++) result[i, i] = this[i, i];

        return result;
    }

    public static bool operator !=(Matrix a, Matrix b) => !(a == b);

    public static Matrix Scale(float x, float y, float z) {
        Matrix result = Identity(4);
        result[0, 0] = x;
        result[1, 1] = y;
        result[2, 2] = z;

        return result;
    }

    public static Matrix Rotate(float degrees, Axis axis) =>
        axis switch {
            Axis.X => RotateX(DegreesToRadians(degrees)),
            Axis.Y => RotateY(DegreesToRadians(degrees)),
            Axis.Z => RotateZ(DegreesToRadians(degrees)),
            _      => throw new InvalidOperationException("Invalid axis"),
        };

    private static float DegreesToRadians(float degrees) => MathF.PI * degrees / 180;

    private static Matrix RotateX(float radians) {
        Matrix result = Identity(4);
        result[1, 1] = MathF.Cos(radians);
        result[1, 2] = -MathF.Sin(radians);
        result[2, 1] = MathF.Sin(radians);
        result[2, 2] = MathF.Cos(radians);

        return result;
    }

    private static Matrix RotateY(float radians) {
        Matrix result = Identity(4);
        result[0, 0] = MathF.Cos(radians);
        result[0, 2] = -MathF.Sin(radians);
        result[2, 0] = MathF.Sin(radians);
        result[2, 2] = MathF.Cos(radians);

        return result;
    }

    private static Matrix RotateZ(float radians) {
        Matrix result = Identity(4);
        result[0, 0] = MathF.Cos(radians);
        result[0, 1] = -MathF.Sin(radians);
        result[1, 0] = MathF.Sin(radians);
        result[1, 1] = MathF.Cos(radians);

        return result;
    }

    public static Matrix Translation(float x, float y, float z) {
        Matrix result = Identity(4);
        result[0, 2] = x;
        result[1, 2] = y;
        result[2, 3] = z;

        return result;
    }

    public static Matrix View(float r, float theta, float phi) {
        Matrix orientation = new(
            new[,] {
                       { -MathF.Sin(theta), MathF.Cos(theta), 0, 0 },
                       { -MathF.Cos(theta) * MathF.Cos(phi), -MathF.Cos(phi)  * MathF.Sin(theta), MathF.Sin(phi), 0 },
                       { MathF.Cos(theta)  * MathF.Sin(phi), MathF.Sin(theta) * MathF.Sin(phi), MathF.Cos(phi), -r },
                       { 0, 0, 0, 1 },
                   }
        );

        return orientation;
    }

    public Vector ToVector() {
        if (Rows != 1 && Cols != 1) throw new InvalidOperationException("Matrix must be a 1xN or Nx1 matrix");

        return new(Grid.Cast<float>().ToArray());
    }
}