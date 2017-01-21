// Generated code -- http://www.redblobgames.com/grids/Hexonagons/

using System;
using System.Linq;
using System.Collections.Generic;


class Point
{
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public readonly double x;
    public readonly double y;
}

class Hexon
{
    public Hexon(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
    }
    public readonly int q;
    public readonly int r;
    public readonly int s;

    static public Hexon Add(Hexon a, Hexon b)
    {
        return new Hexon(a.q + b.q, a.r + b.r, a.s + b.s);
    }


    static public Hexon Subtract(Hexon a, Hexon b)
    {
        return new Hexon(a.q - b.q, a.r - b.r, a.s - b.s);
    }


    static public Hexon Scale(Hexon a, int k)
    {
        return new Hexon(a.q * k, a.r * k, a.s * k);
    }

    static public List<Hexon> directions = new List<Hexon>{new Hexon(1, 0, -1), new Hexon(1, -1, 0), new Hexon(0, -1, 1), new Hexon(-1, 0, 1), new Hexon(-1, 1, 0), new Hexon(0, 1, -1)};

    static public Hexon Direction(int direction)
    {
        return Hexon.directions[direction];
    }


    static public Hexon Neighbor(Hexon Hexon, int direction)
    {
        return Hexon.Add(Hexon, Hexon.Direction(direction));
    }

    static public List<Hexon> diagonals = new List<Hexon>{new Hexon(2, -1, -1), new Hexon(1, -2, 1), new Hexon(-1, -1, 2), new Hexon(-2, 1, 1), new Hexon(-1, 2, -1), new Hexon(1, 1, -2)};

    static public Hexon DiagonalNeighbor(Hexon Hexon, int direction)
    {
        return Hexon.Add(Hexon, Hexon.diagonals[direction]);
    }


    static public int Length(Hexon Hexon)
    {
        return (int)((Math.Abs(Hexon.q) + Math.Abs(Hexon.r) + Math.Abs(Hexon.s)) / 2);
    }


    static public int Distance(Hexon a, Hexon b)
    {
        return Hexon.Length(Hexon.Subtract(a, b));
    }

}

struct FractionalHexon
{
    public FractionalHexon(double q, double r, double s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
    }
    public readonly double q;
    public readonly double r;
    public readonly double s;

    static public Hexon HexonRound(FractionalHexon h)
    {
        int q = (int)(Math.Round(h.q));
        int r = (int)(Math.Round(h.r));
        int s = (int)(Math.Round(h.s));
        double q_diff = Math.Abs(q - h.q);
        double r_diff = Math.Abs(r - h.r);
        double s_diff = Math.Abs(s - h.s);
        if (q_diff > r_diff && q_diff > s_diff)
        {
            q = -r - s;
        }
        else
            if (r_diff > s_diff)
            {
                r = -q - s;
            }
            else
            {
                s = -q - r;
            }
        return new Hexon(q, r, s);
    }


    static public FractionalHexon HexonLerp(Hexon a, Hexon b, double t)
    {
        return new FractionalHexon(a.q + (b.q - a.q) * t, a.r + (b.r - a.r) * t, a.s + (b.s - a.s) * t);
    }


    static public List<Hexon> HexonLinedraw(Hexon a, Hexon b)
    {
        int N = Hexon.Distance(a, b);
        List<Hexon> results = new List<Hexon>{};
        double step = 1.0 / Math.Max(N, 1);
        for (int i = 0; i <= N; i++)
        {
            results.Add(FractionalHexon.HexonRound(FractionalHexon.HexonLerp(a, b, step * i)));
        }
        return results;
    }

}

struct OffsetCoord
{
    public OffsetCoord(int col, int row)
    {
        this.col = col;
        this.row = row;
    }
    public readonly int col;
    public readonly int row;
    static public int EVEN = 1;
    static public int ODD = -1;

    static public OffsetCoord QoffsetFromCube(int offset, Hexon h)
    {
        int col = h.q;
        int row = h.r + (int)((h.q + offset * (h.q & 1)) / 2);
        return new OffsetCoord(col, row);
    }


    static public Hexon QoffsetToCube(int offset, OffsetCoord h)
    {
        int q = h.col;
        int r = h.row - (int)((h.col + offset * (h.col & 1)) / 2);
        int s = -q - r;
        return new Hexon(q, r, s);
    }


    static public OffsetCoord RoffsetFromCube(int offset, Hexon h)
    {
        int col = h.q + (int)((h.r + offset * (h.r & 1)) / 2);
        int row = h.r;
        return new OffsetCoord(col, row);
    }


    static public Hexon RoffsetToCube(int offset, OffsetCoord h)
    {
        int q = h.col - (int)((h.row + offset * (h.row & 1)) / 2);
        int r = h.row;
        int s = -q - r;
        return new Hexon(q, r, s);
    }

}

struct Orientation
{
    public Orientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double start_angle)
    {
        this.f0 = f0;
        this.f1 = f1;
        this.f2 = f2;
        this.f3 = f3;
        this.b0 = b0;
        this.b1 = b1;
        this.b2 = b2;
        this.b3 = b3;
        this.start_angle = start_angle;
    }
    public readonly double f0;
    public readonly double f1;
    public readonly double f2;
    public readonly double f3;
    public readonly double b0;
    public readonly double b1;
    public readonly double b2;
    public readonly double b3;
    public readonly double start_angle;
}

struct Layout
{
    public Layout(Orientation orientation, Point size, Point origin)
    {
        this.orientation = orientation;
        this.size = size;
        this.origin = origin;
    }
    public readonly Orientation orientation;
    public readonly Point size;
    public readonly Point origin;
    static public Orientation pointy = new Orientation(Math.Sqrt(3.0), Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
    static public Orientation flat = new Orientation(3.0 / 2.0, 0.0, Math.Sqrt(3.0) / 2.0, Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, Math.Sqrt(3.0) / 3.0, 0.0);

    static public Point HexonToPixel(Layout layout, Hexon h)
    {
        Orientation M = layout.orientation;
        Point size = layout.size;
        Point offset = layout.origin;
        double x = (M.f0 * h.q + M.f1 * h.r) * size.x;
        double y = (M.f2 * h.q + M.f3 * h.r) * size.y;
        return new Point(x + offset.x, y + offset.y);
    }


    static public FractionalHexon PixelToHexon(Layout layout, Point p)
    {
        Orientation M = layout.orientation;
        Point size = layout.size;
        Point offset = layout.origin;
        Point pt = new Point((p.x - offset.x) / size.x, (p.y - offset.y) / size.y);
        double q = M.b0 * pt.x + M.b1 * pt.y;
        double r = M.b2 * pt.x + M.b3 * pt.y;
        return new FractionalHexon(q, r, -q - r);
    }


    static public Point HexonCornerOffset(Layout layout, int corner)
    {
        Orientation M = layout.orientation;
        Point size = layout.size;
        double angle = 2.0 * Math.PI * (corner + M.start_angle) / 6;
        return new Point(size.x * Math.Cos(angle), size.y * Math.Sin(angle));
    }


    static public List<Point> PolygonCorners(Layout layout, Hexon h)
    {
        List<Point> corners = new List<Point>{};
        Point center = Layout.HexonToPixel(layout, h);
        for (int i = 0; i < 6; i++)
        {
            Point offset = Layout.HexonCornerOffset(layout, i);
            corners.Add(new Point(center.x + offset.x, center.y + offset.y));
        }
        return corners;
    }

}



// Tests


class Tests
{
    public Tests()
    {
    }

    static public void EqualHexon(String name, Hexon a, Hexon b)
    {
        if (!(a.q == b.q && a.s == b.s && a.r == b.r))
        {
            Tests.Complain(name);
        }
    }


    static public void EqualOffsetcoord(String name, OffsetCoord a, OffsetCoord b)
    {
        if (!(a.col == b.col && a.row == b.row))
        {
            Tests.Complain(name);
        }
    }


    static public void EqualInt(String name, int a, int b)
    {
        if (!(a == b))
        {
            Tests.Complain(name);
        }
    }


    static public void EqualHexonArray(String name, List<Hexon> a, List<Hexon> b)
    {
        Tests.EqualInt(name, a.Count, b.Count);
        for (int i = 0; i < a.Count; i++)
        {
            Tests.EqualHexon(name, a[i], b[i]);
        }
    }


    static public void TestHexonArithmetic()
    {
        Tests.EqualHexon("Hexon_add", new Hexon(4, -10, 6), Hexon.Add(new Hexon(1, -3, 2), new Hexon(3, -7, 4)));
        Tests.EqualHexon("Hexon_subtract", new Hexon(-2, 4, -2), Hexon.Subtract(new Hexon(1, -3, 2), new Hexon(3, -7, 4)));
    }


    static public void TestHexonDirection()
    {
        Tests.EqualHexon("Hexon_direction", new Hexon(0, -1, 1), Hexon.Direction(2));
    }


    static public void TestHexonNeighbor()
    {
        Tests.EqualHexon("Hexon_neighbor", new Hexon(1, -3, 2), Hexon.Neighbor(new Hexon(1, -2, 1), 2));
    }


    static public void TestHexonDiagonal()
    {
        Tests.EqualHexon("Hexon_diagonal", new Hexon(-1, -1, 2), Hexon.DiagonalNeighbor(new Hexon(1, -2, 1), 3));
    }


    static public void TestHexonDistance()
    {
        Tests.EqualInt("Hexon_distance", 7, Hexon.Distance(new Hexon(3, -7, 4), new Hexon(0, 0, 0)));
    }


    static public void TestHexonRound()
    {
        Hexon a = new Hexon(0, 0, 0);
        Hexon b = new Hexon(1, -1, 0);
        Hexon c = new Hexon(0, -1, 1);
        Tests.EqualHexon("Hexon_round 1", new Hexon(5, -10, 5), FractionalHexon.HexonRound(FractionalHexon.HexonLerp(new Hexon(0, 0, 0), new Hexon(10, -20, 10), 0.5)));
        Tests.EqualHexon("Hexon_round 2", a, FractionalHexon.HexonRound(FractionalHexon.HexonLerp(a, b, 0.499)));
        Tests.EqualHexon("Hexon_round 3", b, FractionalHexon.HexonRound(FractionalHexon.HexonLerp(a, b, 0.501)));
        Tests.EqualHexon("Hexon_round 4", a, FractionalHexon.HexonRound(new FractionalHexon(a.q * 0.4 + b.q * 0.3 + c.q * 0.3, a.r * 0.4 + b.r * 0.3 + c.r * 0.3, a.s * 0.4 + b.s * 0.3 + c.s * 0.3)));
        Tests.EqualHexon("Hexon_round 5", c, FractionalHexon.HexonRound(new FractionalHexon(a.q * 0.3 + b.q * 0.3 + c.q * 0.4, a.r * 0.3 + b.r * 0.3 + c.r * 0.4, a.s * 0.3 + b.s * 0.3 + c.s * 0.4)));
    }


    static public void TestHexonLinedraw()
    {
        Tests.EqualHexonArray("Hexon_linedraw", new List<Hexon>{new Hexon(0, 0, 0), new Hexon(0, -1, 1), new Hexon(0, -2, 2), new Hexon(1, -3, 2), new Hexon(1, -4, 3), new Hexon(1, -5, 4)}, FractionalHexon.HexonLinedraw(new Hexon(0, 0, 0), new Hexon(1, -5, 4)));
    }


    static public void TestLayout()
    {
        Hexon h = new Hexon(3, 4, -7);
        Layout flat = new Layout(Layout.flat, new Point(10, 15), new Point(35, 71));
        Tests.EqualHexon("layout", h, FractionalHexon.HexonRound(Layout.PixelToHexon(flat, Layout.HexonToPixel(flat, h))));
        Layout pointy = new Layout(Layout.pointy, new Point(10, 15), new Point(35, 71));
        Tests.EqualHexon("layout", h, FractionalHexon.HexonRound(Layout.PixelToHexon(pointy, Layout.HexonToPixel(pointy, h))));
    }


    static public void TestConversionRoundtrip()
    {
        Hexon a = new Hexon(3, 4, -7);
        OffsetCoord b = new OffsetCoord(1, -3);
        Tests.EqualHexon("conversion_roundtrip even-q", a, OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, a)));
        Tests.EqualOffsetcoord("conversion_roundtrip even-q", b, OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, b)));
        Tests.EqualHexon("conversion_roundtrip odd-q", a, OffsetCoord.QoffsetToCube(OffsetCoord.ODD, OffsetCoord.QoffsetFromCube(OffsetCoord.ODD, a)));
        Tests.EqualOffsetcoord("conversion_roundtrip odd-q", b, OffsetCoord.QoffsetFromCube(OffsetCoord.ODD, OffsetCoord.QoffsetToCube(OffsetCoord.ODD, b)));
        Tests.EqualHexon("conversion_roundtrip even-r", a, OffsetCoord.RoffsetToCube(OffsetCoord.EVEN, OffsetCoord.RoffsetFromCube(OffsetCoord.EVEN, a)));
        Tests.EqualOffsetcoord("conversion_roundtrip even-r", b, OffsetCoord.RoffsetFromCube(OffsetCoord.EVEN, OffsetCoord.RoffsetToCube(OffsetCoord.EVEN, b)));
        Tests.EqualHexon("conversion_roundtrip odd-r", a, OffsetCoord.RoffsetToCube(OffsetCoord.ODD, OffsetCoord.RoffsetFromCube(OffsetCoord.ODD, a)));
        Tests.EqualOffsetcoord("conversion_roundtrip odd-r", b, OffsetCoord.RoffsetFromCube(OffsetCoord.ODD, OffsetCoord.RoffsetToCube(OffsetCoord.ODD, b)));
    }


    static public void TestOffsetFromCube()
    {
        Tests.EqualOffsetcoord("offset_from_cube even-q", new OffsetCoord(1, 3), OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, new Hexon(1, 2, -3)));
        Tests.EqualOffsetcoord("offset_from_cube odd-q", new OffsetCoord(1, 2), OffsetCoord.QoffsetFromCube(OffsetCoord.ODD, new Hexon(1, 2, -3)));
    }


    static public void TestOffsetToCube()
    {
        Tests.EqualHexon("offset_to_cube even-", new Hexon(1, 2, -3), OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, new OffsetCoord(1, 3)));
        Tests.EqualHexon("offset_to_cube odd-q", new Hexon(1, 2, -3), OffsetCoord.QoffsetToCube(OffsetCoord.ODD, new OffsetCoord(1, 2)));
    }


    static public void TestAll()
    {
        Tests.TestHexonArithmetic();
        Tests.TestHexonDirection();
        Tests.TestHexonNeighbor();
        Tests.TestHexonDiagonal();
        Tests.TestHexonDistance();
        Tests.TestHexonRound();
        Tests.TestHexonLinedraw();
        Tests.TestLayout();
        Tests.TestConversionRoundtrip();
        Tests.TestOffsetFromCube();
        Tests.TestOffsetToCube();
    }


    static public void Main()
    {
        Tests.TestAll();
    }


    static public void Complain(String name)
    {
        Console.WriteLine("FAIL " + name);
    }

}

