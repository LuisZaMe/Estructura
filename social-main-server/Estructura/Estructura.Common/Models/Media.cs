using Estructura.Common.Enums;
using System;

namespace Estructura.Common.Models
{
    public class Media
    {
        public long Id { get; set; }
        public string ImageName { get; set; }
        public string ImageRoute { get; set; }
        public string MediaURL { get; set; }
        public StoreMediaType StoreMediaType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Base64Image { get; set; }
    }
}
