﻿namespace CubeRenderding;

public class AxisZ {
    private int size;

    public List<Vector> Vertexbuffer;

    public AxisZ(int size = 100) {
        this.size = size;

        Vertexbuffer = new();
        Vertexbuffer.Add(new(0, 0));
        Vertexbuffer.Add(new(0, 0, -size));
    }

    public void Draw(Graphics g, List<Vector> vb) {
        var pen = new Pen(Color.Blue, 2f);
        g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
        var font = new Font("Arial", 10);
        var p    = new PointF(vb[1].X, vb[1].Y);
        g.DrawString("z", font, Brushes.Blue, p);
    }
}