using UnityEngine;

/// <summary>
/// Base class of all interactive game objects on scene
/// </summary>
public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// Name of object for user
    /// </summary>
    [SerializeField]
    private string m_Nickname;
    public string NickName => m_Nickname;

    
}


