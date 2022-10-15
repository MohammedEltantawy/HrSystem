using HrSystem.Data.Models;
using HrSystem.Services;
using HrSystemApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystemApi.Serives
{

    public class CandidateManager
    {
        private readonly CandidateService _candidateService;

        public CandidateManager(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public string CreateCandidate(CandidateVM candidate)
        {
            var obj = new Candidate
            {
                Name = candidate.Name,
                Email = candidate.Email,
                Mobile = candidate.Mobile,
                CreatedAt = DateTime.Now
            };
            var result = _candidateService.CreateCandidate(obj);
            if (result) return obj.Id.ToString();
            return "Error";
        }
    }
}
