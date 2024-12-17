﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class SocioeconomicStudy
    {
        public long Id { get; set; }
        public long StudyId { get; set; }
        public bool IdentificationCard { get; set; }
        public bool AddressProof { get; set; }
        public bool BirthCertificate { get; set; }
        public bool CURP { get; set; }
        public bool StudiesProof { get; set; }
        public bool SocialSecurityNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}