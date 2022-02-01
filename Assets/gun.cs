using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public Rigidbody body;
    public GameObject bullet;

    public Transform barrelTip;
    public float hitPower = 1;
    public float recoilPower = 1;
    public float range = 100;
    public LayerMask layer;

    public AudioClip shootSound;
    public float shootVolume = 1f;
    public GameObject guideline;

    private void Start()
    {
        if (body == null && GetComponent<Rigidbody>() != null)
            body = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        //Play the audio sound
        if (shootSound)
            AudioSource.PlayClipAtPoint(shootSound, transform.position, shootVolume);

       GameObject bt=  Instantiate(bullet, barrelTip.position, Quaternion.identity);
        bt.GetComponent<Rigidbody>().AddForce(transform.right * hitPower,ForceMode.Impulse);
        body.AddForce(barrelTip.transform.up * recoilPower * 5, ForceMode.Impulse);
    }
    public void drawguide()
    {
        guideline.SetActive(true);
    }
    public void dontdrawline()
    {
        guideline.SetActive(false);
    }
}
