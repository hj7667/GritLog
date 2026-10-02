using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GritLog.Models;
using GritLog.Data;

namespace GritLog.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var habits = _context.Habits.ToList();
        return View(habits);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Habit habit)
    {
        if (habit.Id == 0)
        {
            habit.CreatedDate = DateTime.Now;
            _context.Habits.Add(habit);
        }
        else
        {
            var existing = _context.Habits.Find(habit.Id);
            if (existing == null) return NotFound();

            existing.Name = habit.Name;
            existing.IsActive = habit.IsActive;
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}