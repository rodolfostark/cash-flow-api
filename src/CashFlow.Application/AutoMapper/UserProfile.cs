﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class UserProfile : Profile
{
    public UserProfile()
    {
        RequestToEntity();
        EntityToResponse();
    }
    private void RequestToEntity()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(user => user.Password, config => config.Ignore());
    }
    private void EntityToResponse()
    {
        CreateMap<User, ResponseRegisteredUserJson>();
    }
}
