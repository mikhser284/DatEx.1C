namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Базовый справочник с изображением </summary>
    public class BaseImageLookup : BaseLookup
    {
        /// <summary> Изображение (Id) </summary>
        public Guid ImageId { get; set; }

        /// <summary> Изображение </summary>
        public Image Image { get; set; }
    }
}
