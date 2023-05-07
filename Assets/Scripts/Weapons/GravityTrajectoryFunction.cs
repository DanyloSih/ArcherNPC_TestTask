using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class GravityTrajectoryFunction : ITrajectoryFunction
    {
        public struct ParabolaCoefficients
        {
            public float A;
            public float B;
            public float C;
        }

        public Vector2 TargetPosition { get; set; }
        public float TrajectoryHeight { get; set; }
        public Vector2 OriginPosition { get; set; }

        public GravityTrajectoryFunction(Vector2 originPosition, Vector2 targetPosition, float trajectoryHeight)
        {
            TrajectoryHeight = trajectoryHeight;
            OriginPosition = originPosition;
            TargetPosition = targetPosition;
        }

        public Trajectory2DInfo GetTrajectoryInfoByFunction(float time)
        {
            var pointA = GetPositionByTime(time);
            var pointB = GetPositionByTime(time + 0.01f);
            var direction = pointB - pointA;

            return new Trajectory2DInfo(pointA, direction);
        }

        private Vector2 GetPositionByTime(float time)
        {
            return CalculateProjectilePosition(
                OriginPosition, TargetPosition, time);
        }

        private Vector2 CalculateProjectilePosition(Vector2 initialPoint, Vector2 targetPoint, float time)
        {
            Vector2 origin = new Vector2(
                Mathf.Min(initialPoint.x, targetPoint.x), 
                Mathf.Min(initialPoint.y, targetPoint.y));

            Vector2 point1 = initialPoint - origin;
            Vector2 point3 = targetPoint - origin;
            Vector2 point2 = targetPoint - initialPoint;
            point2.x /= 2f;
            point2.y = point1.y + TrajectoryHeight;

            var parabolaCoefs = CalculateParabolaCoefficients(point1, point2, point3);
            float y = parabolaCoefs.A * time * time + parabolaCoefs.B * time + parabolaCoefs.C;
     
            Vector2 result = origin;
            result.x += time;
            result.y += y;

            return result;
        }

        private ParabolaCoefficients CalculateParabolaCoefficients(Vector2 point1, Vector2 point2, Vector2 point3)
        {
            float x1 = point1.x;
            float x2 = point2.x;
            float x3 = point3.x;

            float y1 = point1.y;
            float y2 = point2.y;
            float y3 = point3.y;

            ParabolaCoefficients result = new ParabolaCoefficients();
            float denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
            result.A = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
            result.B = (x3 * x3 * (y1 - y2) + x1 * x1 * (y2 - y3) + x2 * x2 * (y3 - y1)) / denom;
            result.C = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;

            return result;
        }
    }
}
