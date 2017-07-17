using App2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App2.APIService
{
   public class API
    {
        //http://www.compliancestudio.io/blog/xamarin-forms-expandable-listview
        //https://github.com/Kimserey/AccordionView
        //http://www.redbitdev.com/cross-platform-animations-using-xamarin-forms/

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
                    response_model.FullName= jObj["full_name"].ToString();
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
        public List<NotificationListMdl> PostNotification(LoginMdl lgmdl)
        {
            ResponseModel _response_model = new ResponseModel();
            List<NotificationListMdl> response_model = new List<NotificationListMdl>();
            Notifications _notifications = null;
            NotificationDate _notificationdate = null;
            Tags _tags = null;

            List<Notifications> _listnotification = new List<Notifications>();
            List<NotificationDate> _listnotification_dates = new List<NotificationDate>();
            List<Tags> _listtag = new List<Tags>();
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
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(dataObjects);
                    _response_model.Error = jObj["error"].ToString();
                    _response_model.TagType = jObj["tagtype"].ToString();
                    _notificationdate = new NotificationDate();
                    _tags = new Tags();
                    _notifications = new Notifications();
                    foreach (var data in jObj["list"])
                    {
                        _notificationdate = new NotificationDate();
                        _notificationdate.Date = (data["date"].ToString());
                        _notificationdate.NotCount = (data["notcount"].ToString());
                        foreach (var data1 in data["tags"])
                        {
                            _tags = new Tags();
                            _tags.Tag = (data1["tag"].ToString());
                            _tags.NotCount = (data1["notcount"].ToString());
                            _tags.TotalAmt = (data1["total_amount"].ToString());

                            try
                            {
                                foreach (var data2 in data1["notifications"])
                                {
                                    _notifications = new Notifications();
                                    _notifications.Amount_received = (data2["amount_received"].ToString());
                                    _notifications.Company_name = (data2["company_name"].ToString());
                                    _notifications.Current_outstanding = (data2["current_outstanding"].ToString());
                                    _notifications.Information_type = (data2["information_type"].ToString());
                                    _notifications.Party_id = (data2["party_id"].ToString());
                                    _notifications.Party_name = (data2["party_name"].ToString());
                                    _notifications.Party_outstanding = (data2["party_outstanding"].ToString());
                                    _notifications.Site_id = (data2["site_id"].ToString());
                                    _notifications.Site_name = (data2["site_name"].ToString());
                                    _notifications.Tagtype = (data2["tagtype"].ToString());
                                    _listnotification.Add(_notifications);
                                }
                                _listtag.Add(_tags);
                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }

                        _listnotification_dates.Add(_notificationdate);
                    }

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
