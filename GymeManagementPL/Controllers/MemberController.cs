using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymeManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        #region Get ALl Members
        public ActionResult Index()
        {
            var members = memberService.GetAllMembers();
            return View(members);
        }
        #endregion

        #region Get Member Data
        public ActionResult MemberDetails(int id)
        {
            if(id<=0)
                return RedirectToAction(nameof(Index));

            var member=memberService.GetMemberDetails(id);
            if(member is null)return RedirectToAction(nameof(Index));
            return View(member);
        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));
            var healthRecord=memberService.GetMemberHealthRecordDetails(id);
            if (healthRecord is null) return RedirectToAction(nameof(Index));
            return View(healthRecord);
        }
        #endregion

        #region Create Member
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMember(CraeteMemberViewModel createdMememer)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "check data messing fields");
                return View(nameof(Create),createdMememer);
            }
            bool result=memberService.CreateMember(createdMememer);
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

        #region Edit Member
        public ActionResult MemberEdit(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessege"] = "id cn not be 0 or less ";
                return RedirectToAction(nameof(Index));
            }
            var Member=memberService.GetMemberDetails(id);
            if(Member is null)
            {
                TempData["ErrorMessege"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);
        }

        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id,MemberToUpdateViewModel MemberToEdit)
        {
            if(!ModelState.IsValid)
            {
                return View(MemberToEdit);
            }
            var result=memberService.UpdateMemberDetails(id,MemberToEdit);
            if(result)
            {
                TempData["Success"] = "Member updated successfully";
            }
            else
            {
                TempData["ErrorMessege"] = "Member Failed to be updated";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete Member
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessege"] = "id cn not be 0 or less ";
                return RedirectToAction(nameof(Index));
            }
            var member=memberService.GetMemberDetails(id);
            if(member is null)
            {
                TempData["ErrorMessege"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId=id;
            return View();
        }
        [HttpPost]
        public ActionResult DeletedConfirmed([FromForm]int id)
        {
            var result=memberService.RemoveMember(id);
            if (result)
            {
                TempData["Success"] = "Member Deleted successfully";
            }
            else
            {
                TempData["ErrorMessege"] = "Member Failed to be Deleted";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
