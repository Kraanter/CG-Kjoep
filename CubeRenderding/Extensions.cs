namespace CubeRenderding;

public static class Extensions {
    public static List<Vector> RotateVectors(this List<Vector> vectors, float degrees, Axis axis) =>
        vectors.ApplyTransformation(Matrix.Rotate(degrees, axis));

    public static List<Vector> RotateX(this List<Vector> vectors, float degrees) =>
        vectors.RotateVectors(degrees, Axis.X);

    public static List<Vector> RotateY(this List<Vector> vectors, float degrees) =>
        vectors.RotateVectors(degrees, Axis.Y);

    public static List<Vector> RotateZ(this List<Vector> vectors, float degrees) =>
        vectors.RotateVectors(degrees, Axis.Z);

    public static List<Vector> ScaleVectors(this List<Vector> vectors, float x, float y, float z) =>
        vectors.ApplyTransformation(Matrix.Scale(x, y, z));

    public static List<Vector> ScaleVectors(this List<Vector> vectors, Vector scale) =>
        vectors.ScaleVectors(scale.X, scale.Y, scale.Z);

    public static List<Vector> TranslateVectors(this List<Vector> vectors, float x, float y, float z) =>
        vectors.ApplyTransformation(Matrix.Translation(x, y, z));

    public static List<Vector> TranslateVectors(this List<Vector> vectors, Vector translation) =>
        vectors.TranslateVectors(translation.X, translation.Y, translation.Z);

    public static List<Vector> ApplyProjection(this List<Vector> vectors, float distance) =>
        vectors.Select(vector => vector.ApplyProjection(distance)).ToList();

    public static List<Vector> ApplyTransformation(this List<Vector> vectors, Matrix transformation) =>
        vectors.Select(vector => vector * transformation).ToList();
}