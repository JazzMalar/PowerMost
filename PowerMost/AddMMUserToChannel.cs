using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Add, "MMUserToChannel")]
    public class AddMMUserToChannel : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string User { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public string Team { get; set; }

        [Parameter(Mandatory = true, Position = 3)]
        public string Channel { get; set; }

        protected override void ProcessRecord()
        {
            api.AddUserToChannel(User, Team, Channel); 
        }

    }
}
