using UnityEngine;


namespace SpaceShooter
{
    /// <summary>
    /// Position limiter. Works in conjunction with LevelBoundary,if it is on scene.
    /// Throws at an object that needs to be restricted
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var levelBoundary = LevelBoundary.Instance;
            var r = levelBoundary.Radius;

            if(transform.position.magnitude > r)
            {
                if(levelBoundary.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (levelBoundary.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }
            }
        }
    }
}

