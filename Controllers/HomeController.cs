using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GritLog.Models;

namespace GritLog.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
