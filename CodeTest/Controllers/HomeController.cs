using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Quote.Contracts;
using Quote.Models;

namespace PruebaIngreso.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuoteEngine quote;

        public HomeController(IQuoteEngine quote)
        {
            this.quote = quote;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Test(string testCode)
        {
            if (string.IsNullOrWhiteSpace(testCode))
            {
                testCode = "E-U10-PRVPARKTRF";
            }
            var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                TourCode = testCode,
                Language = Language.Spanish
            };

            var result = await this.quote.Quote(request);
            var tour =  result.Tours.FirstOrDefault();
            ViewBag.Message = "Test 1 Correcto";
            return View(tour);
        }

        public ActionResult Test2()
        {
            ViewBag.Message = "Test 2 Correcto";
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Test3(string test3Code)
        {
            var result =  await this.quote.GetMarginAsync(test3Code);
            if(result.StatusCode == HttpStatusCode.OK)
            {
                ViewBag.Message = result.Message;

            }
            else
            {
                ViewBag.Message = "{ \"margin\": 0.0 }";

            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Test4(string test4Code)
        {
            var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                TourCode = test4Code,
                Language = Language.Spanish
            };

            var result = await this.quote.Quote(request);
            return View(result.TourQuotes);
        }
    }
}