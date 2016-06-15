using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryMultiScan
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public int PageViews { get; set; }
    }
        
    public class Post
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public int ViewCount { get; set; }
    }
}
