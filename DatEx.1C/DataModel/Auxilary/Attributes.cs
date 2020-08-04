
namespace DatEx.OneC.DataModel.Auxilary
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreatioPropertyMapAttribute : Attribute
    {
        public String CreatioName { get; private set; }

        public String CreatioType { get; set; }

        public String OneCName { get; private set; }

        public String Remarks { get; set; }

        public CreatioPropertyMapAttribute(String creatioType, String creatioName, String oneCName)
        {
            CreatioType = creatioType;
            CreatioName = creatioName;
            OneCName = oneCName;
        }
    }

    public class CreatioAuxAttribute : Attribute {}

    public class CreatioIgnoreAttribute : Attribute { }

    public class CreatioTypeMapAttribute : Attribute
    {
        public String CreatioName { get; private set; }

        public String OneCName { get; private set; }

        public CreatioTypeMapAttribute(String creatioName, String oneCName)
        {
            CreatioName = creatioName;
            OneCName = oneCName;
        }
    }
}
