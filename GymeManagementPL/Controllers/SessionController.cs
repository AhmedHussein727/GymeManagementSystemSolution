using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymeManagementPL.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        #region Get All Sessions
        public ActionResult Index()
        {
            var sessions=sessionService.GetAllSessions();
            return View(sessions);
        }
        #endregion

        #region Session Details
        public ActionResult Details(int id)
        {
            if(id<=0)
            {
                return RedirectToAction(nameof(Index));
            }
            var session=sessionService.GetSessionById(id);
            if(session==null)
            {
                return RedirectToAction(nameof(Index));

            }
            return View(session);
        }
        #endregion

        #region Create session
        public ActionResult Create()
        {
            loadDropDowensForCategories();
            loadDropDowensForTrainers();
            return View();
        }
        [HttpPost]
        public ActionResult Create([FromRoute] int id, CreateSessionViewModel CreatedSession)
        {
            if(!ModelState.IsValid)
            {
                loadDropDowensForTrainers();
                loadDropDowensForCategories();
                return View(nameof(Create), CreatedSession);
            }
            var result = sessionService.CteateSession(CreatedSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Created Succesfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create ";
                loadDropDowensForTrainers();
                loadDropDowensForCategories();
                return View(CreatedSession);

            }


        }
        #endregion

        #region Edit Session
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "id cn not be 0 or less ";
                return RedirectToAction(nameof(Index));
            }
            var Session = sessionService.GetSessionToUpdate(id);
            if (Session is null)
            {
                TempData["ErrorMessage"] = "Session Can not be updated";
                return RedirectToAction(nameof(Index));
            }
            loadDropDowensForTrainers();
            return View(Session);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute]int id, UpdateSessionViewModel updatedSession )
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "check data messing fields");
                loadDropDowensForTrainers();
                return View(nameof(Edit), updatedSession);
            }
            var result = sessionService.UpdateSession(updatedSession, id);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Updated Succesfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Update ";
            }


            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["SuccessMessage"] = "Invalid SessionId";

                return RedirectToAction(nameof(Index));
            }
            var result=sessionService.GetSessionById(id);
            if (result is null)
            {
                TempData["ErrorMessage"] = "Session Not found ";

                return RedirectToAction(nameof(Index));
            }
            ViewBag.sessionId = result.Id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result=sessionService.RemoveSession(id);
            if(result)
            {
                TempData["SuccessMessage"] = "Session deleted succsesfuly";
            }
            else
            {
                TempData["ErrorMessage"] = "canot delete this session";

            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Helper
        private void loadDropDowensForCategories()
        {
            var categories = sessionService.GetCategoriesForDropDowen();
            ViewBag.categories = new SelectList(categories, "Id", "Name");
        }
        private void loadDropDowensForTrainers()
        {

            var trainers = sessionService.GetTrainersForDropDowen();
            ViewBag.trainers = new SelectList(trainers, "Id", "Name");
        } 
        #endregion
    }
}
