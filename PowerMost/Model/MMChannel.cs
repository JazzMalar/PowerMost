using System;
using System.Collections.Generic;
using System.Text;

namespace PowerMost.Model
{
    public class MMChannel
    {

        public enum ChannelType
        {
            O,
            P
        }

        public string id { get; set; }
        public Int64 create_at { get; set; }
        public Int64 update_at { get; set; }
        public Int64 delete_at { get; set; }
        public string team_id { get; set; }
        public string type {get; set;}
        public string display_name { get; set; }
        public string name { get; set; }
        public string header { get; set; }
        public string purpose { get; set; }
        public Int64 last_post_at { get; set; }
        public int total_msg_count { get; set; }
        public Int64 extra_update_at { get; set; }
        public string creator_id { get; set; }

    }
}
