using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Estructura.Common.Request;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Estructura.Test
{
    [TestClass]
    public class TestBase
    {
        protected string BaseAddress = "https://localhost:5001/api/";
        public bool UseAuthenticatedAPI = true; //Change to false if a anonimous call is needed (like login o reauth)
        protected string AuthToken;
        protected string RefreshToken;
        private Client.ApiClient _apiClient;
        public Client.ApiClient ApiClient
        {
            get
            {
                _apiClient = UseAuthenticatedAPI && !string.IsNullOrWhiteSpace(AuthToken) ? new Client.ApiClient(AuthToken, BaseAddress, RefreshToken) : new Client.ApiClient(BaseAddress);
                return _apiClient;
            }
            set { _apiClient = value; }
        }

        private IWebHost VServer { get; set; }

        //Request auth token for the current session, will not set the environment untill it get required
        [TestInitialize]
        public async Task Init()
        {
            var loginRequest = new LoginRequest()
            {
                AppBuildVersion = TestSettings.CompatibilityVersion.Compatible,
                Password = TestSettings.User.Password,
                Email = TestSettings.User.EmailAddress
                //Email = "yasido@hotmail.com"
                //Email = "analista@email.com" // Analista
                //Email = "hector.rodriguez@beesoftware.mx"
                //Email = "analista2@email.com"
            };
            var login = await ApiClient.AuthService.LoginAsync(loginRequest);
            AuthToken = login.Token;
            RefreshToken = login.RefreshToken;
        }
        public TestBase()
        {
            VServer = new WebHostBuilder()
           .UseKestrel()//base kestrel
           .UseContentRoot(Directory.GetCurrentDirectory())
           .UseConfiguration(new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.test.json", optional: true)
               .Build())
           //.UseIISIntegration()//support IIS
           .UseStartup<Estructura.Startup>()
           .Build();
            Task.Run(() => VServer.Run());
        }
    }
}
