using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoUseWebApi
{
    [Serializable]
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Book()
        {

        }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(Book product)
        {
            Console.WriteLine($"Name: {product.Name}");
        }

        static async Task<Uri> CreateProductAsync(Book product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/values", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        //static async Task<Book> GetProductAsync(string path)
        //{
        //    Book product = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<Book>();
        //    }
        //    return product;
        //}

        static async Task<Book> UpdateProductAsync(Book product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/values/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Book>();
            return product;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/values/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:64978/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var product=await CreateProductAsync(new Book {Id=1,Name="1234444" });
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
