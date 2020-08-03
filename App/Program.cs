using System;
using System.Collections.Generic;
using System.Linq;
using DatEx._1C;
using DatEx._1C.DataModel;
using DatEx.Creatio;
using DatEx.Creatio.DataModel.Terrasoft.Base;
using ITIS = DatEx.Creatio.DataModel.ITIS;

namespace App
{
    class Program
    {
        private static ClientOfCreatio CreatioHttpClient = ClientOfCreatio.LogIn("http://creatio-dev3/", "Supervisor", "Supervisor");
        private static SettingsForClientOf1C settings = new SettingsForClientOf1C("http://creatio-dev3:81/Dev03_1C/odata/standard.odata/", "Администратор", "");
        public static ClientOf1C OneCHttpClient = new ClientOf1C(settings);

        

        static void Main(string[] args)
        {
            var contacts = CreatioHttpClient.ODataGet<ITIS.Contact>();
            var mZorin = contacts.FirstOrDefault(x => x.Surname.Contains("Зорін"));
            //OneCGetContractorsByIdentifiers();
            //OneCGetContractorsByIdentifier();
            //OneCGetContractorsByCodesOfEdrpo();
            //OneCGetContractorByCodesOfEdrpo();
            //OneCGetEmployee();
            //OneCGetGetContactInfo();
        }

        public static void OneCGetEmployee()
        {
            OneCHttpClient.GetEmployees().ShowOneCObjects();
        }

        public static void OneCGetGetContactInfo()
        {
            OneCHttpClient.GetContactInfo().ShowOneCObjects();
        }

        public static void OneCGetContractorByCodesOfEdrpo()
        {
            OneCHttpClient.GetContractorsByCodeOfEdrpo("40623794").ShowOneCObjects();
        }

        public static void OneCGetContractorsByCodesOfEdrpo()
        {
            List<String> codesOfEdrpo = new List<string>
            {
                "40623794",
                "37652846",
                "39137820",
                "37630010",
                "40792278"
            };
            OneCHttpClient.GetContractorsByCodeOfEdrpo(codesOfEdrpo).ShowOneCObjects();
        }

        public static void OneCGetContractorsByIdentifier()
        {
            OneCHttpClient.GetContracorsByIds(new Guid("848b5acf-83ed-11e6-80ba-00155d65b717")).ShowOneCObjects();
        }

        public static void OneCGetContractorsByIdentifiers()
        {
            List<Guid> identifiers = new List<Guid>
            {
                new Guid("848b5acf-83ed-11e6-80ba-00155d65b717"),
                new Guid("059ee0c6-7ff2-11e6-80ba-00155d65b717"),
                new Guid("ffe137d9-7018-11e6-80b9-00155d65b717"),
                new Guid("3c2d8c7f-7670-11e6-80ba-00155d65b717"),
                new Guid("86569aec-7f0d-11e6-80ba-00155d65b717"),
            };
            OneCHttpClient.GetContracorsByIds(identifiers).ShowOneCObjects();
        }

    }

    public static class Ext_OneCObject
    {
        public static void ShowOneCObjects<T>(this IEnumerable<T> objects) where T : OneCBase
        {
            foreach(T obj in objects)
            {
                obj.Show();
                Console.WriteLine($"\n\n");
            }
        }
    }
}
