using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using Matternet;

namespace PowerMost
{
    [Cmdlet(VerbsCommon.Set, "MMUserPhoto")]
    public class SetMMUserPhoto : BaseClass
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string User { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public string PathToPicture { get; set; }


        protected override void ProcessRecord()
        {
            api.SetUserPhoto(User, PathToPicture); 
        }

    }
}
