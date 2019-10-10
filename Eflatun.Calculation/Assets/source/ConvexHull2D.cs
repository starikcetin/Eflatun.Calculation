using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace starikcetin.Eflatun.UnityCommon.Utils.Calculation
{
    //
    // Original source: http://csharphelper.com/blog/2014/07/find-the-convex-hull-of-a-set-of-points-in-c/
    // Formatted a little bit for eye-candy, otherwise the algorithm is exactly the same as the original.
    //
    // Note:
    // The original author mentions this algorithm is not efficient. Implement and use QuickHull algorithm if
    // performance is a major concern.
    //

    /// <summary>
    /// Utilities for making 2D convex hulls.
    /// </summary>
    public static class ConvexHull2D
    {
        /// <summary>
        /// Returns the points that make up a polygon's convex hull. This method leaves the <paramref name="points"/> list unchanged.
        /// </summary>
        public static IList<Vector2> MakeConvexHull(this IList<Vector2> points)
        {
            // Cull.
            IList<Vector2> culledPoints = HullCull(points);

            // Find the remaining point with the smallest y value.
            // If there's a tie, take the one with the smaller x value.
            Vector2 bestPt = culledPoints[0];
            foreach (Vector2 pt in culledPoints)
            {
                if ((pt.y < bestPt.y) || ((pt.y == bestPt.y) && (pt.x < bestPt.x)))
                {
                    bestPt = pt;
                }
            }

            // Move this point to the convex hull.
            var hull = new List<Vector2>();
            hull.Add(bestPt);
            culledPoints.Remove(bestPt);

            // Start wrapping up the other points.
            float sweepAngle = 0;
            while (true)
            {
                // Find the point with smallest AngleValue from the last point.
                float x = hull[hull.Count - 1].x;
                float y = hull[hull.Count - 1].y;
                bestPt = culledPoints[0];
                float bestAngle = 3600;

                // Search the rest of the points.
                foreach (Vector2 pt in culledPoints)
                {
                    float testAngle = AngleValue(x, y, pt.x, pt.y);
                    if ((testAngle >= sweepAngle) &&
                        (bestAngle > testAngle))
                    {
                        bestAngle = testAngle;
                        bestPt = pt;
                    }
                }

                // See if the first point is better. If so, we are done.
                float firstAngle = AngleValue(x, y, hull[0].x, hull[0].y);
                if ((firstAngle >= sweepAngle) &&
                    (bestAngle >= firstAngle))
                {
                    // The first point is better. We're done.
                    break;
                }

                // Add the best point to the convex hull.
                hull.Add(bestPt);
                culledPoints.Remove(bestPt);

                sweepAngle = bestAngle;

                // If all of the points are on the hull, we're done.
                if (culledPoints.Count == 0) break;
            }

            return hull;
        }

        /// <summary>
        /// Find the points nearest the upper left, upper right, lower left, and lower right corners.
        /// </summary>
        private static void GetMinMaxCorners(IList<Vector2> points, out Vector2 ul, out Vector2 ur, out Vector2 ll,
            out Vector2 lr)
        {
            // Start with the first point as the solution.
            ul = points[0];
            ur = ul;
            ll = ul;
            lr = ul;

            // Search the other points.
            foreach (Vector2 pt in points)
            {
                if (-pt.x - pt.y > -ul.x - ul.y) ul = pt;
                if (pt.x - pt.y > ur.x - ur.y) ur = pt;
                if (-pt.x + pt.y > -ll.x + ll.y) ll = pt;
                if (pt.x + pt.y > lr.x + lr.y) lr = pt;
            }
        }

        /// <summary>
        /// Find a box that fits inside the MinMax quadrilateral.
        /// </summary>
        private static Rect GetMinMaxBox(IList<Vector2> points)
        {
            // Find the MinMax quadrilateral.
            Vector2 ul, ur, ll, lr;
            GetMinMaxCorners(points, out ul, out ur, out ll, out lr);

            // Get the coordinates of a box that lies inside this quadrilateral.
            float xmin = ul.x;
            float ymin = ul.y;

            float xmax = ur.x;
            if (ymin < ur.y) ymin = ur.y;

            if (xmax > lr.x) xmax = lr.x;
            float ymax = lr.y;

            if (xmin < ll.x) xmin = ll.x;
            if (ymax > ll.y) ymax = ll.y;

            return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        /// <summary>
        /// Cull points out of the convex hull that lie inside the trapezoid defined by the vertices with smallest and largest x and y coordinates. Return the points that are not culled.
        /// </summary>
        private static IList<Vector2> HullCull(IList<Vector2> points)
        {
            Rect cullingBox = GetMinMaxBox(points);
            return points.Where(pt =>
                pt.x <= cullingBox.xMin || pt.x >= cullingBox.xMax || pt.y <= cullingBox.yMin ||
                pt.y >= cullingBox.yMax).ToList();
        }

        /// <summary>
        /// Return a number that gives the ordering of angles WRST horizontal from the point (x1, y1) to (x2, y2).
        /// </summary>
        private static float AngleValue(float x1, float y1, float x2, float y2)
        {
            float t;

            float dx = x2 - x1;
            float ax = Mathf.Abs(dx);
            float dy = y2 - y1;
            float ay = Mathf.Abs(dy);

            if (ax + ay == 0)
            {
                // if the two points are the same, return 360.
                t = 360f / 9f;
            }
            else
            {
                t = dy / (ax + ay);
            }

            if (dx < 0)
            {
                t = 2 - t;
            }
            else if (dy < 0)
            {
                t = 4 + t;
            }

            return t * 90;
        }
    }
}
