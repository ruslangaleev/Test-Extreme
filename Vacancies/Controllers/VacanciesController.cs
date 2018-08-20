using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Vacancies.Data.Models;
using Vacancies.Services.Services;

namespace Vacancies.Controllers
{
    [Route("api/v1/vacancies")]
    public class VacanciesController : Controller
    {
        private readonly IVacanciesManager _vacanciesManager;

        public VacanciesController(IVacanciesManager vacanciesManager)
        {
            _vacanciesManager = vacanciesManager;
        }

        [HttpGet]
        public async Task<IEnumerable<Vacancy>> GetVacancies()
        {
            return await _vacanciesManager.GetVacancies();
        }

        [HttpGet]
        public async Task<IEnumerable<Vacancy>> GetVacancies(string rubricId)
        {
            return await _vacanciesManager.GetVacancies(rubricId);
        }

        [HttpGet]
        public async Task<IEnumerable<Rubric>> GetRubrics()
        {
            return await _vacanciesManager.GetRubrics();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVacancies()
        {
            if (await _vacanciesManager.UpdateVacancies())
            {
                return Ok("Выполняется обновление вакансий...");
            }

            return Ok("Вакансии обновлены");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
