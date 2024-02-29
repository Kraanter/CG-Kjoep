namespace CubeRenderding;

public class AxisX {
    public readonly List<Vector> Vertexbuffer;
    private         int          size;

    public AxisX(int size = 100) {
        this.size = size;

        Vertexbuffer = new();
        Vertexbuffer.Add(new(0, 0));
        Vertexbuffer.Add(new(size, 0));
    }

    public void Draw(Graphics g, List<Vector> vb) {
        var pen = new Pen(Color.Red, 2f);
        g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
        var font = new Font("Arial", 10);
        var p    = new PointF(vb[1].X, vb[1].Y);
        g.DrawString("x", font, Brushes.Red, p);
    }
}