using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.OneC
{
    public class HttpClientOfOneCSettings
    {
        public String ServiceBaseAddress { get; private set; }
        public String AgentLogin { get; private set; }
        public String AgentPassword { get; private set; }

        public String Domain { get; set; }
        public Guid GuidOfEmailContactInfo { get; set; }
        public Guid GuidOfPhoneContactInfo { get; set; }
        public Guid GuidOfWorkPhoneContactInfo { get; set; }

        public HttpClientOfOneCSettings(String serviceBaseAddress, String agentLogin, String agentPassword)
        {
            ServiceBaseAddress = serviceBaseAddress;
            AgentLogin = agentLogin;
            AgentPassword = agentPassword;
        }

        public static HttpClientOfOneCSettings GetDefaultSettings(String serviceBaseAddress, String agentLogin, String agentPassword)
        {
            return new HttpClientOfOneCSettings(serviceBaseAddress, agentLogin, agentPassword)
            {
                Domain = "@kustoagro.com",
                GuidOfEmailContactInfo = new Guid("6b1ae98e-bb91-11ea-80c7-00155d65b747"),
                GuidOfPhoneContactInfo = new Guid("f1862c22-bb94-11ea-80c7-00155d65b747"),
                GuidOfWorkPhoneContactInfo = new Guid("08188400-bb94-11ea-80c7-00155d65b747")
            };
        }
    }
}
