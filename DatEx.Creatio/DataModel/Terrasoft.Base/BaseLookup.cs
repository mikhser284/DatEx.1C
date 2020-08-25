namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Базовый справочник </summary>
    [CreatioType("Базовый справочник")]
    public class BaseLookup : BaseEntity
    {
        /// <summary> Название </summary>
        [CreatioProp("Строка", "Название", Color = ConsoleColor.Yellow)]
        public String Name { get; set; }

        /// <summary> Описание </summary>
        [CreatioProp("Строка", "Описание")]
        public String Description { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
