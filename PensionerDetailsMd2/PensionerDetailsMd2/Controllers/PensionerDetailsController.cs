using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PensionerDetailsMd2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailsController : ControllerBase
    {

        //(creating list)
        List<PensionerDetails> pensioners = new List<PensionerDetails>();

        public PensionerDetailsController()
        {
            pensioners.Add(
                new PensionerDetails()
                {
                    Name = "bunk seenu",
                    dateOfBirth = new DateTime(1990, 01, 02),
                    PAN = "123456789",
                    AadharNumber = 123456789,
                    salaryEarned = 45000,
                    allowances = 500,
                    pensionType = "family",
                    bankType = "publicbank",
                    bankName = "SBI",
                    accountNumber = "SBI00001BS"
                }
                );
        }
        //(end of creating list)

        // GET: api/<PensionerDetailsController>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(pensioners);
        }

        // GET api/<PensionerDetailsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            PensionerDetails p = new PensionerDetails();
            p = pensioners.Find(x => x.AadharNumber ==id);
            if (p == null)
                return BadRequest("No Such Id Exists");
            return Ok(p);
        }

        // POST api/<PensionerDetailsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PensionerDetailsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PensionerDetailsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
