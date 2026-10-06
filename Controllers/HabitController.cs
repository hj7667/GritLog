using GritLog.Data;
using Microsoft.AspNetCore.Mvc;

namespace GritLog.Controllers
{
    public class HabitController : Controller
    {

        private readonly AppDbContext _context;      // 1. 담아둘 필드 선언
        public HabitController(AppDbContext context) // 2. 생성자: ASP.NET이 알아서 넣어줌
        {
            _context = context;                      // 3. 필드에 저장
        }
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

        [HttpPost] // Post 요청일때만 실행
        [ValidateAntiForgeryToken] // 폼에 넣은 위조방지 토큰 검사
        public IActionResult Delete(int id) // 삭제 버튼이 보내준 습관 ID를 받음 
        {
            // TODO: 찾아서 삭제 (연관 HabitLog 처리)
            // 1. 
            var habit = _context.Habits.Find(id); // 1. Id로 한 줄 찾기
            if (habit == null) return NotFound(); // 2. 없으면 204

            _context.Habits.Remove(habit); // 3. 삭제 표시
            _context.SaveChanges(); // 4. 진짜 DB 에 반영

            return RedirectToAction(nameof(Manage)); // 끝나면 Manage 페이지로 이동
        }
    }
}