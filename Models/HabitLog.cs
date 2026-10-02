namespace GritLog.Models;

// Habit 을 실행한 기록
public class HabitLog
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public Habit? Habit { get; set; }
    public DateTime Date { get; set; }
    public bool IsCompleted { get; set; }
}