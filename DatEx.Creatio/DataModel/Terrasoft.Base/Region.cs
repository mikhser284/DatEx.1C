namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Области/штаты </summary>
    [CreatioType("Области/штаты")]
    public class Region : BaseLookup
    {
        /// <summary> Страна </summary>
        public Country Country { get; set; }

        /// <summary> Часовой пояс </summary>
        public TimeZone TimeZone { get; set; }
    }
}
