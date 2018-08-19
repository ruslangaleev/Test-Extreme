using System;
using System.Collections.Generic;
using System.Text;
using Vacancies.Data.Models;

namespace Vacancies.Services.Services.ResourceModels
{
    public class VacanciesInfo
    {
        public int Count { get; set; }

        public IEnumerable<Vacancy> vacancies { get; set; }
    }
}
