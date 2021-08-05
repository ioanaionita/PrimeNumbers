using System;
using System.ComponentModel.DataAnnotations;

namespace PrimeNumbers.Models
{
    public class Primes
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Length { get; set; }
        public int[,] PrimeNumbers { get; set; }
        public Primes()
        {
        }
    }
}
