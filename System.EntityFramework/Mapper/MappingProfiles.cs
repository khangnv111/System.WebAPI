using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Models.Model.Admin;
using System.Models.ViewModel.User;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<SaveUser, User>();
            CreateMap<User, SaveUser>();

            CreateMap<SaveGroup, Group>();
            CreateMap<Group, SaveGroup>();
        }
    }
}
