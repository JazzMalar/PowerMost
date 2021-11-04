using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Remove, "MMUser")]
    public class RemoveMMUser : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string username { get; set; }

        protected override void ProcessRecord()
        {
            api.DeleteUser(username); 
        }

    }
}
