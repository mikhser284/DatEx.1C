namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Часовой пояс </summary>
    public class TimeZone : BaseCodeLookup
    {
        /// <summary> Временное смещение </summary>
        public String Offset { get; set; }

        /// <summary> Американский код </summary>
        public String CodeAmerican { get; set; }
    }
}
