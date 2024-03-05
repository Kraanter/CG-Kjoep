namespace CubeRenderding;

public partial class Form1 : Form {
    // Window dimensions
    private const int WIDTH  = 800;
    private const int HEIGHT = 600;

    private readonly ApplicationSettings _appSettings = new();

    // Objects
    private readonly Cube _cube;

    // Axes
    private readonly AxisX _xAxis;
    private readonly AxisY _yAxis;
    private readonly AxisZ _zAxis;

    public Form1() {
        InitializeComponent();

        KeyDown += Form1_KeyDown;

        // Listen to all key events
        KeyPreview = true;

        Width          = WIDTH;
        Height         = HEIGHT;
        DoubleBuffered = true;

        // Define axes
        _xAxis = new(3);
        _yAxis = new(3);
        _zAxis = new(3);

        _cube = new(Color.Red);
    }

    protected override void OnPaint(PaintEventArgs e) {
        base.OnPaint(e);

        _appSettings.Draw(e.Graphics);

        // Transfor viewport to the center of the screen and make the y coördinates go upward
        e.Graphics.TranslateTransform(WIDTH / 2, HEIGHT / 2);

        Matrix viewTransform = Matrix.View(_appSettings.R, _appSettings.Theta, _appSettings.Phi);

        // Draw axes
        // _xAxis.Draw(e.Graphics, _xAxis.Vertexbuffer);
        // _yAxis.Draw(e.Graphics, _yAxis.Vertexbuffer);
        // _zAxis.Draw(e.Graphics, _zAxis.Vertexbuffer);
        _xAxis.Draw(e.Graphics, _xAxis.Vertexbuffer.ApplyTransformation(viewTransform).ApplyProjection(_appSettings.D));
        _yAxis.Draw(e.Graphics, _yAxis.Vertexbuffer.ApplyTransformation(viewTransform).ApplyProjection(_appSettings.D));
        _zAxis.Draw(e.Graphics, _zAxis.Vertexbuffer.ApplyTransformation(viewTransform).ApplyProjection(_appSettings.D));

        // Apply transformations
        _cube.Draw(e.Graphics, _cube.Vertexbuffer.ApplyTransformation(viewTransform).ApplyProjection(_appSettings.D));
    }

    private void Form1_KeyDown(object? sender, KeyEventArgs e) {
        if (e.KeyCode == Keys.Escape) ExitApp();

        _appSettings.KeyDown(e.KeyCode, e.Shift);

        Invalidate();
    }

    private static float ExitApp() {
        Application.Exit();

        return 1f;
    }
}