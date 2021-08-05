PrimeNumbers

Versions used: 
	- Microsoft.AspNetCore.App version: 3.1.10, 
	- Microsoft.NETCore.App version: 3.1.10

Project Structure:

The project is using an MVC architecture for obtaining the integer N input via a form, then outputting the prime number matrix on a separate web page. 
The matrix is loaded dynamically onto the page. 
The model used can be seen as the matrix data and the matrix dimensions (in this case, N). 

Prime Number Algorithm:

- The proposed algorithm follows Eratosthenes’ sieve (https://www.geeksforgeeks.org/sieve-of-eratosthenes/), which takes an interval [A, B] and will display all the prime number within that interval, by marking the numbers that are multiples. In this way, the numbers that are not marked remain only the prime numbers. The algorithm was adapted by reducing the number of steps for marking the multiples:
			for example, having 5 and looking for it’s multiples, it will start with 5*5, considering that the previous multiples have been found 
- As the aim was to obtain N*N prime numbers, the interval is larger. 

In order to determine how big the interval has to be, I have adapted the Prime Number Theorem (https://www.britannica.com/science/prime-number-theorem) that says:
Between 1 and N, there are N/ln(N) numbers (approximately). 
This means that we will find approximately N prime numbers in [1, N*ln(N)]. 
By testing, I have adjusted the interval to [1, N*(ln(N)+1)] to make sure all the prime numbers are found. 

For finding N*N numbers, the following searching interval was used:

				[2, N*N*(ln(N*N)+1)]
