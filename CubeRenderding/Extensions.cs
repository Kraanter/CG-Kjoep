namespace CubeRenderding;

public static class Extensions {
    public static List<Vector> RotateVectors(this List<Vector> vectors, float degrees) =>
        vectors.ApplyTransformation(Matrix.Rotate(degrees));

    public static List<Vector> ScaleVectors(this List<Vector> vectors, float x, float y) =>
        vectors.ApplyTransformation(Matrix.Scale(x, y));

    public static List<Vector> ScaleVectors(this List<Vector> vectors, Vector scale) =>
        vectors.ScaleVectors(scale.X, scale.Y);

    public static List<Vector> TranslateVectors(this List<Vector> vectors, float x, float y) =>
        vectors.ApplyTransformation(Matrix.Translation(x, y));

    public static List<Vector> TranslateVectors(this List<Vector> vectors, Vector translation) =>
        vectors.TranslateVectors(translation.X, translation.Y);

    public static List<Vector> ApplyTransformation(this List<Vector> vectors, Matrix transformation) {
        List<Vector> result = new();
        foreach (Vector vector in vectors) result.Add(vector * transformation);

        return result;
    }
}