using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;

namespace PowerMost
{
    [Cmdlet(VerbsLifecycle.Enable, "MMUser")]
    public class EnableMMUser : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string username { get; set; }


        protected override void ProcessRecord()
        {
            api.EnableUser(username); 
        }

    }
}
