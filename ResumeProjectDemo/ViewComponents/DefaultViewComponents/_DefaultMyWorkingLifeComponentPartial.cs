using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;

namespace ResumeProjectDemo.ViewComponents.DefaultViewComponents
{
    public class _DefaultMyWorkingLifeComponentPartial : ViewComponent
    {
        private readonly ResumeContext _context;
        public _DefaultMyWorkingLifeComponentPartial(ResumeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.MyWorkingLives.ToList();
            return View(values);
        }
    }
}
