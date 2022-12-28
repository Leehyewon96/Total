using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    UnityEvent PlayerEvent = null;

    Animator animator = null;
    CharacterController characterController = null;
    Vector3 moveDirection;// = Vector3.zero;
    Vector3 aimDirVec;// = Vector3.zero;

    float speed = 10.0f;
    WaitForSeconds shootWaiting = new WaitForSeconds(0.3f);

    public GameObject Sword = null;

    bool isAimed = false;
    public GameObject aimStart = null;
    public GameObject bullet = null;
    public GameObject bulletPos = null;
    Transform bulletPosition;
    GameObject newBullet = null;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerEvent == null)
            PlayerEvent = new UnityEvent();

        //PlayerEvent.AddListener(Slash);
        PlayerEvent.AddListener(AttackAnim);
        //PlayerEvent.AddListener();

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();

        if (Input.GetMouseButtonDown(0))
        {
            PlayerEvent.Invoke();
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveDirection = new Vector3(x, 0, z);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        //moveDirection.y -= 20.0f * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        if (characterController.velocity.magnitude > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    void Rotation()
    {
        //transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * 10.0f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection - this.transform.position), Time.deltaTime * 5.0f);
    }

    void AttackAnim()
    {
        if (animator == null)
            return;

        animator.SetTrigger("Shoot");
    }
    //ÃÑ °ø°Ý
    void Aim()
    {
        //Ray ray = new Ray();
        RaycastHit[] raycastHits = Physics.SphereCastAll(aimStart.transform.position, 10.0f, this.transform.forward, 100.0f);

        if (raycastHits.Length == 0)
            return;

        isAimed = true;
        float minDistance = 0;
        int minIndex = 0;
        for (int i = 0; i < raycastHits.Length; ++i)
        {
            if (raycastHits[i].collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (minDistance >= raycastHits[i].distance)
                {
                    minIndex = i;
                }
            }
        }

        Vector3 vec = raycastHits[minIndex].collider.gameObject.transform.position - this.transform.position;
        aimDirVec = vec.normalized;

        ShootBullet();
    }

    void ShootBullet()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return shootWaiting;
        if (bullet != null)
        {
            if (!isAimed)
                aimDirVec = this.transform.forward.normalized;

            isAimed = false;
            bulletPosition = bulletPos.transform;
            newBullet = Instantiate(bullet, bulletPosition);
            newBullet.transform.forward = aimDirVec;
            newBullet.GetComponent<Bullet>().dirVec = aimDirVec;
        }
    }
}
