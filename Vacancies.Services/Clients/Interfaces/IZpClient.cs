using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vacancies.Data.Models;

namespace Vacancies.Services.Clients.Interfaces
{
    public interface IZpClient
    {
        Task<IEnumerable<Rubric>> GetRubrics();

        Task<IEnumerable<Vacancy>> GetVacancies(int limit = 25, int offset = 0);
    }
}
