using CubeRenderding;

namespace CubeRenderdingTests.RenderPipelineTests;

public class ViewportTests {
    [Test]
    [TestCaseSource(typeof(ViewportTestCases), nameof(ViewportTestCases.ViewportTranslationTestCases))]
    public Vector ViewportTranslation(Vector v, float width, float height) => v.ApplyViewport(width, height);

    [Test]
    [TestCaseSource(typeof(ViewportTestCases), nameof(ViewportTestCases.Viewport2dTo3dProjectionTestCases))]
    public Vector Viewport3dTo2dProjection(Vector v, float distance) => v.ApplyProjection(distance);

    [Test]
    [TestCaseSource(typeof(ViewportTestCases), nameof(ViewportTestCases.WorldToCameraTransformationTestCases))]
    public Vector WorldToCameraTransformation(Vector v, float r, float theta, float phi) {
        Matrix matrix = Matrix.View(r, theta, phi);

        return matrix * v;
    }

    private static float FloatingpointRound(float value) => MathF.Round(value, 4);
}

internal static class ViewportTestCases {
    public static List<TestCaseData> ViewportTranslationTestCases =>
        new() {
                  new TestCaseData(new Vector(),           100, 100).Returns(new Vector(50,  50)),
                  new TestCaseData(new Vector(-100, -100), 100, 100).Returns(new Vector(-50, 150)),
                  new TestCaseData(new Vector(50,   50),   100, 100).Returns(new Vector(100, 0)),
              };

    public static List<TestCaseData> Viewport2dTo3dProjectionTestCases =>
        new() {
                  new TestCaseData(new Vector(2,  1,  -5), 50).Returns(new Vector(20,   10)),
                  new TestCaseData(new Vector(20, 10, 2),  10).Returns(new Vector(-100, -50)),
                  new TestCaseData(new Vector(0,  3),      800).Returns(new Vector(0,   3)),
                  new TestCaseData(new Vector(0,  0),      1).Returns(new Vector(0,     0)),
              };

    public static List<TestCaseData> WorldToCameraTransformationTestCases =>
        new() {
                  // TODO: Find out why these cases all return the same result
                  new TestCaseData(new Vector(), 5, 0,  0).Returns(new Vector(0,  0, -5)),
                  new TestCaseData(new Vector(), 5, 90, 0).Returns(new Vector(0,  0, -5)),
                  new TestCaseData(new Vector(), 5, 0,  90).Returns(new Vector(0, 0, -5)),
                  new TestCaseData(new Vector(), 5, 45, 45).Returns(new Vector(0, 0, -5)),
              };
}