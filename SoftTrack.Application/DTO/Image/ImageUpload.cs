﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Manage.DTO
{
    public class ImageUpload
    {
        public int ReportId { get; set; }
        public string Image1 { get; set; }
        public IFormFile Images { get; set; }
    }
}
