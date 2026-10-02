namespace GritLog.Models;

// 습관 자체 정보
public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }
}

