using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class SyncSettings
    {
        public SyncSettings() { }

        public String EmailDomain { get; set; }
        
        /// <summary> Guid типа записи информационного регистра котора хронит eMail </summary>
        public Guid OneCGuidOfEmailContactInfo { get; set; }
        public Guid OneCGuidOfPhoneContactInfo { get; set; }
        public Guid OneCGuidOfWorkPhoneContactInfo { get; set; }

        /// <summary> Guid записи Справочника Тип контакта со значением "Сотрудник" </summary>
        public Guid CreatioGuidOfContactsWithTypeOurEmployees { get; set; }

        public Dictionary<String, Guid> MapGenderInOneC_GenderGuidInCreatio = new Dictionary<string, Guid>();

        public Dictionary<String, Guid> MapEmploymentTypeEnumInOneS_EmploymentTypeGuidInCreatio = new Dictionary<string, Guid>();

        public static SyncSettings GetDefaultSettings()
        {
            SyncSettings settings = new SyncSettings()
            {
                EmailDomain = "@kustoagro.com",
                OneCGuidOfEmailContactInfo = new Guid("6b1ae98e-bb91-11ea-80c7-00155d65b747"),
                OneCGuidOfPhoneContactInfo = new Guid("f1862c22-bb94-11ea-80c7-00155d65b747"),
                OneCGuidOfWorkPhoneContactInfo = new Guid("08188400-bb94-11ea-80c7-00155d65b747"),
                CreatioGuidOfContactsWithTypeOurEmployees = new Guid("60733efc-f36b-1410-a883-16d83cab0980"),
            };

            settings.MapGenderInOneC_GenderGuidInCreatio.Add("Мужской", new Guid("eeac42ee-65b6-df11-831a-001d60e938c6"));
            settings.MapGenderInOneC_GenderGuidInCreatio.Add("Женский", new Guid("fc2483f8-65b6-df11-831a-001d60e938c6"));
            //
            settings.MapEmploymentTypeEnumInOneS_EmploymentTypeGuidInCreatio.Add("ОсновноеМестоРаботы", new Guid("13BF7A1E-89D2-4888-BC9D-A831EC597FAE"));
            settings.MapEmploymentTypeEnumInOneS_EmploymentTypeGuidInCreatio.Add("ВнутреннееСовместительство", new Guid("5195D27F-F8B3-4872-B992-A2729135EF7E"));
            settings.MapEmploymentTypeEnumInOneS_EmploymentTypeGuidInCreatio.Add("Совместительство", new Guid("2362FD46-0EBB-4210-9872-086B716648CD"));

            return settings;
        }
    }
}
