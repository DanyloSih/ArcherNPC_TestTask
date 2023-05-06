using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public struct Trajectory2DInfo
    {
        public Vector2 TrajectoryPoint;
        public Vector2 TangentDirection;

        public Trajectory2DInfo(Vector2 trajectoryPoint, Vector2 tangentDirection)
        {
            TrajectoryPoint = trajectoryPoint;
            TangentDirection = tangentDirection;
        }
    }
}
