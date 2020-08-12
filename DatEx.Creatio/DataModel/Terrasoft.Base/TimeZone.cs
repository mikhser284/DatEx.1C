namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Часовой пояс </summary>
    [CreatioType("Часовой пояс")]
    public class TimeZone : BaseCodeLookup
    {
        /// <summary> Временное смещение </summary>
        public String Offset { get; set; }

        /// <summary> Американский код </summary>
        public String CodeAmerican { get; set; }
    }
}
