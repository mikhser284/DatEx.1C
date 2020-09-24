using System;
using System.Collections.Generic;
using System.Linq;
using DatEx.OneS;
using OneC = DatEx.OneS.DataModel;
using DatEx.Creatio;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
using System.Collections.Concurrent;

namespace App
{
    [System.Runtime.InteropServices.Guid("D4741F7B-5AA3-470B-A935-B26494017ED3")]
    partial class Program
    {
        private static HttpClientOfCreatio HttpClientOfCreatio = 
            HttpClientOfCreatio.LogIn("http://185.59.101.152:51080/", "Supervisor", "Supervisor"); // Creatio-Dev1
            //HttpClientOfCreatio.LogIn("http://185.59.101.152:50080/", "Supervisor", "Supervisor"); // Creatio-Dev3
        public static HttpClientOfOneC HttpClientOfOneS =
            new HttpClientOfOneC(new HttpClientOfOneCSettings("http://185.59.101.152:51081/Dev01_1C/odata/standard.odata/", "Администратор", "")); // Creatio-Dev1
            //new HttpClientOfOneC(new HttpClientOfOneCSettings("http://185.59.101.152:50081/Dev03_1C/odata/standard.odata/", "Администратор", "")); // Creatio-Dev3


        static void Main(string[] args)
        {
            SyncSettings settings = SyncSettings.GetDefaultSettings();


            try
            {
                //var unit = HttpClientOfCreatio.GetSingleObjById<ITIS.Unit>(new Guid("4cd297f2-3d10-4bce-b8a5-9a9465e6d93f"));
                //var group = HttpClientOfCreatio.GetSingleObjById<ITIS.ITISNomenclatureGroups>(new Guid("df7e2d21-643b-409a-927c-a6f59c248474"));
                //var contact = HttpClientOfCreatio.GetSingleObjById<ITIS.Contact>(new Guid("410006e1-ca4e-4502-a9ec-e54d922d2c00"));

                //ITIS.ITISCompaniesNomenclature nmc = new ITIS.ITISCompaniesNomenclature
                //{
                //    ITISName = "Елемент фільтра паливного",
                //    ITISNotes = "Примечания",
                //    ITISOneCCode = "8765431",
                //    ITISNomenclatureUnitOfMeasurementId = new Guid("4cd297f2-3d10-4bce-b8a5-9a9465e6d93f"),
                //    ITISNomenclaturesGroupId = new Guid("df7e2d21-643b-409a-927c-a6f59c248474"),
                //    ITISOneSId = new Guid("2c214b1a-49a9-486f-8062-2fd9022db7a9"),
                //    ITISOwnerId = new Guid("410006e1-ca4e-4502-a9ec-e54d922d2c00")
                    
                //};

                //nmc = HttpClientOfCreatio.CreateObj(nmc);

                ExcellWriter excellWriter = new ExcellWriter(@$"X:\Mappings\{DateTime.Now: yyyy.MM.dd HH-mm-ss} Mappings__OneS_Creatio.xlsx");
                excellWriter.SaveMappingsInfo();

                SyncContractors(settings, new List<string> { "38500619" });

                //SyncNomenclature(settings);

                //SyncEmployeesAndCareers(settings);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
