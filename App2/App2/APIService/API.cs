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
            StringContent content;
            try
            {
                var RestURL = BaseURL + "index.php";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);
                
                JObject j = new JObject();
                j.Add("username", lgmdl.Username);
                j.Add("password", lgmdl.Password);
                j.Add("firebasetoken", lgmdl.Firebasetoken);
                j.Add("device_id", lgmdl.DeviceID);
                j.Add("tagtype", lgmdl.Tagtype);

                var json = JsonConvert.SerializeObject(j);
                content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(RestURL, content).Result; // Blocking call!
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
                content = null;
            }
            return response_model;
        }
        #endregion

    }
}
