namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Учетные записи во внешних ресурсах </summary>
    public class SocialAccount : BaseEntityNotes
    {
        /// <summary> Логин </summary>
        public String Login { get; set; }

        /// <summary> Общая </summary>
        public Boolean Public { get; set; }

        /// <summary> Access Token </summary>
        public String AccessToken { get; set; }

        /// <summary> Access Secret Token</summary>
        public String AccessSecretToken { get; set; }

        /// <summary> Consumer Key </summary>
        public String ConsumerKey { get; set; }

        /// <summary> Тип </summary>
        public CommunicationType Type { get; set; }

        /// <summary> Пользователь </summary>
        public SysAdminUnit User { get; set; }

        /// <summary> Пользоватлеь соц. сети </summary>
        public String SocialId { get; set; }

        /// <summary> Срок действия истек </summary>
        public Boolean IsExpired { get; set; }

        /// <summary> Название </summary>
        public String Name { get; set; }
    }
}
