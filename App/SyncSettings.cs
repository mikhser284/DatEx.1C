using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class SyncSettings
    {
        public SyncSettings() { }

        public String EmailDomain { get; set; }
        
        /// <summary> Guid типа записи информационного регистра которая хронит eMail </summary>
        public Guid OneCGuidOfEmailContactInfo { get; set; }

        /// <summary> Guid типа записи информационного регистра которая хронит телефон (по умолчанию: F1862C22-BB94-11EA-80C7-00155D65B747) </summary>
        public Guid OneCGuidOfPhoneContactInfo { get; set; }

        /// <summary> Guid типа записи информационного регистра которая хронит рабочий телефон (по умолчанию: 08188400-BB94-11EA-80C7-00155D65B747) </summary>
        public Guid OneCGuidOfWorkPhoneContactInfo { get; set; }

        /// <summary> Guid записи Справочника Тип контакта со значением "Сотрудник" (по умолчанию: 60733EFC-F36B-1410-A883-16D83CAB0980) </summary>
        public Guid CreatioGuidOfContactsWithTypeOurEmployees { get; set; }

        /// <summary> Guid записи Справочника Тип контрагента контакта со значением "Наша компания" (по умолчанию: 57412FAD-53E6-DF11-971B-001D60E938C6) </summary>
        public Guid CreatioGuidOfOurCompany { get; set; }

        /// <summary> Guid записи Справочника Форма собственности контрагента контакта со значением "ТОВ" (по умолчанию: 54441A90-B515-4616-9390-2C1FEE7F3428) </summary>
        public Guid CreatioGuidOfLLCOwnershipType { get; set; }

        public Dictionary<String, Guid> Map_OneSEnum_Gender_CreatioGuidOf_Gender = new Dictionary<string, Guid>();

        public Dictionary<String, Guid> Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType = new Dictionary<string, Guid>();

        public Dictionary<Boolean, Guid> Map_OneSEnum_LegalStatus_CreatioGuidOf_ITISCounterpartyLegalStatus = new Dictionary<Boolean, Guid>();

        public static SyncSettings GetDefaultSettings()
        {
            SyncSettings settings = new SyncSettings()
            {
                EmailDomain = "@kustoagro.com",
                OneCGuidOfEmailContactInfo = new Guid("6B1AE98E-BB91-11EA-80C7-00155D65B747"),
                OneCGuidOfPhoneContactInfo = new Guid("F1862C22-BB94-11EA-80C7-00155D65B747"),
                OneCGuidOfWorkPhoneContactInfo = new Guid("08188400-BB94-11EA-80C7-00155D65B747"),
                CreatioGuidOfContactsWithTypeOurEmployees = new Guid("60733EFC-F36B-1410-A883-16D83CAB0980"),
                CreatioGuidOfOurCompany = new Guid("57412FAD-53E6-DF11-971B-001D60E938C6"),
                CreatioGuidOfLLCOwnershipType = new Guid("54441A90-B515-4616-9390-2C1FEE7F3428"),
            };

            settings.Map_OneSEnum_Gender_CreatioGuidOf_Gender.Add("Мужской", new Guid("EEAC42EE-65B6-DF11-831A-001D60E938C6"));
            settings.Map_OneSEnum_Gender_CreatioGuidOf_Gender.Add("Женский", new Guid("FC2483F8-65B6-DF11-831A-001D60E938C6"));
            //
            settings.Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType.Add("ОсновноеМестоРаботы", new Guid("13BF7A1E-89D2-4888-BC9D-A831EC597FAE"));
            settings.Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType.Add("ВнутреннееСовместительство", new Guid("5195D27F-F8B3-4872-B992-A2729135EF7E"));
            settings.Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType.Add("Совместительство", new Guid("2362FD46-0EBB-4210-9872-086B716648CD"));
            //
            settings.Map_OneSEnum_LegalStatus_CreatioGuidOf_ITISCounterpartyLegalStatus.Add(true, new Guid("64B85345-9745-4BEE-8D1E-3D10E49BF7E6")); // Резидент
            settings.Map_OneSEnum_LegalStatus_CreatioGuidOf_ITISCounterpartyLegalStatus.Add(false, new Guid("2BDAFE9B-92FB-4A18-8EB4-65F604E35D8F")); // Не резидент
            return settings;
        }
    }
}
