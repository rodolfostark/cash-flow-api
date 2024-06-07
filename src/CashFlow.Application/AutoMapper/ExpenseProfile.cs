﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        RequestToEntity();
        EntityToResponse();
    }
    private void RequestToEntity()
    {
        CreateMap<RequestRegisterExpenseJson, Expense>();
    }
    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, ResponseExpenseJson>();
    }
}