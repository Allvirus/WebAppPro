using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCTest.Data.Http
{
    public class HttpFactory : IHttpFactory
    {
        private const string BaseUrl = "https://china.blsspainvisa.com/chinese/";
        private const string RefererUrl = "https://china.blsspainvisa.com/chinese/book_appointment.php";
        private const string RefererUrl1 = "https://china.blsspainvisa.com/chinese/message.php";

        private readonly IHttpClientFactory _httpClientFactory;

        public HttpFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<string> HttpGet(string uri, string cookie)
        {

            //最新的httpClientFactory方法
            var client = _httpClientFactory.CreateClient();
            var url = BaseUrl + uri;

            if (!string.IsNullOrEmpty(cookie))
                client.DefaultRequestHeaders.Add("Cookie", cookie);
            client.DefaultRequestHeaders.Add("Referer", RefererUrl);

            var response = await client.GetAsync(url);
            var html = await client.GetStringAsync(url);


            var responseCookies = response.Headers.GetValues("Set-Cookie");
            var responseCookie = string.Join("", responseCookies);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseCookie;


            #region HttpClient请求方式
            //老版httpClientGet请求
            //var client = new HttpClient();
            //var responseString = await client.GetStringAsync("https://china.blsspainvisa.com/chinese/index.php");
            //return Ok(responseString);


            //老版httpClientPost请求

            //var Cookie = "PHPSESSID=1oqt6rrkntqh7bvq0n2cbvh8u1; AWSALB=nwCEnzWyZl0upPHl6oymHpKejIII3Y3yyxSB91Melzo3/4Mz4OyOLLuiubjqBSoipWl4GgXklosccxV3VixGZmJF+e2c2S9ZjdrIZipDV0mDqo4OCqE3k02xhv56; _ga=GA1.2.17736923.1544508397; _gid=GA1.2.28098300.1544508397; AWSALB=8IS0C6+rP/x2WQbskDRpcgRH2EBte4EKHR+g0dRJHHoEvagoKu7V+ytWnLlDHwXWkKMJ2F1pdUahL7SG4U4bBNeKCesLo9snTyLr8BFPsSCWqClAT40vobHCjVsg";
            //var client = new HttpClient();
            //var values = new List<KeyValuePair<string, string>>
            //{
            //    new KeyValuePair<string, string>("app_type", "Individual"),
            //    new KeyValuePair<string, string>("centre ", "3#14"),
            //    new KeyValuePair<string, string>("category", "Normal"),
            //    new KeyValuePair<string, string>("phone_code", "86"),
            //    new KeyValuePair<string, string>("phone", "15657525415"),
            //    new KeyValuePair<string, string>("email", "asd@qq.com"),
            //    new KeyValuePair<string, string>("countryID", ""),
            //    new KeyValuePair<string, string>("member", "2"),
            //    new KeyValuePair<string, string>("save", "Continue"),
            //};

            //var content = new FormUrlEncodedContent(values);

            //client.DefaultRequestHeaders.Add("Cookie", Cookie);

            //var response = await client.PostAsync("https://china.blsspainvisa.com/chinese/book_appointment.php", content);

            //var cookie = response.Headers.GetValues("Set-Cookie");

            //var responseString = await response.Content.ReadAsStringAsync();

            //return Ok(cookie);
            #endregion

        }

        public async Task<string> HttpPost(string uri, string cookie, List<KeyValuePair<string, string>> values)
        {
            //最新的httpClientFactory方法

            #region  //放在上级调用中

            //var Cookie = "PHPSESSID=1oqt6rrkntqh7bvq0n2cbvh8u1; AWSALB=nwCEnzWyZl0upPHl6oymHpKejIII3Y3yyxSB91Melzo3/4Mz4OyOLLuiubjqBSoipWl4GgXklosccxV3VixGZmJF+e2c2S9ZjdrIZipDV0mDqo4OCqE3k02xhv56; _ga=GA1.2.17736923.1544508397; _gid=GA1.2.28098300.1544508397; AWSALB=8IS0C6+rP/x2WQbskDRpcgRH2EBte4EKHR+g0dRJHHoEvagoKu7V+ytWnLlDHwXWkKMJ2F1pdUahL7SG4U4bBNeKCesLo9snTyLr8BFPsSCWqClAT40vobHCjVsg";
            //var values = new List<KeyValuePair<string, string>>
            //{
            //    new KeyValuePair<string, string>("app_type", "Individual"),
            //    new KeyValuePair<string, string>("centre ", "3#14"),
            //    new KeyValuePair<string, string>("category", "Normal"),
            //    new KeyValuePair<string, string>("phone_code", "86"),
            //    new KeyValuePair<string, string>("phone", "15657525415"),
            //    new KeyValuePair<string, string>("email", "asd@qq.com"),
            //    new KeyValuePair<string, string>("countryID", ""),
            //    new KeyValuePair<string, string>("member", "2"),
            //    new KeyValuePair<string, string>("save", "Continue"),
            //};
            #endregion

            var content = new FormUrlEncodedContent(values);

            var client = _httpClientFactory.CreateClient();
            var url = BaseUrl + uri;

            client.DefaultRequestHeaders.Add("Cookie", cookie);
            client.DefaultRequestHeaders.Add("Referer", RefererUrl);
            
            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseCookies = response.Headers.GetValues("Set-Cookie");
            var responseCookie = string.Join("", responseCookies);
            return responseCookie;


        }
    }

}
