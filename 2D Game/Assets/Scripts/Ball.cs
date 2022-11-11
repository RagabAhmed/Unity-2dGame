using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float destructDelay=5f;
    private void OnBecameInvisible()
    {
        Destroy(gameObject,destructDelay);
    }
    void OnDestroy()
    {
        GameObject.FindObjectOfType<BallHandler>().InitiateRespawn();
    }
}
