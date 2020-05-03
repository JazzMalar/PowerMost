using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Matternet;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Add, "MMUser")]
    public class AddMMUser : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string username { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public string email { get; set; }
        
        [Parameter(Mandatory = true, Position = 3)]
        public string password { get; set; }

        [Parameter(Mandatory = true, Position = 4)]
        public string firstname { get; set; }

        [Parameter(Mandatory = true, Position = 5)]
        public string lastname { get; set; }

        protected override void ProcessRecord()
        {
            api.CreateEmailUser(email, username, password, firstname, lastname); 
        }

    }
}
