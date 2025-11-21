using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.TrainerViewMofels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymeManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }

        #region Get ALl Trainers
        public ActionResult Index()
        {
            var trainers = trainerService.GetAllTeainers();
            return View(trainers);
        }
        #endregion

        #region Get Trainer Data
        public ActionResult TrainerDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            var trainer = trainerService.GetTrainerDetails(id);
            if (trainer is null) return RedirectToAction(nameof(Index));
            return View(trainer);
        }

        #endregion

        #region Create Trainer
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "check data messing fields");
                return View(nameof(Create), createdTrainer);
            }
            bool result = trainerService.CreateTrainer(createdTrainer);
            if (result)
            {
                TempData["SuccessMessege"] = "trainer Created Succesfully";
            }
            else
            {
                TempData["ErrorMessege"] = "Failed to create ";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Trainer
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessege"] = "id cn not be 0 or less ";
                return RedirectToAction(nameof(Index));
            }
            var trainer = trainerService.GetTrainerToUpdate(id);
            if (trainer is null)
            {
                TempData["ErrorMessege"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel TrainerToEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(TrainerToEdit);
            }
            var result = trainerService.UpdateTrainerDetails(id, TrainerToEdit);
            if (result)
            {
                TempData["Success"] = "trainer updated successfully";
            }
            else
            {
                TempData["ErrorMessege"] = "trainer Failed to be updated";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete Trainer
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessege"] = "id cn not be 0 or less ";
                return RedirectToAction(nameof(Index));
            }
            var trainer = trainerService.GetTrainerDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessege"] = "trainer not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;
            return View();
        }
        [HttpPost]
        public ActionResult DeletedConfirmed([FromForm] int id)
        {
            var result = trainerService.RemoveTrainer(id);
            if (result)
            {
                TempData["Success"] = "trainer Deleted successfully";
            }
            else
            {
                TempData["ErrorMessege"] = "trainer Failed to be Deleted";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
