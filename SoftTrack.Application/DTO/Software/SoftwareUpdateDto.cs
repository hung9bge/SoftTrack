﻿using SoftTrack.Domain;

namespace SoftTrack.Manage.DTO
{
    public class SoftwareUpdateDto
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Os { get; set; }
        public int Status { get; set; }
    }
}
