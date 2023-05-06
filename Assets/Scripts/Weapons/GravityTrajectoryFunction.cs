using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class GravityTrajectoryFunction : ITrajectoryFunction
    {
        public float Speed { get; set; }
        public float Mass { get; set; }
        public Vector2 InitialDirection { get; set; }
        public Vector2 InitialPosition { get; set; }

        public GravityTrajectoryFunction(
            float speed,
            float mass,
            Vector2 initialDirection,
            Vector2 initialPosition)
        {
            Speed = speed;
            Mass = mass;
            InitialDirection = initialDirection;
            InitialPosition = initialPosition;
        }

        public Trajectory2DInfo GetTrajectoryInfoByFunction(float x)
        {
            // Calculate the initial velocity based on the initial direction, the projectile's _speed, and its _mass
            Vector2 initialVelocity = InitialDirection.normalized * Speed * Mathf.Sqrt(Mass);

            // Calculate the gravitational acceleration based on the projectile's _mass
            Vector2 gravity = Physics2D.gravity * Mass;

            // Calculate the time of flight based on the initial velocity and the height of the initial position
            float timeOfFlight = CalculateTimeOfFlight(initialVelocity.y, InitialPosition.y, gravity.y);

            // Calculate the horizontal displacement based on the initial velocity and the time of flight
            float horizontalDisplacement = initialVelocity.x * timeOfFlight;

            // Calculate the vertical displacement based on the initial velocity, the time of flight, and the gravitational acceleration
            float verticalDisplacement = CalculateVerticalDisplacement(initialVelocity.y, timeOfFlight, gravity.y);

            // Calculate the position of the projectile at the given X coordinate
            Vector2 position = InitialPosition + new Vector2(horizontalDisplacement, verticalDisplacement);

            // Calculate the tangent direction at the given position
            Vector2 tangentDirection = CalculateTangentDirectionAtPosition(position, InitialPosition + new Vector2(horizontalDisplacement / 2f, 0f));

            // Set the trajectory point and tangent direction in the result struct
            Trajectory2DInfo result = new Trajectory2DInfo
            {
                TrajectoryPoint = position,
                TangentDirection = tangentDirection
            };

            return result;
        }

        private float CalculateTimeOfFlight(float initialVerticalVelocity, float initialHeight, float gravity)
        {
            // Calculate the time of flight based on the initial vertical velocity, the initial height, and the gravitational acceleration
            float timeOfFlight = (-initialVerticalVelocity + Mathf.Sqrt(initialVerticalVelocity * initialVerticalVelocity - 2f * gravity * initialHeight)) / gravity;

            return timeOfFlight;
        }

        private float CalculateVerticalDisplacement(float initialVerticalVelocity, float time, float gravity)
        {
            // Calculate the vertical displacement based on the initial vertical velocity, the time, and the gravitational acceleration
            float verticalDisplacement = initialVerticalVelocity * time + 0.5f * gravity * time * time;

            return verticalDisplacement;
        }

        private Vector2 CalculateTangentDirectionAtPosition(Vector2 position, Vector2 referencePosition)
        {
            // Calculate the direction from the projectile's position to the reference position
            Vector2 directionToReference = (referencePosition - position).normalized;

            // Rotate the direction by 90 degrees to get the tangent direction
            Vector2 tangentDirection = new Vector2(directionToReference.y, -directionToReference.x);

            return tangentDirection;
        }
    }
}
