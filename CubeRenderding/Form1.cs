namespace CubeRenderding;

public partial class Form1 : Form {
    // Window dimensions
    private const int WIDTH  = 800;
    private const int HEIGHT = 600;

    // Objects
    private readonly Square _square;
    private readonly Square _square2;
    private readonly Square _square3;
    private readonly Square _square4;

    // Axes
    private readonly AxisX _xAxis;
    private readonly AxisY _yAxis;

    public Form1() {
        InitializeComponent();

        KeyDown += Form1_KeyDown;

        Width          = WIDTH;
        Height         = HEIGHT;
        DoubleBuffered = true;

        // Define axes
        _xAxis = new(200);
        _yAxis = new(200);

        // Create objects
        _square  = new(Color.Purple);
        _square2 = new(Color.Orange);
        _square3 = new(Color.Cyan);
        _square4 = new(Color.DarkBlue);
    }

    protected override void OnPaint(PaintEventArgs e) {
        base.OnPaint(e);

        // Transfor viewport to the center of the screen and make the y coördinates go upward
        e.Graphics.TranslateTransform(WIDTH / 2, HEIGHT / 2);

        // e.Graphics.ScaleTransform(1, -1);

        // Draw axes
        _xAxis.Draw(e.Graphics, _xAxis.Vertexbuffer);
        _yAxis.Draw(e.Graphics, _yAxis.vertexbuffer);

        // Draw square
        _square.Draw(e.Graphics, _square.vertexbuffer.RotateVectors(20));
        _square2.Draw(e.Graphics, _square2.vertexbuffer.TranslateVectors(125, 75));
        _square3.Draw(e.Graphics, _square3.vertexbuffer.ScaleVectors(2, 2));
        _square4.Draw(e.Graphics, _square4.vertexbuffer);
    }

    private void Form1_KeyDown(object? sender, KeyEventArgs e) {
        if (e.KeyCode == Keys.Escape) Application.Exit();
    }
}