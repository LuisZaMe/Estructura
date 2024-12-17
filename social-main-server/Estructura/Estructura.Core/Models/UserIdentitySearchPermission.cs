using Estructura.Common.Models;

namespace Estructura.Core.Models
{
    public class UserIdentitySearchPermission
    {
        public Identity Admin { get; set; }
        public bool FullSearch { get; set; }
        public string QueryFilter { get; set; }
        public long AdminId { get; set; }
    }
}
