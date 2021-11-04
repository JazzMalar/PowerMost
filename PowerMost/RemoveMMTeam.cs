using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Remove, "MMTeam")]
    public class RemoveMMTeam : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string Team { get; set; }


        protected override void ProcessRecord()
        {
            api.DeleteTeam(Team); 
        }

    }
}
