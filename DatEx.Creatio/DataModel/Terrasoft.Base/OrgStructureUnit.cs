namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Элемент организационной струкруры </summary>
    public class OrgStructureUnit : BaseEntity
    {
        /// <summary> Название </summary>
        public String Name { get; set; }

        /// <summary> Руководитель </summary>
        public Employee Head { get; set; }

        /// <summary> Родительское подразделение </summary>
        public OrgStructureUnit Parent { get; set; }

        /// <summary> Полное название </summary>
        public String FullName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
