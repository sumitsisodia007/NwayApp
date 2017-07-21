﻿using App2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App2.APIService
{
    public class API
    {
        

        public readonly string BaseURL = "http://192.168.1.2/enway_real/webservice/";

        #region Login
        public ResponseModel postLogin(LoginMdl lgmdl)
        {
            ResponseModel response_model = new ResponseModel();
            try
            {
                var RestURL = BaseURL + "index.php";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);


                var values = new Dictionary<string, string>
                {
                    { "username", lgmdl.Username},
                    { "password", lgmdl.Password },
                    { "firebasetoken", lgmdl.Firebasetoken},
                    { "device_id", lgmdl.DeviceID},
                    { "tagtype", lgmdl.Tagtype}
                };

                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync(RestURL, content).Result; // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(dataObjects);
                    response_model.Message = jObj["message"].ToString();
                    response_model.Error = jObj["error"].ToString();
                    response_model.FullName = jObj["full_name"].ToString();
                    response_model.User_Id = jObj["user_id"].ToString();
                    response_model.Min_Receipt_Amt = jObj["min_receipt_amount"].ToString();
                    response_model.Notification_Day_Count = jObj["notification_day_count"].ToString();
                    response_model.TagType = jObj["tagtype"].ToString();

                }

            }
            catch (Exception ex)
            {
                // StaticMethods.AndroidSnackBar(e.Message);
            }
            finally
            {
                //content = null;
            }
            return response_model;
        }
        #endregion

        #region PostNotification
        public ObservableCollection<NotificationListMdl> PostNotification(LoginMdl lgmdl)
        {
            ResponseModel _response_model = new ResponseModel();
            ObservableCollection<NotificationListMdl> response_model = new ObservableCollection<NotificationListMdl>();
            //Receipt_Notifications _notifications = null;
            //Cancelletion_Notifications _cancelletion_notifications = null;
            //NotificationDate _notificationdate = null;
            //Tags _tags = null;

            //ObservableCollection<Receipt_Notifications> _listnotification = new ObservableCollection<Receipt_Notifications>();
            //ObservableCollection<NotificationDate> _listnotification_dates = new ObservableCollection<NotificationDate>();
            //ObservableCollection<Cancelletion_Notifications> _list_cancelletion = new ObservableCollection<Cancelletion_Notifications>();
            //ObservableCollection<Tags> _listtag = new ObservableCollection<Tags>();
            try
            {
                var RestURL = BaseURL + "index.php";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);


                var values = new Dictionary<string, string>
                {
                    { "user_id", "1"},
                    { "device_id", "123"},
                    { "company_name", "CENTURY 21 TOWN PLANNERS PVT. LTD."},
                    { "party_id", "7875"},
                    { "tagtype", "notifications"}
                };

                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync(RestURL, content).Result; // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(result);
                    IList<NotificationListMdl.TestCases> tTestCases = JsonConvert.DeserializeObject<List<NotificationListMdl.TestCases>>(result);
                    // _response_model.Error = jObj["error"].ToString();
                    //_response_model.TagType = jObj["tagtype"].ToString();
                    //_notificationdate = new NotificationDate();
                    //_tags = new Tags();
                    //_notifications = new Receipt_Notifications();
                    //foreach (var data in jObj["list"])
                    //{
                    //    _notificationdate = new NotificationDate();
                    //    _notificationdate.Date = (data["date"].ToString());
                    //    _notificationdate.NotCount = (data["notcount"].ToString());
                    //    _listnotification_dates.Add(_notificationdate);
                    //    foreach (var data1 in data["tags"])
                    //    {
                    //        _tags = new Tags();
                    //        _tags.Tag = (data1["tag"].ToString());
                    //        _tags.NotCount = (data1["notcount"].ToString());
                    //        _tags.TotalAmt = (data1["total_amount"].ToString());
                    //        _listtag.Add(_tags);

                    //        //if (_tags.Tag == "invoice_cancelletion")
                    //        //{
                    //        //    foreach (var data2 in data1["notifications"])
                    //        //    {
                    //        //        _cancelletion_notifications = new Cancelletion_Notifications();
                    //        //        _cancelletion_notifications.Invoice_code = (data2["invoice_code"].ToString());
                    //        //        _cancelletion_notifications.Invoice_date = (data2["invoice_date"].ToString());
                    //        //        _cancelletion_notifications.Customer_id = (data2["customer_name"].ToString());
                    //        //        _cancelletion_notifications.Customer_name = (data2["customer_id"].ToString());
                    //        //        _cancelletion_notifications.Cancelled_by = (data2["cancelled_by"].ToString());
                    //        //        _cancelletion_notifications.Cancelled_by_id = (data2["cancelled_by_id"].ToString());
                    //        //        _cancelletion_notifications.Information_type = (data2["information_type"].ToString());
                    //        //        _cancelletion_notifications.Tagtype = (data2["tagtype"].ToString());

                    //        //        _list_cancelletion.Add(_cancelletion_notifications);
                    //        //    }
                    //        //}
                    //        //else
                    //        //{
                    //        //    foreach (var data2 in data1["notifications"])
                    //        //    {
                    //        //        _notifications = new Receipt_Paid_Notifications();
                    //        //        _notifications.Amount_received = (data2["amount_received"].ToString());
                    //        //        _notifications.Company_name = (data2["company_name"].ToString());
                    //        //        _notifications.Current_outstanding = (data2["current_outstanding"].ToString());
                    //        //        _notifications.Information_type = (data2["information_type"].ToString());
                    //        //        _notifications.Party_id = (data2["party_id"].ToString());
                    //        //        _notifications.Party_name = (data2["party_name"].ToString());
                    //        //        _notifications.Party_outstanding = (data2["party_outstanding"].ToString());
                    //        //        _notifications.Site_id = (data2["site_id"].ToString());
                    //        //        _notifications.Site_name = (data2["site_name"].ToString());
                    //        //        _notifications.Tagtype = (data2["tagtype"].ToString());
                    //        //        _listnotification.Add(_notifications);
                    //        //    }
                    //        //}
                    //    }
                    //}
                }

            }
            catch (Exception ex)
            {
                // StaticMethods.AndroidSnackBar(e.Message);
            }
            finally
            {
                //content = null;
            }
            return response_model;
        }
        #endregion

    }
}
