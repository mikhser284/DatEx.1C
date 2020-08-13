namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    [CreatioType("Должность сотрудника")]

    /// <summary> Должность сотрудника </summary>
    public class EmployeeJob : DatEx.Creatio.DataModel.Terrasoft.Base.EmployeeJob
    {
        /// <summary> Внешний идентификатор</summary>
        [CreatioProp("Guid", "Внешний идентификатор")]
        public Guid ExternalId { get; set; }
    }
}
