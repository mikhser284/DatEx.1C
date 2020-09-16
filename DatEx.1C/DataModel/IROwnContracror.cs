using DatEx.OneS.DataModel.Auxilary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.OneS.DataModel
{
    [OneS("InformationRegister_СобственныеКонтрагенты", "InformationRegister_СобственныеКонтрагенты", "СобственныеКонтрагенты", "СобственныеКонтрагенты")]
    [JsonObject("InformationRegister_СобственныеКонтрагенты")]
    public class IROwnContracror : OneSObject
    {
        [OneS("Guid", "Контрагент_Key", "Справочник.Контрагенты", "Контрагент", Color = ConsoleColor.Blue)]
        [JsonProperty("Контрагент_Key")]
        public Guid KeyContractorId { get; set; }


        // Организация, ФизЛицо
        [OneS("String", "ВидСвязи", "Перечисление.ВидыСобственныхКонтрагентов", "ВидСвязи", Color = ConsoleColor.Blue)]
        [JsonProperty("ВидСвязи")]
        public String KeyTypeOfRelationship { get; set; }

        [OneS("String", "Объект", "СправочникСсылка.Организации, СправочникСсылка.ФизическиеЛица", "Объект", Color = ConsoleColor.Magenta)]
        [JsonProperty("Объект")]
        public Guid ObjectId { get; set; }

        [OneS("String", "НаименованиеАнгл", "Строка", "НаименованиеАнгл", Color = ConsoleColor.Magenta)]
        [JsonProperty("НаименованиеАнгл")]
        public String NameEng { get; set; }
    }

}
