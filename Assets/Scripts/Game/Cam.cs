using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    /// <summary>
    /// Transform of player
    /// </summary>
    Transform Player;

    /// <summary>
    /// Position of tha camera Relative to player
    /// </summary>
    [SerializeField]
    Vector3 CameraPos;
    /// <summary>
    /// Camera posiition relative to world
    /// </summary>
    Vector3 CameraPosWorld;

    /// <summary>
    /// Speed of the camera
    /// </summary>
    [SerializeField]
    float speed;

    /// <summary>
    /// Player transform position
    /// </summary>
    Vector3 PlayerPos
    {
        get
        {
            return Player.position;
        }
        set
        {
            Player.position = value;
        }
    }


    private void Awake()
    {
        AssignPlayer(out Player);
    }

    private void Update()
    {
        CalculateCameraPosition();
    }

    private void LateUpdate()
    {
        RepositionTheCamera();
    }

    /// <summary>
    /// Just read the name of function, it moves camera to where it shoud be SMOOTHLY, in fact camera always moves almost no matter what because of this thing
    /// </summary>
    private void RepositionTheCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position, CameraPosWorld, Vector3.Distance(transform.position, CameraPosWorld) * speed * Time.deltaTime);
    }

    /// <summary>
    /// CameraPosWorld = CameraPos + PlayerPos; refuses to elaborate further; leaves you to wonder;
    /// </summary>
    private void CalculateCameraPosition()
    {
        CameraPosWorld = CameraPos + PlayerPos;
    }

    /// <summary>
    /// Used to find transform of obfect tagged as "Player"
    /// </summary>
    /// <param name="Player">Transform of the player</param>
    private void AssignPlayer(out Transform @Player)
    {
        @Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

}
