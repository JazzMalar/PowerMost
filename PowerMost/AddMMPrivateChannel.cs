using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Matternet;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Add, "MMPrivateChannel")]
    public class AddMMPrivateChannel : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string Team { get; set; }
        
        [Parameter(Mandatory = true, Position = 2)]
        public string Channel { get; set; }

        [Parameter(Mandatory = true, Position = 3)]
        public string Displayname { get; set; }

        protected override void ProcessRecord()
        {
            api.CreatePrivateChannel(Team, Channel, Displayname); 
        }

    }
}
