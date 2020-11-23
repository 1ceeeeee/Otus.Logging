using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Otus.Service2.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class Test : ControllerBase
  {
    public async Task<string> Index()
    {
      
      /*
       * HttpClientDiagnosticSource.Push(HttpRequestMessage)
       * 
       * HttpClientDiagnosticSource.Subscribe(Action<HttpRequestMessage> func)
       * 
       */
      
      var httpClient = new HttpClient
      {
        BaseAddress = new Uri("https://localhost:5003")
      };

      
      var forecast = await httpClient.GetStringAsync("/weatherforecast");
      
      return forecast;
    }
  }
}