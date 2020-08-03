namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    /// <summary> Город </summary>
    public class City : BaseLookup
    {
        /// <summary> Страна </summary>
        public Country Country { get; set; }

        /// <summary> Область/штат </summary>
        public Region Region { get; set; }

        /// <summary> Часовой пояс </summary>
        public TimeZone TimeZone { get; set; }
    }
}
