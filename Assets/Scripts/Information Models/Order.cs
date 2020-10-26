using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Paypal.Model.Order;

namespace Paypal.Model
{
    public class Order
    {
        [System.Serializable]
        public class Payer
        {
            public string payment_method;
        }
        [System.Serializable]
        public class Links
        {
            public string href;
            public string rel;
            public string method;
        }
        [System.Serializable]
        public class Details
        {
            public string subtotal;
            public string tax;
            public string shipping;
            public string handling_fee;
            public string insurance;
            public string shipping_discount;
        }
        [System.Serializable]
        public class Amount
        {
            public string total;
            public string currency;
            public Details details;
        }
        [System.Serializable]
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
        [System.Serializable]
        public class ItemList
        {
            public List<Item> items;
        }
        [System.Serializable]
        public class Transaction
        {
            public Amount amount;
            public string description;
            public string custom;
            public string invoice_number;
            public ItemList item_list;
        }
        [System.Serializable]
        public class RedirectUrls
        {
            public string return_url = "www.curtishodgson.com";
            public string cancel_url = "www.curtishodgson.com";
        }
        [System.Serializable]
        public class PayPalPaymentCreation
        {
            public string id; // this is what we need

            public DateTime create_time;
            public DateTime update_time;

            public string intent;
            public Payer payer = new Payer();
            public List<Transaction> transactions = new List<Transaction>();
            public RedirectUrls redirect_urls = new RedirectUrls();
            public List<Links> links = new List<Links>();
        }

        [System.Serializable]
        public class PayPalAuthToken
        {
            public string scope;
            public string access_token;
            public string token_type;
            public string app_id;
            public int expires_in;
            public string nonce;
        }

    }
    [System.Serializable]
    public class Verify
    {
        public string intent;
        public string state;
        public string cart;
        public Payer payer;
        public List<Transaction> transactions;
        public RedirectUrls redirect_urls;
        public List<Link> links;
        [System.Serializable]
        public class PayerInfo
        {
            public string payer_id;
        }
        [System.Serializable]
        public class Payer
        {
            public string payment_method;
            public string status;
            public PayerInfo payer_info;
        }
        [System.Serializable]
        public class RedirectUrls
        {
            public string return_url;
            public string cancel_url;
        }
        [System.Serializable]
        public class Link
        {
            public string href;
            public string rel;
            public string method;
        }
    }
}





