using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vacancies.Data.Models;
using Vacancies.Data.Repositories;

namespace Vacancies.Services.Services.Logic
{
    public class VacanciesManager : IVacanciesManager
    {
        public readonly IVacanciesRepository _vacanciesRepository;

        public int TimeOutDays;

        public VacanciesManager(IVacanciesRepository vacanciesRepository)
        {
            _vacanciesRepository = vacanciesRepository ?? throw new ArgumentNullException(nameof(vacanciesRepository));

            // Указать TimeOutDays
            TimeOutDays = 2;
        }

        public async Task<bool> UpdateVacancies()
        {
            var versionInfo = await _vacanciesRepository.GetLastVersion();
            if (versionInfo == null)
            {
                // Обновить...
            }
            var difference = (DateTime.Now - versionInfo.UpdateAt).Days;
            if (difference < TimeOutDays)
            {
                return false;
            }

            return true;
        }
    }
}
