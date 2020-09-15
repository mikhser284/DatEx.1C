namespace DatEx.OneS.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DatEx.OneS.DataModel.Auxilary;
    using Newtonsoft.Json;

    [OneS("Catalog_ЕдиницыИзмерения", "Catalog_ЕдиницыИзмерения", "Справочник.ЕдиницыИзмерения", "Справочник.ЕдиницыИзмерения")]
    [JsonObject("Catalog_ЕдиницыИзмерения")]
    public class MeasureUnit : OneSBaseLookup

    {
    }
}
