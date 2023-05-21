using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace kapalicarsi.Controllers
{
    [ApiController]
    [Route("/kapalicarsi")]
    public class KapalicarsiController : Controller
    {
        [HttpGet(Name = "All")]
        public string Get()
        {
            List<KapalicarsiModel> data = Scrapper.Get();
            var result = JsonConvert.SerializeObject(data.ToArray());
            return result;
        }
    }
}
