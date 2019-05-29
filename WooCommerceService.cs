using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CleverPortal.Services
{
    public class WooCommerceService : IWooCommerceService
    {
        public async Task<string> GetWooCommerceOrders(string websiteURL, string consumerKey, string consumerSecret, string afterDate, string beforeDate)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", consumerKey, consumerSecret)
                        )));

                bool morepages = true;
                int page = 1;

                string masterJson = "";

                while (morepages)
                {
                    string queryURL = websiteURL + "/wp-json/wc/v2/orders?";
                    if (!String.IsNullOrEmpty(afterDate)) queryURL += "after=" + afterDate + "&";
                    if (!String.IsNullOrEmpty(beforeDate)) queryURL += "before=" + beforeDate + "&";
                    queryURL += "per_page=100&page=" + page.ToString();

                    HttpResponseMessage response = await client.GetAsync(queryURL);

                    string rawContent = await response.Content.ReadAsStringAsync();

                    if(rawContent.StartsWith('\n')) rawContent = rawContent.Remove(0, 1);

                    if (rawContent != "[]" && 
                        ((rawContent.StartsWith("{") && rawContent.EndsWith("}")) ||
                            (rawContent.StartsWith("[") && rawContent.EndsWith("]"))))
                    {
                        if (page == 1) masterJson = rawContent;
                        else
                        {
                            rawContent = rawContent.Remove(0, 1);
                            masterJson = masterJson.Remove(masterJson.Length -1, 1) + "," + rawContent;
                        }
                        page++;
                    }
                    
                    else morepages = false;
                }

                return masterJson;
            }
        }
    }
}
