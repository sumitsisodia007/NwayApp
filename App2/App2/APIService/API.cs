﻿using App2.Model;
using App2.NativeMathods;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App2.APIService
{
    public class API
    {
        public readonly string RestUrl = @"http://c21.enway.co.in//webservice/index.php";
      // public readonly string RestUrl = @"http://192.168.1.2/enway_real/webservice/index.php";
       
        #region Login WebService
        public LoginResponseMdl PostLogin(LoginMdl lgmdl)
        {
            LoginResponseMdl jsonResponse = new LoginResponseMdl();
            try
            {
                HttpResponseMessage response;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(RestUrl);

                    var values = new Dictionary<string, string>
                    {
                        { "username", lgmdl.Username},
                        { "password", lgmdl.Password },
                        { "firebasetoken", lgmdl.Firebasetoken},
                        { "iosdevicetoken", lgmdl.IosToken},
                        { "device_id", lgmdl.DeviceId},
                        { "tagtype", "signin"}
                    };

                    var content = new FormUrlEncodedContent(values);
                    response = client.PostAsync(RestUrl, content).Result;
                }
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject.Parse(jsonresult);
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
                HttpClient client = new HttpClient {BaseAddress = new Uri(RestUrl)};

               
                
                //Create List of KeyValuePairs
                List<KeyValuePair<string, string>> notificationProperties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("company_id", nav.CompanyId),
                    new KeyValuePair<string, string>("party_id", nav.PartyId),
                    new KeyValuePair<string, string>("tagtype", "notifications")
                };

                //Add 'single' parameters
                //Loop over String array and add all instances to our bodyPoperties
                foreach (var dir in nav.SiteIdMdls)
                {
                    if (dir.ChkId == true)
                    {
                        notificationProperties.Add(
                            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                    }
                }

                //convert your bodyProperties to an object of FormUrlEncodedContent
                var dataContent = new FormUrlEncodedContent(notificationProperties.ToArray());

                //var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync(RestUrl, dataContent).Result; 
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject.Parse(jsonresult);
                    jsonResponse = JsonConvert.DeserializeObject<NotificationListMdl>(jsonresult);
                }
            }
            catch (Exception )
            {
               // StaticMethods.ShowToast(ex.Message);
            }
            return jsonResponse;
        }
        #endregion

        #region Notification Setting WebService
        public async Task<string> NotificationSetting(NavigationMdl navigation)
        {
            string msg=null;
            try
            {
                HttpClient client = new HttpClient {BaseAddress = new Uri(RestUrl)};
                var values = new Dictionary<string, string>
                        {
                            { "user_id", navigation.UserId},
                            { "device_id", navigation.DeviceId},
                            { "min_receipt_amount", navigation.MinReceiptAmount},
                            { "notification_day_count", navigation.NotificationDayCount},
                            { "tagtype", "settings"}
                        };


                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(RestUrl, content);
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
            PayableNotificationMdl responseModel = new PayableNotificationMdl();
            try
            {
                HttpClient client = new HttpClient {BaseAddress = new Uri(RestUrl)};

                List<KeyValuePair<string, string>> payableProperties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("company_id", nav.CompanyId),
                    new KeyValuePair<string, string>("party_id", nav.PartyId),
                    new KeyValuePair<string, string>("tagtype", nav.TagType)
                };

                //Add 'single' parameters
                //Loop over String array and add all instances to our bodyPoperties
                foreach (var dir in nav.SiteIdMdls)
                {
                    if (dir.ChkId == true)
                    {
                        payableProperties.Add(
                            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                    }
                }

                //convert your bodyProperties to an object of FormUrlEncodedContent
                // var dataContent = new FormUrlEncodedContent(_notificationProperties.ToArray());


                var content = new FormUrlEncodedContent(payableProperties.ToArray());

                var response =await client.PostAsync(RestUrl, content); 
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject.Parse(jsonresult);
                    responseModel = JsonConvert.DeserializeObject<PayableNotificationMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            return responseModel;
        }
        #endregion

        #region GetParty AutoComplete WebService
        public async Task<PartysearchMdl> GetParty(NavigationMdl nav)
        {
            PartysearchMdl partysearchlistmdl = new PartysearchMdl();
            try
            {
                HttpClient client = new HttpClient {BaseAddress = new Uri(RestUrl)};
                List<KeyValuePair<string, string>> partyProp = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("company_id", nav.CompanyId),
                    new KeyValuePair<string, string>("party_name", nav.PartyName),
                    new KeyValuePair<string, string>("tagtype", "partylist")
                };


                foreach (var dir in nav.SiteIdMdls)
                {
                    if (dir.ChkId == true)
                    {
                        partyProp.Add(
                            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                    }
                }

                var content = new FormUrlEncodedContent(partyProp);
                var response = await client.PostAsync(RestUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject.Parse(jsonresult);
                    partysearchlistmdl = JsonConvert.DeserializeObject<PartysearchMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            return partysearchlistmdl;
        }
        #endregion

        #region CashFlow WebService 
        public CashFlowMdl CashFlowDetails(NavigationMdl nav)
        {
            var jsonResponse = new CashFlowMdl();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(RestUrl) };
                var notificationProperties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("company_id", nav.CompanyId),
                    new KeyValuePair<string, string>("tagtype", "bank_transaction"),

                };

                foreach (var dir in nav.SiteIdMdls)
                {
                    if (dir.ChkId == true)
                    {
                        notificationProperties.Add(
                            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                    }
                }

                var dataContent = new FormUrlEncodedContent(notificationProperties.ToArray());

                var response = client.PostAsync(RestUrl, dataContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject.Parse(jsonresult);
                    jsonResponse = JsonConvert.DeserializeObject<CashFlowMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                // StaticMethods.ShowToast(ex.Message);
            }
            return jsonResponse;
        }
        #endregion

        #region Electricity Consumption WebService 
        public ElectricityMdl ElectricityCuns(NavigationMdl nav)
        {
            var jsonResponse = new ElectricityMdl();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(RestUrl) };
                var notificationProperties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("company_id", nav.CompanyId),
                    new KeyValuePair<string, string>("tagtype", "elec_consumption"),

                };
                foreach (var dir in nav.SiteIdMdls)
                {
                    if (dir.ChkId == true)
                    {
                        notificationProperties.Add(
                            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                    }
                }

                var dataContent = new FormUrlEncodedContent(notificationProperties.ToArray());
                var response = client.PostAsync(RestUrl, dataContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject.Parse(jsonresult);
                    jsonResponse = JsonConvert.DeserializeObject<ElectricityMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                // StaticMethods.ShowToast(ex.Message);
            }
            return jsonResponse;
        }
        #endregion

        #region Home Page
        public HomeMdl MainHomeMdl(NavigationMdl nav)
        {
            var jsonResponse = new HomeMdl();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(RestUrl) };
                var notificationProperties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("company_id", nav.CompanyId),
                    new KeyValuePair<string, string>("firebasetoken", nav.Tokan),
                    new KeyValuePair<string, string>("iosdevicetoken", nav.Tokan),
                    new KeyValuePair<string, string>("tagtype", "homepage"),

                };
                foreach (var dir in nav.SiteIdMdls)
                {
                    if (dir.ChkId == true)
                    {
                        notificationProperties.Add(
                            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                    }
                }

                var dataContent = new FormUrlEncodedContent(notificationProperties.ToArray());
                var response = client.PostAsync(RestUrl, dataContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject.Parse(jsonresult);
                     jsonResponse = JsonConvert.DeserializeObject<HomeMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                // StaticMethods.ShowToast(ex.Message);
            }
            return jsonResponse;
        }
        #endregion
      
        // ExpiredSoon Page,InvoiceCancellation Page This is not used api
        #region ExpiredSoon Page
        public async Task<ExpiredSoonMdl> ExpiredSoon(NavigationMdl nav)
        {
            var jsonResponse = new ExpiredSoonMdl();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(RestUrl) };
                var notificationProperties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("company_id", nav.CompanyId),
                    new KeyValuePair<string, string>("tagtype", "booking_expiry_list"),
                };
                foreach (var dir in nav.SiteIdMdls)
                {
                    if (dir.ChkId == true)
                    {
                        notificationProperties.Add(
                            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                    }
                }

                var dataContent = new FormUrlEncodedContent(notificationProperties.ToArray());
                var response =await client.PostAsync(RestUrl, dataContent);
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = await response.Content.ReadAsStringAsync();
                    JObject.Parse(jsonresult);
                    jsonResponse = JsonConvert.DeserializeObject<ExpiredSoonMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                // StaticMethods.ShowToast(ex.Message);
            }
            return jsonResponse;
        }
        #endregion

        #region InvoiceCancellation Page
        public async Task<CancellationMdl> InvoiceCancellation(NavigationMdl nav)
        {
            var jsonResponse = new CancellationMdl();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(RestUrl) };
                var notificationProperties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("company_id", nav.CompanyId),
                    new KeyValuePair<string, string>("tagtype", "invoice_cancellation_list"),
                };

                foreach (var dir in nav.SiteIdMdls)
                {
                    if (dir.ChkId == true)
                    {
                        notificationProperties.Add(
                            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                    }
                }

                var dataContent = new FormUrlEncodedContent(notificationProperties.ToArray());
                var response = await client.PostAsync(RestUrl, dataContent);
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject.Parse(jsonresult);
                    jsonResponse = JsonConvert.DeserializeObject<CancellationMdl>(jsonresult);
                }
            }
            catch (Exception ex)
            {
                // StaticMethods.ShowToast(ex.Message);
            }
            return jsonResponse;
        }
        #endregion

        #region DataDump to Server
        public async Task<string>  DefaultCall()
        {
            string product = null;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://c21.enway.co.in/android_app_schedular/android_app_schedular.php?data_type=all");
            if (response.IsSuccessStatusCode)
            {
               var jsonresult = response.Content.ReadAsStringAsync().Result;
               
                product = jsonresult.ToString();
            }
            return product;
        }
        #endregion

        #region GetParty AutoComplete WebService
        public async Task<string> SaveExp_Cancel(NavigationMdl nav)
        {
           string responcereturn = null;
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri(RestUrl) };
                List<KeyValuePair<string, string>> partyProp = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", nav.UserName),
                    new KeyValuePair<string, string>("password", nav.Password),
                    new KeyValuePair<string, string>("user_id", nav.UserId),
                    new KeyValuePair<string, string>("device_id", nav.DeviceId),
                    new KeyValuePair<string, string>("expire_day_count", nav.ExpireDayCount.ToString()),
                    new KeyValuePair<string, string>("invoice_cancel_day_count", nav.CancelDayCount.ToString()),
                    new KeyValuePair<string, string>("tagtype", "exp_settings")
                };

                //foreach (var dir in nav.SiteIdMdls)
                //{
                //    if (dir.ChkId == true)
                //    {
                //        partyProp.Add(
                //            new KeyValuePair<string, string>("site_id[]", dir.SiteId.ToString()));
                //    }
                //}

                var content = new FormUrlEncodedContent(partyProp);
                var response = await client.PostAsync(RestUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonresult = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(jsonresult);
                    responcereturn = jObj["message"].ToString();
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            return responcereturn;
        }
        #endregion
    }
}
