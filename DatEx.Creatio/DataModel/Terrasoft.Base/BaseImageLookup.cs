namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Базовый справочник с изображением </summary>
    [CreatioType("Базовый справочник с изображением")]
    public class BaseImageLookup : BaseLookup
    {
        /// <summary> Изображение (Id) </summary>
        public Guid ImageId { get; set; }

        /// <summary> Изображение </summary>
        public Image Image { get; set; }
    }
}
