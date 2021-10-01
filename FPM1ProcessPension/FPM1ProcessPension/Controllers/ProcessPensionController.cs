using FPM1ProcessPension.Entiries;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FPM1ProcessPension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        PensionerInput ip = new PensionerInput() {Name = "bunk seenu", AaadharNumber = 123456789, DateofBirth = new DateTime(1999, 01, 01),PAN = "AB",PensionType = "self"};
       
       
        [HttpGet]
        public ActionResult Get()
        {
            PensionerDetails pdb = new PensionerDetails();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/PensionerDetails/");
                var responseTask = client.GetAsync($"{ip.AaadharNumber}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var result1 = result.Content.ReadAsStringAsync().Result;
                    pdb = JsonConvert.DeserializeObject<PensionerDetails>(result1);
                }

            }
            double calcPension;
            if (ip.PensionType.Equals("self"))
                calcPension = pdb.salaryEarned * 0.8 + pdb.allowances;
            else
                calcPension = pdb.salaryEarned * 0.5 + pdb.allowances;
            if (pdb.bankType.Equals("public"))
                calcPension += 500;
            else
                calcPension += 550;
            //should be implemented in post method
            int statCode = 0;
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("http://localhost:55345/api/PensioneDisbursements/");
                var responseTask = client.GetAsync($"{ip.AaadharNumber}");//($"{calcPension}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    statCode = 10;
                }
                else
                    statCode = 20;

            }
            if (statCode == 10) 
                return Ok(calcPension); 
            return BadRequest("IncorrectDetails");

            
        }

        // GET: api/<ProcessPensionController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<ProcessPensionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProcessPensionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProcessPensionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProcessPensionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
