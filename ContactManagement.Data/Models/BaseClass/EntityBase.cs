using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Data.Models.BaseClass
{
     public class EntityBase
    {
        public int UniqueId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
