namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Город </summary>
    [CreatioType("Город")]
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
