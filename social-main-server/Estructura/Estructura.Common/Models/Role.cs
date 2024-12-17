using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public string RoleDescription { get; set; }
        public Common.Enums.Role ParentRole { get; set; }
    }
}
