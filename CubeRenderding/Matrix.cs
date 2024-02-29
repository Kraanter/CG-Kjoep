using System.Text;

namespace CubeRenderding;

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

    public static Matrix Scale(float x, float y) {
        Matrix result = Identity(3);
        result[0, 0] = x;
        result[1, 1] = y;

        return result;
    }

    public static Matrix Rotate(float degrees) {
        float radians = degrees * MathF.PI / 180;
        float cos     = MathF.Cos(radians);
        float sin     = MathF.Sin(radians);

        Matrix result = Identity(3);
        result[0, 0] = cos;
        result[0, 1] = -sin;
        result[1, 0] = sin;
        result[1, 1] = cos;

        return result;
    }

    public static Matrix Translation(float x, float y) {
        Matrix result = Identity(3);
        result[0, 2] = x;
        result[1, 2] = y;

        return result;
    }
}