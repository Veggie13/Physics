using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    public static class Physics
    {
        public static void Step(double dt, double K, double g, double X, double Y, double Vx, double Vy, out double oX, out double oY, out double oVx, out double oVy)
        {
            double D = K * Math.Sqrt(Vx * Vx + Vy * Vy);
            double Dx = -D * Vx;
            double Dy = -D * Vy;

            oX = X + Vx * dt;
            oY = Y + Vy * dt;
            oVx = Vx + Dx * dt;
            oVy = Vy + dt * (Dy - g);
        }

        delegate void F(double v, out double F, out double slope);
        public static double NewtonRoot(double eps, double x0, F f)
        {
            double ff, slope, xNext = x0;
            do
            {
                x0 = xNext;
                f(x0, out ff, out slope);
                xNext = x0 - ff / slope;
            } while (Math.Abs(xNext - x0) > eps && Math.Abs(ff) > eps);

            return 0.5 * (xNext + x0);
        }

        public static void FindArcPeak(double dt, double K, double g, double angle, double v, out double l, out double h)
        {
            double x, y, vx, vy, x2 = 0, y2 = 0, vx2, vy2;
            vx2 = v * Math.Cos(angle);
            vy2 = v * Math.Sin(angle);
            do
            {
                x = x2;
                y = y2;
                vx = vx2;
                vy = vy2;
                Step(dt, K, g, x, y, vx, vy, out x2, out y2, out vx2, out vy2);
            } while (y2 >= y);

            l = x;
            h = y;
        }
    }
}
