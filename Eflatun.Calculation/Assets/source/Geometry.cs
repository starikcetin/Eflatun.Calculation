using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace starikcetin.Eflatun.UnityCommon.Utils.Calculation
{
    /// <summary>
    /// Includes 3D Geometry functions.
    /// </summary>
    public static class Geometry
    {
        /// <summary>
        /// Finds the closest <see cref="Vector3"/> in <paramref name="allTargets"/>.
        /// </summary>
        public static Vector3 FindClosest(this Vector3 origin, IList<Vector3> allTargets)
        {
            if (allTargets == null)
            {
                throw new ArgumentNullException("allTargets");
            }

            switch (allTargets.Count)
            {
                case 0: return Vector3.zero;
                case 1: return allTargets[0];
            }

            float closestDistance = Mathf.Infinity;
            var closest = Vector3.zero;

            foreach (var iteratingTarget in allTargets)
            {
                float distanceSqr = (iteratingTarget - origin).sqrMagnitude;

                if (distanceSqr < closestDistance)
                {
                    closestDistance = distanceSqr;
                    closest = iteratingTarget;
                }
            }

            return closest;
        }

        /// <summary>
        /// Finds the closest <see cref="Transform"/> in <paramref name="allTargets"/>.
        /// </summary>
        public static Transform FindClosest(this Vector3 origin, IList<Transform> allTargets)
        {
            if (allTargets == null)
            {
                throw new ArgumentNullException("allTargets");
            }

            switch (allTargets.Count)
            {
                case 0: return null;
                case 1: return allTargets[0];
            }

            float closestDistance = Mathf.Infinity;
            Transform closest = null;

            foreach (var iteratingTarget in allTargets)
            {
                float distanceSqr = (iteratingTarget.position - origin).sqrMagnitude;

                if (distanceSqr < closestDistance)
                {
                    closestDistance = distanceSqr;
                    closest = iteratingTarget;
                }
            }

            return closest;
        }

        /// <summary>
        /// Finds the closest <see cref="GameObject"/> in <paramref name="allTargets"/>.
        /// </summary>
        public static GameObject FindClosest(this Vector3 origin, IList<GameObject> allTargets)
        {
            if (allTargets == null)
            {
                throw new ArgumentNullException("allTargets");
            }

            switch (allTargets.Count)
            {
                case 0: return null;
                case 1: return allTargets[0];
            }

            float closestDistance = Mathf.Infinity;
            GameObject closest = null;

            foreach (var iteratingTarget in allTargets)
            {
                float distanceSqr = (iteratingTarget.transform.position - origin).sqrMagnitude;

                if (distanceSqr < closestDistance)
                {
                    closestDistance = distanceSqr;
                    closest = iteratingTarget;
                }
            }

            return closest;
        }

        /// <summary>
        /// <para>Returns the 3D center of all the points given.</para>
        /// <para>If <paramref name="weighted"/> is true, center point will be closer to the area that points are denser; if false, center will be the geometric exact center of bounding box of points.</para>
        /// </summary>
        public static Vector3 FindCenter(this IList<Vector3> points, bool weighted)
        {
            switch (points.Count)
            {
                case 0: return Vector3.zero;
                case 1: return points[0];
            }

            if (weighted)
            {
                return points.Aggregate(Vector3.zero, (current, point) => current + point) / points.Count;
            }

            var bound = new Bounds {center = points[0]};
            foreach (var point in points)
            {
                bound.Encapsulate(point);
            }

            return bound.center;
        }

        /// <summary>
        /// <para>Returns the 3D center of all the points given.</para>
        /// <para>If <paramref name="weighted"/> is true, center point will be closer to the area that points are denser; if false, center will be the geometric exact center of bounding box of points.</para>
        /// </summary>
        public static Vector3 FindCenter(this IList<GameObject> gameObjects, bool weighted)
        {
            switch (gameObjects.Count)
            {
                case 0: return Vector3.zero;
                case 1: return gameObjects[0].transform.position;
            }

            if (weighted)
            {
                return gameObjects.Aggregate(Vector3.zero,
                           (current, gameObject) => current + gameObject.transform.position) / gameObjects.Count;
            }

            var bound = new Bounds {center = gameObjects[0].transform.position};
            foreach (var gameObject in gameObjects)
            {
                bound.Encapsulate(gameObject.transform.position);
            }

            return bound.center;
        }

        /// <summary>
        /// <para>Returns the 3D center of all the points given.</para>
        /// <para>If <paramref name="weighted"/> is true, center point will be closer to the area that points are denser; if false, center will be the geometric exact center of bounding box of points.</para>
        /// </summary>
        public static Vector3 FindCenter(this IList<Transform> transforms, bool weighted)
        {
            switch (transforms.Count)
            {
                case 0: return Vector3.zero;
                case 1: return transforms[0].position;
            }

            if (weighted)
            {
                return transforms.Aggregate(Vector3.zero, (current, transform) => current + transform.position) /
                       transforms.Count;
            }

            var bound = new Bounds {center = transforms[0].position};
            foreach (var transform in transforms)
            {
                bound.Encapsulate(transform.position);
            }

            return bound.center;
        }
    }
}
