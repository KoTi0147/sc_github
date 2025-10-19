#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;
using Playniax.Pyro;
using JetBrains.Annotations;
using Playniax.SpaceShooterArtPack02;

public class LeftMove : MonoBehaviour
{
    public Rigidbody2D rigid;

    public int delayTime = 0;
    public int velocityValue = 0;
    public bool isLeft = true;
    public bool offmove = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isLeft == true)
        {
            rigid.linearVelocity = Vector3.left * velocityValue;
        }
        else
        {
            rigid.linearVelocity = Vector3.right * velocityValue;
        }
    }

    private IEnumerator DelayMotionEffects()
    {
        yield return new WaitForSeconds(delayTime);

        GetComponent<MotionEffects>().enabled = true;
        velocityValue = 0;

        GetComponent<ClawBoss>().returnPoint = transform.localPosition.x;
    }

    void Awake()
    {
        StartCoroutine(DelayMotionEffects());
    } 
}
