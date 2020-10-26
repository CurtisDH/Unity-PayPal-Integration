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

    [Serializable]
    public class ExecuteResponse
    {
        public string id;
        public string intent;
        public string state;
        public string cart;
        public Payer payer;
        public List<Transaction> transactions;
        public List<object> failed_transactions;
        public DateTime create_time;
        public DateTime update_time;
        public List<Link2> links;

        [Serializable]
        public class ShippingAddress
        {
            public string recipient_name;
            public string line1;
            public string city;
            public string state;
            public string postal_code;
            public string country_code;
        }
        [Serializable]
        public class PayerInfo
        {
            public string email;
            public string first_name;
            public string last_name;
            public string payer_id;
            public ShippingAddress shipping_address;
            public string country_code;
        }
        [Serializable]
        public class Payer
        {
            public string payment_method;
            public string status;
            public PayerInfo payer_info;
        }
        [Serializable]
        public class Details
        {
            public string subtotal;
            public string tax;
            public string shipping;
            public string insurance;
            public string handling_fee;
            public string shipping_discount;
            public string discount;
        }
        [Serializable]
        public class Amount
        {
            public string total;
            public string currency;
            public Details details;
        }
        [Serializable]
        public class Payee
        {
            public string merchant_id;
            public string email;
        }
        [Serializable]
        public class Item
        {
            public string name;
            public string sku;
            public string description;
            public string price;
            public string currency;
            public string tax;
            public int quantity;
        }
        [Serializable]
        public class ShippingAddress2
        {
            public string recipient_name;
            public string line1;
            public string city;
            public string state;
            public string postal_code;
            public string country_code;
        }
        [Serializable]
        public class ItemList
        {
            public List<Item> items;
            public ShippingAddress2 shipping_address;
        }
        [Serializable]
        public class Details2
        {
            public string subtotal;
            public string tax;
            public string shipping;
            public string insurance;
            public string handling_fee;
            public string shipping_discount;
            public string discount;
        }
        [Serializable]

        public class Amount2
        {
            public string total;
            public string currency;
            public Details2 details;
        }
        [Serializable]
        public class TransactionFee
        {
            public string value;
            public string currency;
        }
        [Serializable]
        public class ReceivableAmount
        {
            public string value;
            public string currency;
        }
        [Serializable]
        public class Link
        {
            public string href;
            public string rel;
            public string method;
        }
        [Serializable]
        public class Sale
        {
            public string id;
            public string state;
            public Amount2 amount;
            public string payment_mode;
            public string protection_eligibility;
            public string protection_eligibility_type;
            public TransactionFee transaction_fee;
            public ReceivableAmount receivable_amount;
            public string exchange_rate;
            public string parent_payment;
            public DateTime create_time;
            public DateTime update_time;
            public List<Link> links;
        }
        [Serializable]
        public class RelatedResource
        {
            public Sale sale;
        }
        [Serializable]
        public class Transaction
        {
            public Amount amount;
            public Payee payee;
            public string description;
            public string invoice_number;
            public ItemList item_list;
            public List<RelatedResource> related_resources;
        }
        [Serializable]
        public class Link2
        {
            public string href;
            public string rel;
            public string method;
        }
    }


}





