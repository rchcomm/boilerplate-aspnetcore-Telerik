using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class TB_Folder_Meta
    {
        public long FileID { get; set; }
        public DateTime CreationDate { get; set; }
        public long FolderID { get; set; }
        public bool IsFlage { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
    }
}
