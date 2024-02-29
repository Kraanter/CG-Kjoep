namespace CubeRenderding;

public class Square {
    private readonly Color color;
    private readonly float weight;
    private          int   size;

    public List<Vector> vertexbuffer;

    public Square(Color color, int size = 100, float weight = 3) {
        this.color  = color;
        this.size   = size;
        this.weight = weight;

        vertexbuffer = new();
        vertexbuffer.Add(new(-size, -size));
        vertexbuffer.Add(new(size, -size));
        vertexbuffer.Add(new(size, size));
        vertexbuffer.Add(new(-size, size));
    }

    public void Draw(Graphics g, List<Vector> vb) {
        var pen = new Pen(color, weight);
        g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
        g.DrawLine(pen, vb[1].X, vb[1].Y, vb[2].X, vb[2].Y);
        g.DrawLine(pen, vb[2].X, vb[2].Y, vb[3].X, vb[3].Y);
        g.DrawLine(pen, vb[3].X, vb[3].Y, vb[0].X, vb[0].Y);
    }
}