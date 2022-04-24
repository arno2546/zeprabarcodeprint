using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace posdesktop.Services
{
    class CommonHttpService<T>
    {
        public T Get(string Url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConnectionString.GetPosTestApiIP);
                var responseTask = client.GetAsync(Url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<T>();
                    readTask.Wait();
                    var T = readTask.Result;
                    return T;
                }
                else
                {
                    return default;
                }
            }
        }
        public IEnumerable<T> GetAll(string Url, string uriString = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uriString == null ? new Uri(ConnectionString.GetPosTestApiIP) : new Uri(uriString);
                var responseTask = client.GetAsync(Url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<T[]>();
                    readTask.Wait();
                    var Ts = readTask.Result;
                    return Ts;
                }
                else
                {
                    return new List<T>();
                }
            }
        }

        public static async Task<HttpResponseMessage> GetAllAsync(string Url)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConnectionString.GetPosTestApiIP);
                var responseTask = await client.GetAsync(Url);
                return responseTask;
            }         
        }

        public int put(string url, T entity)
        {
            using (var client = new HttpClient()){                
                client.BaseAddress = new Uri(ConnectionString.GetPosTestApiIP);
                var response = client.PutAsJsonAsync(url, entity).Result;
                if (response.IsSuccessStatusCode)
                {
                    return 1;
                }
                else
                    return -1;
            }
        }

        public List<T> PostBatch(string url, List<T> entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConnectionString.GetPosTestApiIP);
                var responseTask = client.PostAsJsonAsync(url, entity);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<T[]>();
                    readTask.Wait();
                    var Ts = readTask.Result;
                    return Ts.ToList();
                }
                else
                {
                    return new List<T>();
                }
            }
        }

        public HttpResponseMessage Post(string url, T entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConnectionString.GetPosTestApiIP);
                var responseTask = client.PostAsJsonAsync(url, entity);
                responseTask.Wait();
                var result = responseTask.Result;
                return result;
                //if (result.IsSuccessStatusCode)
                //{
                //    //var readTask = result.Content.ReadAsAsync<T[]>();
                //    //readTask.Wait();
                //    //var Ts = readTask.Result;
                //    //return Ts.ToList();
                //}
                //else
                //{
                //    //return new List<T>();
                //}
            }
        }
    }
}
