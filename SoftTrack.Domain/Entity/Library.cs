using System;
using System.Collections.Generic;

namespace SoftTrack.Domain  
{
    public partial class Library
    {
        public int LibraryId { get; set; }
        public int AppId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string LibraryKey { get; set; }
        public DateTime Start_Date { get; set; }
        public int Time { get; set; }
        public int Status { get; set; }

        public virtual Application App { get; set; }
    }
}
