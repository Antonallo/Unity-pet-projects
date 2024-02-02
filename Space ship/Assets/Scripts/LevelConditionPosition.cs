using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        private bool m_Reached;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if(Vector3.Distance(Player.Instance.ActiveShip.transform.position, gameObject.transform.position) < m_Radius)
                    {
                        m_Reached = true;
                    }
                }
                return m_Reached;
            }
        }

#if UNITY_EDITOR

        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }

#endif
    }
}

