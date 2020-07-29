using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx._1C
{
    public class SettingsForClientOf1C
    {
        public String ServiceBaseAddress { get; private set; }
        public String AgentLogin { get; private set; }
        public String AgentPassword { get; private set; }

        public SettingsForClientOf1C(String serviceBaseAddress, String agentLogin, String agentPassword)
        {
            ServiceBaseAddress = serviceBaseAddress;
            AgentLogin = agentLogin;
            AgentPassword = agentPassword;
        }


    }
}
