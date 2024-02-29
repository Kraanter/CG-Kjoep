using CubeRenderding;

namespace CubeRenderdingTests.VectorTests;

public class VectorConstructorTests {
    [Test] public void EmptyConstructorCreatesNullVector() => Assert.AreEqual(new Vector(), new Vector(0, 0));

    [TestCase(2,  3)]
    [TestCase(3,  7)]
    [TestCase(-6, 0.76f)]
    public void XAndYConstructorCreatesVector(float x, float y) {
        var v = new Vector(x, y);
        Assert.AreEqual(x, v.X);
        Assert.AreEqual(y, v.Y);
        Assert.AreEqual(1, v.W);
    }

    [TestCase(1,   2,       3)]
    [TestCase(-87, 381.81f, -93.28f)]
    public void XYandWConstructorCreatesVector(float x, float y, float w) {
        var v = new Vector(x, y, w);
        Assert.AreEqual(x, v.X);
        Assert.AreEqual(y, v.Y);
        Assert.AreEqual(w, v.W);
    }
}