using CubeRenderding;

namespace CubeRenderdingTests.MatrixTests;

public class MatrixTransformationTests {
    [Test]
    [TestCaseSource(
        typeof(MatrixTransformationTestCases),
        nameof(MatrixTransformationTestCases.RotationMatrixTestCases)
    )]
    public Matrix RotationMatrix(float angle) => Matrix.Rotate(angle, Axis.Z);

    [Test]
    [TestCaseSource(typeof(MatrixTransformationTestCases), nameof(MatrixTransformationTestCases.ScaleMatrixTestCases))]
    public Matrix ScaleMatrix(float x, float y, float z) => Matrix.Scale(x, y, z);

    [Test]
    [TestCaseSource(
        typeof(MatrixTransformationTestCases),
        nameof(MatrixTransformationTestCases.TranslationMatrixTestCases)
    )]
    public Matrix TranslationMatrix(float x, float y, float z) => Matrix.Translation(x, y, z);
}

public static class MatrixTransformationTestCases {
    public static List<TestCaseData> RotationMatrixTestCases =>
        new() {
                  new TestCaseData(90).Returns(new Matrix(0,          -1,          1,          0)),
                  new TestCaseData(180).Returns(new Matrix(-1,        0,           0,          -1)),
                  new TestCaseData(270).Returns(new Matrix(0,         1,           -1,         0)),
                  new TestCaseData(360).Returns(new Matrix(1,         0,           0,          1)),
                  new TestCaseData(45).Returns(new Matrix(0.7071068f, -0.7071068f, 0.7071068f, 0.7071068f)),
                  new TestCaseData(30).Returns(new Matrix(0.8660254f, -0.5f,       0.5f,       0.8660254f)),
              };

    public static List<TestCaseData> ScaleMatrixTestCases =>
        new() {
                  new TestCaseData(2, 2, 1).Returns(new Matrix(2, 0, 0, 2)),
                  new TestCaseData(3, 2, 1).Returns(new Matrix(3, 0, 0, 2)),
                  new TestCaseData(2, 3, 1).Returns(new Matrix(2, 0, 0, 3)),
                  new TestCaseData(4, 4, 1).Returns(new Matrix(4, 0, 0, 4)),
              };

    public static List<TestCaseData> TranslationMatrixTestCases =>
        new() {
                  new TestCaseData(2, 2, 2).Returns(new Matrix(1, 0, 0, 2, 0, 1, 0, 2, 0, 0, 1, 2, 0, 0, 0, 1)),
                  new TestCaseData(3, 2, 1).Returns(new Matrix(1, 0, 0, 3, 0, 1, 0, 2, 0, 0, 1, 1, 0, 0, 0, 1)),
                  new TestCaseData(2, 3, 4).Returns(new Matrix(1, 0, 0, 2, 0, 1, 0, 3, 0, 0, 1, 4, 0, 0, 0, 1)),
                  new TestCaseData(4, 4, 4).Returns(new Matrix(1, 0, 0, 4, 0, 1, 0, 4, 0, 0, 1, 4, 0, 0, 0, 1)),
              };
}