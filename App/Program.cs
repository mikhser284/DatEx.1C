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

            //SyncNomenclature(settings);

            //SyncEmployeesAndCareers(settings);

            try
            {
                ExcellWriter excellWriter = new ExcellWriter(@$"X:\Mappings\{DateTime.Now: yyyy.MM.dd HH-mm-ss} ContactsMappings.xlsx");
                excellWriter.SaveMappingsInfo();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
