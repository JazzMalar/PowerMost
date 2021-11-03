using System;
using System.Collections.Generic;
using System.Text;

namespace Matternet
{
    public class MMTeam
    {

        public enum TeamType
        {
            O,
            I
        }
        public string id { get; set; }
        public Int64 create_at { get; set; }
        public Int64 update_at { get; set; }
        public Int64 delete_at { get; set; }
        public string display_name { get; set; }
        public string name {get; set;}
        public string description { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public bool allow_open_invite { get; set; }
        public string allowed_domains { get; set; }
        public string invite_id { get; set; }
    }
}
