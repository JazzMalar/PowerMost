using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using PowerMost.Model;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Add, "MMTeam")]
    public class AddMMTeam : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string Team { get; set; }
        [Parameter(Mandatory = true, Position = 2)]
        public string Displayname { get; set; }
        [Parameter(Mandatory = true, Position = 3)]
        public MMTeam.TeamType TeamType { get; set; }

        protected override void ProcessRecord()
        {
            api.CreateTeam(Team, Displayname, TeamType); 
        }

    }
}
