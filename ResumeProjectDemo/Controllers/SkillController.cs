using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;

namespace ResumeProjectDemo.Controllers
{
    public class SkillController : Controller
    {
        private readonly ResumeContext _context;
        public SkillController(ResumeContext context)
        {
            _context = context;
        }
        public IActionResult SkillList()
        {
            var values = _context.Skills.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View();
        }
        
        [HttpPost]

        public IActionResult CreateSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return RedirectToAction("SkillList");
        }
        [HttpGet]
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null) return RedirectToAction("SkillList");
            _context.Skills.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("SkillList");
        }
        public IActionResult UpdateSkill(int? id)
        {
            if (id == null) return RedirectToAction("SkillList");

            var value = _context.Skills.Find(id.Value);
            if (value == null) return RedirectToAction("SkillList");

            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
            return RedirectToAction("SkillList");
        }

    }
}
