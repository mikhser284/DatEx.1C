using System;
using System.Collections.Generic;
using System.Text;

namespace App.Auxilary
{
    public class DocAttribute : Attribute
    {
        /// <summary> Дружественное название </summary>
        public String FriendlyName { get; set; }

        /// <summary> Примечания </summary>
        public String Remarks { get; set; }


        public DocAttribute(String remarks)
        {
            Remarks = remarks;
        }
        
        public DocAttribute(String friendlyName, String remarks) : this(remarks)
        {
            FriendlyName = friendlyName;
        }
    }
}
