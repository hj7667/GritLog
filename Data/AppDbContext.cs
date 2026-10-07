using Microsoft.EntityFrameworkCore;
using GritLog.Models;

namespace GritLog.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    // DbSet 테이블 하나를 코드에서 다루는 통로
    // 마이그레이션때 EF 이줄을 보고 아 Habits이라는 테이블을 만들어야겠다고 판단함
    // 이걸로 
    // _context.Habits.TodList();
    // _context.Habits.Add(habit);
    // _context.Habits.Find(id);
    // _context.Habits.Remove(habit);
    // context 를 가져와서 쓰는것
    // DbSet 테이블 하나를 코드에서 다루는 통로
    // 마이그레이션때 EF 이줄을 보고 아 Habits이라는 테이블을 만들어야겠다고 판단함
    // 이걸로 
    // _context.Habits.TodList();
    // _context.Habits.Add(habit);
    // _context.Habits.Find(id);
    // _context.Habits.Remove(habit);

    public DbSet<Habit> Habits { get; set; }
    public DbSet<HabitLog> HabitLogs { get; set; }
}