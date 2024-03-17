using CubeRenderding;

namespace CubeRenderdingTests.MatrixTests;

public class MatrixMathTests {
    [Test]
    [TestCaseSource(typeof(MatrixMathTestCases), nameof(MatrixMathTestCases.AddTwoMatricesTestCases))]
    public Matrix AddTwoMatrices(Matrix m1, Matrix m2) => m1 + m2;

    [Test]
    [TestCaseSource(typeof(MatrixMathTestCases), nameof(MatrixMathTestCases.SubtractTwoMatricesTestCases))]
    public Matrix SubtractTwoMatrices(Matrix m1, Matrix m2) => m1 - m2;

    [Test]
    [TestCaseSource(typeof(MatrixMathTestCases), nameof(MatrixMathTestCases.MultiplyTwoMatricesTestCases))]
    public Matrix MultiplyTwoMatrices(Matrix m1, Matrix m2) => m1 * m2;

    [Test]
    [TestCaseSource(typeof(MatrixMathTestCases), nameof(MatrixMathTestCases.MultiplyMatrixByVectorTestCases))]
    public Vector MultiplyMatrixByVector(Matrix matrix, Vector vector) => matrix * vector;

    [Test]
    [TestCaseSource(typeof(MatrixMathTestCases), nameof(MatrixMathTestCases.MultiplyMatrixByScalarTestCases))]
    public Matrix MultiplyMatrixByScalar(Matrix m, float scalar) => m * scalar;

    [Test]
    [TestCaseSource(typeof(MatrixMathTestCases), nameof(MatrixMathTestCases.DivideMatrixByScalarTestCases))]
    public Matrix DivideMatrixByScalar(Matrix m, float scalar) => m / scalar;
}

public static class MatrixMathTestCases {
    public static List<TestCaseData> AddTwoMatricesTestCases => [
        new TestCaseData(
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } }),
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })
        ).Returns(new Matrix(new float[,] { { 2, 4 }, { 6, 8 } })),

        new TestCaseData(
            new Matrix(new[,] { { -4, -2 }, { 3.9f, -9.3f } }),
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })
        ).Returns(new Matrix(new[,] { { -3, 0 }, { 6.9f, -5.3f } })),
    ];

    public static List<TestCaseData> SubtractTwoMatricesTestCases => [
        new TestCaseData(
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } }),
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })
        ).Returns(new Matrix(new float[,] { { 0, 0 }, { 0, 0 } })),

        new TestCaseData(
            new Matrix(new[,] { { -4, -2 }, { 3.9f, -9.3f } }),
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })
        ).Returns(new Matrix(new[,] { { -5, -4 }, { 0.9f, -13.3f } })),
    ];

    public static List<TestCaseData> MultiplyTwoMatricesTestCases => [
        new TestCaseData(
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } }),
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })
        ).Returns(new Matrix(new float[,] { { 7, 10 }, { 15, 22 } })),

        new TestCaseData(
            new Matrix(new[,] { { -4, -2 }, { 3.9f, -9.3f } }),
            new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })
        ).Returns(new Matrix(new[,] { { -10, -16 }, { -24, -29.4f } })),
    ];

    public static List<TestCaseData> MultiplyMatrixByVectorTestCases => [
        new TestCaseData(Matrix.Identity(4), new Vector(1, 0)).Returns(new Vector(1,     0)),
        new TestCaseData(Matrix.Identity(4), new Vector(1, 1,  1)).Returns(new Vector(1, 1,  1)),
        new TestCaseData(Matrix.Identity(4), new Vector(1, -1, 1)).Returns(new Vector(1, -1, 1)),
        new TestCaseData(Matrix.Identity(4), new Vector(2.5f, -1.1f, 15.8f)).Returns(
            new Vector(2.5f, -1.1f, 15.8f)
        ),

        new TestCaseData(Matrix.Scale(2), new Vector(1, 1,  1)).Returns(new Vector(2, 2,  2)),
        new TestCaseData(Matrix.Scale(2), new Vector(1, -1, 1)).Returns(new Vector(2, -2, 2)),
        new TestCaseData(Matrix.Scale(2), new Vector(2.5f, -1.1f, 15.8f)).Returns(
            new Vector(5, -2.2f, 31.6f)
        ),

        new TestCaseData(Matrix.Translation(1, 1, 1), new Vector(1, 1,  1)).Returns(new Vector(2, 2, 2)),
        new TestCaseData(Matrix.Translation(1, 1, 1), new Vector(1, -1, 1)).Returns(new Vector(2, 0, 2)),
        new TestCaseData(Matrix.Translation(1, 1, 1), new Vector(2.5f, -1.1f, 15.8f)).Returns(
            new Vector(3.5f, -0.1f, 16.8f)
        ),
    ];

    public static List<TestCaseData> MultiplyMatrixByScalarTestCases => [
        new TestCaseData(new Matrix(new float[,] { { 1, 2 }, { 3, 4 } }), 2).Returns(
            new Matrix(new float[,] { { 2, 4 }, { 6, 8 } })
        ),

        new TestCaseData(new Matrix(new[,] { { -4, -2 }, { 3.9f, -9.3f } }), 3).Returns(
            new Matrix(new[,] { { -12, -6 }, { 11.7f, -27.9f } })
        ),
    ];

    public static List<TestCaseData> DivideMatrixByScalarTestCases => [
        new TestCaseData(new Matrix(new float[,] { { 1, 2 }, { 3, 4 } }), 2).Returns(
            new Matrix(new[,] { { 0.5f, 1 }, { 1.5f, 2 } })
        ),

        new TestCaseData(new Matrix(new[,] { { -4, -2 }, { 3.9f, -9.3f } }), 3).Returns(
            new Matrix(new[,] { { -1.33333337f, -0.6666667f }, { 1.3f, -3.1f } })
        ),
    ];
}