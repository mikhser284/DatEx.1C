namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;

    [CreatioType("Контрагент")]
    /// <summary> Контрагент </summary>
    public class Account : BaseEntity
    {
        /// <summary> Название </summary>
        [JsonProperty("Name", Order = 2)]
        [CreatioProp("Название", Color = ConsoleColor.Yellow)]
        public String Name { get; set; }

        /// <summary> Ответственный (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [JsonProperty("OwnerId", Order = 3)]
        [CreatioProp("Ответственный (Id)")]
        public Guid? OwnerId { get; set; }

        /// <summary> Ответственный </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Ответственный")]
        public Contact Owner { get; set; }

        /// <summary> Форма собственности (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [JsonProperty("OwnershipId", Order = 9)]
        [CreatioProp("Форма собственности (Id)", Color = ConsoleColor.Blue)]
        public Guid? OwnershipId { get; set; }

        /// <summary> Форма собственности </summary>
        [Map]
        [JsonIgnoreSerialization]
        [CreatioProp("Форма собственности", Color = ConsoleColor.Yellow)]
        public AccountOwnership Ownership { get; set; }

        /// <summary> Основной контакт (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [JsonProperty("PrimaryContactId", Order = 10)]
        [CreatioProp("Основной контакт (Id)")]
        public Guid? PrimaryContactId { get; set; }

        /// <summary> Основной контакт </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Основной контакт")]
        public Contact PrimaryContact { get; set; }

        /// <summary> Родительский контрагент (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Родительский контрагент (Id)", Color = ConsoleColor.Blue)]
        public Guid? ParentId { get; set; }

        /// <summary> Родительский контрагент </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Родительский контрагент", Color = ConsoleColor.Yellow)]
        public Account Parent { get; set; }

        /// <summary> Отрасль (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Отрасль (Id)")]
        public Guid? IndustryId { get; set; }

        /// <summary> Отрасль </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Отрасль")]
        public AccountIndustry Industry { get; set; }

        /// <summary> Код </summary>
        [CreatioProp("Код")]
        public String Code { get; set; }

        /// <summary> Тип (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Значение может браться из объекта настроек, когда это наша компания")]
        [Map(true)]
        [CreatioProp("Тип (Id)", Color = ConsoleColor.Blue)]
        public Guid? TypeId { get; set; }

        /// <summary> Тип </summary>
        [Map]
        [JsonIgnoreSerialization]
        [CreatioProp("Тип", Color = ConsoleColor.Yellow)]
        public AccountType Type { get; set; }

        /// <summary> Основной телефон </summary>
        [CreatioProp("Основной телефон")]
        public String Phone { get; set; }

        /// <summary> Дополнительный телефон </summary>
        [CreatioProp("Дополнительный телефон")]
        public String AdditionalPhone { get; set; }

        /// <summary> Факс </summary>
        [CreatioProp("Факс")]
        public String Fax { get; set; }

        /// <summary> Web </summary>
        [CreatioProp("Web")]
        public String Web { get; set; }

        /// <summary> Тип адреса (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Тип адреса (Id)")]
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
        [CreatioProp("Город (Id)")]
        public Guid? CityId { get; set; }

        /// <summary> Город </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Город")]
        public City City { get; set; }

        /// <summary> Область/штат (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Область/штат (Id)")]
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
        [CreatioProp("Страна (Id)")]
        public Guid? CountryId { get; set; }

        /// <summary> Страна </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Страна")]
        public Country Country { get; set; }

        /// <summary> Категория (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Категория (Id)")]
        public Guid? AccountCategoryId { get; set; }

        /// <summary> Категория </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Категория")]        
        public AccountCategory AccountCategory { get; set; }

        /// <summary> Количество сотрудников (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Количество сотрудников (Id)")]
        public Guid? EmployeesNumberId { get; set; }

        /// <summary> Количество сотрудников </summary>
        [CreatioProp("Количество сотрудников")]
        [JsonIgnoreSerialization]
        public AccountEmployeesNumber EmployeesNumber { get; set; }

        /// <summary> Годовой оборот (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Годовой оборот (Id)")]
        public Guid? AnnualRevenueId { get; set; }

        /// <summary> Годовой оборот </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Годовой оборот")]
        public AccountAnnualRevenue AnnualRevenue { get; set; }

        /// <summary> Заметки </summary>
        [CreatioProp("Заметки")]
        public String Notes { get; set; }

        /// <summary> Альтернативное название </summary>
        [Map(true, DataType.Lookup, "Catalog_Организации", DataType.String, "Наименование")]
        [CreatioProp("Альтернативное название", Color = ConsoleColor.Yellow)]
        public String AlternativeName { get; set; }

        /// <summary> GPS N </summary>
        [CreatioProp("GPS N")]
        public String GPSN { get; set; }

        /// <summary> GPS E </summary>
        [CreatioProp("GPS E")]
        public String GPSE { get; set; }

        /// <summary> Полнота наполнениия данных </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Полнота наполнениия данных")]
        public Double Completeness { get; set; }

        /// <summary> Логотип контрагента (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Логотип контрагента (Id)")]
        public Guid? AccountLogoId { get; set; }

        /// <summary> Логотип контрагента </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Логотип контрагента")]
        public Image AccountLogo { get; set; }

        /// <summary> ИНН </summary>
        [Map]
        [CreatioProp("ИНН", Color = ConsoleColor.Yellow)]
        public String INN { get; set; }

        /// <summary> ЕДРПОУ </summary>
        [Map]
        [CreatioProp("ЕДРПОУ", Color = ConsoleColor.Yellow)]
        public String KPP { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
