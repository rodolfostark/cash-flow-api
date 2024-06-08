﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;

    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses
            .AddAsync(expense);
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses
            .AsNoTracking()
            .ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        var expense = await _dbContext.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(expense => expense.Id == id);
        return expense;
    }
    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
    {
        var expense = await _dbContext.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(expense => expense.Id == id);
        return expense;
    }
    public async Task<bool> Delete(long id)
    {
        var expense = await _dbContext.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(expense => expense.Id == id);
        if (expense is null) 
        { 
            return false;
        }
        _dbContext.Expenses.Remove(expense);
        return true;
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }
}
