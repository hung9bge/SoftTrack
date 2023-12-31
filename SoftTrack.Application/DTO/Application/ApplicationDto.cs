﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Manage.DTO
{
    public class ApplicationDto
    {
        public int AppId { get; set; }
        public int AccId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Type { get; set; }
        public string Os { get; set; }
        public string Osversion { get; set; }
        public string Description { get; set; }
        public string Download { get; set; }
        public string Docs { get; set; }
        public string Language { get; set; }
        public string Db { get; set; }
        public int? Status { get; set; }

    }
}
