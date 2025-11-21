using GymeManagementBLL.ViewModels.AnalyticsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.Services.Interfaces
{
    public interface IAnalytictsService
    {
        AnalyticsViewModel GetAnalyticsData();
    }
}
