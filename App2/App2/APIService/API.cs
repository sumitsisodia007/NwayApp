using App2.Model;
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
        

        public readonly string RestURL = "http://192.168.1.2/enway_real/webservice/index.php";

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
        public NotificationListMdl PostNotification(LoginMdl lgmdl)
        {
            ResponseModel _response_model = new ResponseModel();

            Receipt_Notifications _Receipt_notifications = null;
            Cancelletion_Notifications _cancel_notifications = null;
            Invoice_Event _invoice_notifications = null;
            Paid_Notifications _Paid_notifications = null;
            NotificationDate _date = null;
            Tags _tags = null;

            ObservableCollection<NotificationDate> _list_dates = new ObservableCollection<NotificationDate>();
            ObservableCollection<Tags> _listtag = new ObservableCollection<Tags>();
            ObservableCollection<Receipt_Notifications> _list_receipt= new ObservableCollection<Receipt_Notifications>();
            ObservableCollection<Paid_Notifications> _list_paid= new ObservableCollection<Paid_Notifications>();
            ObservableCollection<Invoice_Event> _list_invoice = new ObservableCollection<Invoice_Event>();
            ObservableCollection<Cancelletion_Notifications> _list_cancelletion = new ObservableCollection<Cancelletion_Notifications>();
            
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

                var response = client.PostAsync(RestURL, content).Result; // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(result);
                    //NotificationListMdl jsonResponse = JsonSerializer.Deserialize<NotificationListMdl>(jObj);
                    //IList<NotificationListMdl.TestCases> tTestCases = JsonConvert.DeserializeObject<List<NotificationListMdl.TestCases>>(result);
                    //List<NotificationListMdl> tTestCases = JsonConvert.DeserializeObject<List<NotificationListMdl>>(result);
                    _response_model.Error = jObj["error"].ToString();
                    _response_model.TagType = jObj["tagtype"].ToString();

                    foreach (var data in jObj["list"])
                    {
                        _date = new NotificationDate();
                        _date.Date = (data["date"].ToString());
                        _date.NotCount = (data["notcount"].ToString());
                        _list_dates.Add(_date);
                        foreach (var data1 in data["tags"])
                        {
                            _tags = new Tags();
                            _tags.Tag = (data1["tag"].ToString());
                            _tags.TagNotCount = (data1["notcount"].ToString());
                            _tags.TotalAmt = (data1["total_amount"].ToString());
                            //_tags.Date = (data["date"].ToString());
                            _listtag.Add(_tags);
                            if (_tags.Tag == "invoice_cancelletion")
                            {
                                foreach (var data2 in data1["notifications"])
                                {
                                    _cancel_notifications = new Cancelletion_Notifications();
                                    
                                    _cancel_notifications.CancelInvoice_code = (data2["invoice_code"].ToString());
                                    _cancel_notifications.CancelInvoice_date = (data2["invoice_date"].ToString());
                                    _cancel_notifications.CancelCustomer_id = (data2["customer_name"].ToString());
                                    _cancel_notifications.CancelCustomer_name = (data2["customer_id"].ToString());
                                    _cancel_notifications.CancelCancelled_by = (data2["cancelled_by"].ToString());
                                    _cancel_notifications.CancelCancelled_by_id = (data2["cancelled_by_id"].ToString());
                                    _cancel_notifications.CancelInformation_type = (data2["information_type"].ToString());
                                    _cancel_notifications.CancelTagtype = (data2["tagtype"].ToString());
                                   // _cancel_notifications.Date = (data["date"].ToString());
                                    _list_cancelletion.Add(_cancel_notifications);
                                }
                            }
                            else if (_tags.Tag == "receipt")
                            {
                                foreach (var data2 in data1["notifications"])
                                {
                                    
                                    _Receipt_notifications = new Receipt_Notifications();
                                    _Receipt_notifications.Amount_received = (data2["amount_received"].ToString());
                                    _Receipt_notifications.Company_name = (data2["company_name"].ToString());
                                    _Receipt_notifications.Current_outstanding = (data2["current_outstanding"].ToString());
                                    _Receipt_notifications.Information_type = (data2["information_type"].ToString());
                                    _Receipt_notifications.Party_id = (data2["party_id"].ToString());
                                    _Receipt_notifications.Party_name = (data2["party_name"].ToString());
                                    _Receipt_notifications.Party_outstanding = (data2["party_outstanding"].ToString());
                                    _Receipt_notifications.Site_id = (data2["site_id"].ToString());
                                    _Receipt_notifications.Site_name = (data2["site_name"].ToString());
                                    _Receipt_notifications.Tagtype = (data2["tagtype"].ToString());
                                   // _Receipt_notifications.Date = (data["date"].ToString());
                                    _list_receipt.Add(_Receipt_notifications);
                                }
                            }
                            else if (_tags.Tag == "paid")
                            {
                                foreach (var data2 in data1["notifications"])
                                {
                                   
                                    _Paid_notifications = new Paid_Notifications();
                                    _Paid_notifications.Amount_received = (data2["amount_received"].ToString());
                                    _Paid_notifications.Company_name = (data2["company_name"].ToString());
                                    _Paid_notifications.Current_outstanding = (data2["current_outstanding"].ToString());
                                    _Paid_notifications.Information_type = (data2["information_type"].ToString());
                                    _Paid_notifications.Party_id = (data2["party_id"].ToString());
                                    _Paid_notifications.Party_name = (data2["party_name"].ToString());
                                    _Paid_notifications.Party_outstanding = (data2["party_outstanding"].ToString());
                                    _Paid_notifications.Site_id = (data2["site_id"].ToString());
                                    _Paid_notifications.Site_name = (data2["site_name"].ToString());
                                    _Paid_notifications.Tagtype = (data2["tagtype"].ToString());
                                   // _Paid_notifications.Date = (data["date"].ToString());
                                    _list_paid.Add(_Paid_notifications);
                                }
                            }
                            else
                            {
                                foreach (var data2 in data1["notifications"])
                                {
                                    _invoice_notifications = new Invoice_Event();
                                    _invoice_notifications.Company_name = (data2["company_name"].ToString());
                                    _invoice_notifications.Information_type = (data2["information_type"].ToString());
                                    _invoice_notifications.Party_id = (data2["party_id"].ToString());
                                    _invoice_notifications.Party_name = (data2["party_name"].ToString());
                                    _invoice_notifications.Site_id = (data2["site_id"].ToString());
                                    _invoice_notifications.Site_name = (data2["site_name"].ToString());
                                    _invoice_notifications.Tagtype = (data2["tagtype"].ToString());
                                    _invoice_notifications.Tagtype = (data2["invoice_code"].ToString());
                                    _invoice_notifications.Tagtype = (data2["invoice_date"].ToString());
                                    _invoice_notifications.Tagtype = (data2["converted_to"].ToString());
                                    _invoice_notifications.Tagtype = (data2["event_date"].ToString());
                                   // _invoice_notifications.Date = (data["date"].ToString());
                                    _list_invoice.Add(_invoice_notifications);
                                }
                            }
                        }
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
            return null;//list_response_model;
        }
        #endregion

        #region Recievable TodayCollection Tabel
        public List<ReceivableMdl> ReceivableTable()
        {
            ResponseModel response_model = new ResponseModel();
            List<ReceivableMdl> list_recmdl = new List<ReceivableMdl>();
            ReceivableMdl recmdl=null;
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

                var response = client.PostAsync(RestURL, content).Result; // Blocking call!
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
                        recmdl = new ReceivableMdl();
                        recmdl.Perticular= (data["perticular"].ToString());
                        recmdl.Total_Due = (data["total_due"].ToString());
                        recmdl.Receive = (data["receive"].ToString());
                        recmdl.Balance= (data["balance"].ToString());
                        recmdl.txtWidth= calcScreenWidth / 4 - 20;
                        list_recmdl.Add(recmdl);


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
            return list_recmdl;
        }
        #endregion
    }
}
