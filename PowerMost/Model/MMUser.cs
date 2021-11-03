using System;
using System.Collections.Generic;
using System.Text;

namespace Matternet
{
    public class MMUser
    {
        public string id { get; set; }
        public Int64 create_at { get; set; }
        public Int64 update_at { get; set; }
        public Int64 delete_at { get; set; }
        public string username { get; set; }
        public string first_name {get; set;}
        public string last_name { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string auth_service { get; set; }
        public string roles { get; set; }
        public string locale { get; set; }
        public Int64 last_password_update { get; set; }
        public Int64 last_picture_update { get; set; }
        public int failed_attempts { get; set; }
        public bool mfa_active { get; set; }
        public string password { get; set; }
        public Int64 terms_of_service_create_at { get; set; }

    }
}
