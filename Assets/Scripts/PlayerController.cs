using System.Collections;
using BigRookGames.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

    private CharacterController _controller;
    private Animator _anim;
    private GunfireController _gunfireController;
    [FormerlySerializedAs("_cam")] public Transform cam;
    
    [SerializeField]
    private float moveSpeed = 100;
    [SerializeField]
    private float turnSpeed = 150;


    void Awake()
    {
        _gunfireController = GetComponentInChildren<GunfireController>();
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        ShootGun();
        Move();
    }

    private void Move()
    {
        
        var horizontal = Input.GetAxisRaw("Mouse X");
        var vertical = Input.GetAxisRaw("Vertical");
        
        
        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);

        if (vertical != 0)
        {
            
            _controller.SimpleMove(transform.forward * moveSpeed * vertical);
            _anim.SetBool("Walking", true);
        }else 
            _anim.SetBool("Walking", false);

        
    }

    private void ShootGun()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_gunfireController.autoFire)
        {
            StartCoroutine(ShootCoroutine());
        }
    }

    IEnumerator ShootCoroutine()
    {
        _gunfireController.autoFire = true;
        _anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(_gunfireController.shotDelay);
        _gunfireController.autoFire = false;
    }
    
}
