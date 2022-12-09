using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Ders1Api.Models
{
    public class Api2
    {
        HttpClient client;
        public Api2()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            client = new HttpClient(handler);
        }

        public void apiSifirla()
        {
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            client = new HttpClient(handler);
        }
        public string requestPost(string url, Dictionary<string, string> dict)
        {
            apiSifirla();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var Content = new FormUrlEncodedContent(dict);
            HttpResponseMessage responseOtel = client.PostAsync(url, Content).Result;
            string result = responseOtel.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string requestPostJson(string url, object model)
        {
            apiSifirla();

            string json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseOtel = client.PostAsync(url, stringContent).Result;
            string result = responseOtel.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string requestGet(string url)
        {
            apiSifirla();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            HttpResponseMessage responseOtel = client.GetAsync(url).Result;
            string result = responseOtel.Content.ReadAsStringAsync().Result;
            result = System.Net.WebUtility.HtmlDecode(result);

            return result;
        }

        public string requestGetBase64(string apiKey, int carId)
        {
            string url = "resimurl";

            HttpResponseMessage response = client.GetAsync(url).Result;
            var result = response.Content.ReadAsByteArrayAsync().Result;
            return Convert.ToBase64String(result);
        }


        public string kes(string txt, string ilk, string son)
        {
            try
            {
                string parcali1 = (txt.Split(new string[] { ilk }, StringSplitOptions.None))[1];
                string parcali2 = (parcali1.Split(new string[] { son }, StringSplitOptions.None))[0];
                return parcali2.Trim();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> kesList(string txt, string ilk, string son)
        {
            try
            {
                List<string> list = new List<string>();
                string[] parcali1 = (txt.Split(new string[] { ilk }, StringSplitOptions.None));
                for (int i = 1; i < parcali1.Length; i++)
                {
                    string parcali2 = (parcali1[i].Split(new string[] { son }, StringSplitOptions.None))[0];
                    list.Add(parcali2.Trim());

                }
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string requestPostJsonAndroidVeIos(string url, object model)
        {
            apiSifirla();

            string json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            // client.DefaultRequestHeaders.Add("Authorization", "key=AAAAGFjobXo:APA91bEK8LFmRgtcuU-LWdN6HDqIBJQJl6HhferMk--3NEevVIOtFl_y6jFyzhYGH95umA27N5-q1I9aFVKVK_NmRj98bkvYaBGxh-AT74KbEXrMzTjN5bxv5L9vUiSCi4P6x879cx5C");


            client.DefaultRequestHeaders.TryAddWithoutValidation(
                    "Authorization", "key= " + "AAAAGFjobXo:APA91bEK8LFmRgtcuU-LWdN6HDqIBJQJl6HhferMk--3NEevVIOtFl_y6jFyzhYGH95umA27N5-q1I9aFVKVK_NmRj98bkvYaBGxh-AT74KbEXrMzTjN5bxv5L9vUiSCi4P6x879cx5C");



            HttpResponseMessage responseOtel = client.PostAsync(url, stringContent).Result;
            string result = responseOtel.Content.ReadAsStringAsync().Result;
            return result;
        }



    }
}