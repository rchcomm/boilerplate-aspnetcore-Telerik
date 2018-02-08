using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class TB_Folder_Info
    {
        public TB_Folder_Info()
        {
            TB_File_Info = new HashSet<TB_File_Info>();
        }

        public long ID { get; set; }
        public DateTime CreationDate { get; set; }
        public int FileCount { get; set; }
        public bool IsFlag { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public long ParentID { get; set; }
        public long Size { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string UserID { get; set; }

        public ICollection<TB_File_Info> TB_File_Info { get; set; }
    }
}
