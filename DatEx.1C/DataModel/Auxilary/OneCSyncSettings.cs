using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.OneS.DataModel
{
    public class OneCSyncSettings
    {
        public String Domain { get; set; }
        public Guid GuidOfEmailContactInfo { get; set; }
        public Guid GuidOfPhoneContactInfo { get; set; }
        public Guid GuidOfWorkPhoneContactInfo { get; set; }

        public OneCSyncSettings() { }

        public static OneCSyncSettings GetDefaultSettings()
        {
            return new OneCSyncSettings
            {
                Domain = "@kustoagro.com",
                GuidOfEmailContactInfo = new Guid("6b1ae98e-bb91-11ea-80c7-00155d65b747"),
                GuidOfPhoneContactInfo = new Guid("f1862c22-bb94-11ea-80c7-00155d65b747"),
                GuidOfWorkPhoneContactInfo = new Guid("08188400-bb94-11ea-80c7-00155d65b747")
            };
        }
    }
}
