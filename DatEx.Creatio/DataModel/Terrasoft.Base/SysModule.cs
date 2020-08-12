namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Раздел </summary>
    [CreatioType("Раздел")]
    public class SysModule : BaseEntity
    {
        /// <summary> Заголовок </summary>
        public String Caption { get; set; }

        /// <summary> Объект раздела </summary>
        public SysModuleEntry SysModuleEntry { get; set; }

        /// <summary> Изображение (16х16) </summary>
        public Image Image16 { get; set; }

        /// <summary> Изображение (20х20) </summary>
        public Image Image20 { get; set; }

        /// <summary> Режим групп раздела </summary>
        public SysModuleFolderMode FolderMode { get; set; }

        /// <summary> Использовать в поиске </summary>
        public Boolean GlobalSearcAvailable { get; set; }

        /// <summary> Содержит аналитику </summary>
        public Boolean HasAnalytics { get; set; }

        /// <summary> Содержит процессы </summary>
        public Boolean HasActions { get; set; }

        /// <summary> Содержит группу Недавние </summary>
        public Boolean HasRecent { get; set; }

        /// <summary> Code </summary>
        public String Code { get; set; }

        /// <summary> Идентификатор контекстной справки </summary>
        public String HelpContextId { get; set; }

        /// <summary> Заголовок модуля </summary>
        public String ModuleHeader { get; set; }

        /// <summary> Атрибут </summary>
        public String Attribute { get; set; }

        /// <summary> Уникаьный идентификатор страницы раздела </summary>
        public Guid SysPageSchemaUid { get; set; }

        /// <summary> Уникальный идентификатор схемы страницы редактирования раздела </summary>
        public Guid CardSchemaUId { get; set; }

        /// <summary> Уникальный идентификатор схемы модуля раздела </summary>
        public Guid SectionModuleSchemaUId { get; set; }

        /// <summary> Уникальный идентификатор схемы раздела </summary>
        public Guid SectionSchemaUId { get; set; }

        /// <summary> Уникальный идентификатор схемы модуля страницы редактирования </summary>
        public Guid CardModuleUId { get; set; }

        /// <summary> Значение колонки типа </summary>
        public Guid TypeColumnValue { get; set; }

        /// <summary> Изображение (32x32) </summary>
        public Image Igame32 { get; set; }

        /// <summary> Лого </summary>
        public Image Logo { get; set; }

        /// <summary> Настройки визирования </summary>
        public SysModuleVisa SysModuleVisa { get; set; }

        /// <summary> Системный раздел </summary>
        public Boolean IsSystem { get; set; }

        /// <summary> Типы раздела </summary>
        public Int32 Type { get; set; }
    }
}
