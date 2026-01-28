using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using System.Linq;

namespace ResumeProjectDemo.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ResumeContext _context;

        public StatisticsController(ResumeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Genel sayılar
            ViewBag.AboutCount = _context.Abouts.Count();
            ViewBag.ExperienceCount = _context.Experiences.Count();
            ViewBag.PortfolioCount = _context.Portfolios.Count();
            ViewBag.ServiceCount = _context.Services.Count();
            ViewBag.SkillCount = _context.Skills.Count();

            // Messages
            ViewBag.MessageCount = _context.Messages.Count();
            ViewBag.MessageUnreadCount = _context.Messages.Count(x => x.IsRead == false);
            ViewBag.MessageReadCount = _context.Messages.Count(x => x.IsRead == true);

            // Testimonials
            ViewBag.TestimonialCount = _context.Testimonials.Count();
            ViewBag.TestimonialApprovedCount = _context.Testimonials.Count(x => x.IsConfirm == true);
            ViewBag.TestimonialPendingCount = _context.Testimonials.Count(x => x.IsConfirm == false);

            // İstersen kategoriyi de ekleyelim (varsa)
            ViewBag.CategoryCount = _context.Categories.Count();

            // Grafik için (Chart.js’e direkt basabilmek için sayılar)
            ViewBag.ChartLabels = new[] { "About", "Experience", "Portfolio", "Service", "Skill", "Testimonial", "Message" };
            ViewBag.ChartData = new[]
            {
                (int)ViewBag.AboutCount,
                (int)ViewBag.ExperienceCount,
                (int)ViewBag.PortfolioCount,
                (int)ViewBag.ServiceCount,
                (int)ViewBag.SkillCount,
                (int)ViewBag.TestimonialCount,
                (int)ViewBag.MessageCount
            };

            // Testimonial donut için
            ViewBag.TestimonialDonutLabels = new[] { "Onaylı", "Beklemede" };
            ViewBag.TestimonialDonutData = new[]
            {
                (int)ViewBag.TestimonialApprovedCount,
                (int)ViewBag.TestimonialPendingCount
            };

            // Message donut için
            ViewBag.MessageDonutLabels = new[] { "Okunmuş", "Okunmamış" };
            ViewBag.MessageDonutData = new[]
            {
                (int)ViewBag.MessageReadCount,
                (int)ViewBag.MessageUnreadCount
            };

            return View();
        }
    }
}
