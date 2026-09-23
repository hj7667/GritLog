using Microsoft.AspNetCore.Mvc;

namespace GritLog.Controllers
{
    public class HabitController : Controller
    {
   

        public IActionResult Manage() => View();

        // 통계: /Habit/Stats
        public IActionResult Stats() => View();

        // ---- 아래는 모달 폼이 404 안 나게 걸어둔 자리 (Phase 2에서 DB 붙이기) ----

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, bool isActive)
        {
            // TODO: _context.Habits.Add(...); _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string name, bool isActive)
        {
            // TODO: 찾아서 수정 후 저장
            return RedirectToAction(nameof(Manage));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // TODO: 찾아서 삭제 (연관 HabitLog 처리)
            return RedirectToAction(nameof(Manage));
        }
    }
}