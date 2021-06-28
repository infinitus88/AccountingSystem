using AccountingSystem.Api.Models.Cards;
using AccountingSystem.Api.Models.Users;
using AccountingSystem.Data.Model;
using AccountingSystem.Queries.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UserMappings();
            CardMappings();
            LoginMappings();
        }

        private void UserMappings()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<UpdateUserModel, User>();
            CreateMap<CreateUserModel, User>().AfterMap<TrimAllStringProperty>();
        }

        private void LoginMappings()
        {
            CreateMap<UserWithToken, UserWithTokenModel>();
        }

        private void CardMappings()
        {
            CreateMap<Card, CardModel>();
            CreateMap<CardModel, Card>();
            CreateMap<CreateCardModel, Card>();
            CreateMap<UpdateCardModel, Card>();
        }
    }

    internal class TrimAllStringProperty : IMappingAction<object, object>
    {
        public void Process(object source, object destination, ResolutionContext context)
        {
            var stringProperties = destination.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(destination, null);
                if (currentValue != null)
                {
                    stringProperty.SetValue(destination, currentValue.Trim(), null);
                }
            }
        }
    }
}
