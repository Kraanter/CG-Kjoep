namespace CubeRenderding;

public class ApplicationSettings {
    public float D;
    public float Phi;
    public float R;
    public float Scale;
    public float Theta;
    public float XRot;
    public float XTranslate;
    public float YRot;
    public float YTranslate;
    public float ZRot;
    public float ZTranslate;

    public ApplicationSettings() { Reset(); }

    public float Reset() {
        D     = 800;
        R     = 10;
        Theta = -100;
        Phi   = -10;

        XRot       = 0;
        YRot       = 0;
        ZRot       = 0;
        XTranslate = 0;
        YTranslate = 0;
        ZTranslate = 0;
        Scale      = 1;

        return 2f;
    }

    public void KeyDown(Keys key, bool invert) {
        float constant = invert ? -1 : 1;

        float rotationAmount = 1f       * constant;
        float radians        = MathF.PI / 180;
        float sizeAmount     = 1f       * constant;

        float _ = key switch {
                      Keys.Y        => YRot += rotationAmount,
                      Keys.X        => XRot += rotationAmount,
                      Keys.Z        => ZRot += rotationAmount,
                      Keys.S        => Scale += sizeAmount,
                      Keys.T        => Theta += rotationAmount * radians,
                      Keys.P        => Phi += rotationAmount   * radians,
                      Keys.R        => R += sizeAmount,
                      Keys.D        => D += sizeAmount,
                      Keys.C        => Reset(),
                      Keys.Up       => XTranslate += sizeAmount,
                      Keys.Down     => XTranslate -= sizeAmount,
                      Keys.Right    => YTranslate += sizeAmount,
                      Keys.Left     => YTranslate -= sizeAmount,
                      Keys.PageUp   => ZTranslate += sizeAmount,
                      Keys.PageDown => ZTranslate -= sizeAmount,
                      _             => 0f,
                  };
    }

    public void Draw(Graphics g) {
        var font  = new Font("Arial", 12);
        var brush = new SolidBrush(Color.Black);
        var p     = new PointF(10, 10);

        g.DrawString($"xRot: {XRot}", font, brush, p);
        p.Y += 20;
        g.DrawString($"yRot: {YRot}", font, brush, p);
        p.Y += 20;
        g.DrawString($"zRot: {ZRot}", font, brush, p);
        p.Y += 20;
        g.DrawString($"xTranslate: {XTranslate}", font, brush, p);
        p.Y += 20;
        g.DrawString($"size: {Scale}", font, brush, p);
        p.Y += 20;
        g.DrawString($"theta: {Theta}", font, brush, p);
        p.Y += 20;
        g.DrawString($"phi: {Phi}", font, brush, p);
        p.Y += 20;
        g.DrawString($"r: {R}", font, brush, p);
        p.Y += 20;
        g.DrawString($"d: {D}", font, brush, p);
    }
}