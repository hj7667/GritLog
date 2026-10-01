using Microsoft.EntityFrameworkCore;
using GritLog.Models;

namespace GritLog.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

}