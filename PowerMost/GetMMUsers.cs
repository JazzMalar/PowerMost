using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Get, "MMUsers")]
    public class GetMMUsers : BaseClass
    {
        [Parameter(Mandatory = false)]
        public string username { get; set; }

        [Parameter(Mandatory = false)]
        public string email { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter ActiveOnly { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter InactiveOnly { get; set; }


        protected override void ProcessRecord()
        {

            if (ActiveOnly)
            {
                WriteObject(api.GetUsers(true, false));
            }
            else if (InactiveOnly)
            {
                WriteObject(api.GetUsers(false, true));
            }
            else if (username != null)
            {
                WriteObject(api.GetUserByUserName(username));
            }
            else if (email != null)
            {
                WriteObject(api.GetUserByEMail(email));
            }
            else
            {
                WriteObject(api.GetUsers());
            }
        }

    }
}
