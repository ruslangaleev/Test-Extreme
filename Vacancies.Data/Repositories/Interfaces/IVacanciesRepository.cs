using System.Collections.Generic;
using System.Threading.Tasks;
using Vacancies.Data.Models;

namespace Vacancies.Data.Repositories
{
    public interface IVacanciesRepository
    {
        /// <summary>
        /// Возвращает информацию о последнем обновлении вакансий.
        /// </summary>
        Task<VersionInfo> GetLastVersion();

        Task AddVersionInfo(VersionInfo versionInfo);

        Task UpdateVersionInfo(VersionInfo versionInfo);

        Task AddRangeRubrics(IEnumerable<Rubric> rubrics);

        Task AddRangeVacancies(IEnumerable<Vacancy> vacancies);
    }
}
