

namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using DatEx.Creatio.DataModel.Terrasoft.Calendar;
    using System;

    /// <summary> Контакт </summary>
    public class Contact : BaseEntity
    {
        /// <summary> ФИО </summary>
        public String Name { get; set; }

        /// <summary> Ответственный (Id) </summary>
        public Guid OwnerId { get; set; }

        /// <summary> Ответственный </summary>
        public Contact Owner { get; set; }

        /// <summary> Приветствие </summary>
        public String Dear { get; set; }

        /// <summary> Обращение (Id) </summary>
        public Guid SalutationTypeId { get; set; }

        /// <summary> Обращение </summary>
        public ContactSalutationType SalutationType { get; set; }

        /// <summary> Пол (Id) </summary>
        public Guid GenderId { get; set; }

        /// <summary> Пол </summary>
        public Gender Gender { get; set; }

        /// <summary> Контрагент (Id) </summary>
        public Guid AccountId { get; set; }

        /// <summary> Контрагент </summary>
        public Account Account { get; set; }

        /// <summary> Роль (Id) </summary>
        public Guid DecisionRoleId { get; set; }

        /// <summary> Роль </summary>
        public ContactDecisionRole DecisionRole { get; set; }

        /// <summary> Тип (Id) </summary>
        public Guid TypeId { get; set; }

        /// <summary> Тип </summary>
        public ContactType Type { get; set; }

        /// <summary> Должность (Id) </summary>
        public Guid JobId { get; set; }

        /// <summary> Должность </summary>
        public Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        public String JobTitle { get; set; }

        /// <summary> Департамент (Id) </summary>
        public Guid DepartmentId { get; set; }

        /// <summary> Департамент </summary>
        public Department Department { get; set; }

        /// <summary> Дата рождения </summary>
        public DateTime BirthDate { get; set; }

        /// <summary> Рабочий телефон </summary>
        public String Phone { get; set; }

        /// <summary> Мобильный телефон </summary>
        public String MobilePhone { get; set; }

        /// <summary> Домашний телефон </summary>
        public String HomePhone { get; set; }

        /// <summary> Skype </summary>
        public String Skype { get; set; }

        /// <summary> Email </summary>
        public String Email { get; set; }

        /// <summary> Тип адреса (Id) </summary>
        public Guid AddressTypeId { get; set; }

        /// <summary> Тип адреса </summary>
        public AddressType AddressType { get; set; }

        /// <summary> Адрес </summary>
        public String Address { get; set; }

        /// <summary> Город (Id) </summary>
        public Guid CityId { get; set; }

        /// <summary> Город </summary>
        public City City { get; set; }

        /// <summary> Область/штат (Id) </summary>
        public Guid RegionId { get; set; }

        /// <summary> Область/штат </summary>
        public Region Region { get; set; }

        /// <summary> Индекс </summary>
        public String Zip { get; set; }

        /// <summary> Страна (Id) </summary>
        public Guid CountryId { get; set; }

        /// <summary> Страна </summary>
        public Country Country { get; set; }

        /// <summary> Не использовать Email </summary>
        public Boolean DoNotUseEmail { get; set; }

        /// <summary> Не использовать телефон </summary>
        public Boolean DoNotUseCall { get; set; }

        /// <summary> Не использовать факс </summary>
        public Boolean DoNotUseFax { get; set; }

        /// <summary> Не использовать SMS </summary>
        public Boolean DoNotUseSms { get; set; }

        /// <summary> Не использовать почту </summary>
        public Boolean DoNotUseMail { get; set; }

        /// <summary> Заметки </summary>
        public String Notes { get; set; }

        /// <summary> Facebook </summary>
        public String Facebook { get; set; }

        /// <summary> LinkedIn </summary>
        public String LinkedIn { get; set; }

        /// <summary> Twitter </summary>
        public String Twitter { get; set; }

        /// <summary> FacebookId </summary>
        public String FacebookId { get; set; }

        /// <summary> LinkedInId </summary>
        public String LinkedInId { get; set; }

        /// <summary> TwitterId </summary>
        public String TwitterId { get; set; }

        /// <summary> Аккаунт Twitter для доступа к данным (Id) </summary>
        public Guid TwitterAFDAId { get; set; }

        /// <summary> Аккаунт Twitter для доступа к данным </summary>
        public SocialAccount TwitterAFDA { get; set; }

        /// <summary> Аккаунт Facebook для доступа к данным (Id) </summary>
        public Guid FacebookAFDAId { get; set; }

        /// <summary> Аккаунт Facebook для доступа к данным </summary>
        public SocialAccount FacebookAFDA { get; set; }

        /// <summary> Аккаунт LinkedIn для доступа к данным (Id) </summary>
        public Guid LinkedInAFDAId { get; set; }

        /// <summary> Аккаунт LinkedIn для доступа к данным </summary>
        public SocialAccount LinkedInAFDA { get; set; }

        /// <summary> Фото (Id) </summary>
        public Guid PhotoId { get; set; }

        /// <summary> Фото </summary>
        public Image Photo { get; set; }

        /// <summary> GPS N </summary>
        public String GPSN { get; set; }

        /// <summary> GPS E </summary>
        public String GPSE { get; set; }

        /// <summary> Фамилия </summary>
        public String Surname { get; set; }

        /// <summary> Имя </summary>
        public String GivenName { get; set; }

        /// <summary> Отчество </summary>
        public String MiddleName { get; set; }

        /// <summary> Подтвержден </summary>
        public Boolean Confirmend { get; set; }

        /// <summary> Полнота заполнения данных </summary>
        public Double Completeness { get; set; }

        /// <summary> Язык общения (Id) </summary>
        public Guid LanguageId { get; set; }

        /// <summary> Язык общения </summary>
        public SysLanguage Language { get; set; }

        /// <summary> Календарь (Id) </summary>
        public Guid CalendarId { get; set; }

        /// <summary> Календарь </summary>
        public Calendar Calendar { get; set; }

        /// <summary> Возраст </summary>
        public Int32 Age { get; set; }        
    }
}
