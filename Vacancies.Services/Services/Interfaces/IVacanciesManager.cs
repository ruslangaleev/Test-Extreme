using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vacancies.Data.Models;

namespace Vacancies.Services.Services
{
    public interface IVacanciesManager
    {
        Task<bool> UpdateVacancies();

        Task<IEnumerable<Rubric>> GetRubrics();

        Task<IEnumerable<Vacancy>> GetVacancies();

        Task<IEnumerable<Vacancy>> GetVacancies(string rubricId);
    }
}
