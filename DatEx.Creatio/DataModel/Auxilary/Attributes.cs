using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.Creatio.DataModel.Auxilary
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CreatioPropAttribute : Attribute
    {
        /// <summary> Тип свойства </summary>
        public String CreatioType { get; private set; }

        /// <summary> Заголовок свойства </summary>
        public String CreatioTitle { get; private set; }

        /// <summary> цвет при выводе на консоль </summary>
        public ConsoleColor Color { get; set; } = ConsoleColor.DarkGray;

        /// <summary> Примечания </summary>
        public String Remarks { get; set; }

        public CreatioPropAttribute(String creatioTitle)
        {
            CreatioTitle = creatioTitle;
        }

        public CreatioPropAttribute(String creatioType, String creatioTitle)
        {
            CreatioType = creatioType;
            CreatioTitle = creatioTitle;
        }
    }

    /// <summary> Данное свойство не существует в модели данных от ITIS но необходимо для синхронизации с 1C </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CreatioPropNotExistInDataModelOfITISAttribute : Attribute { }


    [AttributeUsage(AttributeTargets.Class)]
    public class CreatioTypeAttribute : Attribute
    {
        /// <summary> Заголовок типа </summary>
        public String Title { get; private set; }

        /// <summary> цвет при выводе на консоль </summary>
        public ConsoleColor Color { get; set; } = ConsoleColor.DarkGray;

        /// <summary> Примечания </summary>
        public String Remarks { get; set; }

        public CreatioTypeAttribute(String title)
        {
            Title = title;
        }
    }

    public class MapFromOneSRemarksAttribute : Attribute
    {
        public String Remarks { get; set; }
        
        public MapFromOneSRemarksAttribute(String remarks)
        {
            Remarks = remarks;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MapFromOneSPropAttribute : Attribute
    {
        /// <summary> Вид типа данных </summary>
        public OneSDataTypeKind DataTypeKind { get; private set; }

        /// <summary> Название типа данных </summary>
        public String DataTypeName { get; private set; }

        /// <summary> Тип данных проецируемого свойства </summary>
        public String MapablePropDataType { get; private set; }

        /// <summary> Название проецируемого свойства </summary>
        public String MapablePropName { get; private set; }


        /// <summary> Проекция свойства из 1С на свойство из Creatio </summary>
        /// <param name="dataTypeKind"> Вид типа данных </param>
        /// <param name="dataTypeName"> Название типа данных </param>
        /// <param name="mapablePropDataType"> Тип данных проэцируемого свойства </param>
        /// <param name="mapablePropName"> Название проэцируемого свойства </param>
        public MapFromOneSPropAttribute(OneSDataTypeKind dataTypeKind, String dataTypeName, String mapablePropDataType, String mapablePropName)
        {
            DataTypeKind = dataTypeKind;
            DataTypeName = dataTypeName;
            MapablePropDataType = mapablePropDataType;
            MapablePropName = mapablePropName;
        }
    }

    /// <summary> Тип данных в 1С </summary>
    public enum OneSDataTypeKind
    {
        /// <summary> Справочник </summary>
        Lookup,
        /// <summary> Информационный регистр </summary>
        InfoReg,
        /// <summary> Перечисление </summary>
        Enum,
        /// <summary> Строка </summary>
        String,
        /// <summary> Дата/время </summary>
        DateTime,
        /// <summary> Целое число </summary>
        Int,
        /// <summary> Дроброе число </summary>
        Float,
        /// <summary> Булево </summary>
        Bool,
        /// <summary> Guid (идентификатор) </summary>
        Guid,

    }
}
