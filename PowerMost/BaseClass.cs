using System;
using System.Management.Automation;
using Matternet; 

namespace PowerMost
{
    public class BaseClass : PSCmdlet
    {
        protected MMApiWrapper api; 

        [Parameter(Mandatory = true)]
        public String BaseUrl { get; set; }

        [Parameter(ParameterSetName="SessionToken", Mandatory = true)]
        public String ApiUser { get; set; }

        [Parameter(ParameterSetName = "SessionToken", Mandatory = true)]
        public String ApiPass { get; set; }

        [Parameter(ParameterSetName = "UserAccessToken", Mandatory = true)]
        public String AccessToken { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (ParameterSetName.Equals("UserAccessToken"))
            {
                api = new MMApiWrapper(BaseUrl, AccessToken);
            } 
            else
            {
                api = new MMApiWrapper(BaseUrl, ApiUser, ApiPass); 
            }

        }

    }
}
