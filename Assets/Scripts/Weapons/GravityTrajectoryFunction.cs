using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class GravityTrajectoryFunction : ITrajectoryFunction
    {
        public Vector2 TargetPosition { get; set; }
        public Vector2 OriginPosition { get; set; }

        public GravityTrajectoryFunction(Vector2 originPosition, Vector2 targetPosition)
        {
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
            var delta = targetPoint - initialPoint;
            var a = delta.y / (delta.x * delta.x);
            Vector2 result = initialPoint;
            result.x += time;
            result.y += time * time * a;
            return result;
        }
    }
}
