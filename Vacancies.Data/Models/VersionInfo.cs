using System;

namespace Vacancies.Data.Models
{
    /// <summary>
    /// Информация о обновлении.
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// Дата обновления.
        /// </summary>
        public DateTime UpdateAt { get; set; }
        
        /// <summary>
        /// Рубрики обновлены?
        /// </summary>
        public bool IsRubricUpdated { get; set; }

        /// <summary>
        /// Количество вакансий для обновления.
        /// </summary>
        public int CountVacancies { get; set; }

        /// <summary>
        /// Количество обновленных вакансий.
        /// </summary>
        public int CountUpdatedVacancies { get; set; }
    }
}
