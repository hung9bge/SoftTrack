using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Manage.DTO
{
    public class LibraryCreateDto
    {

        public int AppId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string LibraryKey { get; set; }
        public string Start_Date { get; set; }
        public int Time { get; set; }
        public int Status { get; set; }

    }
}
