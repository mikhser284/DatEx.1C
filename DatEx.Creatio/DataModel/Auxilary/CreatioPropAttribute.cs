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
}
