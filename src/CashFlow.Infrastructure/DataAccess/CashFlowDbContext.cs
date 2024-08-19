using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
public class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
    public CashFlowDbContext(DbContextOptions options) : base(options)
    {

    }
}
