using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using static Paypal.Model.Order;

public class PayPalManager : MonoBehaviour
{
    [SerializeField]
    TextAsset[] jsonStrings;
    #region Credentials
    [SerializeField]
    string _clientID = "Ad2gQettf7zTXTK4W1iBadI5MR0Sno3HyPwgxqlfksBU9N02kh2Y7_bGxBth_a8wkqlrhyVRqR_30hiL";
    [SerializeField]
    string _secret = "";

    #endregion

    #region URLS
    string _authTokenURL = "https://api.sandbox.paypal.com/v1/oauth2/token", _paymentURL = "https://api.sandbox.paypal.com/v1/payments/payment";
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

                var data = JsonUtility.FromJson<PayPalAuthToken>(request.downloadHandler.text);
                Debug.Log(data.access_token);
                Debug.Log(data.token_type);
                Debug.Log("Request Sent!");
                StartCoroutine(GetPaymentID(data.access_token));
            }

        }
    }
    IEnumerator GetPaymentID(string accessToken)
    {
        PayPalPaymentCreation Payment = new PayPalPaymentCreation();
        if (jsonStrings != null)
        {
            Payment = JsonUtility.FromJson<PayPalPaymentCreation>(jsonStrings[0].ToString()); //Order can easily be setup ui buttons etc or gameplay elements.
            Payment.transactions[0].invoice_number += System.Guid.NewGuid().ToString();
        }

        var request = new UnityWebRequest(_paymentURL, "POST");
        string PaymentString = (JsonUtility.ToJson(Payment));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(PaymentString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + accessToken);
        yield return request.SendWebRequest();
        if (request.isNetworkError || !string.IsNullOrEmpty(request.error))
        {
            Debug.LogError(request.downloadHandler.text);
            Debug.LogError(request.error);
        }
        else
        {
            var data = JsonUtility.FromJson<PayPalPaymentCreation>(request.downloadHandler.text);
            Payment = data;
            Debug.Log("request sent");
            Application.OpenURL(Payment.links[1].href);
            
        }
    }



}
