using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.App.Models.Dto
{
    public class FileUploadInfoDto
    {
        public long Id { get; set; }
        public long FileId { get; set; }
        public string Uuid { get; set; }
    }
}
