using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    [Header("Input")]
    [SerializeField] PlayerInput input;

    [Header("Movement")]
    [SerializeField] Vector2 buoyancyVelocity = new Vector2(0f, 0f);
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float accelerationTime = 0.5f;
    [SerializeField] float decelerationTime = 0.5f;

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("Health")]
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] float oneTimeDamagePercent;


    Rigidbody2D rb;
    new Collider2D collider;
    Vector2 moveDirection;
    Vector2 previousVelocity;
    Coroutine moveCoroutine;
    Color colorDamp;


    float t;
    WaitForSeconds waitDecelerationTime;

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        rb.gravityScale = 0f;
        animator = GetComponent<Animator>();

        waitDecelerationTime = new WaitForSeconds(decelerationTime);
        colorDamp = m_spriteRenderer.color;
    }

    private void OnEnable() {
        input.onMove += Move;
        input.onStopMove += StopMove;
        
    }
    private void OnDisable() {
        input.onMove -= Move;
        input.onStopMove -= StopMove;

        StopCoroutine(nameof(MoveRangeLimatationCoroutine));
    }

    void OnAnimationEnd(){
        input.EnableGameInput();
        StartCoroutine(nameof(MoveRangeLimatationCoroutine));
        rb.velocity = buoyancyVelocity;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
    }

# region Move

    void Move(Vector2 moveInput){
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveDirection = moveInput.normalized;
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveDirection * moveSpeed + buoyancyVelocity));
        StopCoroutine(nameof(DecelerationCoroutine));
    }
    void StopMove(){
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveDirection = Vector2.zero;
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime, buoyancyVelocity));
        StartCoroutine(nameof(DecelerationCoroutine));
    }
    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity)
    {
        t = 0f;
        previousVelocity = rb.velocity;

        while (t < 1f)
        {
            t += Time.fixedDeltaTime / time;
            rb.velocity = Vector2.Lerp(previousVelocity, moveVelocity, t);
            yield return waitForFixedUpdate;
        }
    }
    IEnumerator DecelerationCoroutine()
    {
        yield return waitDecelerationTime;

    }
    IEnumerator MoveRangeLimatationCoroutine(){
        while(true){
            transform.position = ViewPort.Instance.PlayerMoveablePostion(transform.position);
            yield return waitForFixedUpdate;
        }
    }

#endregion

# region Collision
# endregion

#region Health
    void TakeDamage(){
        colorDamp.a = colorDamp.a + 1*oneTimeDamagePercent*0.01f;
        m_spriteRenderer.color = colorDamp;
    }
# endregion

#region End
#endregion

}
