using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Matternet;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Remove, "MMChannel")]
    public class RemoveMMChannel : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string Team { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public string Channel { get; set; }


        protected override void ProcessRecord()
        {
            api.DeleteChannel(Team, Channel); 
        }

    }
}
