using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;

namespace ResumeProjectDemo.Controllers
{
    public class AboutController : Controller
    {
        private readonly ResumeContext _context;
        public AboutController(ResumeContext context)
        {
            _context = context;
        }
        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList();
            return View(values);
        }

        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]

        public IActionResult CreateAbout(About about)
        {
            _context.Abouts.Add(about);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }
        public IActionResult DeleteAbout(int id)
        {
            var values = _context.Abouts.Find(id);
            {
                _context.Abouts.Remove(values);
                _context.SaveChanges();
                return RedirectToAction("AboutList");
            }
        }
        public IActionResult UpdateAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            return View(value);
        }

        [HttpPost]

        public IActionResult UpdateAbout(About about)
        {
            _context.Abouts.Update(about);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }
    }
}
