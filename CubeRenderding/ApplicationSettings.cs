using Timer = System.Threading.Timer;

namespace CubeRenderding;

public class ApplicationSettings {
    private const float          THETASTART = -100;
    private const float          PHISTART   = -10;
    private       AnimationPhase _animationPhase;
    private       bool           _animationReverse;
    private       Timer?         _timer;
    public        float          D;
    public        float          Phi;
    public        float          R;
    public        float          Scale;
    public        float          Theta;
    public        float          XRot;
    public        float          XTranslate;
    public        float          YRot;
    public        float          YTranslate;
    public        float          ZRot;
    public        float          ZTranslate;

    public ApplicationSettings() { Reset(); }

    public float Reset() {
        StopAnimation();
        _animationPhase   = AnimationPhase.First;
        _animationReverse = false;

        D     = 800;
        R     = 10;
        Theta = THETASTART;
        Phi   = PHISTART;

        XRot       = 0;
        YRot       = 0;
        ZRot       = 0;
        XTranslate = 0;
        YTranslate = 0;
        ZTranslate = 0;
        Scale      = 1;

        return 2f;
    }

    public void KeyDown(Keys key, bool invert, Action invalidate) {
        float constant = invert ? -1 : 1;

        float rotationAmount = 1f       * constant;
        float radians        = MathF.PI / 180;
        float sizeAmount     = 1f       * constant;

        float _ = key switch {
                      // Animation
                      Keys.A => StartAnimation(invalidate),
                      Keys.C => Reset(),

                      // Rotation axis
                      Keys.Y => YRot += rotationAmount,
                      Keys.X => XRot += rotationAmount,
                      Keys.Z => ZRot += rotationAmount,

                      // Transformation
                      Keys.S => Scale += sizeAmount,

                      // Camera
                      Keys.T => Theta += rotationAmount,
                      Keys.P => Phi += rotationAmount,
                      Keys.R => R += sizeAmount,
                      Keys.D => D += sizeAmount,

                      // Arrow keys
                      Keys.Up    => XTranslate += sizeAmount,
                      Keys.Down  => XTranslate -= sizeAmount,
                      Keys.Right => YTranslate += sizeAmount,
                      Keys.Left  => YTranslate -= sizeAmount,

                      // Decimal or pageup
                      Keys.Oemcomma  => ZTranslate += sizeAmount,
                      Keys.OemPeriod => ZTranslate -= sizeAmount,
                      Keys.PageUp    => ZTranslate += sizeAmount,
                      Keys.PageDown  => ZTranslate -= sizeAmount,
                      _              => 0f,
                  };

        invalidate();
    }

    public float StartAnimation(Action invalidate) {
        if (_timer is not null) return 0f;

        _timer = new(Animation, invalidate, 0, 50);

        return 3f;
    }

    public void StopAnimation() {
        _timer?.Dispose();
        _timer = null;
    }

    public void Animation(object? state) {
        const float RADIAN = 1;
        if (_animationPhase == AnimationPhase.First)
            FirstPhase();
        else if (_animationPhase == AnimationPhase.Second)
            SecondPhase();
        else if (_animationPhase == AnimationPhase.Third) ThirdPhase();

        if (_animationPhase < AnimationPhase.Third)
            Theta -= RADIAN;
        else if (_animationPhase == AnimationPhase.Third)
            Phi += RADIAN;
        else if (_animationPhase == AnimationPhase.Fourth) {
            Theta += Theta == THETASTART ? 0 : RADIAN;
            Phi   += Phi   == PHISTART ? 0 : -RADIAN;
        }

        if (Theta == THETASTART && Phi == PHISTART) _animationPhase = AnimationPhase.First;

        if (state is Action invalidate) invalidate();
    }

    private void ThirdPhase() {
        // Phase 3: Rotate 45° over Y -axis and back then switch to phase 1
        if (YRot < 45 && !_animationReverse)
            YRot += 1;
        else if (YRot > 0 && _animationReverse)
            YRot -= 1;
        else {
            if (YRot == 0) _animationPhase = AnimationPhase.Fourth;
            _animationReverse = !_animationReverse;
        }
    }

    private void SecondPhase() {
        // Phase 2: Rotate 45° over X -axis and back then switch to phase 3
        if (XRot < 45 && !_animationReverse)
            XRot += 1;
        else if (XRot > 0 && _animationReverse)
            XRot -= 1;
        else {
            if (XRot == 0) _animationPhase = AnimationPhase.Third;
            _animationReverse = !_animationReverse;
        }
    }

    private void FirstPhase() {
        // Phase 1: Scale until 1.5x and shrink (stepsize 0.01) then switch to phase 2
        if (Scale < 1.5 && !_animationReverse)
            Scale += 0.01f;
        else if (Scale > 1 && _animationReverse)
            Scale -= 0.01f;
        else {
            _animationReverse = !_animationReverse;
            if (Scale == 1) _animationPhase = AnimationPhase.Second;
        }
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
        g.DrawString($"yTranslate: {YTranslate}", font, brush, p);
        p.Y += 20;
        g.DrawString($"zTranslate: {ZTranslate}", font, brush, p);
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
        p.Y += 20;
        g.DrawString($"phase: {_animationPhase}", font, brush, p);
    }

    private enum AnimationPhase {
        First, Second, Third,
        Fourth,
    }
}