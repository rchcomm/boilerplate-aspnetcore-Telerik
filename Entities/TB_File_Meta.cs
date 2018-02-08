using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class TB_File_Meta
    {
        public long FileID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public long LimitCount { get; set; }
        public long TotalCount { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
    }
}
