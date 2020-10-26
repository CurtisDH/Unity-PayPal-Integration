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
    string _authTokenURL = "https://api.sandbox.paypal.com/v1/oauth2/token", _paymentURL = "https://api.sandbox.paypal.com/v1/payments/payment",_executeURL;
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
            StartCoroutine(GetPayerID(Payment.links[0].href,accessToken));
        }
    }
    IEnumerator GetPayerID(string url,string accessToken)
    {
        bool orderApproved = false;
        while (orderApproved == false)
        {
            yield return new WaitForSeconds(5);
            using (var request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + accessToken);
                yield return request.SendWebRequest();
                if (request.isNetworkError || !string.IsNullOrEmpty(request.error))
                {
                    Debug.Log(request.downloadHandler.text);
                    Debug.LogError(request.error);

                }
                else
                {
                    var data = JsonUtility.FromJson<Paypal.Model.Verify>(request.downloadHandler.text);
                    if (data.payer.status == "VERIFIED")
                    {
                        Debug.Log("ORDER APPROVED STATUS VERIFIED");
                        orderApproved = true;
                        Debug.Log(data.links[0].href);
                        Debug.Log(data.links[1].href);
                        Debug.Log(data.links[2].href);
                        Debug.Log(data.payer.payer_info.payer_id);
                        _executeURL = data.links[1].href;
                        StartCoroutine(ExecutePayment(data, accessToken));
                    }
                }
            }
        }
    }

    IEnumerator ExecutePayment(Paypal.Model.Verify payer_ID,string accessToken)
    {
        string payerID = JsonUtility.ToJson(payer_ID.payer.payer_info);
        byte[] rawbody = Encoding.UTF8.GetBytes(payerID);
        using (var request = new UnityWebRequest(_executeURL, "POST"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(rawbody);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + accessToken);
            yield return request.SendWebRequest();
            if (request.isNetworkError || !string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.downloadHandler.text);
            }
            else
            {
                Debug.Log("Request sent successfully");
                Debug.Log(request.downloadHandler.text);
                var data = JsonUtility.FromJson<Paypal.Model.ExecuteResponse>(request.downloadHandler.text);
                Debug.Log("Order Completed:" + data.state);
            }

        }
    }
}
