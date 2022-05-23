using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class negun : MonoBehaviour {
    [Header("gun properties")]
	public float range;
    public float damage;
    public float initcount;
    private float count;


    ////////////////
	public Camera fpscam,SceneCam;
    public int _FOV = 55,_AimFOV =40;
    public Vector3 adjustatAim = Vector3.zero;
    public Animator GunArm;
	public GameObject impact;
	public ParticleSystem gunsho;
	public AudioClip shoots,totalReload;
    // Use this for initialization
    public GameEvent Onshoot;

    public int reloadcounter =6;
	/// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        count =initcount;
        GunArm.SetBool("Aim",GameManager.instance._aimpos);
        canshoot = true;
        changeFOV();
    }


    public void changeFOV(){
        if(GunArm.GetBool("Aim")){
            SceneCam.fieldOfView = _AimFOV;
            fpscam.fieldOfView = (_AimFOV/2)+10;
            this.transform.localPosition = adjustatAim;
        }else{
            SceneCam.fieldOfView = _FOV;
            fpscam.fieldOfView = _FOV;
            this.transform.localPosition = Vector3.zero;
        }
        //StartCoroutine(lerpFOV());
    }

   /* IEnumerator lerpFOV(){
        yield return new WaitForSeconds(0.1f);
        if(GunArm.GetBool("Aim")){
            float progress = SceneCam.fieldOfView;
            while (progress >= _AimFOV){
                SceneCam.fieldOfView -= Time.deltaTime*0.5f;
            }
            SceneCam.fieldOfView = _AimFOV;
        }else{
            float progress = SceneCam.fieldOfView;
            while (progress <= _FOV){
                SceneCam.fieldOfView += Time.deltaTime*0.5f;
            }
            SceneCam.fieldOfView = _FOV;
        }
    }*/

    public float shootAnimSpeed =1.0f,ReloadAnimSpeed=1.0f;
	// Update is called once per frame
	public bool countloc;
    private int lastFingerId;

	void Update () {
		
		if(countloc){
			count-= Time.deltaTime;
		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			// Handle finger movements based on touch phase.
			switch (touch.phase)
			{
			// Record initial touch position.
			case TouchPhase.Began:

                    if(Datamanager._instance._gameStateNow ==GameState.gameStart)
                    {
                        countloc = true;
                    }
                    lastFingerId = Input.GetTouch(0).fingerId;

                    
				break;

				// Determine direction by comparing the current touch position with the initial one.
			case TouchPhase.Moved:
				
				break;

				// Report that a direction has been chosen when the finger is lifted.
			case TouchPhase.Ended:

                    if (count <= 0)
                    {
                        if (Datamanager._instance._gameStateNow == GameState.gameStart&& !Datamanager._instance._thisGameData.UIcontrols)
                        {
                            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                            //  {
                            if (lastFingerId == Input.GetTouch(0).fingerId && !Datamanager._instance._thisGameData.UIcontrols)
                            {
                                Shoot();
                            }
                              //  }
                        }
                    }
                    countloc = false;
				    count = initcount;
				    break;

			}
		}

#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0)) {
            if (Datamanager._instance._gameStateNow == GameState.gameStart&& !Datamanager._instance._thisGameData.UIcontrols)
            {            
                Shoot();
            }
        }
#endif


	}
    public bool canshoot = true,reloading =false;
    int reloadcount = 0;

    public void Shoot(){
        Debug.Log("Raise-Shoot");
        //Onshoot.Raise();
        /*if (reloadcount == reloadcounter && !reloading)
        {
            StartCoroutine(GunReload());
        }
        else
        {*/
            if (canshoot)
            {
                Debug.Log("SS-Fire-shoot");
                GunArm.speed = shootAnimSpeed;
                if(GunArm.GetBool("Aim") == true){
                GunArm.Play("Aim Fire", 0, 0f);
                }else{
                GunArm.Play("Fire", 0, 0f);
                }
                SoundManager.PlaySFX(shoots,false,0);
                GameManager.instance.BulletsUpdate();
                if(gunsho!=null){
                    //instantiate gun tip smoke
                }
                RaycastHit hit;
                if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
                {
                    Debug.Log(hit.transform.name);
                    target tar = hit.transform.GetComponent<target>();
                    if (tar != null)
                    {
                        Debug.Log("Enter");
                        tar.takedamage(damage);
                        if (GameManager.instance.levelno.value == 1 && GameManager.instance.worldno.value == 0 && !GameManager.instance.tutdone)
                        {
                            StartCoroutine(GameManager.instance.closetut());
                        }

                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal * 2000);
                        if (tar.GetComponent<BoxCollider>() != null)
                        {
                            tar.GetComponent<BoxCollider>().enabled = false;
                        }
                        }
                    }
                   
                    GameObject imp = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(imp, 2f);
                }
                //GameManager.TotalBullets = GameManager.TotalBullets - 1;
                reloadcount += 1;
                                //UI Update
            }
       // }
    }

    IEnumerator GunReload()
    {
        reloadcount = 0;
        canshoot = false;
        reloading = true;
        GunArm.speed =ReloadAnimSpeed;
        GunArm.Play("Reload Out Of Ammo", 0, 0f);
        SoundManager.PlaySFX(totalReload,false,0);
        yield return new WaitForSeconds(2f);
        canshoot = true;
        reloading = false;
    }
}
