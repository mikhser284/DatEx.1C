namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using Newtonsoft.Json;
    using System;
    using System.Runtime.Serialization;


    /// <summary> Форма собственности контрагента </summary>
    public class AccountOwnership : BaseLookup
    {
        private Guid? _countryId;
        /// <summary> Страна (Id) </summary>
        public Guid? CountryId { get => _countryId; set => _countryId = value.AsNullable(); }

        [JsonIgnore]
        /// <summary> Страна </summary>
        public Country Country { get; set; }

    }
}
