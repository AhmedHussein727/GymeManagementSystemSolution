using GymeManagementBLL.ViewModels.TrainerViewMofels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel>GetAllTeainers();
        bool CreateTrainer(CreateTrainerViewModel createdTrainer);

        TrainerViewModel? GetTrainerDetails(int TrainerID);

        TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerID);
        bool UpdateTrainerDetails(int TrainerId,TrainerToUpdateViewModel UpdatedTrainer);
        bool RemoveTrainer(int TrainerID);

    }
}
