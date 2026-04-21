
using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.MemberSession;
using Microsoft.AspNetCore.Mvc;

namespace GymeManagementPL.Controllers
{
    public class MemberSessionController:Controller
    {
        private readonly IMemberSessionSevice _memberSessionSevice;
        private readonly IMemberService _memberService;
        private readonly ISessionService _sessionService;

        public MemberSessionController(IMemberSessionSevice memberSessionSevice,IMemberService memberService,
            ISessionService sessionService)
        {
            _memberSessionSevice = memberSessionSevice;
            _memberService = memberService;
            _sessionService = sessionService;
        }

        #region Get all Sessions
        public ActionResult Index()
        {
            var Sessions = _memberSessionSevice.GetAllSessions();
            return View(Sessions);
        }
        #endregion


        #region create Membersession
        public IActionResult Create(int sessionId)
        {
            var vm = new CreateMemberSessionViewModel
            {
                SessionId = sessionId,
                Members = _memberService.GetAllMembers()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMemberSessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Members = _memberService.GetAllMembers(); 

                ModelState.AddModelError("DataInvalid", "Check missing fields");
                return View(model);
            }

            var result = _memberSessionSevice.CreateMemberSession(model);

            if (result)
                TempData["SuccessMessege"] = "Booking Created Successfully";
            else
                TempData["ErrorMessege"] = "Failed to create booking";

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region GetMembersForUpcomingSession()
        public IActionResult GetMembersForUpcomingSession(int sessionId)
        {
            if (sessionId <= 0)
            {
                return BadRequest();
            }
            ViewBag.SessionId = sessionId;
            var data=_memberSessionSevice.GetMembersForUpcomingSession(sessionId);
            if (data == null)
            {
                TempData["ErrorMessege"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }
        #endregion

        #region GetMembersForOnGoingSession()
        public IActionResult GetMembersForOnGoingSessions(int sessionId)
        {
            if (sessionId <= 0)
            {
                return BadRequest();
            }
            ViewBag.SessionId = sessionId;
            var data = _memberSessionSevice.GetMembersForOnGoingSession(sessionId);
            return View(data);
        }
        #endregion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsAttended(int memberSessionId)
        {
            var result = _memberSessionSevice.MarkAsAttended(memberSessionId);

            if (result)
                TempData["SuccessMessege"] = "Attendance marked successfully";
            else
                TempData["ErrorMessege"] = "Failed to mark attendance";

            return RedirectToAction(nameof(Index));
        }

        #region Delete MemberSession
        public IActionResult Cancel(int memberSessionId)
        {
           var Result= _memberSessionSevice.Cancel(memberSessionId);

            if (Result)
                TempData["Success"] = "Member Session Deleted Successfuly";
            else
                TempData["ErrorMessege"] = "Member Session Deleted Successfuly";

            return RedirectToAction(nameof(Index));


        }

        #endregion




    }
}
