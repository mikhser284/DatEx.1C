using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.OneS
{
    public class HttpClientOfOneCSettings
    {
        public String ServiceBaseAddress { get; private set; }
        public String AgentLogin { get; private set; }
        public String AgentPassword { get; private set; }

        public HttpClientOfOneCSettings(String serviceBaseAddress, String agentLogin, String agentPassword)
        {
            ServiceBaseAddress = serviceBaseAddress;
            AgentLogin = agentLogin;
            AgentPassword = agentPassword;
        }        
    }    
}
