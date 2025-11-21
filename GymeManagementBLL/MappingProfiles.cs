using AutoMapper;
using GymeManagementBLL.ViewModels.MemberViewModels;
using GymeManagementBLL.ViewModels.PlanViewModels;
using GymeManagementBLL.ViewModels.SessionViewModels;
using GymeManagementBLL.ViewModels.TrainerViewMofels;
using GymeManagementDAL.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            MapSession();
            MapMember();
            MapTrainer();
            MapPlan();
        }

        private void MapSession()
        {
            CreateMap<Sessions, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore()); 

            CreateMap<CreateSessionViewModel, Sessions>();

            CreateMap<UpdateSessionViewModel, Sessions>()
                    .ForMember(dest => dest.Trainer, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.MemberSessions, opt => opt.Ignore())
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                    .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.TrainerId));

;

            CreateMap<Sessions, UpdateSessionViewModel>()
                .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.TrainerId));



            CreateMap<Category, CategorySelectViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<Trainer, TrainerSelectViewModel>();
             
                

        }


        private void MapMember()
        {
            CreateMap<CraeteMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));

            CreateMap<CraeteMemberViewModel, Address>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
                .ForMember(dest => dest.Streat, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));

            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Streat}-{src.Address.City}"));


            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Streat))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Streat = src.Street;
                    dest.Address.City = src.City;
                    dest.CreatedAt=DateTime.Now;
                });
        }

        private void MapTrainer()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                        BuildingNumber = src.BuildingNumber,
                        Streat = src.Street,
                        City = src.City
                }));

            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialities.ToString()))
                .ForMember(dest=>dest.Address,opt=>opt.MapFrom(src=>$"{src.Address.BuildingNumber}-{src.Address.Streat}-{src.Address.City}"));

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Streat))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            CreateMap<TrainerToUpdateViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Streat = src.Street;
                dest.UpdatedAt = DateTime.Now;
            });




        }
        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();
            CreateMap<Plan, UpdatePlanViewModel>();
                

            CreateMap<UpdatePlanViewModel,Plan>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest=>dest.UpdatedAt,opt=>opt.MapFrom(src=>DateTime.Now));

        }

    }
}
