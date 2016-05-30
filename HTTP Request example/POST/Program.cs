using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace POST
{
    class Program
    {
        readonly static string url = "http://localhost:20876"; // Should be in a config file
        static void Main(string[] args)
        {
            //Register
            //Register();

            //Get Token
            Token token = Login().Result;
            Console.WriteLine(string.Format("Got acces_token: {0} {1}", token.token_type, token.access_token));

            //HTTP GET
            Console.WriteLine(Get(token).Result);

            //Logout
            Console.WriteLine(Logout(token).Result);

            Console.Read();

        }

        static async Task<Token> Login()
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", "test@test.nl" },
                    { "password", "Test123!" }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(string.Format("{0}/Token", url), content);

                var responseString = await response.Content.ReadAsStringAsync();

                Token token = JsonConvert.DeserializeObject<Token>(responseString);


                return token;
            }
        }
        static async Task<string> Logout(Token token)
        {
            // Create a client
            HttpClient httpClient = new HttpClient();

            // Add a new Request Message
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, string.Format("{0}/api/Account/Logout", url));

            // Add our custom headers
            requestMessage.Headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            // Send the request to the server
            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

            // Just as an example I'm turning the response into a string here
            string responseAsString = await response.Content.ReadAsStringAsync();

            //client.DefaultRequestHeaders.Add("Authorization", access_token);
            //var responseString = await client.GetStringAsync("http://localhost:8560/api/Account/UserInfo");
            return responseAsString;
        }
        static async Task<string> Register()
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "Email", "test@test.nl" },
                    { "Password", "Test123!" },
                    { "ConfirmPassword", "Test123!" }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(string.Format("{0}/api/Account/Register", url), content);

                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
        }
        static async Task<string> Get(Token token)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine(string.Format("Sending get with access_token: {0} {1}", token.token_type, token.access_token));
                // Create a client
                HttpClient httpClient = new HttpClient();

                // Add a new Request Message
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}/api/Account/UserInfo", url));

                // Add our custom headers
                requestMessage.Headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

                // Send the request to the server
                HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

                // Just as an example I'm turning the response into a string here
                string responseAsString = await response.Content.ReadAsStringAsync();

                //client.DefaultRequestHeaders.Add("Authorization", access_token);
                //var responseString = await client.GetStringAsync("http://localhost:8560/api/Account/UserInfo");
                return responseAsString;
            }
        }
    }

    class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        public DateTime issued { get; set; }
        public DateTime expires { get; set; }
    }
}
