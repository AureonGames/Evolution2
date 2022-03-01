using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using System.Collections;

public class PlayFabLogin : MonoBehaviour
{
    public Transform cmT;
    ConnectionManager cm;
    /*Change this in the future, only for testing*/
    public static string pfID;
    public static string pfType;

    public IEnumerator PlayfabAuth(ConnectionManager cmi)
    {
        cm = cmi;
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "8207E"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        //PlayerPrefs.DeleteAll();
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        else
        {
#if UNITY_ANDROID
            //var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
            var requestNew = new LoginWithCustomIDRequest {CustomId = "Robito", CreateAccount = true };
            // PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginMobileSuccess, OnLoginMobileFailure);
            PlayFabClientAPI.LoginWithCustomID(requestNew, OnLoginMobileSuccess, OnLoginMobileFailure);
            yield return null;
#endif
#if UNITY_IOS
            var requestIOS = new LoginWithIOSDeviceIDRequest { DeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginMobileSuccess, OnLoginMobileFailure);
            yield return null;
#endif
        }
        Debug.Log("Dentro de Auth de Playfab");
        yield return null;
    }

#region Login
    private void OnLoginSuccess(LoginResult result)
    {
        pfID = result.EntityToken.Entity.Id;
        pfType = result.EntityToken.Entity.Type;

        Debug.Log("Congratulations, you made your first successful API call!");
        //Change bool nextstep = true;
        cm.goToNextStep = true;
    }
    private void OnLoginMobileSuccess(LoginResult result)
    {
        pfID = result.EntityToken.Entity.Id;
        pfType = result.EntityToken.Entity.Type;

        Debug.Log("Congratulations, you made your first successful API call!");
        cm.goToNextStep = true;
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        cm.goToNextStep = true;
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Congratulations, it failed");

       cm.goToNextStep = true;
    }
    private void OnLoginMobileFailure(PlayFabError error)
    {

        Debug.Log(error.GenerateErrorReport());
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Congratulations, register failed");
        Debug.LogError(error.GenerateErrorReport());
    }

    public static string ReturnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }
}
#endregion