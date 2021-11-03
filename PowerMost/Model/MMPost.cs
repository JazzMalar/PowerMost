using System;
using System.Collections.Generic;
using System.Text;

namespace Matternet
{
    public class MMPost
    {
        public string id { get; set; }
        public Int64 create_at { get; set; }
        public Int64 update_at { get; set; }
        public Int64 delete_at { get; set; }
        public Int64 edit_at { get; set; }
        public string user_id { get; set; }
        public string channel_id {get; set;}
        public string root_id { get; set; }
        public string parent_id { get; set; }
        public string original_id { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public string hashtag { get; set; }
        public string pending_post_id { get; set; }
    }
}
