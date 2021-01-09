using System.Collections.Generic;
using System.Linq;
using FourCharacterPhrase.Server.Dao;
using FourCharacterPhrase.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FourCharacterPhrase.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnswerNumberController : ControllerBase
    {
        // GET: AnswerNumber
        [HttpGet]
        public IEnumerable<AnswerNumberEntity> Get()
        {
            var daoD_AnswerNumber = new DaoD_AnswerNumber();
            return daoD_AnswerNumber.GetAnswerNumberList().OrderByDescending(m => m.Count).ThenBy(m => m.ElapsedTime).ToList();
        }

        //POST: AnswerNumber
        [HttpPost]
        public void Post([FromBody] AnswerNumberEntity value)
        {
            var daoD_AnswerNumber = new DaoD_AnswerNumber();
            daoD_AnswerNumber.Save(value);
        }

        [HttpDelete]
        public void Delete()
        {
            var daoD_AnswerNumber = new DaoD_AnswerNumber();
            daoD_AnswerNumber.Delete();
        }
    }
}
