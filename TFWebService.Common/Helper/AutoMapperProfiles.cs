using AutoMapper;
using TFWebService.Data.Dtos.Api.Auth;
using TFWebService.Data.Dtos.Api.Learn;
using TFWebService.Data.Dtos.Api.UserFitnessDetail.MainDetail;
using TFWebService.Data.Dtos.Api.UserFitnessDetail.TrackDetail;
using TFWebService.Data.Dtos.CANDC.DeviceInfo;
using TFWebService.Data.Dtos.CANDC.LocateInfo;
using TFWebService.Data.Models;

namespace TFWebService.Common.Helper
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailDto>();
            CreateMap<FoodForAddDto, FoodsCalories>();
            CreateMap<FitnessForAddDto, FitnessCalories>();
            CreateMap<DeviceForAddDto, Device>();
            CreateMap<locationForAddDto, Location>();
            CreateMap<MainDetailForUpdateDto, MainDetails>();
            CreateMap<TrackDetailForUpdateDto, TrackDetails>();
            CreateMap<MainDetails, MainDetailForUpdateDto>();
            CreateMap<TrackDetails, TrackDetailForUpdateDto>();
            /*
            CreateMap<DeviceRegister, RegisterVariablesForWriteParameterDto>()                                
                .ForMember(des => des.RegisterName, opt =>
                {
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(des => des.CastType, opt =>
                {
                    opt.MapFrom(src => src.RegisterCastType.Title);
                })
                .ForMember(des => des.SubCategory, opt =>
                {
                    opt.MapFrom(src => src.SubCategory.Name);
                });

            CreateMap<RegisterVariablesForWriteParameterDto, RegisterVariablesForWriteParameterClassModel>();


            CreateMap<DeviceRegister, RegisterForExportDto>()
                .ForMember(des => des.RegisterCastTypeTitle, opt =>
                {
                    opt.MapFrom(src => src.RegisterCastType.Title);
                })
                .ForMember(des => des.RegisterCategoryName, opt =>
                {
                    opt.MapFrom(src => src.RegisterCategory.Name);
                })
                .ForMember(des => des.SubCategory, opt =>
                {
                    opt.MapFrom(src => src.SubCategory.Name);
                });

            CreateMap<RegisterVariablesForWriteParameterDto, RegisterForRecordDto>();
            CreateMap<RegisterVariablesForWriteParameterDto, RegisterVariablesForPushParameterDto>();

            CreateMap<DeviceRegister, RegisterBaseClassModels>();
            CreateMap<Device, DeviceBaseClassModels>();
            CreateMap<DeviceSetting, SettingBaseClassModels>();
            */
        }
    }
}
