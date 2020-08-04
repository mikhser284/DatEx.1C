using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.Creatio.DataModel.Auxilary
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CreatioPropAttribute : Attribute
    {
        /// <summary> Заголовок свойства </summary>
        public String Title { get; private set; }

        public CreatioPropAttribute(String title)
        {
            Title = title;
        }
    }



    [AttributeUsage(AttributeTargets.Class)]
    public class CreatioTypeAttribute : Attribute
    {
        /// <summary> Заголовок типа </summary>
        public String Title { get; private set; }

        public CreatioTypeAttribute(String title)
        {
            Title = title;
        }
    }
}
