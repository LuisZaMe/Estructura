using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Estructura.Common.Models;
using Estructura.Core.Models;
using System.Data.SqlClient;
using System.Linq;

namespace Estructura.DAL.Repositories
{
    public class BaseRepository
    {
        protected readonly Core.ConfigurationReflection.APIConfig Config;
        public readonly Core.Models.AppUser AppUser;

        public BaseRepository(IOptions<Core.ConfigurationReflection.APIConfig> options, IHttpContextAccessor httpAccesor)
        {
            Config = options.Value;
            //Get user context
            AppUser = new Core.Models.AppUser(httpAccesor.HttpContext.User);

            //Get environment by header, must send Environment variable as "Environment", the value is a index that references the index on the config section
            //Microsoft.Extensions.Primitives.StringValues environment = "";
            //int environmentIndex = -1;

            //if (httpAccesor.HttpContext.Request.Headers.TryGetValue("Environment", out environment) && !string.IsNullOrWhiteSpace(environment))
            //    environmentIndex = int.Parse(environment);

            //if (httpAccesor.HttpContext.User.Identity.IsAuthenticated && AppUser != null) //Always priorise token environment index over header token
            //    environmentIndex = AppUser.EnvironmentIndex;

            //            if (environmentIndex < 0)
            //            {
            //#if DEBUG
            //                environmentIndex = 0;
            //#else
            //                throw new ArgumentException("No session & no environment provided");
            //#endif
            //            }

            AuthenticationEnvironment = GetEnvironment();
        }

        private Core.ConfigurationReflection.Environment _authenticationEnvironment;
        private Core.ConfigurationReflection.Environment AuthenticationEnvironment
        {
            get
            {
                return _authenticationEnvironment;
            }
            set { _authenticationEnvironment = value; }
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(AuthenticationEnvironment.Connection);
            }
        }

        private Core.ConfigurationReflection.Environment GetEnvironment()
        {
            return Config.Environment;
        }

        public Core.ConfigurationReflection.Environment GetCurrentEnvironment()
        {
            return AuthenticationEnvironment;
        }

        public AppUser GetAppUser()
        {
            return this.AppUser;
        }
    }
}
