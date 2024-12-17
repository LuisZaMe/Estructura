using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class StudyPictures
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public Media Media1 { get; set; }
        public Media Media2 { get; set; }
        public Media Media3 { get; set; }
        public Media Media4 { get; set; }
        public Media Media5 { get; set; }
        public Media Media6 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DateTime DeletedAt { get; set; }

        public void ForEach(Action<object> value)
        {
            throw new NotImplementedException();
        }
    }
}
