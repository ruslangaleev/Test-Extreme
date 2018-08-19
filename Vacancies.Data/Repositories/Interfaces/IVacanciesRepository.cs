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
    }
}
