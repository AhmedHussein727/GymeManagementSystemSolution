using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.AnalyticsViewModels;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.Services.Classes
{
    public class AnalyticService : IAnalytictsService
    {
        private readonly IUnitOfWork UnitOfWork;

        public AnalyticService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        public AnalyticsViewModel GetAnalyticsData()
        {
            var sessions=UnitOfWork.SessionRepository.GetAll();
            return new AnalyticsViewModel
            {
                ActiveMembers=UnitOfWork.GetRepository<MemberShip>().GetAll(x=>x.Status=="Active").Count(),
                TotalMembers=UnitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers=UnitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpComingSessions= sessions.Count(x=>x.StartDate>DateTime.Now),
                OnGoingSessions= sessions.Count(x=>x.StartDate<DateTime.Now && x.EndDate>DateTime.Now),
                CompletedSessions= sessions.Count(x=>x.EndDate<DateTime.Now)
            };
        }
    }
}
