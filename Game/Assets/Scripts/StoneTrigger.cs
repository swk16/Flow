using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrigger : MonoBehaviour
{
    [SerializeField] BoxCollider2D trigger;
    [SerializeField] GameObject stone;
    [SerializeField] float time;
    [SerializeField] Animation anim;
    
    float bezierX;
    float bezierY;
    Vector2 bezierPoint;

    float t;
    Coroutine stoneBezierCoroutine;
    Vector3 stonePosition;
    GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        if(trigger == null) {
            trigger = GetComponent<BoxCollider2D>();
        }
    }  

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            t = time;
            stonePosition = stone.transform.position;
            bezierX = other.gameObject.transform.position.x + 6;
            bezierY = stone.transform.position.y;
            bezierPoint = new Vector2(bezierX, bezierY);
            
        
            if(stoneBezierCoroutine != null) {
                StopCoroutine(stoneBezierCoroutine);
            }
            stoneBezierCoroutine = StartCoroutine(StoneBezier());
        }
    }

    private static Vector3 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
 
        Vector3 p = uu * p0;
        p += 2 * u * t * (Vector3)p1;
        p += tt * (Vector3)p2;
 
        return p;
    }

    IEnumerator StoneBezier() {
        while(t > 0) {
            t -= Time.deltaTime;
            stone.transform.position = CalculateCubicBezierPoint(t,player.transform.position,  bezierPoint, stonePosition);
            yield return null;
        }
        trigger.enabled = false;
        stone.SetActive(false);
    }
}
