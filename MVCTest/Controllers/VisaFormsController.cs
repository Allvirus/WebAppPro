using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTest.Data;
using MVCTest.Data.Http;
using MVCTest.Models;

namespace MVCTest.Controllers
{
    [Authorize]
    public class VisaFormsController : Controller
    {
        private readonly DataContext _context;
        private readonly IHttpFactory _httpFactory;

        public VisaFormsController(DataContext context, IHttpFactory httpFactory)
        {
            _context = context;
            _httpFactory = httpFactory;
        }

        // GET: VisaForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.VisaForm.ToListAsync());
        }

        // GET: VisaForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaForm = await _context.VisaForm
                .FirstOrDefaultAsync(m => m.ID == id);
            if (visaForm == null)
            {
                return NotFound();
            }

            return View(visaForm);
        }

        // GET: VisaForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VisaForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,app_type,centre,category,phone_code,phone,email,member,save,app_date,app_date_hidden,app_time,captcha,countryID,dateOfBirth,first_name,last_name,loc_final,loc_selected,mission_selected,missionId,nationalityId,passport_no,passportType,pptExpiryDate,pptIssueDate,pptIssuePalace")] VisaForm visaForm)
        {
            if (ModelState.IsValid)
            {
                //第一次访问 Get
                var uri = "book_appointment.php";
                var cookie = string.Empty;
                var responseCookie = await _httpFactory.HttpGet(uri, cookie);

                //第二次访问 Post  提交第一个表格

                cookie = responseCookie;
                var param = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("app_type", visaForm.app_type),
                    new KeyValuePair<string, string>("centre ", visaForm.centre),
                    new KeyValuePair<string, string>("category", visaForm.category),
                    new KeyValuePair<string, string>("phone_code", visaForm.phone_code),
                    new KeyValuePair<string, string>("phone", visaForm.phone),
                    new KeyValuePair<string, string>("email", visaForm.email),
                    new KeyValuePair<string, string>("countryID", ""),
                    new KeyValuePair<string, string>("member", visaForm.member),
                    new KeyValuePair<string, string>("save", "Continue"),
                };
                responseCookie = await _httpFactory.HttpPost(uri, cookie, param);

                //第三次访问 post  同意界面 获取cookie

                cookie = responseCookie;
                param = new List<KeyValuePair<string, string>>();
                param.Add(new KeyValuePair<string, string>("agree", "Agree"));
                responseCookie = await _httpFactory.HttpPost(uri, cookie, param);

                //第四次访问 Get  获取详细表格申请的Cookie
                cookie = responseCookie;
                uri = "appointment.php";
                responseCookie = await _httpFactory.HttpGet(uri, cookie);

                //第五次访问 Post  提交详细表格 
                cookie = responseCookie;
                param = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>(nameof(visaForm.app_date), visaForm.app_date.ToShortDateString()),
                    new KeyValuePair<string, string>("app_date_hidden ", visaForm.app_date.ToShortDateString()),
                    new KeyValuePair<string, string>("app_time", visaForm.app_time),
                    new KeyValuePair<string, string>("captcha", visaForm.captcha),
                    new KeyValuePair<string, string>("countryID", visaForm.countryID),
                    new KeyValuePair<string, string>("dateOfBirth", visaForm.dateOfBirth.ToShortDateString()),
                    new KeyValuePair<string, string>("first_name", visaForm.first_name),
                    new KeyValuePair<string, string>("last_name", visaForm.last_name),
                    new KeyValuePair<string, string>("loc_final", visaForm.loc_final),
                    new KeyValuePair<string, string>("loc_selected", visaForm.loc_selected),
                    new KeyValuePair<string, string>("mission_selected", visaForm.mission_selected),
                    new KeyValuePair<string, string>("missionId", visaForm.missionId),
                    new KeyValuePair<string, string>("nationalityId", visaForm.nationalityId),
                    new KeyValuePair<string, string>("passport_no", visaForm.passport_no),
                    new KeyValuePair<string, string>("passportType", visaForm.passportType),
                    new KeyValuePair<string, string>("phone", visaForm.phone),
                    new KeyValuePair<string, string>("phone_code", visaForm.phone_code),
                    new KeyValuePair<string, string>("pptExpiryDate", visaForm.pptExpiryDate.ToShortDateString()),
                    new KeyValuePair<string, string>("pptIssueDate", visaForm.pptIssueDate.ToShortDateString()),
                    new KeyValuePair<string, string>("pptIssuePalace", visaForm.pptIssuePalace),
                    new KeyValuePair<string, string>("save", "Submit"),
                    new KeyValuePair<string, string>("VisaTypeId", "93"),
                };
                responseCookie = await _httpFactory.HttpPost(uri, cookie, param);


                //第六次访问 Get  获取详细表格
                cookie = responseCookie;
                uri = "message.php";
                responseCookie = await _httpFactory.HttpGet(uri, cookie);

                _context.Add(visaForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visaForm);
        }

        // GET: VisaForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaForm = await _context.VisaForm.FindAsync(id);
            if (visaForm == null)
            {
                return NotFound();
            }
            return View(visaForm);
        }

        // POST: VisaForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,app_type,centre,category,phone_code,phone,email,member,save,app_date,app_date_hidden,app_time,captcha,countryID,dateOfBirth,first_name,last_name,loc_final,loc_selected,mission_selected,missionId,nationalityId,passport_no,passportType,pptExpiryDate,pptIssueDate,pptIssuePalace")] VisaForm visaForm)
        {
            if (id != visaForm.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visaForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisaFormExists(visaForm.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(visaForm);
        }

        // GET: VisaForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaForm = await _context.VisaForm
                .FirstOrDefaultAsync(m => m.ID == id);
            if (visaForm == null)
            {
                return NotFound();
            }

            return View(visaForm);
        }

        // POST: VisaForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visaForm = await _context.VisaForm.FindAsync(id);
            _context.VisaForm.Remove(visaForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisaFormExists(int id)
        {
            return _context.VisaForm.Any(e => e.ID == id);
        }
    }
}
