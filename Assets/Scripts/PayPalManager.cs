using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PayPalManager : MonoBehaviour
{

    #region Credentials
    [SerializeField]
    string _clientID = "Ad2gQettf7zTXTK4W1iBadI5MR0Sno3HyPwgxqlfksBU9N02kh2Y7_bGxBth_a8wkqlrhyVRqR_30hiL";
    string _secret = "";

    #endregion

    #region URLS
    string _authTokenURL = "https://api.sandbox.paypal.com/v1/oauth2/token";
    #endregion


    void Start()
    {
        StartCoroutine(GetAccessToken());
    }


    IEnumerator GetAccessToken()
    {

        WWWForm form = new WWWForm();
        string base64 = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(_clientID + ":" + _secret));
        form.AddField("grant_type", "client_credentials");
        using (var request = UnityWebRequest.Post(_authTokenURL, form))
        {
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Accept-Language", "en_US");
            request.SetRequestHeader("Authorization", "Basic " + base64);
            yield return request.SendWebRequest();

            if (request.isNetworkError || !string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.downloadHandler.text);
            }
            else
            {

                var data = JsonUtility.FromJson<AuthTokenData>(request.downloadHandler.text);
                Debug.Log(data.access_token);
                Debug.Log(data.token_type);
                Debug.Log("Request Sent!");
            }

        }
    }
    [Serializable]
    public class AuthTokenData
    {
        public string access_token;
        public string token_type;


    }

    public class PaymentInformation
    {
        public string intent;


    }
    public class redirect_urls
    {
        public string return_url = "www.google.com";
        public string cancel_url = "www.google.com";
    }
    public class payer
    {
        public string payment_method = "paypal";
    }


}
