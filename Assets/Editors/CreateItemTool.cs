using Paypal.Model;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using static Paypal.Model.Order;

public class CreateItemTool : EditorWindow
{
    public PayPalPaymentCreation Order;
    string path;

    string Cost, Currency, Name, Description;

    bool toggle;


    [MenuItem("Editors/Tools/Create Order")]
    static void Init()
    {
        CreateItemTool window = (CreateItemTool)EditorWindow.GetWindow(typeof(CreateItemTool));
        window.minSize = new Vector2(200f, 200f);
        window.Show();

    }
    void OnEnable()
    {
        Order = new PayPalPaymentCreation();

        path = Application.dataPath + "/Resources/Orders/";
    }


    void OnGUI()
    {

        GUILayout.BeginHorizontal();
        GUILayout.Label("Cost", EditorStyles.boldLabel);
        Cost = GUILayout.TextField(Cost);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Currency, USD,EUR,AUD etc", EditorStyles.boldLabel);
        Currency = GUILayout.TextField(Currency);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name", EditorStyles.boldLabel);
        Name = GUILayout.TextField(Name);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Description", EditorStyles.boldLabel);
        Description = GUILayout.TextField(Description);
        GUILayout.EndHorizontal();
        toggle = EditorGUILayout.BeginToggleGroup("Filled out all information", toggle);
        if (GUILayout.Button("Create JSON file"))
        {
            Debug.Log(Name);
            Order.transactions = new List<Transaction>
            {
                new Transaction()
            };
            #region setting variables
            Order.transactions[0].amount = new Amount();
            Order.transactions[0].amount.total = Cost;
            Order.transactions[0].amount.currency = Currency;
            Order.transactions[0].description = Description;
            Order.transactions[0].amount.details = new Details();
            Order.transactions[0].amount.details.subtotal = Cost;
            Order.transactions[0].amount.details.tax = "0.00";
            Order.transactions[0].amount.details.shipping = "0.00";
            Order.transactions[0].amount.details.handling_fee = "0.00";
            Order.transactions[0].amount.details.insurance = "0.00";
            Order.transactions[0].amount.details.shipping_discount = "0.00";
            Order.transactions[0].description = Description;
            Order.transactions[0].invoice_number = System.Guid.NewGuid().ToString();
            Order.transactions[0].item_list = new ItemList();
            Order.transactions[0].item_list.items = new List<Item>();
            Order.transactions[0].item_list.items.Add(new Item());
            Order.transactions[0].item_list.items[0].name = Name;
            Order.transactions[0].item_list.items[0].sku = "0";
            Order.transactions[0].item_list.items[0].quantity = "1";
            Order.transactions[0].item_list.items[0].description = Description;
            Order.transactions[0].item_list.items[0].price = Cost;
            Order.transactions[0].item_list.items[0].currency = Currency;
            Order.intent = "sale";
            Order.payer = new Payer();
            Order.payer.payment_method = "paypal";
            #endregion
            string orderJson = JsonUtility.ToJson(Order);
            Debug.Log(orderJson);
            using (FileStream fs = File.Create(path + Name + ".json"))
            {
                AddInfo(fs, orderJson);
            }

        }
        EditorGUILayout.EndToggleGroup();
    }
    void AddInfo(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

}

#region old

#endregion
