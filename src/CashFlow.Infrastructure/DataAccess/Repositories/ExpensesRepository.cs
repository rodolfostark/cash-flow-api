﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
public class ExpensesRepository : IExpensesRepository
{
    public void Add(Expense expense)
    {
        var dbContext = new CashFlowDbContext();
        dbContext.Expenses.Add(expense);
        dbContext.SaveChanges();
    }
}
