using GritLog.Data;
using Microsoft.EntityFrameworkCore;

// 1. 앱 설정 준비 시작
var builder = WebApplication.CreateBuilder(args);

// 2. 서비스 등록(DI 컨테이너 등록)
builder.Services.AddControllersWithViews(); // mvc 쓸거임 등록
builder.Services.AddDbContext<AppDbContext>(options => // db 연결 쓸거임
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. 설정 끝, 실제 앱 객체 생성
var app = builder.Build();

//4. 미들웨어 파이프 라인 구성 단계 요청이 지나가는 통로를 순서대로 쌓는 중임
if (!app.Environment.IsDevelopment()) 
{
    // 운영 환경전용
    app.UseExceptionHandler("/Home/Error"); // 에러 발생 오류 페이지
    app.UseHsts(); // 무조건 https 로 접속 강제헤도 보냄
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

//5. URL  컨트롤러 연결 규칙
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//6. 서버 시작, 여기서 대기
app.Run();