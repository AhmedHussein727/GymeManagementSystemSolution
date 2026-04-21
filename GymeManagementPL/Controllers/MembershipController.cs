
using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.MembershipsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymeManagementPL.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IMembershipsService _membershipsService;
        private readonly IMemberService _memberService;
        private readonly IPlanService _planService;

        public MembershipController(IMembershipsService membershipsService,IMemberService memberService,IPlanService planService)
        {
            _membershipsService = membershipsService;
            _memberService = memberService;
            _planService = planService;
        }
        #region Get all Memberships
        public ActionResult Index()
        {
            var Memberships = _membershipsService.GetAllActiveMemberships();
            return View(Memberships);
        }
        #endregion

        #region Create Membership Get
        public ActionResult Create()
        {
            var vm = new CreateMembershipViewModel
            {
                Members = _memberService.GetAllMembers().ToList(),
                Plans = _planService.GetAllPlans().ToList(),
            };


            return View(vm);
        }
        #endregion

        #region Create Membership Post
        [HttpPost]
        public ActionResult Create(CreateMembershipViewModel createMembershipViewModel)
        {
            if (!ModelState.IsValid)
            {
                createMembershipViewModel.Members = _memberService.GetAllMembers().ToList();
                createMembershipViewModel.Plans = _planService.GetAllPlans().ToList();
                ModelState.AddModelError("DataInvalid", "check data messing fields");
                return View(nameof(Create), createMembershipViewModel);
            }

            var result=_membershipsService.CreateMembership(createMembershipViewModel);
            if (result)
            {
                TempData["SuccessMessege"] = "Member Created Succesfully";
            }
            else
            {
                TempData["ErrorMessege"] = "Failed to create ";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete Membership
        [HttpPost]
        public ActionResult Cancel(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessege"] = "Invalid membership id";
                return RedirectToAction(nameof(Index));
            }

            var result = _membershipsService.Cancel(Id);

            if (result)
                TempData["SuccessMessege"] = "Membership canceled successfully";
            else
                TempData["ErrorMessege"] = "Cannot cancel (not found or already expired)";

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }

}

