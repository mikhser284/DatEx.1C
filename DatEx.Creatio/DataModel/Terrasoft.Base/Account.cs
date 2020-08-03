namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Контрагент </summary>
    public class Account : BaseEntity
    {
        /// <summary> Название </summary>
        public String Name { get; set; }


        /// <summary> Ответственный (Id) </summary>
        public Guid OwnerId { get; set; }

        /// <summary> Ответственный </summary>
        public Contact Owner { get; set; }

        /// <summary> Форма собственности (Id) </summary>
        public Guid OwnershipId { get; set; }

        /// <summary> Форма собственности </summary>
        public AccountOwnership Ownership { get; set; }

        /// <summary> Основной контакт (Id) </summary>
        public Guid PrimaryContactId { get; set; }

        /// <summary> Основной контакт </summary>
        public Contact PrimaryContact { get; set; }

        /// <summary> Родительский контрагент (Id) </summary>
        public Guid ParentId { get; set; }

        /// <summary> Родительский контрагент </summary>
        public Account Parent { get; set; }

        /// <summary> Отрасль (Id)</summary>
        public Guid IndustryId { get; set; }

        /// <summary> Отрасль </summary>
        public AccountIndustry Industry { get; set; }

        /// <summary> Код </summary>
        public String Code { get; set; }

        /// <summary> Тип (Id) </summary>
        public Guid TypeId { get; set; }

        /// <summary> Тип </summary>
        public AccountType Type { get; set; }

        /// <summary> Основной телефон </summary>
        public String Phone { get; set; }

        /// <summary> Дополнительный телефон </summary>
        public String AdditionalProne { get; set; }

        /// <summary> Факс </summary>
        public String Fax { get; set; }

        /// <summary> Web </summary>
        public String Web { get; set; }

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

        /// <summary> Категория (Id) </summary>
        public Guid AccountCategoryId { get; set; }

        /// <summary> Категория </summary>
        public AccountCategory AccountCategory { get; set; }
        
        /// <summary> Количество сотрудников (Id) </summary>
        public Guid EmployeesNumberId { get; set; }

        /// <summary> Количество сотрудников </summary>
        public AccountEmployeesNumber EmployeesNumber { get; set; }

        /// <summary> Годовой оборот (Id) </summary>
        public Guid AnnualRevenueId { get; set; }

        /// <summary> Годовой оборот </summary>
        public AccountAnnualRevenue AnnualRevenue { get; set; }

        /// <summary> Заметки </summary>
        public String Notes { get; set; }

        /// <summary> Альтернативное название </summary>
        public String AlternativeName { get; set; }

        /// <summary> GPS N </summary>
        public String GPSN { get; set; }

        /// <summary> GPS E </summary>
        public String GPSE { get; set; }

        /// <summary> Полнота наполнениия данных </summary>
        public Double Completeness { get; set; }

        /// <summary> Логотип контрагента (Id) </summary>
        public Guid AccountLogoId { get; set; }

        /// <summary> Логотип контрагента </summary>
        public Image AccountLogo { get; set; }

        /// <summary> ИНН </summary>
        public String INN { get; set; }

        /// <summary> КПП </summary>
        public String KPP { get; set; }
    }
}
