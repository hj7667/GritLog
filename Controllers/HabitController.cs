using GritLog.Data;
using GritLog.Models;
using Microsoft.AspNetCore.Mvc;

namespace GritLog.Controllers
{
    public class HabitController : Controller
    {
        private readonly AppDbContext _context;
        public HabitController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Manage()
        {
            var habits = _context.Habits.ToList();
            return View(habits);
        }

        public IActionResult Stats() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, bool isActive)
        {
            var habit = new Habit
            {
                Name = name,
                IsActive = isActive,
                CreatedDate = DateTime.Now
            };
            _context.Habits.Add(habit);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string name, bool isActive)
        {
            var habit = _context.Habits.Find(id);
            if (habit == null) return NotFound();

            habit.Name = name;
            habit.IsActive = isActive;
            _context.SaveChanges();

            return RedirectToAction(nameof(Manage));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var habit = _context.Habits.Find(id);
            if (habit == null) return NotFound();

            _context.Habits.Remove(habit);
            _context.SaveChanges();

            return RedirectToAction(nameof(Manage));
        }
    }
}