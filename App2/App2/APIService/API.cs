using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace App2.APIService
{
    public class API
    {
         // public readonly string RestURL = @"http://c21.enway.co.in//webservice/index.php";
        public readonly string RestURL = @"http://192.168.1.2/enway_real/webservice/index.php";
       
        #region Login WebService
        public LoginResponseMdl PostLogin(LoginMdl lgmdl)
        {
            LoginResponseMdl jsonResponse = new LoginResponseMdl();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);

                var values = new Dictionary<string, string>
                {
                    { "username", lgmdl.Username},
                    { "password", lgmdl.Password },
                    { "firebasetoken", lgmdl.Firebasetoken},
                    { "iosdevicetoken", lgmdl.IosToken},
                    { "device_id", lgmdl.DeviceID},
                    { "tagtype", lgmdl.Tagtype}
                };

                var content = new FormUrlEncodedContent(values);

                var response =  client.PostAsync(RestURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(jsonresult);
                    jsonResponse = JsonConvert.DeserializeObject<LoginResponseMdl>(jsonresult);
                }

            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            return jsonResponse;
        }
        #endregion

        #region Notification WebService
        public NotificationListMdl PostNotification(NavigationMdl nav)
        {
             NotificationListMdl jsonResponse = new NotificationListMdl();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);
                
                ResponseModel res = StaticMethods.GetLocalSavedData();
                
                //Create List of KeyValuePairs
                List<KeyValuePair<string, string>> _notificationProperties = new List<KeyValuePair<string, string>>();
              
                //Add 'single' parameters
                _notificationProperties.Add(new KeyValuePair<string, string>("username", "elensoft"));
                _notificationProperties.Add(new KeyValuePair<string, string>("password", "1"));
                _notificationProperties.Add(new KeyValuePair<string, string>("user_id", "1"));
                _notificationProperties.Add(new KeyValuePair<string, string>("device_id", "123456"));
                _notificationProperties.Add(new KeyValuePair<string, string>("company_id", "1"));
                _notificationProperties.Add(new KeyValuePair<string, string>("party_id", "1"));
                _notificationProperties.Add(new KeyValuePair<string, string>("tagtype", "notifications"));
                //Loop over String array and add all instances to our bodyPoperties
                foreach (var dir in nav._site_Id)
                {
                    _notificationProperties.Add(new KeyValuePair<string, string>("site_id[]", dir.Site_id.ToString()));
                }

                //convert your bodyProperties to an object of FormUrlEncodedContent
                var dataContent = new FormUrlEncodedContent(_notificationProperties.ToArray());

                //var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync(RestURL, dataContent).Result; 
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(jsonresult);
                    jsonResponse = JsonConvert.DeserializeObject<NotificationListMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
               // StaticMethods.ShowToast(ex.Message);
            }
            return jsonResponse;
        }
        #endregion

        #region Notification Setting WebService

        public async Task<string> NotificationSetting(NavigationMdl td_ntf)
        {
            string msg=null;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);
                var values = new Dictionary<string, string>
                        {
                            { "user_id", td_ntf.User_id},
                            { "device_id", td_ntf.Device_id},
                            { "min_receipt_amount", td_ntf.Min_Receipt_Amount},
                            { "notification_day_count", td_ntf.Notification_Day_Count},
                            { "tagtype", td_ntf.Tag_type}
                        };


                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(RestURL, content);
                if (response.IsSuccessStatusCode)
                {
                    
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(dataObjects);
                    msg = jObj["error"].ToString();
                }
            }
            catch (Exception e)
            {
                 StaticMethods.ShowToast(e.Message);
            }
            return msg;
        }
        #endregion

        #region Payable Today and Total Collection WebService
        public async Task<PayableNotificationMdl> PayableTable(NavigationMdl nav)
        {
            PayableNotificationMdl response_model = new PayableNotificationMdl();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);

                List<KeyValuePair<string, string>> _notificationProperties = new List<KeyValuePair<string, string>>();

                //Add 'single' parameters
                _notificationProperties.Add(new KeyValuePair<string, string>("username", nav.User_name));
                _notificationProperties.Add(new KeyValuePair<string, string>("password", nav.Password));
                _notificationProperties.Add(new KeyValuePair<string, string>("user_id", nav.User_id));
                _notificationProperties.Add(new KeyValuePair<string, string>("device_id", nav.Device_id));
                _notificationProperties.Add(new KeyValuePair<string, string>("company_id", nav.Company_Id));
                _notificationProperties.Add(new KeyValuePair<string, string>("party_id",nav.Party_id));
                _notificationProperties.Add(new KeyValuePair<string, string>("tagtype", nav.Tag_type));
                //Loop over String array and add all instances to our bodyPoperties
                foreach (var dir in nav._site_Id)
                {
                    _notificationProperties.Add(new KeyValuePair<string, string>("site_id[]", dir.Site_id.ToString()));
                }

                //convert your bodyProperties to an object of FormUrlEncodedContent
               // var dataContent = new FormUrlEncodedContent(_notificationProperties.ToArray());


                var content = new FormUrlEncodedContent(_notificationProperties.ToArray());

                var response =await client.PostAsync(RestURL, content); 
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(jsonresult);
                    response_model = JsonConvert.DeserializeObject<PayableNotificationMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            return response_model;
        }
        #endregion

        #region GetParty AutoComplete WebService
        public async Task<PartysearchMdl> GetParty(NavigationMdl nav)
        {
            PartysearchMdl _partysearchlistmdl = new PartysearchMdl();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);
                List<KeyValuePair<string, string>> _getParty_Prop = new List<KeyValuePair<string, string>>();

                _getParty_Prop.Add(new KeyValuePair<string, string>("username", nav.User_name));
                _getParty_Prop.Add(new KeyValuePair<string, string>("password", nav.Password));
                _getParty_Prop.Add(new KeyValuePair<string, string>("user_id", nav.User_id));
                _getParty_Prop.Add(new KeyValuePair<string, string>("device_id", nav.Device_id));
                _getParty_Prop.Add(new KeyValuePair<string, string>("company_id", nav.Company_Id));
                _getParty_Prop.Add(new KeyValuePair<string, string>("party_name", nav.Party_Name));
                _getParty_Prop.Add(new KeyValuePair<string, string>("tagtype", "partylist"));

                foreach (var dir in nav._site_Id)
                {
                    _getParty_Prop.Add(new KeyValuePair<string, string>("site_id[]", dir.Site_id.ToString()));
                }
                //var values = new Dictionary<string, string>
                //        {
                //            { "user_id", keyName.User_id},
                //            { "device_id",keyName.Device_id},
                //            { "company_name", keyName.Company_name},
                //            { "party_name", keyName.Party_Name},
                //            { "tagtype", "partylist"}
                //        };

                var content = new FormUrlEncodedContent(_getParty_Prop);
                var response = await client.PostAsync(RestURL, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(jsonresult);
                    _partysearchlistmdl = JsonConvert.DeserializeObject<PartysearchMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            return _partysearchlistmdl;
        }
        #endregion
    }
}
