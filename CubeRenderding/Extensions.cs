namespace CubeRenderding;

public static class Extensions {
    public static List<Vector> RotateVectors(this List<Vector> vectors, float degrees) =>
        vectors.ApplyTransformation(Matrix.Rotate(degrees));

    public static List<Vector> ApplyTransformation(this List<Vector> vectors, Matrix transformation) {
        List<Vector> result = new();
        foreach (Vector vector in vectors) result.Add(vector * transformation);

        return result;
    }
}