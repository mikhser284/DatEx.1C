using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class SyncSettings
    {
        public SyncSettings() { }

        public String EmailDomain { get; set; }
        public Guid OneCGuidOfEmailContactInfo { get; set; }
        public Guid OneCGuidOfPhoneContactInfo { get; set; }
        public Guid OneCGuidOfWorkPhoneContactInfo { get; set; }
        public Guid CreatioGuidOfContactsWithTypeOurEmployees { get; set; }

        public static SyncSettings GetDefaultSettings()
        {
            return new SyncSettings()
            {
                EmailDomain = "@kustoagro.com",
                OneCGuidOfEmailContactInfo = new Guid("6b1ae98e-bb91-11ea-80c7-00155d65b747"),
                OneCGuidOfPhoneContactInfo = new Guid("f1862c22-bb94-11ea-80c7-00155d65b747"),
                OneCGuidOfWorkPhoneContactInfo = new Guid("08188400-bb94-11ea-80c7-00155d65b747"),
                CreatioGuidOfContactsWithTypeOurEmployees = new Guid("60733efc-f36b-1410-a883-16d83cab0980"),
            };
        }
    }
}
