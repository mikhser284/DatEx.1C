
namespace DatEx.OneC.DataModel.Auxilary
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class OneSAttribute : Attribute
    {
        public String OneCType { get; private set; }


        public String OneCName { get; private set; }

        
        public String ODataType { get; private set; }

        
        public String ODataName { get; private set; }


        public ConsoleColor Color { get; set; } = ConsoleColor.DarkGray;


        public String Remarks { get; set; }


        public OneSAttribute(String oDataType, String oDataName, String oneCType, String oneCName)
        {
            OneCType = oneCType;
            OneCName = oneCName;

            ODataType = oDataType;
            ODataName = oDataName;
        }
    }
}
