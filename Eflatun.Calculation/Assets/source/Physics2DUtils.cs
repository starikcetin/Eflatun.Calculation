using UnityEngine;

namespace starikcetin.Eflatun.UnityCommon.Utils.Calculation
{
    /// <summary>
    /// 2D physics utilities.
    /// </summary>
    public static class Physics2DUtils
    {
        /// <summary>
        /// Calculates the initial velocity for given values.
        /// </summary>
        /// <param name="force">The force.</param>
        /// <param name="mass">The mass of object.</param>
        /// <param name="applyDuration">The duration force applies to object.</param>
        public static Vector2 CalculateInitialVelocity(Vector2 force, float mass, float applyDuration)
        {
            return (force / mass) * applyDuration;
        }
    }
}
