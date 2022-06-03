using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;
public class appentilementcheck : MonoBehaviour
{

    public GameObject check;
    void Awake()
    {
        try
        {
            Core.AsyncInitialize("5747453735270274");
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallback);
        }
        catch (UnityException e)
        {
            Debug.LogError("Platform failed to initialize due to exception.");
            Debug.LogException(e);
            // Immediately quit the application.
            UnityEngine.Application.Quit();
        }
    }


    // Called when the Oculus Platform completes the async entitlement check request and a result is available.
    void EntitlementCallback(Message msg)
    {
        if (msg.IsError) // User failed entitlement check
        {
            // Implements a default behavior for an entitlement check failure -- log the failure and exit the app.
           // check.SetActive(false);
            Debug.LogError("You are NOT entitled to use this app.");
            
            UnityEngine.Application.Quit();
        }
        else // User passed entitlement check
        {
            // Log the succeeded entitlement check for debugging.
            check.SetActive(false);
            Debug.Log("You are entitled to use this app.");
        }
    }
}
