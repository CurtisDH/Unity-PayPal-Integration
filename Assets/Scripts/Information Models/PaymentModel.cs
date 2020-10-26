using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentModel : MonoBehaviour
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Payer
    {
        public string payment_method;
    }

    public class Details
    {
        public string subtotal;
        public string tax;
        public string shipping;
        public string handling_fee;
        public string insurance;
        public string shipping_discount;
    }

    public class Amount
    {
        public string total;
        public string currency;
        public Details details;
    }

    public class Item
    {
        public string name;
        public string sku;
        public string price;
        public string currency;
        public string quantity;
        public string description;
        public string tax;
    }

    public class ShippingAddress
    {
        public string recipient_name;
        public string line1;
        public string line2;
        public string city;
        public string state;
        public string phone;
        public string postal_code;
        public string country_code;
    }

    public class ItemList
    {
        public List<Item> items;
        public ShippingAddress shipping_address;
    }

    public class Transaction
    {
        public Amount amount;
        public string description;
        public string custom;
        public string invoice_number;
        public ItemList item_list;
    }

    public class Link
    {
        public string href;
        public string rel;
        public string method;
    }

        public string id;
        public DateTime create_time;
        public DateTime update_time;
        public string state;
        public string intent;
        public Payer payer;
        public List<Transaction> transactions;
        public List<Link> links;





}
