using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float destroytime;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="floor")
        {
            StartCoroutine(destroyball());
        }
        
    }
    IEnumerator destroyball()
    {
        yield return new WaitForSeconds(destroytime);
        Destroy(gameObject);
        
        Debug.Log("working");
    }
}
