using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Add, "MMUserToTeam")]
    public class AddMMUserToTeam : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string User { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public string Team { get; set; }

        protected override void ProcessRecord()
        {
            api.AddUserToTeam(User, Team); 
        }

    }
}
