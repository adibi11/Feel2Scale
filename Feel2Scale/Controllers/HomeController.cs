using System.Diagnostics;
using Feel2Scale.Models;
using Microsoft.AspNetCore.Mvc;
using Feel2Scale.Data;

namespace Feel2Scale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ScaleData scaleData = new ScaleData
            {
                Id = 1,
                ScaleName = "Major Scale",
                Scale = new List<string> { "I", "II", "III", "IV", "V", "VI", "VII" },
                Chords = new List<string> { "C", "Dm", "Em", "F", "G", "Am", "Bdim" },
                Instruments = new List<string> { "Guitar", "Piano", "Violin" },
                Effects = new List<string> { "Reverb", "Delay" },
                Message = "This is a major scale, commonly used in many genres."
            };

            EmotionPrompt emotionPrompt = new EmotionPrompt
            {
                UserEmotion = "Happy",
                AiData = scaleData
            };
            return View(emotionPrompt);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
