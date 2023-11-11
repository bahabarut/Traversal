using AutoMapper;
using DTOLayer.DTOs.AnnouncementDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using EntityLayer.Concrete;

namespace TraversalCore.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AnnouncementAddDTO, Announcement>();
            CreateMap<Announcement, AnnouncementAddDTO>();

            CreateMap<AppUserRegisterDTO, AppUser>();
            CreateMap<AppUser, AppUserRegisterDTO>();

            CreateMap<AppUserLoginDTO, AppUser>();
            CreateMap<AppUser, AppUserLoginDTO>();

            CreateMap<AnnouncementListDTO, Announcement>();
            CreateMap<Announcement, AnnouncementListDTO>();
            
            CreateMap<AnnouncementUpdateDTO, Announcement>();
            CreateMap<Announcement, AnnouncementUpdateDTO>();

        }
    }



    //GENERIC AUTO MAPPER
    //public class MapProfile<Tsource, T2dest> : Profile where T2dest : class where Tsource : class
    //{
    //    public MapProfile()
    //    {
    //        CreateMap<Tsource, T2dest>();
    //        CreateMap<T2dest, Tsource>();
    //    }
    //}
}
