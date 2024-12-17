using Estructura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public partial class Identity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public Enums.Role Role { get; set; }
        public bool IsActive { get; set; }
        public CompanyInformation CompanyInformation { get; set; }
        public long? UnderAdminUserId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public long? RoleId { get; set; }
    }
}
