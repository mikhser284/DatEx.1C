using DatEx.Creatio.DataModel.Auxilary;

namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using DatEx.Creatio.DataModel.Terrasoft.Calendar;
    using Newtonsoft.Json;

    /// <summary> Контакт </summary>    
    [CreatioType("Контакт")]
    public class Contact : BaseEntity
    {
        [JsonIgnoreSerialization]
        [CreatioProp("Employee", "Сотрудник", Remarks ="Не существует в oData, создано для удобства")]
        public Employee Employee { get; set; }

        /// <summary> ФИО </summary>
        [MapRemarks("Отключить авто заполнение фамилии имени и отчества из свойства Name; Наоборот брать Name из них: this.Name = $\"{this.Surname} {c.GivenName} {c.MiddleName}\"")]
        [Map(true)]
        [CreatioProp("ФИО", Color = ConsoleColor.Yellow)]
        public String Name { get; set; }

        /// <summary> Ответственный (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Ответственный (Id)")]
        public Guid? OwnerId { get; set; }

        /// <summary> Ответственный </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Ответственный")]
        public Contact Owner { get; set; }

        /// <summary> Приветствие </summary>
        [CreatioProp("Приветствие")]
        public String Dear { get; set; }

        /// <summary> Обращение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Обращение (Id)")]
        public Guid? SalutationTypeId { get; set; }

        /// <summary> Обращение </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Обращение")]
        public ContactSalutationType SalutationType { get; set; }

        /// <summary> Пол (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Используя объект настроек синхронизации проецировать значение перечисления ПолФизическихЛиц из 1С на Guid Справочника<Пол>")]
        [Map(true, DataType.Lookup, "Catalog_ФизическиеЛица", DataType.Enum, "Пол(ПолФизическихЛиц)")]
        [CreatioProp("Guid", "Пол (Id)", Color = ConsoleColor.Blue)]
        public Guid? GenderId { get; set; }

        /// <summary> Пол </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Пол", Color = ConsoleColor.Yellow)]
        public Gender Gender { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Контрагентдолжен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Guid", "Контрагент (Id)", Color = ConsoleColor.Blue)]
        public Guid? AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Контрагент", Color = ConsoleColor.Yellow)]
        public Account Account { get; set; }

        /// <summary> Роль (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Роль (Id)")]
        public Guid? DecisionRoleId { get; set; }

        /// <summary> Роль </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Роль")]
        public ContactDecisionRole DecisionRole { get; set; }

        /// <summary> Тип (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Брать значение Guid из объекта настроек синхронизации (Guid записи Справочника Тип контакта со значением \"Сотрудник\")")]
        [Map(true)]
        [CreatioProp("Guid", "Тип (Id)", Color = ConsoleColor.Blue)]
        public Guid? TypeId { get; set; }

        /// <summary> Тип </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Тип", Color = ConsoleColor.Yellow)]
        public ContactType Type { get; set; }

        /// <summary> Должность (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Должность контакта должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Guid", "Должность (Id)", Color = ConsoleColor.Blue)]
        public Guid? JobId { get; set; }

        /// <summary> Должность </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Должность", Color = ConsoleColor.Yellow)]
        public Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        [MapRemarks("т.е. из связанного с Catalog_СотрудникиОрганизаций объекта Catalog_ДолжностиОрганизаций")]
        [Map(true, DataType.Lookup, "Catalog_ДолжностиОрганизаций", DataType.String, "Наименование")]
        [CreatioProp("Полное название должности", Color = ConsoleColor.Yellow)]
        public String JobTitle { get; set; }

        /// <summary> Департамент (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Департамент (Id)", Color = ConsoleColor.Blue)]
        public Guid? DepartmentId { get; set; }

        /// <summary> Департамент </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Департамент", Color = ConsoleColor.Yellow)]
        public Department Department { get; set; }

        /// <summary> Дата рождения </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [Map(true, DataType.Lookup, "Catalog_ФизическиеЛица", DataType.Date, "ДатаРождения")]
        [CreatioProp("Дата рождения", Color = ConsoleColor.Yellow)]
        public DateTime? BirthDate { get; set; }

        /// <summary> Рабочий телефон </summary>
        [MapRemarks("Для того чтобы получить нужную запись инфо. регистра с Рабочим телефоном используется объект настроек синхронизации")]
        [Map(true, DataType.InfoReg, "InformationRegister_КонтактнаяИнформация", DataType.String, "Представление")]
        [CreatioProp("Рабочий телефон", Color = ConsoleColor.Yellow)]
        public String Phone { get; set; }

        /// <summary> Мобильный телефон </summary>
        [MapRemarks("Для того чтобы получить нужную запись инфо. регистра с Телефоном используется объект настроек синхронизации")]
        [Map(true, DataType.InfoReg, "InformationRegister_КонтактнаяИнформация", DataType.String, "Представление")]
        [CreatioProp("Мобильный телефон", Color = ConsoleColor.Yellow)]
        public String MobilePhone { get; set; }

        /// <summary> Домашний телефон </summary>
        [CreatioProp("Домашний телефон")]
        public String HomePhone { get; set; }

        /// <summary> Skype </summary>
        [CreatioProp("Skype")]
        public String Skype { get; set; }

        /// <summary> Email </summary>
        [MapRemarks("Для того чтобы получить нужную запись инфо. регистра с Email используется объект настроек синхронизации")]
        [Map(true, DataType.InfoReg, "InformationRegister_КонтактнаяИнформация", DataType.String, "Представление")]
        [CreatioProp("Email", Color = ConsoleColor.Yellow)]
        public String Email { get; set; }

        /// <summary> Тип адреса (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Тип адреса (Id)")]
        public Guid? AddressTypeId { get; set; }

        /// <summary> Тип адреса </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Тип адреса")]
        public AddressType AddressType { get; set; }

        /// <summary> Адрес </summary>
        [CreatioProp("Адрес")]
        public String Address { get; set; }

        /// <summary> Город (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Город (Id)")]
        public Guid? CityId { get; set; }

        /// <summary> Город </summary>
        [CreatioProp("Город")]
        public City City { get; set; }

        /// <summary> Область/штат (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Область/штат (Id)")]
        public Guid? RegionId { get; set; }

        /// <summary> Область/штат </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Область/штат")]
        public Region Region { get; set; }

        /// <summary> Индекс </summary>
        [CreatioProp("Индекс")]
        public String Zip { get; set; }

        /// <summary> Страна (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Страна (Id)в")]
        public Guid? CountryId { get; set; }

        /// <summary> Страна </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Guid", "Страна")]
        public Country Country { get; set; }

        /// <summary> Не использовать Email </summary>
        [CreatioProp("Не использовать Email")]
        public Boolean? DoNotUseEmail { get; set; }

        /// <summary> Не использовать телефон </summary>
        [CreatioProp("Не использовать телефон")]
        public Boolean? DoNotUseCall { get; set; }

        /// <summary> Не использовать факс </summary>
        [CreatioProp("Не использовать факс")]
        public Boolean? DoNotUseFax { get; set; }

        /// <summary> Не использовать SMS </summary>
        [CreatioProp("Не использовать SMS")]
        public Boolean? DoNotUseSms { get; set; }

        /// <summary> Не использовать почту </summary>
        [CreatioProp("Не использовать почту")]
        public Boolean? DoNotUseMail { get; set; }

        /// <summary> Заметки </summary>
        [CreatioProp("Заметки")]
        public String Notes { get; set; }

        /// <summary> Facebook </summary>
        [CreatioProp("Facebook")]
        public String Facebook { get; set; }

        /// <summary> LinkedIn </summary>
        [CreatioProp("LinkedIn")]
        public String LinkedIn { get; set; }

        /// <summary> Twitter </summary>
        [CreatioProp("Twitter")]
        public String Twitter { get; set; }

        /// <summary> FacebookId </summary>
        [CreatioProp("FacebookId")]
        public String FacebookId { get; set; }

        /// <summary> LinkedInId </summary>
        [CreatioProp("LinkedInId")]
        public String LinkedInId { get; set; }

        /// <summary> TwitterId </summary>
        [CreatioProp("TwitterId")]
        public String TwitterId { get; set; }

        /// <summary> Аккаунт Twitter для доступа к данным (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Аккаунт Twitter для доступа к данным (Id)")]
        public Guid? TwitterAFDAId { get; set; }

        /// <summary> Аккаунт Twitter для доступа к данным </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Аккаунт Twitter для доступа к данным")]
        public SocialAccount TwitterAFDA { get; set; }

        /// <summary> Аккаунт Facebook для доступа к данным (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Аккаунт Facebook для доступа к данным (Id)")]
        public Guid? FacebookAFDAId { get; set; }

        /// <summary> Аккаунт Facebook для доступа к данным </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Аккаунт Facebook для доступа к данным")]
        public SocialAccount FacebookAFDA { get; set; }

        /// <summary> Аккаунт LinkedIn для доступа к данным (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Аккаунт LinkedIn для доступа к данным (Id)")]
        public Guid? LinkedInAFDAId { get; set; }

        /// <summary> Аккаунт LinkedIn для доступа к данным </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Аккаунт LinkedIn для доступа к данным")]
        public SocialAccount LinkedInAFDA { get; set; }

        /// <summary> Фото (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Фото (Id)")]
        public Guid? PhotoId { get; set; }

        /// <summary> Фото </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Фото")]
        public Image Photo { get; set; }

        /// <summary> GPS N </summary>
        [CreatioProp("GPS N")]
        public String GPSN { get; set; }

        /// <summary> GPS E </summary>
        [CreatioProp("GPS E")]
        public String GPSE { get; set; }

        /// <summary> Фамилия </summary>
        [Map(true, DataType.InfoReg, "InformationRegister_ФИОФизЛиц", DataType.String, "Фамилия")]
        [CreatioProp("Фамилия", Color = ConsoleColor.Yellow)]
        public String Surname { get; set; }

        /// <summary> Имя </summary>
        [Map(true, DataType.InfoReg, "InformationRegister_ФИОФизЛиц", DataType.String, "Имя")]
        [CreatioProp("Имя", Color = ConsoleColor.Yellow)]
        public String GivenName { get; set; }

        /// <summary> Отчество </summary>
        [Map(true, DataType.InfoReg, "InformationRegister_ФИОФизЛиц", DataType.String, "Отчество")]
        [CreatioProp("Отчество", Color = ConsoleColor.Yellow)]
        public String MiddleName { get; set; }

        /// <summary> Подтвержден </summary>
        [CreatioProp("Подтвержден")]
        public Boolean? Confirmend { get; set; }

        /// <summary> Полнота заполнения данных </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Полнота заполнения данных")]
        public Double? Completeness { get; set; }

        /// <summary> Язык общения (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Язык общения (Id)")]
        public Guid? LanguageId { get; set; }

        /// <summary> Язык общения </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Язык общения")]
        public SysLanguage Language { get; set; }

        /// <summary> Календарь (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Календарь (Id)")]
        public Guid? CalendarId { get; set; }

        /// <summary> Календарь </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Календарь")]
        public Calendar Calendar { get; set; }

        /// <summary> Возраст </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Возраст")]
        public Int32? Age { get; set; }        
    }
}
