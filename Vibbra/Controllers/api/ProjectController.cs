using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vibbra.Data;
using Newtonsoft.Json;

namespace Vibbra.Controllers.api
{
    [Produces("application/json")]
    [Route("api/v[controller]/Project")]
    public class ProjectController : Controller
    {
        private readonly Contexto _dbContext;

        public ProjectController(Contexto db)
        {
            this._dbContext = db;
        }

        [Authorize]
        [HttpGet]
        public IActionResult get()
        {

            var obj = _dbContext.Projeto.ToList();

            if (obj == null)
                return Content(JsonConvert.SerializeObject(new { _data = "no records" }, Formatting.Indented), "application/json");

            return Content(JsonConvert.SerializeObject(new { _data = obj }, Formatting.Indented), "application/json");
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult get(int id)
        {

            var obj = _dbContext.Projeto.Where(x => x.Project_id == id).FirstOrDefault();

            if (obj == null)
                return Content(JsonConvert.SerializeObject(new { _data = "no records" }, Formatting.Indented), "application/json");

            return Content(JsonConvert.SerializeObject(new { _data = obj }, Formatting.Indented), "application/json");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Projeto projeto)
        {
            var obj = _dbContext.Projeto.Where(x => x.Titulo.ToUpper().Contains(projeto.Titulo.ToUpper())
            && x.Descricao.ToUpper().Contains(projeto.Descricao.ToUpper())
            && x.UsuarioProjeto.Any(y => y.Usuario.Id == _dbContext.Usuario.FirstOrDefault().Id));

            if (obj == null)
                return Content(JsonConvert.SerializeObject(new { _data = "no records" }, Formatting.Indented), "application/json");

            return Content(JsonConvert.SerializeObject(new { _data = obj }, Formatting.Indented), "application/json");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Projeto projeto)
        {
            var _message = string.Empty;

            try
            {
                var projectToUpdate = await _dbContext.Projeto.FindAsync(projeto.UsuarioProjeto.FirstOrDefault().ID);
                projectToUpdate.Titulo = projeto.Titulo;
                projectToUpdate.Descricao = projeto.Descricao;
                _dbContext.Projeto.Update(projectToUpdate);
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