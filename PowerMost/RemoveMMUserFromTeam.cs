using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Matternet;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Remove, "MMUserFromTeam")]
    public class RemoveMMUserFromTeam : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string User { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public string Team { get; set; }

        protected override void ProcessRecord()
        {
            api.RemoveUserFromTeam(User, Team); 
        }

    }
}
