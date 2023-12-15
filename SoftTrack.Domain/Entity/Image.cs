using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int ReportId { get; set; }
        public string Image1 { get; set; }

        //public virtual Report Report { get; set; }
    }
}
