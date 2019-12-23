using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementClient.Helpers
{
    public class WebApiConsumer
    {
        static string webApiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
        public async static Task<string> ConsumePostAsJsonAsync(string navigateurl, object postObject)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(webApiUrl);
                    var responseTask = await client.PostAsJsonAsync(navigateurl, postObject);                                       
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var response = await responseTask.Content.ReadAsStringAsync();
                        return response;
                    }
                    return "Execution is failed";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<string> ConsumeGetAsync(string navigateuri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(webApiUrl);
                    var responseTask = await client.GetAsync(navigateuri);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var response = await responseTask.Content.ReadAsStringAsync();                      
                        return response;
                    }
                    return "Execution is failed";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
