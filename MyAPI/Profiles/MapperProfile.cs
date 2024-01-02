using AutoMapper;
using MyAPI.Models.Domain;
using MyAPI.Models.Dto;

namespace MyAPI.Profiles
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();
        }
    }
}
