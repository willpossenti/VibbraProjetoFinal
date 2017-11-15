using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vibbra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Vibbra.Controllers.api
{
    [Produces("application/json")]
    [Route("api/v[controller]/Time")]
    public class TimeController : Controller
    {
        private readonly Contexto _dbContext;


        public TimeController(Contexto db)
        {
            this._dbContext = db;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult get(int id)
        {

            var obj = _dbContext.Tempo.Where(x => x.Projeto.Any(y => y.Project_id == id)).ToList();

            if (obj == null)
                return Content(JsonConvert.SerializeObject(new { _data = "no records" }, Formatting.Indented), "application/json");

            return Content(JsonConvert.SerializeObject(new { _data = obj }, Formatting.Indented), "application/json");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Tempo tempo)
        {
            var _message = string.Empty;

            try
            {
                _dbContext.Tempo.Add(tempo);
                _dbContext.SaveChanges();

                _message = "save with success";
            }
            catch (Exception ex)
            {

                _message = ex.Message;
            }

            var response = new
            {
                message = _message
            };

            return Json(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Tempo tempo)
        {
            var _message = string.Empty;

            try
            {
                var timeToUpdate = _dbContext.Tempo.Where(x => x.Projeto.Any(y => y.Project_id == tempo.Projeto.FirstOrDefault().Project_id)).FirstOrDefault();
                timeToUpdate.started_at = tempo.started_at;
                timeToUpdate.ended_at = tempo.ended_at;
                _dbContext.Tempo.Update(timeToUpdate);
                _dbContext.SaveChanges();

                _message = "save with success";

            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }

            var response = new
            {
                message = _message
            };

            return Json(response);
        }
    }
}