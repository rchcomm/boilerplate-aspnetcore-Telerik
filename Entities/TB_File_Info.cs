using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class TB_File_Info
    {
        public long ID { get; set; }
        public string ContentType { get; set; }
        public DateTime CreationDate { get; set; }
        public long FolderID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public int Type { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string UserID { get; set; }

        public TB_Folder_Info Folder { get; set; }
    }
}
