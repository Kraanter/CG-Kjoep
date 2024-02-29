using CubeRenderding;

namespace CubeRenderdingTests.MatrixTests;

public class MatrixConstructorTests {
    [TestCase(2, 2)]
    [TestCase(3, 7)]
    [TestCase(1, 4)]
    public void DimensionConstructorCreatesEmptyMatrix(int rows, int cols) {
        var m = new Matrix(rows, cols);
        Assert.AreEqual(rows, m.Rows);
        Assert.AreEqual(cols, m.Cols);

        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) Assert.AreEqual(default(float), m[i, j]);
        }
    }

    [TestCase(new float[] { 1, 2, 3 })]
    [TestCase(new float[] { 0, 0, 0 })]
    [TestCase(new float[] { -10, 7, 2 })]
    [TestCase(new[] { 0.7f, -82, -8.8f })]
    public void SingleDimensionArrayCreatesVector(float[] values) {
        var m = new Matrix(values);
        var i = 0;
        foreach (float value in values) Assert.AreEqual(value, m[0, i++]);
    }

    [TestCase(1, 2, 3, 4)]
    public void FourParamConstructorCreatesTwoByTwo(float a, float b, float c, float d) {
        var m = new Matrix(a, b, c, d);
        Assert.AreEqual(a, m[0, 0]);
        Assert.AreEqual(b, m[0, 1]);
        Assert.AreEqual(c, m[1, 0]);
        Assert.AreEqual(d, m[1, 1]);
    }

    [TestCase(1,    2,  3,  4,  5,    6,  7,  8,  9)]
    [TestCase(0,    0,  0,  0,  0,    0,  0,  0,  0)]
    [TestCase(1,    0,  0,  0,  1,    0,  0,  0,  1)]
    [TestCase(0.5f, 1,  0,  0,  0.5f, 0,  0,  0,  1)]
    [TestCase(-1,   -2, -3, -4, -5,   -6, -7, -8, -9)]
    [TestCase(-1,   2,  3,  4,  5,    6,  7,  8,  9)]
    [TestCase(1,    -2, 3,  4,  5,    6,  7,  8,  9)]
    public void NineParamConstructorCreatesThreeByThree(
        float a,
        float b,
        float c,
        float d,
        float e,
        float f,
        float g,
        float h,
        float i
    ) {
        var m = new Matrix(a, b, c, d, e, f, g, h, i);
        Assert.AreEqual(a, m[0, 0]);
        Assert.AreEqual(b, m[0, 1]);
        Assert.AreEqual(c, m[0, 2]);
        Assert.AreEqual(d, m[1, 0]);
        Assert.AreEqual(e, m[1, 1]);
        Assert.AreEqual(f, m[1, 2]);
        Assert.AreEqual(g, m[2, 0]);
        Assert.AreEqual(h, m[2, 1]);
        Assert.AreEqual(i, m[2, 2]);
    }
}