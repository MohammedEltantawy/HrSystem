using HrSystem.Data.Models;
using HrSystem.Services;
using HrSystemApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystemApi.Serives
{
    public class VacancyManager
    {
        private readonly VacancyService _vacancyService;

        public VacancyManager(VacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public string CreateVacancy(VacancyVM vacancy)
        {
            var obj = new Vacancy
            {
                JobName = vacancy.JobName,
                Department = vacancy.Department,
                Qualifications = vacancy.Qualifications,
                CreatedAt = DateTime.Now
            };
            var result = _vacancyService.CreateVacancy(obj);
            if (result) return obj.Id.ToString();
            return "Error";
        }

    }
}
