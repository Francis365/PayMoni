using PayMoni.Helpers;
using PayMoni.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PayMoni.Services
{
    struct DashboardState
    {
        public static string STATE;
        public const string UserBlacklisted = "User Blacklisted";
        public const string PendingLoan = "Pending Loan";
        public const string Credibility = "Credibility";
        public const string TheUserhasnoBankAccount = "The User has no Bank Account";
        public const string ActiveLoan = "Active Loan";
        

    }
    class WebServices : IDisposable
    {
        public static string phone;


        //First time register urls
        const string send_otp_url = "https://us-central1-eazeepay.cloudfunctions.net/generate_otp";
        const string otp_verification_url = "https://management.litacredit.com.ng/api/GetStarted/";
        const string register_user_url = "https://us-central1-eazeepay.cloudfunctions.net/register_user";

        public WebServices()
        {
        }

        public static async Task<bool> SendOTP(string phoneNumber)
        {
            bool success;

            if (!Settings.IsNetworkAvailable)
            {
                Settings.ToastAlert("Connect to internet");

                return false;
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                using (var httpClient = new HttpClient())
                {
                    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    phone = phoneNumber;
                    var sendOtpData = new
                    {
                        phone = phoneNumber
                    };

                    //serialize data
                    var serializedData = JsonConvert.SerializeObject(sendOtpData);
                    var content = new StringContent(serializedData);

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    try
                    {
                        var jsonResponse = await httpClient.PostAsync(send_otp_url, content);

                        if (jsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;

                            if (jsonResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                var msg = await jsonResponse.Content.ReadAsStringAsync();

                                msg = JsonConvert.DeserializeObject<ResponseMessageModel>(msg).Message;

                                Settings.ToastAlert(msg);
                            }
                            else if (jsonResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                Settings.Logout();

                                Settings.ToastAlert("Session Expired");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        success = false;
                        Settings.ToastAlert("Network error");
                    }
                    


                }
            }
            else
            {
                //post data empty
                success = false;
            }

            return await Task.FromResult(success);
        }

        public static async Task<bool> VerifyOTPForRegistration(string otp, string phone)
        {
            var success = false;

            if (!Settings.IsNetworkAvailable)
            {
                Settings.ToastAlert("Connect to internet");

                return false;
            }

            if (!string.IsNullOrEmpty(otp))
            {
                using (var httpClient = new HttpClient())
                {
                    var otpVerify = new
                    {
                        token = otp,
                        PhoneNumber = phone
                    };

                    //serialize data
                    var serializedData = JsonConvert.SerializeObject(otpVerify);
                    var content = new StringContent(serializedData);

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    try
                    {
                        //post to web
                        var jsonResponse = await httpClient.PostAsync(otp_verification_url, content);

                        if (jsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            //first_token = await jsonResponse.Content.ReadAsStringAsync();

                            //first_token = JsonConvert.DeserializeObject<ResponseMessageModel>(first_token).Message;

                            success = true;
                        }
                        else
                        {
                            success = false;
                            if (jsonResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                var msg = await jsonResponse.Content.ReadAsStringAsync();

                                msg = JsonConvert.DeserializeObject<ResponseMessageModel>(msg).Message;

                                Settings.ToastAlert(msg);
                            }
                            else if (jsonResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                Settings.Logout();

                                Settings.ToastAlert("Session Expired");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        success = false;
                        Settings.ToastAlert("Network error");
                    }



                }
            }
            else
            {
                //post data empty
                await Application.Current.MainPage.DisplayAlert("Warning", "post data empty", "Ok");

                success = false;
            }

            return await Task.FromResult(success);
        }

        public static async Task<ResponseMessageModel> VerifyOTPForLogin(string otp)
        {
            ResponseMessageModel serverResponse;

            if (!Settings.IsNetworkAvailable)
            {
                Settings.ToastAlert("Connect to internet");

                return null;
            }

            if (!string.IsNullOrEmpty(otp))
            {
                using (var httpClient = new HttpClient())
                {

                    //post to web
                    //sms_otp = otp;

                    try
                    {
                        var jsonResponse = await httpClient.GetAsync("");

                        if (jsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var accessToken = await jsonResponse.Content.ReadAsStringAsync();

                            serverResponse = JsonConvert.DeserializeObject<ResponseMessageModel>(accessToken);

                            //first_token = serverResponse.Message;
                        }
                        else
                        {
                            serverResponse = null;

                            if (jsonResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                var msg = await jsonResponse.Content.ReadAsStringAsync();

                                msg = JsonConvert.DeserializeObject<ResponseMessageModel>(msg).Message;

                                Settings.ToastAlert(msg);
                            }
                            else if (jsonResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                Settings.Logout();

                                Settings.ToastAlert("Session Expired");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Settings.ToastAlert("Network error");

                        return null;
                    }
                }
            }
            else
            {
                //post data empty
                await Application.Current.MainPage.DisplayAlert("Warning", "post data empty", "Ok");

                serverResponse = null;
            }

            return serverResponse;
        }

        public static async Task<bool> LoginWithNumberAsync(string phoneNumber)
        {
            bool success;

            if (!Settings.IsNetworkAvailable)
            {
                Settings.ToastAlert("Connect to internet");

                return false;
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                using (var httpClient = new HttpClient())
                {
                    phone = phoneNumber;

                    try
                    {
                        //complete login url
                        var jsonResponse = await httpClient.GetAsync("");

                        if (jsonResponse.IsSuccessStatusCode)
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                            if (jsonResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                var msg = await jsonResponse.Content.ReadAsStringAsync();

                                msg = JsonConvert.DeserializeObject<ResponseMessageModel>(msg).Message;

                                Settings.ToastAlert(msg);
                            }
                            else if (jsonResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                Settings.Logout();

                                Settings.ToastAlert("Session Expired");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        success = false;
                        Settings.ToastAlert("Network error");
                    }

                }
            }
            else
            {
                //post data empty
                success = false;
            }

            return await Task.FromResult(success);
        }

        public static async Task<string> RegisterUserAsync(string bvn, string firstname, string lastname, string email, string city, string token)
        {
            string success;

            if (!Settings.IsNetworkAvailable)
            {
                Settings.ToastAlert("Connect to internet");

                return null;
            }

            if (!string.IsNullOrEmpty(bvn))
            {
                using (var httpClient = new HttpClient())
                {

                    var otpVerify = new
                    {
                        token,
                        bvn,
                        firstname,
                        lastname,
                        email,
                        city
                    };

                    //serialize data
                    var serializedData = JsonConvert.SerializeObject(otpVerify);
                    var content = new StringContent(serializedData);

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    try
                    {

                        //post to web
                        var jsonResponse = await httpClient.PostAsync(register_user_url, content);

                        if (jsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            success = await jsonResponse.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            success = null;

                            if (jsonResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                var msg = await jsonResponse.Content.ReadAsStringAsync();

                                msg = JsonConvert.DeserializeObject<ResponseMessageModel>(msg).Message;

                                Settings.ToastAlert(msg);
                            }
                            else if (jsonResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                Settings.Logout();

                                Settings.ToastAlert("Session Expired");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        success = null;
                        Settings.ToastAlert("Network error");
                    }
                }
            }
            else
            {
                //post data empty
                success = null;
            }

            return await Task.FromResult(success);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }

    class TestResponse
    {
        public string Message { get; set; }
    }
}
