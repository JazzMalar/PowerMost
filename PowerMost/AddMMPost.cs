using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Add, "MMPost")]
    public class AddMMPost : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string Team { get; set; }
        [Parameter(Mandatory = true, Position = 2)]
        public string Channel { get; set; }
        [Parameter(Mandatory = true, Position = 3)]
        public string Message { get; set; }

        protected override void ProcessRecord()
        {
            api.PostToChannel(Message, Team, Channel);
        }

    }
}
