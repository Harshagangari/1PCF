using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessPensionMd1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        // GET: api/<ProcessPensionController>123456789
        //[HttpGet]
        //public ActionResult Get()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:12856/api/pensiondisbursements/");
        //        var responseTask = client.GetAsync($"{12345678}"/*{receivedDetails.aadhaar}*/);
        //        responseTask.Wait();
        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return Ok("Details Matched......Pension to disburse...Rs...");
        //            //var result1 = result.Content.ReadAsStringAsync().Result;
        //            //pd = JsonConvert.DeserializeObject<PensionerDetails>(result1);
        //        }

        //    }
        //    return BadRequest("Invalid Details.....Try Again...!!");http://localhost:12856/api/pensiondisbursements?id=123456789&salaryCalculated=12000
        //}

        [HttpGet]
        [Route("mod1")]
        public ActionResult Get(string dob,int aadharNumber,string Name,string PAN,string pensionType)
        {
            

            //

            //try
            PensionerDetails pd = new PensionerDetails();
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:20268/");
                //var responseTask = client.GetAsync(string.Format("api/pensionerdetails/{0}", aadharNumber));
                //var responseTask = client.GetAsync(string.Format("pensionerdetails/{0}", aadharNumber));
                client.BaseAddress = new Uri("https://localhost:44380/pensionerdetails/"+aadharNumber);
                //string url = string.Format("pensionerdetails/{0}", 123456789);
                var responseTask = client.GetAsync("");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var result1 = result.Content.ReadAsStringAsync().Result;
                    pd = JsonConvert.DeserializeObject<PensionerDetails>(result1);
                }


            }
            double calcPension;
            if (pensionType.Equals("self"))
                calcPension = pd.salaryEarned * 0.8 + pd.allowances;
            else
                calcPension = pd.salaryEarned * 0.5 + pd.allowances;
            if (pd.bankType.Equals("publicbank"))
                calcPension += 500;
            else
                calcPension += 550;



            using (var client = new HttpClient())
            {

                int aid = 12456789;
                double salaryCalculated=12000;

                //var responseTask = client.GetAsync(string.Format("api/pensiondisbursements?&id={0}&salaryCalculated={1}", aadharNumber, calcPension));
                //withoutocelot
                //  client.BaseAddress = new Uri("http://localhost:12856/");
                //var responseTask = client.GetAsync(string.Format("api/pensiondisbursements?&id={0}&salaryCalculated={1}",aadharNumber,calcPension));
                //ocelot
                string url = string.Format("https://localhost:44380/pensiondisbursements/{0}/{1}", aadharNumber, calcPension);
                client.BaseAddress = new Uri(url);
               
                var responseTask = client.GetAsync("");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string op = string.Format("Details Matched......Pension to disburse...Rs...{0}", calcPension);
                    return Ok(op);
                   
                }

            }
            return BadRequest("Invalid Details.....Try Again...!!");
        }

       

        // POST api/<ProcessPensionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        
    }
}
