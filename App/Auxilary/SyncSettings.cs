using App.Auxilary;
using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    [Doc("Объект настроек синхнонизации между системами Creatio и 1C")]
    public class SyncSettings
    {
        public SyncSettings() { }

        [Doc("Email домен"
            , "К какому домену должен принадлежать email пользователя, чтобы он считался сотрудником нашей компании. \nНапример '@kustoagro.com'")]
        public String EmailDomain { get; set; }
        
        [Doc("[1С → Catalog_ВидыКонтактнойИнформации] Guid вида контактной информации об eMail пользователя"
            , "Guid (в 1С) типа записи информационного регистра Catalog_ВидыКонтактнойИнформации которая хронит сведения об eMail пользователя. \n(Для 1С, котора развернута на тестовом сервере Creatio-Dev1 это 6B1AE98E-BB91-11EA-80C7-00155D65B747)")]
        public Guid OneSGuidOfEmailContactInfo { get; set; }

        [Doc("[1С → Catalog_ВидыКонтактнойИнформации] Guid вида контактной информации об телефоне пользователя"
            , "Guid (в 1С) типа записи информационного регистра Catalog_ВидыКонтактнойИнформации которая хронит сведения об телефоне пользователя. \n(Для 1С, котора развернута на тестовом сервере Creatio-Dev1 это F1862C22-BB94-11EA-80C7-00155D65B747)")]
        public Guid OneSGuidOfPhoneContactInfo { get; set; }

        [Doc("[1С → Catalog_ВидыКонтактнойИнформации] Guid вида контактной информации об рабочем телефоне пользователя"
            , "Guid (в 1С) типа записи информационного регистра Catalog_ВидыКонтактнойИнформации которая хронит сведения об рабочем телефоне пользователя. \n(Для 1С, котора развернута на тестовом сервере Creatio-Dev1 это 08188400-BB94-11EA-80C7-00155D65B747)")]
        public Guid OneSGuidOfWorkPhoneContactInfo { get; set; }

        [Doc("[1С → Catalog_Номенклатура] Guid конревой папки номенклатуры"
            , "Guid (в 1С) корневой папки \"Новая номенклатура\" (код: 99016918) с номенклатурой которая подлежит синхронизации \n(для 1С, котора развернута на тестовом сервере Creatio-Dev1 это 491f383d-e148-11e5-80bf-00155dc80407)")]
        public Guid OneSGuidOfNomenclatureRootFolder { get; set; }

        [Doc("[Creatio → Тип контакта] Guid записи \"Cотрудник\""
            , "Guid (в Creatio) записи cправочника \"Тип контакта\" со значением \"Сотрудник\" (для Creatio, которая развернута на тестовом сервере Creatio-Dev1 это 60733EFC-F36B-1410-A883-16D83CAB0980)")]
        public Guid CreatioGuidOfContactsWithTypeOurEmployees { get; set; }

        [Doc("[Creatio → Тип контрагента] Guid записи \"Наша компания\""
            , "Guid (в Creatio) записи cправочника \"Тип контрагента\" со значением \"Наша компания\" (для Creatio, которая развернута на тестовом сервере Creatio-Dev1 это 57412FAD-53E6-DF11-971B-001D60E938C6)")]
        public Guid CreatioGuidOfOurCompany { get; set; }

        [Doc("[Creatio → Форма собственности] Guid записи \"ТОВ\""
            , "Guid записи cправочника \"Форма собственности\" со значением \"ТОВ\" (для Creatio, которая развернута на тестовом сервере Creatio-Dev1 это 54441A90-B515-4616-9390-2C1FEE7F3428)")]
        public Guid CreatioGuidOfLLCOwnershipType { get; set; }

        [Doc("[Creatio → Номенклатура] Guid ответственного по умолчанию (Supervisor)"
            , "Guid записи cправочника \"Контакты\" который являеться ответственным за елемент номенклатуры по умолчанию (для Creatio, которая развернута на тестовом сервере Creatio-Dev1 это 410006e1-ca4e-4502-a9ec-e54d922d2c00 — Supervisor)")]
        public Guid CreatioGuidOfNomenclatureItemOwnerByDefault { get; set; }

        Guid ownerId = new Guid("410006e1-ca4e-4502-a9ec-e54d922d2c00"); // Supervisor

        [Doc("[Creatio → Пол] Проекция"
            , "Проекция Перечисления \"ПолФизическихЛиц\" (1С) на записи справочника \"Пол\" (Creatio)")]
        public Dictionary<String, Guid> Map_OneSEnum_Gender_CreatioGuidOf_Gender { get; set; } = new Dictionary<string, Guid>();

        [Doc("[Creatio → Вид занятости] Проекция"
            , "Проекция Перечисления \"ВидыЗанятостиВОрганизации\" (1С) на записи справочника \"Вид занятости\" (Creatio)")]
        public Dictionary<String, Guid> Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType { get; set; } = new Dictionary<string, Guid>();

        [Doc("[Creatio → Юридический статус контрагента] Проекция"
            , "Проекция булевого значения \"НеЯвляетсяРезидентом\" справочника \"Контрагент\" (1С) на записи справочника \"Юридический статус контрагента\" (Creatio)")]
        public Dictionary<Boolean, Guid> Map_OneSEnum_LegalStatus_CreatioGuidOf_ITISCounterpartyLegalStatus { get; set; } = new Dictionary<Boolean, Guid>();

        public static SyncSettings GetDefaultSettings()
        {
            SyncSettings settings = new SyncSettings()
            {
                EmailDomain = "@kustoagro.com",
                OneSGuidOfEmailContactInfo = new Guid("6B1AE98E-BB91-11EA-80C7-00155D65B747"),
                OneSGuidOfPhoneContactInfo = new Guid("F1862C22-BB94-11EA-80C7-00155D65B747"),
                OneSGuidOfWorkPhoneContactInfo = new Guid("08188400-BB94-11EA-80C7-00155D65B747"),
                OneSGuidOfNomenclatureRootFolder = new Guid("491f383d-e148-11e5-80bf-00155dc80407"),
                CreatioGuidOfContactsWithTypeOurEmployees = new Guid("60733EFC-F36B-1410-A883-16D83CAB0980"),
                CreatioGuidOfNomenclatureItemOwnerByDefault = new Guid("410006e1-ca4e-4502-a9ec-e54d922d2c00"),
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
