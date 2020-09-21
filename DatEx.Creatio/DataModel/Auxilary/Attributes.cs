using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatEx.Creatio.DataModel.Auxilary
{
    /// <summary> Свойство в Creatio </summary>
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



    /// <summary> Устаревшее свойство в Creatio (очень желательно по возможности его удалить) </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ObsoleteCreatioPropAttribute : Attribute
    {
        /// <summary> Заголовок свойства </summary>
        public String CreatioTitle { get; private set; }

        /// <summary> цвет при выводе на консоль </summary>
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Red;

        /// <summary> Примечания </summary>
        public String Remarks { get; set; }

        public ObsoleteCreatioPropAttribute(String creatioTitle)
        {
            CreatioTitle = creatioTitle;
            Remarks = "По возможности очень желательно удалить это свойство из объекта в конфигураторе, так как оно просто замусоривает объек и по существу нигде не используется.";
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

    public class MapRemarksAttribute : Attribute
    {
        public String Remarks { get; set; }
        
        public MapRemarksAttribute(String remarks)
        {
            Remarks = remarks;
        }

        public override string ToString() => Remarks;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MapAttribute : Attribute
    {
        /// <summary> Вид типа данных </summary>
        public DataType? ObjDataType { get; private set; }

        /// <summary> Название типа данных </summary>
        public String ObjName { get; private set; }

        /// <summary> Тип данных проецируемого свойства </summary>
        public DataType? PropDataType { get; private set; }

        /// <summary> Название проецируемого свойства </summary>
        public String PropName { get; private set; }

        /// <summary> Название проецируемого свойства </summary>
        public Boolean Implemented { get; private set; }

        public List<Property> PropertiesChain { get; private set; } = new List<Property>();

        /// <summary> Проекция свойства из 1С на свойство из Creatio </summary>
        /// <param name="objDataType"> Вид типа данных </param>
        /// <param name="objName"> Название типа данных </param>
        /// <param name="propDataType"> Тип данных проэцируемого свойства </param>
        /// <param name="propName"> Название проэцируемого свойства </param>
        public MapAttribute(DataType objDataType, String objName, DataType propDataType, String propName)
        {
            ObjDataType = objDataType;
            ObjName = objName;
            PropDataType = propDataType;
            PropName = propName;
            Implemented = false;
        }

        public MapAttribute(DataType objDataType, String objName, DataType propDataType, String propName, Property[] propertiesChain)
        {
            ObjDataType = objDataType;
            ObjName = objName;
            PropDataType = propDataType;
            PropName = propName;
            PropertiesChain = propertiesChain.ToList();
            Implemented = false;
        }

        public MapAttribute(Boolean implemented, DataType objDataType, String objName, DataType propDataType, String propName)
        {
            ObjDataType = objDataType;
            ObjName = objName;
            PropDataType = propDataType;
            PropName = propName;
            Implemented = implemented;
        }

        public MapAttribute(Boolean implemented, DataType objDataType, String objName, DataType propDataType, String propName, Property[] propertiesChain)
        {
            ObjDataType = objDataType;
            ObjName = objName;
            PropDataType = propDataType;
            PropName = propName;
            PropertiesChain = propertiesChain.ToList();
            Implemented = implemented;
        }

        public MapAttribute(Boolean implemented = false)
        {
            Implemented = implemented;
        }

        public override string ToString()
        {
            if (ObjDataType == null) return String.Empty;
            String propsChain = PropertiesChain.Count == 0 ? "" : $" → {String.Join(" -> ", PropertiesChain)}";
            return $"[{((DataType)ObjDataType).AsString()}] {ObjName} → [{((DataType)PropDataType).AsString()}] {PropName}{propsChain}";
        }
    }

    public class Property
    {
        /// <summary> Тип данных проецируемого свойства </summary>
        public DataType PropDataType { get; private set; }

        /// <summary> Название проецируемого свойства </summary>
        public String PropName { get; private set; }

        public Property(DataType propDataType, String propName)
        {
            PropDataType = propDataType;
            PropName = propName;
        }

        public override string ToString()
        {
            return $"[{((DataType)PropDataType).AsString()}] {PropName}";
        }
    }


    /// <summary> Тип данных в 1С </summary>
    public enum DataType
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
        /// <summary> Дата </summary>
        Date,
        /// <summary> Время </summary>
        Time,
        /// <summary> Целое число </summary>
        Int,
        /// <summary> Дроброе число </summary>
        Float,
        /// <summary> Булево </summary>
        Bool,
        /// <summary> Guid (идентификатор) </summary>
        Guid,

    }


    public static class Ext_EnumOneSDataTypeKind
    {
        private static readonly Dictionary<DataType, String> MapToString = new Dictionary<DataType, string>
        {
            { DataType.Lookup , "Справочник" },
            { DataType.InfoReg , "Информационный регистр" },
            { DataType.Enum , "Перечисление" },
            { DataType.String , "Строка" },
            { DataType.DateTime , "Дата/Время" },
            { DataType.Date , "Дата" },
            { DataType.Time , "Время" },
            { DataType.Int , "Целое число" },
            { DataType.Float , "Дробное число" },
            { DataType.Bool , "Булево" },
            { DataType.Guid , "Guid" },
        };

        public static String AsString(this DataType e) => MapToString[e];
    }
}
