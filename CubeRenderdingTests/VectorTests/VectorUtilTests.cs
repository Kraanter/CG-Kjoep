using CubeRenderding;

namespace CubeRenderdingTests.VectorTests;

public class VectorUtilTests {
    [TestCase(2,   3)]
    [TestCase(3,   7)]
    [TestCase(1,   1)]
    [TestCase(-19, 94)]
    public void NormalizeVector(float x, float y) {
        Vector normalized = new Vector(x, y).Normalize();
        float  length     = normalized.Length();
        Assert.AreEqual(1, FloatingpointRound(length));
    }

    public void NormalizeZeroVectorReturnsZeroVector() {
        var    zero       = new Vector(0, 0);
        Vector normalized = zero.Normalize();
        Assert.AreEqual(0, normalized.X);
        Assert.AreEqual(0, normalized.Y);
    }

    [TestCase(5,   3,   2)]
    [TestCase(3,   5,   2)]
    [TestCase(0,   0,   0)]
    [TestCase(1,   1,   1)]
    [TestCase(-19, 94,  94)]
    [TestCase(94,  -19, 9999)]
    public void TruncateVector(int x, int y, int max) {
        var v = new Vector(x, y);
        v.Truncate(max);
        Assert.LessOrEqual(v.Length(), max);
    }

    [Test]
    [TestCaseSource(typeof(VectorUtilTestCases), nameof(VectorUtilTestCases.DistanceFromVectorToVectorTestCases))]
    public float DistanceFromVectorToVector(Vector v1, Vector v2) => FloatingpointRound(v1.DistanceTo(v2));

    [Test]
    [TestCaseSource(
        typeof(VectorUtilTestCases),
        nameof(VectorUtilTestCases.DistanceSquaredFromVectorToVectorTestCases)
    )]
    public float DistanceSquaredFromVectorToVector(Vector v1, Vector v2) =>
        FloatingpointRound(v1.DistanceToSquared(v2));

    private static float FloatingpointRound(float value) => MathF.Round(value, 4);
}

public static class VectorUtilTestCases {
    public static List<TestCaseData> DistanceFromVectorToVectorTestCases =>
        new() {
                  new TestCaseData(new Vector(1, 2), new Vector(1, 2)).Returns(0),
                  new TestCaseData(new Vector(0, 0), new Vector(1, 0)).Returns(1),
                  new TestCaseData(new Vector(0, 0), new Vector(0, 1)).Returns(1),
                  new TestCaseData(new Vector(0, 0), new Vector(3, 4)).Returns(5),
              };

    public static List<TestCaseData> DistanceSquaredFromVectorToVectorTestCases =>
        new() {
                  new TestCaseData(new Vector(1, 2), new Vector(1, 2)).Returns(0),
                  new TestCaseData(new Vector(0, 0), new Vector(1, 0)).Returns(1),
                  new TestCaseData(new Vector(0, 0), new Vector(0, 5)).Returns(25),
                  new TestCaseData(new Vector(0, 0), new Vector(3, 4)).Returns(25),
              };
}