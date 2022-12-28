using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, Characters
{
    UnityEvent unityEvent = null;
    Animator animator;

    public GameObject sword = null;
    BoxCollider swordCollider = null;

    float moveSpeed = 10.0f;
    float rotSpeed = 50.0f;

    float timer = 0;

    public float curHp;
    public float maxHp = 100.0f;

    Vector3 initPos;
    Vector3 initDir;

    public GameObject playerBody = null;
    public Canvas UI_RoundEnd = null;
    public Canvas UI_Canvas = null;

    void Start()
    {
        unityEvent = new UnityEvent();
        unityEvent.AddListener(Damage);
        unityEvent.AddListener(Slash);
        unityEvent.AddListener(SlashAnim);

        animator = GetComponent<Animator>();
        GameManager.instance.CharacterList.Add(this);

        initPos = this.transform.position;
        initDir = this.transform.forward;

        curHp = maxHp;

        if (sword == null)
            Debug.Log("Ä® ¾øÀ½");
        else
        {
            swordCollider = sword.GetComponent<BoxCollider>();
            swordCollider.enabled = false;
        }
    }

    void Update()
    {
        Rotate();
        Move();
        Heal();

        if (Input.GetKeyDown(KeyCode.Mouse0) && curHp > 0 && GameManager.instance.isReStarted == false)
        {
            unityEvent.Invoke();
        }
    }

    public void Restart()
    {
        curHp = maxHp;
        playerBody.SetActive(true);
        UI_RoundEnd.gameObject.SetActive(false);
        UI_Canvas.gameObject.SetActive(true);
    }

    public void RoundEnd()
    {
        this.transform.position = initPos;
        this.transform.forward = initDir;

        playerBody.SetActive(false);
        UI_RoundEnd.gameObject.SetActive(true);
        UI_Canvas.gameObject.SetActive(false);
    }

    void Move()
    {
        if (GameManager.instance.isRoundEnd)
            return;

        if(Input.GetKey(KeyCode.W))
        {
            MoveAnim();
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveAnim();
            this.transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Run", false);
        }
    }

    void Rotate()
    {
        if (GameManager.instance.isRoundEnd)
            return;

        if (Input.GetKey(KeyCode.A))
        {
            MoveAnim();
            this.transform.Rotate(Vector3.up * -rotSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveAnim();
            this.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Run", false);
        }
    }

    void MoveAnim()
    {
        animator.SetBool("Run", true);
    }

    void Heal()
    {
        timer += Time.deltaTime;
        if(timer >= 3)
        {
            curHp += 10;
            if (curHp > 100)
                curHp = 100;

            timer = 0;
        }
    }

    void Damage()
    {
        if (curHp > 0)
            curHp -= 10.0f;
    }

    void Slash()
    {
        StartCoroutine(SwordOff());
    }

    void SlashAnim()
    {
        animator.SetTrigger("Shoot");
    }

    IEnumerator SwordOff()
    {
        yield return new WaitForSeconds(0.7f);
        swordCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);
        swordCollider.enabled = false;
    }
}
