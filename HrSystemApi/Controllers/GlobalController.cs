using HrSystemApi.Serives;
using HrSystemApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class GlobalController : Controller
    {
        private readonly RequisitionManager _requisitionManager;
        private readonly VacancyManager _vacancyManager;
        private readonly CandidateManager _candidateManager;

        public GlobalController(RequisitionManager requisitionManager, VacancyManager vacancyManager, CandidateManager candidateManager)
        {
            _requisitionManager = requisitionManager;
            _vacancyManager = vacancyManager;
            _candidateManager = candidateManager;
        }

        [HttpPost("CreateVacancy")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "HrManager")]
        public ActionResult CreateVacancy([FromBody] VacancyVM vacancyVM)
        {
            var result = _vacancyManager.CreateVacancy(vacancyVM);
            if (result != "Error")
                return Ok("Success and id =" + result);
            return NotFound();
        }

        [HttpPost("CreateCandidate")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "HrOfficer")]
        public ActionResult CreateCandidate([FromBody] CandidateVM candidateVM)
        {
            var result = _candidateManager.CreateCandidate(candidateVM);
            if (result != "Error")
                return Ok("Success and id =" + result);
            return NotFound();
        }

        [HttpPost("CreateRequisition")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "HrOfficer")]
        public ActionResult CreateRequisition([FromBody] RequisitionVM requisitionVM)
        {
            var result = _requisitionManager.CreateRequisition(requisitionVM);
            if (result != "Error")
                return Ok("Success and id =" + result);
            return NotFound();
        }

        [HttpPost("ManagerApprove")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "HrOfficer")]
        public ActionResult HrManagerApproveRequisition([FromQuery] int requisitionId)
        {
            var result = _requisitionManager.HrManagerApproveRequisition(requisitionId);
            if (result != "Error")
                return Ok("Success and id =" + result);
            return NotFound();
        }

        [HttpPost("DirectorApprove")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "HrOfficer")]
        public ActionResult HrDirectorApproveRequisition([FromQuery] int requisitionId)
        {
            var result = _requisitionManager.HrDirectorApproveRequisition(requisitionId);
            if (result != "Error")
                return Ok("Success and id =" + result);
            return NotFound();
        }

    }
}
