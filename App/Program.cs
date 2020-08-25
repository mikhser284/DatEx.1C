using System;
using System.Collections.Generic;
using System.Linq;
using DatEx.OneC;
using OneC = DatEx.OneC.DataModel;
using DatEx.Creatio;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
using System.Collections.Concurrent;

namespace App
{
    partial class Program
    {
        private static HttpClientOfCreatio CreatioHttpClient = 
            HttpClientOfCreatio.LogIn("http://185.59.101.152:51080/", "Supervisor", "Supervisor"); // Creatio-Dev1
            //HttpClientOfCreatio.LogIn("http://185.59.101.152:50080/", "Supervisor", "Supervisor"); // Creatio-Dev3
        public static HttpClientOfOneC OneCHttpClient =
            new HttpClientOfOneC(new HttpClientOfOneCSettings("http://185.59.101.152:51081/Dev01_1C/odata/standard.odata/", "Администратор", "")); // Creatio-Dev1
                                                                                                                                                   //new HttpClientOfOneC(new HttpClientOfOneCSettings("http://185.59.101.152:50081/Dev03_1C/odata/standard.odata/", "Администратор", "")); // Creatio-Dev3


        static void Main(string[] args)
        {
            SyncSettings settings = SyncSettings.GetDefaultSettings();
            SyncEmployees(settings);


        }
    }
}
