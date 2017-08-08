﻿using App2.Model;
using App2.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static App2.Model.NotificationListMdl;

namespace App2.APIService
{
    public class API
    {
        

        //public readonly string RestURL = @"http://c21.enway.co.in//webservice/index.php";
        public readonly string RestURL = @"http://192.168.1.2/enway_real/webservice/index.php";

        #region Login
        public ResponseModel postLogin(LoginMdl lgmdl)
        {
            ResponseModel response_model = new ResponseModel();
            try
            {
               //var RestURL = BaseURL + "index.php";
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
            catch (Exception )
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
        public NotificationListMdl PostNotification()
        {
             NotificationListMdl jsonResponse = new NotificationListMdl();
            try
            {
                
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
                var response = client.PostAsync(RestURL, content).Result; 
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(jsonresult);
                     jsonResponse = JsonConvert.DeserializeObject<NotificationListMdl>(jsonresult);
                }
            }
            catch (Exception )
            {
               
            }
            return jsonResponse;
        }
        #endregion

        #region Recievable TodayCollection Tabel
        public List<ReceivableNotificationMdl> ReceivableTable()
        {
            ResponseModel response_model = new ResponseModel();
            List<ReceivableNotificationMdl> list_recmdl = new List<ReceivableNotificationMdl>();
            ReceivableNotificationMdl recmdl=null;
            try
            {
               
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);
                var values = new Dictionary<string, string>
                {
                    { "user_id", "1"},
                    { "device_id", "32132"},
                    { "company_name", "CENTURY 21 TOWN PLANNERS PVT. LTD."},
                    //{ "party_id", "4274"},
                    { "tagtype", "receivable_outstanding"}
                };

                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync(RestURL, content).Result; 
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(dataObjects);
                    response_model.Error = jObj["error"].ToString();
                    response_model.TagType = jObj["tagtype"].ToString();
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    foreach (var data in jObj["list"])
                    {
                        recmdl = new ReceivableNotificationMdl();
                        recmdl.Perticular= (data["perticular"].ToString());
                        recmdl.Total_Due = (data["total_due"].ToString());
                        recmdl.Receive = (data["receive"].ToString());
                        recmdl.Balance= (data["balance"].ToString());
                        recmdl.txtWidth= calcScreenWidth / 4 - 20;
                        list_recmdl.Add(recmdl);
                    }

                }
            }
            catch (Exception )
            {
                // StaticMethods.AndroidSnackBar(e.Message);
            }
            return list_recmdl;
        }
        #endregion

        #region Payable TodayCollection Tabel
        public PayableNotificationMdl PayableTable(NavigationMdl td_ntf)
        {
            PayableNotificationMdl response_model = new PayableNotificationMdl();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);

                var values = new Dictionary<string, string>
                        {
                            { "user_id", "1"},
                            { "device_id", td_ntf.Device_id},
                            { "company_name", td_ntf.Company_name},
                            { "party_id", td_ntf.Party_id},
                            { "tagtype", td_ntf.Tag_type}
                        };
               

                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync(RestURL, content).Result; 
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(jsonresult);

                    response_model = JsonConvert.DeserializeObject<PayableNotificationMdl>(jsonresult);

                }
            }
            catch (Exception )
            {
                // StaticMethods.AndroidSnackBar(e.Message);
            }
            return response_model;
        }
        #endregion

        #region AutoComplete
        public async Task<PartysearchMdl> GetParty(NavigationMdl keyName)
        {
            PartysearchMdl _partysearchlistmdl = new PartysearchMdl();
          
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);

                var values = new Dictionary<string, string>
                        {
                            { "user_id", "1"},
                            { "device_id","123456"},
                            { "company_name", keyName.Company_name},
                            { "party_name", keyName.Party_Name},
                            { "tagtype", "partylist"}
                        };


                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync(RestURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(jsonresult);

                    _partysearchlistmdl = JsonConvert.DeserializeObject<PartysearchMdl>(jsonresult);
                }
            }
            catch (Exception)
            {
                // StaticMethods.AndroidSnackBar(e.Message);
            }
            return _partysearchlistmdl;
        }
        #endregion
    }
}
