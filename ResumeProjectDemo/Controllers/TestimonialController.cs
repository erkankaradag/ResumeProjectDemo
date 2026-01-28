using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;
using System.Linq;

namespace ResumeProjectDemo.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ResumeContext _context;

        public TestimonialController(ResumeContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult TestimonialList()
        {
            var values = _context.Testimonials.ToList();
            return View(values);
        }

        // CREATE (GET)
        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial testimonial)
        {
            // default: yeni eklenen yorum beklemede olsun
            if (testimonial.IsConfirm == false)
            {
                // zaten false ise dokunmaya gerek yok
            }

            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        // UPDATE (GET)
        [HttpGet]
        public IActionResult UpdateTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            if (value == null) return RedirectToAction("TestimonialList");
            return View(value);
        }

        // UPDATE (POST)
        [HttpPost]
        public IActionResult UpdateTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Update(testimonial);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        // DELETE
        public IActionResult DeleteTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            if (value == null) return RedirectToAction("TestimonialList");

            _context.Testimonials.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        // ✅ Pasif/Aktif değiştir
        [HttpGet]
        public IActionResult ToggleConfirm(int id)
        {
            var value = _context.Testimonials.Find(id);
            if (value == null) return RedirectToAction("TestimonialList");

            value.IsConfirm = !value.IsConfirm;
            _context.SaveChanges();

            return RedirectToAction("TestimonialList");
        }
    }
}
