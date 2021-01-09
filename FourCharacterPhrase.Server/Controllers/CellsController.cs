using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourCharacterPhrase.Server.Dao;
using FourCharacterPhrase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourCharacterPhrase.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CellsController : ControllerBase
    {
        [HttpGet]
        public List<CellEntity> Get(string name)
        {
            var daoD_Cell = new DaoD_Cell();

            return daoD_Cell.GetCellList(name).OrderBy(m => m.No).ToList();
        }

        [HttpPost]
        public void Post([FromBody] List<CellEntity> value)
        {
            var daoD_Cell = new DaoD_Cell();
            daoD_Cell.Save(value);
        }

    }
}