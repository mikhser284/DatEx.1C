using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DatEx._1C.DataModel
{
    public class Contractor
    {
        [JsonProperty("Ref_Key")]
        public Guid Id { get; set; }

        [JsonProperty("Predefined")]
        public Boolean Predefined { get; set; }

        [JsonProperty("PredefinedDataName")]
        public String PredefinedDataName { get; set; }

        [JsonProperty("DataVersion")]
        public String DataVersion { get; set; }

        [JsonProperty("Description")]
        public String Description { get; set; }


        [JsonProperty("Code")]
        public String Code { get; set; }

        [JsonProperty("Parent_Key")]
        public Guid ParentId { get; set; }

        [JsonProperty("IsFolder")]
        public Boolean IsFolder { get; set; }

        [JsonProperty("DeletionMark")]
        public Boolean DeletionMark { get; set; }


        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }

        [JsonProperty("Комментарий")]
        public String Comment { get; set; }

        [JsonProperty("ДополнительноеОписание")]
        public String AuxilaryDescription { get; set; }

        [JsonProperty("ГоловнойКонтрагент_Key")]
        public Guid PrimaryContractorId { get; set; }

        public override String ToString()
        {
            return ""
                + $"\nId:                 {Id}"
                + $"\nPredefined:         {Predefined}"
                + $"\nPredefinedDataName: {PredefinedDataName}"
                + $"\nDataVersion:        {DataVersion}"
                + $"\nDescription:        {Description}"
                + $"\nCode:               {Code}"
                + $"\nParentId:           {ParentId}"
                + $"\nIsFolder:           {IsFolder}"
                + $"\nDeletionMark:       {DeletionMark}"
                + $"\nFullName:           {FullName}"
                ;
        }
    }


    
}