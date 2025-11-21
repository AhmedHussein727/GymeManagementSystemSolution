using GymeManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymeManagementPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalytictsService analytictsService;

        public HomeController(IAnalytictsService analytictsService)
        {
            this.analytictsService = analytictsService;
        }
        public IActionResult Index()
        {
            var data = analytictsService.GetAnalyticsData();
            return View(data);
        }
    }
}
