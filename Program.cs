using System.Text.Json;

namespace MyRestApi 
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
           await Program.getSingleUser(sharedClient);

        }
        // adding shared base address for all api requests
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://reqres.in/api/"),
        };

        // get request to get Single User 
        static async Task getSingleUser(HttpClient httpClient)
        {
            int u_id = 2    ;
            String url = $"users/{u_id}";
            using HttpResponseMessage response = await httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);

            if(response.IsSuccessStatusCode) { 
            JsonElement root = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            
            var user = root.GetProperty("data");
            var Id = user.GetProperty("id");
            var firstName = user.GetProperty("first_name");
            var lastName = user.GetProperty("last_name");
            var email = user.GetProperty("email");
            var avatar = user.GetProperty("avatar");

            Console.WriteLine($"Id: {Id}\nName: {firstName} {lastName}\nEmail:{email}");
            Console.ReadLine();
            }
            else
            {
                Console.WriteLine("User Not Found: Enter Valid User");
            }
        }

    }
}