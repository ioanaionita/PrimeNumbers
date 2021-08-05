using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeNumbers.Models;

namespace PrimeNumbers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // The method takes n and outputs a matrix of n x n of prime numbers.
        // It is an adaptation of Eratosthenes' sieve, where the prime numbers
        // are searched within a closed interval.
        // The solution marks the multiples as not prime, following that
        // the numbers left will be primes. 
        // As the Prime Numbers Theorem approximates that between [2, N]
        // there are approximately N/ln(N) prime numbers, this number has
        // been adjusted so we can find N*N prime numbers.
        public int[,] primeNumberList (int n)
        {
            int[,] primes = new int[n + 1, n + 1];
            int m = n * n * Convert.ToInt32(Math.Log(n * n) + 1);
            int[] isNotPrime = new int[m + 1];
            for(int i = 0; i < m; i++)
            {
                isNotPrime[i] = 0;
            }
            isNotPrime[0] = 1;
            isNotPrime[1] = 1;
            for(int i = 2; i < Math.Sqrt(m); i++)
            {
                if(isNotPrime[i] == 0)
                {
                    for(int j = i; j <= m/i; j++)
                    {
                        isNotPrime[i * j] = 1;
                    }
                }
            }

            int k1 = 0, k2 = 0;

            for(int i = 0; i < m; i++)
            {
                // If the number is prime, add it to the matrix
                if(isNotPrime[i] == 0)
                {
                    primes[k1, k2] = i;
                    k2++;
                    if(k2 == n)
                    {
                        k2 = 0;
                        k1++;
                    }

                }
            }
            return primes;
        }

        public IActionResult Index(int n)
        {
            //If no number has been introduced, the page redirects to the form.
            if(n <= 0)
            {
                return RedirectToAction("Form");
            }
            Primes primeNumbers = new Primes();
            primeNumbers.Length = n;
            primeNumbers.PrimeNumbers = primeNumberList(n);

            return View(primeNumbers);
        }

        public ActionResult Form()
        {
            Primes primes = new Primes();
            return View(primes);
        }

        // The value for N is validated via the model (by imposing it to be
        // greater than 1, it is required via de view and any other errors are
        // displayed with the help of the controller, so validation is done
        // across all MVC components. 
        [HttpPost]
        public ActionResult Form(Primes prime)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", new { n = prime.Length });
                }
                else
                {
                    return View(prime);
                }
            }
            catch (Exception e)
            {
                return View(prime);
            }
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
