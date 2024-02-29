using CubeRenderding;

namespace CubeRenderdingTests.MatrixTests;

public class MatrixUtilTests {
    [Test]
    [TestCaseSource(typeof(MatrixUtilTestCases), nameof(MatrixUtilTestCases.EqualsMatrixTestCases))]
    public bool EqualsMatrix(Matrix a, Matrix b) => a == b;

    [Test]
    [TestCaseSource(typeof(MatrixUtilTestCases), nameof(MatrixUtilTestCases.IdentityResizedMatrixTestCases))]
    public Matrix IdentityResizedMatrix(Matrix a, int size) => a.IdentityResized(size);

    [Test]
    [TestCaseSource(typeof(MatrixUtilTestCases), nameof(MatrixUtilTestCases.IdentityMatrixTestCases))]
    public Matrix IdentityMatrix(int size) => Matrix.Identity(size);
}

internal static class MatrixUtilTestCases {
    public static List<TestCaseData> EqualsMatrixTestCases =>
        new() {
                  new TestCaseData(new Matrix(1, 2, 3, 4), new Matrix(1, 2, 3, 4)).Returns(true),
                  new TestCaseData(new Matrix(1, 2, 3, 4), new Matrix(1, 2, 3, 5)).Returns(false),
                  new TestCaseData(new Matrix(1, 2, 3, 4), new Matrix(1, 2, 0, 3, 4, 0, 0, 0, 1)).Returns(true),
              };

    public static List<TestCaseData> IdentityResizedMatrixTestCases =>
        new() { new TestCaseData(new Matrix(1, 2, 3, 4), 3).Returns(new Matrix(1, 2, 0, 3, 4, 0, 0, 0, 1)) };

    public static List<TestCaseData> IdentityMatrixTestCases =>
        new() {
                  new TestCaseData(2).Returns(new Matrix(1, 0, 0, 1)),
                  new TestCaseData(3).Returns(new Matrix(1, 0, 0, 0, 1, 0, 0, 0, 1)),
              };
}