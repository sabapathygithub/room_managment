using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementClient.Helpers
{
    public class WebApiConsumer
    {
        public static string ConsumePostAsJsonAsync(string navigateurl, object postObject)
        {
            string url = "https://localhost:44392/api/";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.PostAsJsonAsync(navigateurl, postObject);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var response = readTask.Result;
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

        public static string ConsumeGetAsync(string navigateuri)
        {
            string url = "https://localhost:44392/api/";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.GetAsync(navigateuri);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var response = readTask.Result;
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
