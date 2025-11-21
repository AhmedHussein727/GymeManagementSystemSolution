using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymeManagementPL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }
        #region Get all plans
        public ActionResult Index()
        {
            var plans = planService.GetAllPlans();
            return View(plans);
        }
        #endregion

        #region get plan details
        public ActionResult Details(int id)
        {
            if(id<=0)
            {
                TempData["ErrorMessege"] = "invalid id";
                return RedirectToAction(nameof(Index));
            }
            var plan=planService.GetPlanById(id);
            if(plan==null)
            {
                TempData["ErrorMessege"] = "plan is null";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
        #endregion

        #region Edit plan
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessege"] = "invalid id";
                return RedirectToAction(nameof(Index));
            }
            var plan = planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["ErrorMessege"] = "can not edit";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);

        }
        [HttpPost]
        public ActionResult Edit([FromRoute]int id,UpdatePlanViewModel updatedPlan)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "chek data validation");
                return View(updatedPlan);
            }
            var result = planService.UpdatePlan(id, updatedPlan);
            if(result)
            {
                TempData["SuccessMessege"] = "plan updated succesfuly"; 
            }
            else
            {
                TempData["ErrorMessege"] = "failed to update";

            }
            return RedirectToAction(nameof (Index));

        }
        #endregion

        #region toggle statuse
        public ActionResult Activate([FromRoute]int id)
        {
            var result=planService.ToggleStatus(id);
            if(result)
            {
                TempData["SuccessMessege"] = "plan status changed";
            }
            else
            {
                TempData["ErrorMessege"] = "failed to change plan status";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
