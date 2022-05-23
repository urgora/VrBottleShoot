/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MouseLooking : MonoBehaviour {
    public float sensitivityX = 1;
    public float sensitivityY = 1;
    public float minimumX = -360;
    public float maximumX = 360;
    public float minimumY = -60;
    public float maximumY = 60;
    public float delayMouse = 1;


    private float rotationX = 0;
    private float rotationY = 0;
    private float rotationXtemp = 0;
    private float rotationYtemp = 0;
    private Quaternion originalRotation;
    private float stunY;


    private Vector2 controllerPositionTemp;
    private Vector2 controllerPositionNext;
    public float rotationXTarget;
    public float rotationYTarget;
    public Slider senSlider;

    private Vector2 lastPlayerpos;
    private float lastrotationX, lastrotationY;
    bool drag = false;
    int fingerId = -1;


    public Vector2 DeltaDrag;
    float movespeed = 3.0f;

    void FixedUpdate()
    {
        if (Datamanager._instance._gameStateNow == GameState.gameRevive || Datamanager._instance._gameStateNow == GameState.gameFinish || Datamanager._instance._gameStateNow == GameState.pausePanel|| Datamanager._instance._gameStateNow == GameState.gameFail)
        {
            drag = false;
            if (Input.touchCount <= 0)
            {
                fingerId = -1;
            }
            DeltaDrag = Vector2.zero;
            controllerPositionTemp = lastPlayerpos;
            rotationX = lastrotationX;
            rotationY = lastrotationY;
        }
    }

    void Update()
    {
        if (Datamanager._instance._gameStateNow != GameState.gameStart)
        {
            return;
        }

        sensitivityY = sensitivityX;
        stunY += (0 - stunY) / 20f;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        rotationX += (Input.GetAxis("Mouse X") * sensitivityX);
        rotationY += (Input.GetAxis("Mouse Y") * sensitivityY);
#endif

        for (int i = 0; i < Input.touchCount; ++i)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began && !drag)
                {
                    controllerPositionNext = new Vector2(touch.position.x, Screen.height - touch.position.y);
                    controllerPositionTemp = controllerPositionNext;
                    drag = true;
                    fingerId = touch.fingerId;
                }
                else if (touch.fingerId == fingerId)
                {
                    controllerPositionNext = new Vector2(touch.position.x, Screen.height - touch.position.y);
                    lastPlayerpos = new Vector2(touch.position.x, touch.position.y);
                    DeltaDrag = (controllerPositionNext - controllerPositionTemp);
                    rotationX = rotationXtemp + (DeltaDrag.x * sensitivityX * Time.deltaTime);
                    lastrotationX = rotationXtemp;
                    rotationY = rotationYtemp + (-DeltaDrag.y * sensitivityY * Time.deltaTime);
                    lastrotationY = rotationYtemp;
                    controllerPositionTemp = Vector2.Lerp(controllerPositionTemp, controllerPositionNext, 0.5f);
                }
            

            if (touch.phase == TouchPhase.Ended && touch.fingerId == fingerId)
            {
                drag = false;
                fingerId = -1;
            }
        }
        if (rotationX >= 360)
        {
            rotationX = 0;
            rotationXtemp = 0;
        }
        if (rotationX <= -360)
        {
            rotationX = 0;
            rotationXtemp = 0;
        }

        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);

        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY + stunY, Vector3.left);
        Quaternion finalRot = originalRotation * xQuaternion * yQuaternion;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRot, Time.deltaTime * 3.0f);
        rotationXtemp = rotationX;
        rotationYtemp = rotationY;
    }
    

    public void Stun(float val)
    {
        stunY = val;
    }

    void Start()
    {
        originalRotation = transform.localRotation;
        if (Datamanager._instance != null)
        {
            senSlider.value = Datamanager._instance._thisGameData.sensitivity;
        }
        else
        {
            senSlider.value = 2f;
        }

        sensitivityX = senSlider.value;
        sensitivityY = senSlider.value;
    }
    public void OnSesitivityChange(UnityEngine.UI.Slider _slider)
    {
        
        if (Datamanager._instance != null)
        {
            Datamanager._instance._thisGameData.sensitivity = _slider.value;
            PlayerPrefs.SetFloat("sensitivity", _slider.value);
        }
        else
        {
            //
        }
        sensitivityX = _slider.value;
        sensitivityY = _slider.value;
    }

    static float ClampAngle(float angle, float min, float max)
    {

        if (angle < -360.0f)
            angle += 360.0f;

        if (angle > 360.0f)
            angle -= 360.0f;

        return Mathf.Clamp(angle, min, max);

    }
}*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseLooking : MonoBehaviour
{
    public float sensitivityX = 1;
    public float sensitivityY = 1;
    public float minimumX = -360;
    public float maximumX = 360;
    public float minimumY = -60;
    public float maximumY = 60;
    public float delayMouse = 1;


    private float rotationX = 0;
    private float rotationY = 0;
    private float rotationXtemp = 0;
    private float rotationYtemp = 0;
    private Quaternion originalRotation;
    private float stunY;
    public Slider senSlider;


    private Vector2 controllerPositionTemp;
    private Vector2 controllerPositionNext;
    public float rotationXTarget;
    public float rotationYTarget;

    bool drag = false;
    int fingerId = -1;

    void Update()
    {
       
        sensitivityY = sensitivityX;
        stunY += (0 - stunY) / 20f;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        rotationX += (Input.GetAxis("Mouse X") * sensitivityX);
        rotationY += (Input.GetAxis("Mouse Y") * sensitivityY);
#endif


        // Touch screen controller
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Touch touch = Input.GetTouch(i); 
            if (touch.phase == TouchPhase.Began && !drag)
            {
                controllerPositionNext = new Vector2(Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y);
                controllerPositionTemp = controllerPositionNext;
                drag = true;
                fingerId = touch.fingerId;
            }
            else if (touch.fingerId == fingerId)
            {
                controllerPositionNext = new Vector2(Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y);
                Vector2 deltagrag = (controllerPositionNext - controllerPositionTemp);
                rotationX = rotationXtemp + (deltagrag.x * sensitivityX * Time.deltaTime);
                rotationY = rotationYtemp + (-deltagrag.y * sensitivityY * Time.deltaTime);
                controllerPositionTemp = Vector2.Lerp(controllerPositionTemp, controllerPositionNext, 0.5f);

            }

            if (touch.phase == TouchPhase.Ended && touch.fingerId == fingerId)
            {
                drag = false;
                fingerId = -1;
            }

        }

        if (rotationX >= 360)
        {
            rotationX = 0;
            rotationXtemp = 0;
        }
        if (rotationX <= -360)
        {
            rotationX = 0;
            rotationXtemp = 0;
        }

        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);

        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY + stunY, Vector3.left);

        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        rotationXtemp = rotationX;
        rotationYtemp = rotationY;

    }

    public void Stun(float val)
    {
        stunY = val;
    }

    void Start()
    {
        originalRotation = transform.localRotation;
        if (Datamanager._instance != null)
        {
            senSlider.value = Datamanager._instance._thisGameData.sensitivity;
        }
        else
        {
            senSlider.value = 2f;
        }

        sensitivityX = senSlider.value;
        sensitivityY = senSlider.value;
    }
    public void OnSesitivityChange(UnityEngine.UI.Slider _slider)
    {

        if (Datamanager._instance != null)
        {
            Datamanager._instance._thisGameData.sensitivity = _slider.value;
            PlayerPrefs.SetFloat("sensitivity", _slider.value);
        }
        else
        {
            //
        }
        sensitivityX = _slider.value;
        sensitivityY = _slider.value;
    }


    static float ClampAngle(float angle, float min, float max)
    {

        if (angle < -360.0f)
            angle += 360.0f;

        if (angle > 360.0f)
            angle -= 360.0f;

        return Mathf.Clamp(angle, min, max);

    }
}