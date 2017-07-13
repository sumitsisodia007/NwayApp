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

    }
}
