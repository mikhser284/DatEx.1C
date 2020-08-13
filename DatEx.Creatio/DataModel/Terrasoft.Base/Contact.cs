namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using DatEx.Creatio.DataModel.Terrasoft.Calendar;

    /// <summary> Контакт </summary>    
    [CreatioType("Контакт")]
    public class Contact : BaseEntity
    {
        /// <summary> ФИО </summary>        
        [CreatioProp("ФИО", Color = ConsoleColor.Magenta)]
        public String Name { get; set; }

        /// <summary> Ответственный (Id) </summary>
        [CreatioProp("Guid", "Ответственный (Id)")]
        public Guid OwnerId { get; set; }

        /// <summary> Ответственный </summary>
        [CreatioProp("Ответственный")]
        public Contact Owner { get; set; }

        /// <summary> Приветствие </summary>
        [CreatioProp("Приветствие")]
        public String Dear { get; set; }

        /// <summary> Обращение (Id) </summary>
        [CreatioProp("Guid", "Обращение (Id)")]
        public Guid SalutationTypeId { get; set; }

        /// <summary> Обращение </summary>
        [CreatioProp("Обращение")]
        public ContactSalutationType SalutationType { get; set; }

        /// <summary> Пол (Id) </summary>
        [CreatioProp("Guid", "Пол (Id)", Color = ConsoleColor.Blue)]
        public Guid GenderId { get; set; }

        /// <summary> Пол </summary>
        [CreatioProp("Пол", Color = ConsoleColor.Magenta)]
        public Gender Gender { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [CreatioProp("Guid", "Контрагент (Id)", Color = ConsoleColor.Blue)]
        public Guid AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [CreatioProp("Контрагент", Color = ConsoleColor.Magenta)]
        public Account Account { get; set; }

        /// <summary> Роль (Id) </summary>
        [CreatioProp("Guid", "Роль (Id)")]
        public Guid DecisionRoleId { get; set; }

        /// <summary> Роль </summary>
        [CreatioProp("Роль")]
        public ContactDecisionRole DecisionRole { get; set; }

        /// <summary> Тип (Id) </summary>
        [CreatioProp("Guid", "Тип (Id)", Color = ConsoleColor.Blue)]
        public Guid TypeId { get; set; }

        /// <summary> Тип </summary>
        [CreatioProp("Тип", Color = ConsoleColor.Magenta)]
        public ContactType Type { get; set; }

        /// <summary> Должность (Id) </summary>
        [CreatioProp("Guid", "Должность (Id)", Color = ConsoleColor.Blue)]
        public Guid JobId { get; set; }

        /// <summary> Должность </summary>
        [CreatioProp("Должность", Color = ConsoleColor.Magenta)]
        public Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        [CreatioProp("Полное название должности", Color = ConsoleColor.Magenta)]
        public String JobTitle { get; set; }

        /// <summary> Департамент (Id) </summary>
        [CreatioProp("Guid", "Департамент (Id)", Color = ConsoleColor.Blue)]
        public Guid DepartmentId { get; set; }

        /// <summary> Департамент </summary>
        [CreatioProp("Департамент", Color = ConsoleColor.Magenta)]
        public Department Department { get; set; }

        /// <summary> Дата рождения </summary>
        [CreatioProp("Дата рождения", Color = ConsoleColor.Green)]
        public DateTime BirthDate { get; set; }

        /// <summary> Рабочий телефон </summary>
        [CreatioProp("Рабочий телефон", Color = ConsoleColor.Green)]
        public String Phone { get; set; }

        /// <summary> Мобильный телефон </summary>
        [CreatioProp("Мобильный телефон", Color = ConsoleColor.Green)]
        public String MobilePhone { get; set; }

        /// <summary> Домашний телефон </summary>
        [CreatioProp("Домашний телефон")]
        public String HomePhone { get; set; }

        /// <summary> Skype </summary>
        [CreatioProp("Skype")]
        public String Skype { get; set; }

        /// <summary> Email </summary>
        [CreatioProp("Email", Color = ConsoleColor.Green)]
        public String Email { get; set; }

        /// <summary> Тип адреса (Id) </summary>
        [CreatioProp("Guid", "Тип адреса (Id)")]
        public Guid AddressTypeId { get; set; }

        /// <summary> Тип адреса </summary>
        [CreatioProp("Тип адреса")]
        public AddressType AddressType { get; set; }

        /// <summary> Адрес </summary>
        [CreatioProp("Адрес")]
        public String Address { get; set; }

        /// <summary> Город (Id) </summary>
        [CreatioProp("Guid", "Город (Id)")]
        public Guid CityId { get; set; }

        /// <summary> Город </summary>
        [CreatioProp("Город")]
        public City City { get; set; }

        /// <summary> Область/штат (Id) </summary>
        [CreatioProp("Guid", "Область/штат (Id)")]
        public Guid RegionId { get; set; }

        /// <summary> Область/штат </summary>
        [CreatioProp("Область/штат")]
        public Region Region { get; set; }

        /// <summary> Индекс </summary>
        [CreatioProp("Индекс")]
        public String Zip { get; set; }

        /// <summary> Страна (Id) </summary>
        [CreatioProp("Guid", "Страна (Id)в")]
        public Guid CountryId { get; set; }

        /// <summary> Страна </summary>
        [CreatioProp("Guid", "Страна")]
        public Country Country { get; set; }

        /// <summary> Не использовать Email </summary>
        [CreatioProp("Не использовать Email")]
        public Boolean DoNotUseEmail { get; set; }

        /// <summary> Не использовать телефон </summary>
        [CreatioProp("Не использовать телефон")]
        public Boolean DoNotUseCall { get; set; }

        /// <summary> Не использовать факс </summary>
        [CreatioProp("Не использовать факс")]
        public Boolean DoNotUseFax { get; set; }

        /// <summary> Не использовать SMS </summary>
        [CreatioProp("Не использовать SMS")]
        public Boolean DoNotUseSms { get; set; }

        /// <summary> Не использовать почту </summary>
        [CreatioProp("Не использовать почту")]
        public Boolean DoNotUseMail { get; set; }

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
        [CreatioProp("Guid", "Аккаунт Twitter для доступа к данным (Id)")]
        public Guid TwitterAFDAId { get; set; }

        /// <summary> Аккаунт Twitter для доступа к данным </summary>
        [CreatioProp("Аккаунт Twitter для доступа к данным")]
        public SocialAccount TwitterAFDA { get; set; }

        /// <summary> Аккаунт Facebook для доступа к данным (Id) </summary>
        [CreatioProp("Guid", "Аккаунт Facebook для доступа к данным (Id)")]
        public Guid FacebookAFDAId { get; set; }

        /// <summary> Аккаунт Facebook для доступа к данным </summary>
        [CreatioProp("Аккаунт Facebook для доступа к данным")]
        public SocialAccount FacebookAFDA { get; set; }

        /// <summary> Аккаунт LinkedIn для доступа к данным (Id) </summary>
        [CreatioProp("Guid", "Аккаунт LinkedIn для доступа к данным (Id)")]
        public Guid LinkedInAFDAId { get; set; }

        /// <summary> Аккаунт LinkedIn для доступа к данным </summary>
        [CreatioProp("Аккаунт LinkedIn для доступа к данным")]
        public SocialAccount LinkedInAFDA { get; set; }

        /// <summary> Фото (Id) </summary>
        [CreatioProp("Guid", "Фото (Id)")]
        public Guid PhotoId { get; set; }

        /// <summary> Фото </summary>
        [CreatioProp("Фото")]
        public Image Photo { get; set; }

        /// <summary> GPS N </summary>
        [CreatioProp("GPS N")]
        public String GPSN { get; set; }

        /// <summary> GPS E </summary>
        [CreatioProp("GPS E")]
        public String GPSE { get; set; }

        /// <summary> Фамилия </summary>
        [CreatioProp("Фамилия", Color = ConsoleColor.Green)]
        public String Surname { get; set; }

        /// <summary> Имя </summary>
        [CreatioProp("Имя", Color = ConsoleColor.Green)]
        public String GivenName { get; set; }

        /// <summary> Отчество </summary>
        [CreatioProp("Отчество", Color = ConsoleColor.Green)]
        public String MiddleName { get; set; }

        /// <summary> Подтвержден </summary>
        [CreatioProp("Подтвержден")]
        public Boolean Confirmend { get; set; }

        /// <summary> Полнота заполнения данных </summary>
        [CreatioProp("Полнота заполнения данных")]
        public Double Completeness { get; set; }

        /// <summary> Язык общения (Id) </summary>
        [CreatioProp("Guid", "Язык общения (Id)")]
        public Guid LanguageId { get; set; }

        /// <summary> Язык общения </summary>
        [CreatioProp("Язык общения")]
        public SysLanguage Language { get; set; }

        /// <summary> Календарь (Id) </summary>
        [CreatioProp("Guid", "Календарь (Id)")]
        public Guid CalendarId { get; set; }

        /// <summary> Календарь </summary>
        [CreatioProp("Календарь")]
        public Calendar Calendar { get; set; }

        /// <summary> Возраст </summary>
        [CreatioProp("Возраст")]
        public Int32 Age { get; set; }        
    }
}
