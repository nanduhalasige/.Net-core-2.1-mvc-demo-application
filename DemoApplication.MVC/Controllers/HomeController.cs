using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoApplication.MVC.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using DemoApplication.MVC.Models.Repositories;
using DemoApplication.MVC.ViewModels;

namespace DemoApplication.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IStudentRepository studentRepository;
        public HomeController(IHttpClientFactory clientFactory, IStudentRepository _studentRepository)
        {
            _clientFactory = clientFactory;
            studentRepository = _studentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var currencyObj = await GetCurrencyRates();
            var StudentCount = await studentRepository.GetAll();

            var homePageObj = new HomePage()
            {
                currencyEx = currencyObj,
                StudentCount = StudentCount.Count()
            };
            //var INR = currencyObj.rates..ToString();
            return View(homePageObj);
        }

        private async Task<CurrencyEx> GetCurrencyRates()
        {
            var currencyEx = new CurrencyEx();
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://api.exchangeratesapi.io/latest?symbols=INR").Result.Content.ReadAsStringAsync();
                //"{\"rates\":{\"INR\":81.8765},\"base\":\"EUR\",\"date\":\"2020-03-13\"}"

                Regex pattern = new Regex("[}{]");
                response = pattern.Replace(response, "");
                var split = response.Split(",").ToList();
                split.ForEach(x =>
                {
                    if (x.Contains("INR"))
                    {
                        currencyEx.TargetCurrency = x.Split(":")[2].ToString();
                    }
                    else if (x.Contains("date"))
                    {
                        currencyEx.LastUpdated = x.Split(":")[1].Replace("\\", "").ToString();
                    }
                    else if (x.Contains("base"))
                    {
                        currencyEx.BaseCurrency = x.Split(":")[1].Replace("\\", "").ToString();
                    }
                });
            }
            catch
            {
                currencyEx.BaseCurrency = "EUR";
                currencyEx.LastUpdated = DateTime.Now.ToString();
                currencyEx.TargetCurrency = "00.0";
            }
            return currencyEx;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
