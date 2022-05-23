using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collitioncheck : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
  
    [SerializeField]
    LayerMask layertohit;
      Vector3 currenposition, previousposition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currenposition = transform.position;
        previousposition = transform.position;
        StartCoroutine(delayeddestroy());
    }

    // Update is called once per frame
    void Update()
    {
        //currenposition = transform.position;
        //RaycastHit hit;
        //Vector3 direction = previousposition - currenposition;
        //Ray landingray = new Ray(transform.position,direction);

        //Debug.DrawRay(transform.position, direction, Color.red);
        //float distance = Vector3.Distance(currenposition , previousposition);


        currenposition = transform.position;
        RaycastHit hit;
        Vector3 direction = (currenposition - previousposition).normalized;
        Ray landingray = new Ray(previousposition, direction);

        Debug.DrawRay(transform.position, direction, Color.red);
        float distance = Vector3.Distance(currenposition, previousposition);



        if (Physics.Raycast(landingray,out hit,distance))
        {
            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.gameObject.layer == 10)
                hit.collider.gameObject.GetComponent<target>().Die();
            else
                Destroy(gameObject);


        }
        
        
    }
    private void LateUpdate()
    {
        previousposition = currenposition;
    }


    IEnumerator delayeddestroy()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
