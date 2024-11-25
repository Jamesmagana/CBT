using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


public class Program
{
    public static async Task Main(string[] args)
    {
        // Problem 1-4
        #region MyRegion for Write a function to deserialize the following JSON string into a C# object, and then serialize it back into a JSON string after modifying one of the properties.

        string json = "{\"id\": 1, \"name\": \"Test\", \"price\": 100.0}";

        // Deserialize
        Product product = JsonConvert.DeserializeObject<Product>(json);

        // Modify property
        product.Price = 150.0;

        // Serialize back to JSON
        string updatedJson = JsonConvert.SerializeObject(product);
        Console.WriteLine("____________________________________________________________");
        Console.WriteLine("Result Problem 1-4");
        Console.WriteLine(updatedJson);

        #endregion

        // Problem 5-7
        #region MyRegion for Write a function that takes a list of integers and returns a dictionary where the keys are the integers and the values are the number of times each integer appears in the list. Use LINQ to solve this problem.

        List<int> numbers = new List<int> { 1, 2, 2, 3, 3, 3 };

        var result = numbers.GroupBy(n => n)
                            .ToDictionary(g => g.Key, g => g.Count());
        Console.WriteLine("____________________________________________________________");
        Console.WriteLine("Result Problem 5-7");
        foreach (var kvp in result)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        #endregion

        Console.WriteLine("____________________________________________________________");
        Console.WriteLine("Results Problem 8-10 (a)");

        // 8-10
        #region MyRegion for Write a robust function that reads data from a file and processes it line by line. If a line causes an exception, log the error and continue processing the remaining lines. Ensure that no line is skipped due to errors.

        // Get the path to the bin directory
        string binDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(binDirectory, "data.txt");

        // Content for the file
        string[] lines = {
            "This is the first line of the file.",
            "This is the second line of the file.",
            "This is the third line of the file."
        };

        try
        {
            // Write content to the file
            File.WriteAllLines(filePath, lines);
            Console.WriteLine($"File 'data.txt' created successfully at: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating the file: {ex.Message}");
            return;
        }

        try
        {
            // Read and process the file line by line
            foreach (var line in File.ReadLines(filePath))
            {
                try
                {
                    // Process line
                    Console.WriteLine($"Processing line: {line}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing line: {line}. Error: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        #endregion


        Console.WriteLine("____________________________________________________________");
        Console.WriteLine("Results Problem 8-10 (b)");

        #region MyRegion for Write an asynchronous method that fetches data from two APIs simultaneously and combines their results. Use HttpClient for the API calls.

        string api1 = "https://kimiquotes.pages.dev/api/quote";
        string api2 = "https://kimiquotes.pages.dev/api/quote";

        using HttpClient client = new HttpClient();

        try
        {
            // Start fetching data from both APIs simultaneously
            Task<string> task1 = client.GetStringAsync(api1);
            Task<string> task2 = client.GetStringAsync(api2);

            // Wait for both tasks to complete
            await Task.WhenAll(task1, task2);

            // Retrieve the results
            string result1 = await task1;
            string result2 = await task2;

            // Parse JSON into objects using Newtonsoft.Json
            var data1 = JsonConvert.DeserializeObject<dynamic>(result1);
            var data2 = JsonConvert.DeserializeObject<dynamic>(result2);

            // Combine and display the results
            Console.WriteLine("Combined Results:");
            Console.WriteLine($"API 1 quote: {data1.quote}");
            Console.WriteLine($"API 2 quote: {data2.quote}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        #endregion
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

}
