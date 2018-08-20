using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vacancies.Data.Models;
using Vacancies.Data.Repositories;
using Vacancies.Services.Clients.Interfaces;
using Vacancies.Services.Services.Logic;
using Vacancies.Services.Services.ResourceModels;

namespace Vacancies.Tests
{
    [TestFixture]
    public class VacanciesManagerTests
    {
        [Test]
        public async Task ReturnsFalseIfVacanciesActual()
        {
            // Подготовка
            var vacanciesRepositoryMock = new Mock<IVacanciesRepository>();
            vacanciesRepositoryMock.Setup(t => t.GetLastVersion()).ReturnsAsync(new VersionInfo
            {
                UpdateAt = DateTime.Now.AddHours(-2)
            });
            var zpClient = new Mock<IZpClient>();
            var vacanciesManager = new VacanciesManager(vacanciesRepositoryMock.Object, zpClient.Object);

            // Действие
            var result = await vacanciesManager.UpdateVacancies();

            // Проверка
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ReturnsTrueIfVacanciesUpdated()
        {
            // Подготовка
            var vacanciesRepositoryMock = new Mock<IVacanciesRepository>();
            vacanciesRepositoryMock.Setup(t => t.GetLastVersion()).ReturnsAsync(new VersionInfo
            {
                UpdateAt = DateTime.Now.AddDays(-1)
            });
            var zpClient = new Mock<IZpClient>();
            zpClient.Setup(t => t.GetRubrics()).ReturnsAsync(new List<Rubric>
            {
                new Rubric(),
                new Rubric()
            });
            zpClient.Setup(t => t.GetVacancies(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new VacanciesInfo
            {
                vacancies = new List<Vacancy>
                {
                    new Vacancy(),
                    new Vacancy()
                },
                Count = 500
            });
            var vacanciesManager = new VacanciesManager(vacanciesRepositoryMock.Object, zpClient.Object);

            // Действие
            var result = await vacanciesManager.UpdateVacancies();

            // Проверка
            Assert.IsTrue(result);
        }

        // Проверить, последнее обновление вакансий
        // Если база давно не обновлялась, тогда обновляем
        // Скачать все рубрики, записываем дату
    }
}
