using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web;
using Library.Interface;

namespace Library.Services
{
    public class FunctionalityService : IFunctionalityValidator
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public HttpContext _httpcontext;
        public readonly SemaphoreSlim _semaphoreslim = new SemaphoreSlim(1,1);
        //private static readonly HttpRequestMessage _httpRequestMessage;

        public FunctionalityService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        //public void Test()
        //{
        //    //todo
        //    Task task = ProcessInfo();
        //}
        public async Task<string> ProcessInfo()
        {
            // Asyncronously wait to enter the SemaphoreSlim
            await _semaphoreslim.WaitAsync();

            try
            {
                var cliente = _httpClientFactory.CreateClient();
                using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/mock/get");
                var response = await cliente.SendAsync(httpRequestMessage);
                var contenido = await response.Content.ReadAsStringAsync();
                //aplicar aca httpcontext??
                return contenido;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
            finally
            {
                _semaphoreslim.Release();
            }            
        }
    }
}
