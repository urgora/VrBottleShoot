using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour {
	public float health=50;
	public GameObject bottle,brk;
    public AudioClip bottlebreak;
    // Use this for initialization
    public void takedamage(float amount){
		health -= amount;
		if(health<=0){
			Die ();
		}
		
	}

    GameObject imp = null;
   public	void Die(){
		bottle.SetActive (false);
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }
		imp=Instantiate (brk, bottle.transform.position, bottle.transform.rotation);
        Invoke("throwforce", 0.01f);
        Invoke("offcollision", 0.05f);
        // Destroy (imp,8f);
        SoundManager.PlaySFX(bottlebreak, false, 0);
        FindObjectOfType<level>().noofbottle--;

		//GameManager.instance.BottlesUpdate();
		//GameManager.instance.changeHint2();
        Invoke("deActivateRopeBottle", 1f);
        //UnityMainThreadDispatcher.Instance().Enqueue(() => Destroy(imp, 8f));
        Destroy(imp, 8f);
    }



    public void throwforce()
    {
        //hit.rigidbody.AddForce(-hit.normal * 2000);
        foreach (Transform t in imp.transform)
        {
            t.Rotate(UnityEngine.Random.insideUnitCircle.normalized);
            t.GetComponent<Rigidbody>().AddForce(t.up * 500);
            t.GetComponent<MeshRenderer>().material =this.transform.GetChild(0).GetComponent<MeshRenderer>().material;
        }
    }
    public void offcollision()
    {
        foreach(Transform t in imp.transform)
        {
            Destroy(t.GetComponent<Collider>());
        }
    }

    public void deActivateRopeBottle()
    {
        this.gameObject.SetActive(false);
    }
	IEnumerator re(){
		yield return new WaitForSeconds (3);
		bottle.SetActive (true);
	}
}
