using Estructura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class Doccument
    {
        public long Id { get; set; }
        public string DoccumentName { get; set; }
        public string DoccumentRoute { get; set; }
        public string DoccumentURL { get; set; }
        public StoreMediaType StoreMediaType { get; set; }
        public StoreFileType StoreFileType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Base64Doccument { get; set; }
    }
}
