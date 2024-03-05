using CubeRenderding;

namespace CubeRenderdingTests.VectorTests;

public class VectorMathTests {
    [Test]
    [TestCaseSource(typeof(VectorMathTestCases), nameof(VectorMathTestCases.AddTwoVectorsTestCases))]
    public Vector AddTwoVectors(Vector v1, Vector v2) => v1 + v2;

    [Test]
    [TestCaseSource(typeof(VectorMathTestCases), nameof(VectorMathTestCases.SubtractTwoVectorsTestCases))]
    public Vector SubtractTwoVectors(Vector v1, Vector v2) => v1 - v2;

    [Test]
    [TestCaseSource(typeof(VectorMathTestCases), nameof(VectorMathTestCases.MultiplyVectorWithMatrixTestCases))]
    public Vector MultiplyVectorWithMatrix(Vector v, Matrix m) => v * m;

    [Test]
    [TestCaseSource(typeof(VectorMathTestCases), nameof(VectorMathTestCases.MultiplyVectorByScalarTestCases))]
    public Vector MultiplyVectorByScalar(Vector v, float scalar) => v * scalar;

    [Test]
    [TestCaseSource(typeof(VectorMathTestCases), nameof(VectorMathTestCases.DivideVectorByScalarTestCases))]
    public Vector DivideVectorByScalar(Vector v, float scalar) => v / scalar;

    [Test]
    [TestCaseSource(typeof(VectorMathTestCases), nameof(VectorMathTestCases.NormalizeVectorTestCases))]
    public Vector NormalizeVector(Vector v) => v.Normalize();

    [Test]
    [TestCaseSource(typeof(VectorMathTestCases), nameof(VectorMathTestCases.LengthVectorTestCases))]
    public float LengthVector(Vector v) => v.Length();

    [Test]
    [TestCaseSource(typeof(VectorMathTestCases), nameof(VectorMathTestCases.LengthSquaredVectorTestCases))]
    public float LengthSquaredVector(Vector v) => v.LengthSquared();
}

public static class VectorMathTestCases {
    public static List<TestCaseData> AddTwoVectorsTestCases =>
        new() {
                  new TestCaseData(new Vector(1,  2),  new Vector(1,    2)).Returns(new Vector(2,         4)),
                  new TestCaseData(new Vector(-4, -2), new Vector(3.9f, -9.3f)).Returns(new Vector(-0.1f, -11.3f)),
              };

    public static List<TestCaseData> SubtractTwoVectorsTestCases =>
        new() {
                  new TestCaseData(new Vector(1,  2),  new Vector(1,    2)).Returns(new Vector(0,         0)),
                  new TestCaseData(new Vector(-4, -2), new Vector(3.9f, -9.3f)).Returns(new Vector(-7.9f, 7.3f)),
              };

    public static List<TestCaseData> MultiplyVectorWithMatrixTestCases =>
        new() {
                  new TestCaseData(new Vector(2, 0), Matrix.Rotate(90, Axis.Y)).Returns(new Vector(0,   2)),
                  new TestCaseData(new Vector(2, 2), Matrix.Scale(3, 2, 1)).Returns(new Vector(6,       4)),
                  new TestCaseData(new Vector(2, 2), Matrix.Translation(3, 2, 0)).Returns(new Vector(5, 4)),
                  new TestCaseData(new Vector(2, 2), Matrix.Identity(3)).Returns(new Vector(2,          2)),
              };

    public static List<TestCaseData> MultiplyVectorByScalarTestCases =>
        new() {
                  new TestCaseData(new Vector(1,  2),  2).Returns(new Vector(2,         4)),
                  new TestCaseData(new Vector(-4, -2), 3.9f).Returns(new Vector(-15.6f, -7.8f)),
              };

    public static List<TestCaseData> DivideVectorByScalarTestCases =>
        new() {
                  new TestCaseData(new Vector(1,  2),  2).Returns(new Vector(0.5f,         1)),
                  new TestCaseData(new Vector(-4, -2), 3.9f).Returns(new Vector(-1.02564f, -0.51282f)),
              };

    public static List<TestCaseData> NormalizeVectorTestCases =>
        new() {
                  new TestCaseData(new Vector(1,  2)).Returns(new Vector(0.44721f,   0.89443f)),
                  new TestCaseData(new Vector(-4, -2)).Returns(new Vector(-0.89443f, -0.44721f)),
              };

    public static List<TestCaseData> LengthVectorTestCases =>
        new() { new TestCaseData(new Vector(4, 3)).Returns(5), new TestCaseData(new Vector(3, 4)).Returns(5) };

    public static List<TestCaseData> LengthSquaredVectorTestCases =>
        new() { new TestCaseData(new Vector(1, 2)).Returns(5), new TestCaseData(new Vector(-4, -2)).Returns(20) };
}