using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vacancies.Data.Models;
using Vacancies.Data.Repositories;
using Vacancies.Services.Clients.Interfaces;
using System.Linq;

namespace Vacancies.Services.Services.Logic
{
    public class VacanciesManager : IVacanciesManager
    {
        private readonly IVacanciesRepository _vacanciesRepository;

        private readonly IZpClient _zpClient;

        private readonly int TimeOutDays;

        private readonly int LimitVacancies;

        public VacanciesManager(IVacanciesRepository vacanciesRepository, IZpClient zpClient)
        {
            _vacanciesRepository = vacanciesRepository ?? throw new ArgumentNullException(nameof(vacanciesRepository));
            _zpClient = zpClient ?? throw new ArgumentNullException(nameof(IZpClient));
            // Указать TimeOutDays
            TimeOutDays = 1;
            LimitVacancies = 100;
        }

        public async Task<IEnumerable<Rubric>> GetRubrics()
        {
            return await _vacanciesRepository.GetRubrics();
        }

        public async Task<IEnumerable<Vacancy>> GetVacancies()
        {
            return await _vacanciesRepository.GetVacancies();
        }

        public async Task<IEnumerable<Vacancy>> GetVacancies(string rubricId)
        {
            return await _vacanciesRepository.GetVacancies(rubricId);
        }

        public async Task<bool> UpdateVacancies()
        {
            // Проверяем версию обновлений
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

            // Новая версия
            var updateAt = DateTime.Now;
            versionInfo = new VersionInfo
            {
                UpdateAt = updateAt,
                IsRubricUpdated = false
            };
            await _vacanciesRepository.AddVersionInfo(versionInfo);

            // Добавление новых рубрик
            var rubrics = await _zpClient.GetRubrics();
            rubrics.ToList().ForEach(t => t.UpdateAt = updateAt);
            await _vacanciesRepository.AddRangeRubrics(rubrics);

            versionInfo.IsRubricUpdated = true;
            await _vacanciesRepository.UpdateVersionInfo(versionInfo);

            int count = 0;
            int offset = 0;
            do
            {
                var vacanciesInfo = await _zpClient.GetVacancies(limit: LimitVacancies, offset: offset);
                if (vacanciesInfo == null)
                {
                    versionInfo.ErrorMessage = "Список вакансий пуст.";
                    await _vacanciesRepository.UpdateVersionInfo(versionInfo);
                    // SaveChanges
                    break;
                }
                if (count == 0)
                {
                    count = vacanciesInfo.Count;
                    versionInfo.CountVacancies = vacanciesInfo.Count;
                }
                vacanciesInfo.vacancies.ToList().ForEach(t => t.UpdateAt = updateAt);
                await _vacanciesRepository.AddRangeVacancies(vacanciesInfo.vacancies);

                versionInfo.CountUpdatedVacancies += LimitVacancies;
                await _vacanciesRepository.UpdateVersionInfo(versionInfo);

                offset += LimitVacancies;
            }
            while (count > versionInfo.CountUpdatedVacancies);

            return true;
        }
    }
}
