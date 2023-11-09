using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Software.DTO
{
    public class UpdateDeviceSoftwareDto
    {
        public int DeviceId { get; set; }
        public int SoftwareId { get; set; }
        public int? LicenseId { get; set; }
        public DateTime InstallDate { get; set; }
        public int Status { get; set; }
    }
}
