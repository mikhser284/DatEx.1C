namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Объект администрирования </summary>
    public class SysAdminUnit : BaseLookup
    {
        /// <summary> Тип </summary>
        public Int32 SysAdminUnitTypeValue { get; set; }

        private Guid? _parentRoleId;
        /// <summary> Родительская роль (Id) </summary>
        public Guid? ParentRoleId { get => _parentRoleId; set => _parentRoleId = value.AsNullable(); }

        /// <summary> Родительская роль (Id) </summary>
        public SysAdminUnit ParentRole { get; set; }

        private Guid? _contactId;
        /// <summary> Контакт </summary>
        public Guid? ContactId { get => _contactId; set => _contactId = value.AsNullable(); }

        /// <summary> Контакт </summary>
        public Contact Contact { get; set; }

        private Guid? _accountId;
        /// <summary> Контрагент (Id) </summary>
        public Guid? AccountId { get => _accountId; set => _accountId = value.AsNullable(); }

        /// <summary> Контрагент </summary>
        public Account Account { get; set; }

        /// <summary> Доменный пользователь </summary>
        public Boolean IsDirectoryEntry { get; set; }

        /// <summary> Часовой пояс </summary>
        public String TimeZoneId { get; set; }

        /// <summary> Пароль </summary>
        public String UserPassword { get; set; }

        /// <summary> Активен </summary>
        public Boolean Active { get; set; }

        /// <summary> Синхронизировать с LDAP </summary>
        public Boolean SynchronizeWithLDAP { get; set; }

        /// <summary> Элемент LDAP </summary>
        public String LDAPEntry { get; set; }

        /// <summary> Id элемента LDAP </summary>
        public String LDAPEntryId { get; set; }

        /// <summary> Уникальное имя элемента LDAP </summary>
        public String LDAPEntryDN { get; set; }

        /// <summary> Вход выполнен </summary>
        public Boolean LoggedIn { get; set; }

        private Guid? _sysCultureId;
        /// <summary> Культура (Id) </summary>
        public Guid? SysCultureId { get => _sysCultureId; set => _sysCultureId = value.AsNullable(); }

        /// <summary> Культура </summary>
        public SysCulture SysCulture { get; set; }

        /// <summary> Количество попыток входа </summary>
        public Int32 LoginAttemptCount { get; set; }

        /// <summary> Логин к репозиторию </summary>
        public String SourceControlLogin { get; set; }

        /// <summary> Дата окончания действия пароля </summary>
        public DateTime PasswordExpireDate { get; set; }

        private Guid? _homePageId;
        /// <summary> Базовая страница (Id) </summary>
        public Guid? HomePageId { get => _homePageId; set => _homePageId = value.AsNullable(); }

        /// <summary> Базовая страница </summary>
        public SysModule HomePage { get; set; }

        /// <summary> Тип подключения </summary>
        public Int32 ConnectionType { get; set; }

        /// <summary> Время разблокировки </summary>
        public DateTime UnblockTime { get; set; }

        /// <summary> Сбросить пароль </summary>
        public Boolean ForceChangePassword { get; set; }

        private Guid? _dateTimeFormatId;
        /// <summary> Формат отображения даты и времени </summary>
        public Guid? DateTimeFormatId { get => _dateTimeFormatId; set => _dateTimeFormatId = value.AsNullable(); }

        /// <summary> Формат отображения даты и времени </summary>
        public SysLanguage DateTimeFormat { get; set; }

        /// <summary> Таймаут сессии (минуты) </summary>
        public Int32 SessionTimeout { get; set; }
    }
}
