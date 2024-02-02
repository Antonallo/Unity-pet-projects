using UnityEngine;
using SpaceShooter;

public class TimeLevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float timeLimit = 4f;

    private void Start()
    {
        timeLimit += Time.deltaTime;
    }

    public bool IsCompleted => Time.time > timeLimit;
}
