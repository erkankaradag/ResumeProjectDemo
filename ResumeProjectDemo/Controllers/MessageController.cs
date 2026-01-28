using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;

namespace ResumeProjectDemo.Controllers
{
    public class MessageController : Controller
    {
        private readonly ResumeContext _context;

        public MessageController(ResumeContext context)
        {
            _context = context;
        }

        // =======================
        // ADMIN: MESAJ LİSTESİ
        // =======================
        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _context.Messages
                .OrderByDescending(x => x.SendDate)
                .ToList();

            return View(values);
        }

        // =======================
        // ADMIN: MESAJ GÖRÜNTÜLE (Detay)
        // Not: Görüntüleyince otomatik Okundu yapar
        // =======================
        [HttpGet]
        public IActionResult MessageDetail(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null) return NotFound();

            if (!message.IsRead)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }

            return View(message); // Views/Message/MessageDetail.cshtml
        }

        // =======================
        // ADMIN: OKUNDU / OKUNMADI DEĞİŞTİR
        // =======================
        [HttpPost]
        public IActionResult ToggleRead(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null) return NotFound();

            message.IsRead = !message.IsRead;
            _context.SaveChanges();

            return RedirectToAction("MessageList");
        }

        // =======================
        // ADMIN: MESAJ SİL
        // =======================
        [HttpPost]
        public IActionResult DeleteMessage(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null) return NotFound();

            _context.Messages.Remove(message);
            _context.SaveChanges();

            return RedirectToAction("MessageList");
        }

        // =======================
        // UI: AJAX MESAJ GÖNDER
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage([FromForm] Message message)
        {
            if (message == null)
                return BadRequest(new { ok = false, message = "Geçersiz istek." });

            if (string.IsNullOrWhiteSpace(message.NameSurname) ||
                string.IsNullOrWhiteSpace(message.EmailAddress) ||
                string.IsNullOrWhiteSpace(message.MessageDetail))
            {
                return BadRequest(new { ok = false, message = "Lütfen Ad Soyad, Email ve Mesaj alanlarını doldurun." });
            }

            message.SendDate = DateTime.Now;
            message.IsRead = false;

            _context.Messages.Add(message);
            _context.SaveChanges();

            return Ok(new { ok = true, message = "Mesajınız başarıyla gönderildi. Teşekkürler!" });
        }
    }
}
