using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vacancies.Data.Models;
using Vacancies.Data.Repositories;
using Vacancies.Services.Services.Logic;

namespace Vacancies.Tests
{
    [TestFixture]
    public class VacanciesManagerTests
    {
        [Test]
        public async Task ReturnsFalseIfVacanciesUpdated()
        {
            // Подготовка
            var vacanciesRepositoryMock = new Mock<IVacanciesRepository>();
            vacanciesRepositoryMock.Setup(t => t.GetLastVersion()).ReturnsAsync(new VersionInfo
            {
                UpdateAt = DateTime.Now.AddHours(-2)
            });
            var vacanciesManager = new VacanciesManager(vacanciesRepositoryMock.Object);

            // Действие
            var result = await vacanciesManager.UpdateVacancies();

            // Проверка
            Assert.IsFalse(result);
        }


        // Проверить, последнее обновление вакансий
        // Если база давно не обновлялась, тогда обновляем
        // Скачать все рубрики, записываем дату
    }
}
