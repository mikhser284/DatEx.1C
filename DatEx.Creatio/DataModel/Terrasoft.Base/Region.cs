namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    /// <summary> Области/штаты </summary>
    public class Region : BaseLookup
    {
        /// <summary> Страна </summary>
        public Country Country { get; set; }

        /// <summary> Часовой пояс </summary>
        public TimeZone TimeZone { get; set; }
    }
}
