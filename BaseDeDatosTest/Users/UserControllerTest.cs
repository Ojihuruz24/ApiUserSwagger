using BaseDeDatos;
using BaseDeDatos.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BaseDeDatosTest.Users
{

    public class UserControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client { get; }

        public UserControllerTest(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task AddAndModifyShould()
        {
            HttpResponseMessage getResponse = await GetsUsers();

            getResponse.EnsureSuccessStatusCode();

            HttpResponseMessage postResponse = await PostUser();


            postResponse.EnsureSuccessStatusCode();


            HttpResponseMessage putResponse = await PutUser();


            putResponse.EnsureSuccessStatusCode();

            HttpResponseMessage deleteResponse = await DeleteUser();

            deleteResponse.EnsureSuccessStatusCode();

        }

        private async Task<HttpResponseMessage> PostUser()
        {

            var user = new User
            {
                Identity = 18181818,
                FirtName = "Test Integration",
                LastName = "Fernando Agudelo",
                Cel = 15151515,
                Direction = "Calle 45 12-52",
                Email = "dayansd@hotmail.com"
            };


            StringContent userContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Users", userContent);
            return response;
        }

        private async Task<HttpResponseMessage> PutUser()
        {
            var user = new User
            {
                Identity = 18181818,
                FirtName = "Test Actualizacion",
                LastName = "actualizando",
                Cel = 303030,
                Direction = "Calle 22 12-52",
                Email = "ojihururuzzz@hotmail.com"
            };

            StringContent userContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var putResponse = await _client.PutAsync("api/Users/18181818", userContent);
            return putResponse;
        }


        private async Task<HttpResponseMessage> DeleteUser()
        {
            return await _client.DeleteAsync("/api/Users/18181818");
        }

        private async Task<HttpResponseMessage> GetsUsers()
        {
            return await _client.GetAsync("/api/Users");
        }
    }
}
