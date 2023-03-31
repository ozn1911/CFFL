using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public delegate void EventHandler(object sender, EventArgs e);
public class CharacterController : MonoBehaviour
{
    #region Variables
    #region Components
    Rigidbody _rb;
    #endregion
    #region Conditions

    /// <summary>
    /// Can the character walk right now?
    /// </summary>
    [SerializeField]
    bool CanWalk;
    [SerializeField]
    Platform platform;
    PlatformEnum _platform;
    #endregion
    #region Movement Variables
    /// <summary>
    /// This particular Vector3 is holding which direction character is going to go
    /// </summary>
    Vector3 _move;
    [SerializeField] float _speed;
    #endregion
    #region Events
    
    public static event EventHandler MyEvent;
    #endregion
    #endregion
    #region Unity Functions
    private void Awake()
    {
        _platform = (PlatformEnum)platform;
        GetRigidbody(Rigidbody: out _rb);

        if(MyEvent == null)
        {
            switch (_platform)
            {
                case PlatformEnum.Windows:
                case PlatformEnum.Linux:
                case PlatformEnum.Mac:
                    MyEvent += PCUpdate;
                    break;
                case PlatformEnum.Android:
                    break;
                case PlatformEnum.IOS:
                    break;
                case PlatformEnum.WebGL:
                    break;
            }
        }
    }
    private void Start()
    {
    }
    private void Update()
    {
        MyEvent(this, EventArgs.Empty);

    }
    private void FixedUpdate()
    {
        Movement();
    } 
    #endregion
    #region Platform
    void PCUpdate(object sender, EventArgs e)
    {
        CalculateMovePC();
    }
    void MobileUpdate()
    {

    }
#endregion

    #region Functions
    #region Components
    void GetRigidbody(out Rigidbody @Rigidbody)
    {
        @Rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    #endregion
    #region Movement
    //there may be more of this if i need more platforms
    void CalculateMovePC()
    {
        if(CanWalk)
        {

            //_move = new Vector3(-Input.GetAxisRaw("Vertical"),0,Input.GetAxisRaw("Horizontal"));
            _move = new Vector3(-Input.GetAxis("Vertical"),0,Input.GetAxis("Horizontal"));
            transform.LookAt(transform.position + _move,Vector3.up);
            _move *= 50 * _speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// i seem to use this function in FixedUpdate, i shall try in Update if results are not satifactory
    /// </summary>
    void Movement()
    {
        _rb.velocity = new Vector3(_move.x,_rb.velocity.y, _move.z);
    }
    #endregion
    #endregion
}
