
namespace DatEx._1C.CUI
{
    using System;
    using System.Collections.Generic;
    using DatEx._1C;
    using DatEx._1C.DataModel;

    class Program
    {
        private static SettingsForClientOf1C settings = new SettingsForClientOf1C("http://creatio-dev3:81/Dev03_1C/odata/standard.odata/", "Администратор", "");
        public static ClientOf1C ClientOf1C = new ClientOf1C(settings);

        static void Main(string[] args)
        {
            //GetContractorsByIdentifiers();
            //GetContractorsByIdentifier();
            //GetContractorsByCodesOfEdrpo();
            //GetContractorByCodesOfEdrpo();
            //ShowContractors();
            ShowEmployees();
            //GetGetContactInfo();
        }

        public static void ShowEmployees()
        {
            List<Guid> ids = ClientOf1C.GetIdsOfEmployees();

            ConsoleKeyInfo input;
            Int32 index = 0;
            Int32 count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Index: {index,4}; Count: {count,4}\n\n");
                List<Guid> idsPage = ids.GetRange(index, count);
                //ClientOf1C.GetEmployeesByIds(idsPage).ShowOneCObjects();
                ClientOf1C.GetEmployeesByIds(new Guid("f9e7b11f-609a-11e7-80cb-00155d65b717")).ShowOneCObjects();
                //ClientOf1C.GetEmployeesLike("Зорін").ShowOneCObjects();
                input = Console.ReadKey();
                index += count;
            } while (input.Key != ConsoleKey.Escape);
        }

        public static void ShowContractors()
        {
            List<Guid> ids = ClientOf1C.GetIdsOfContractors();

            ConsoleKeyInfo input;
            Int32 index = 0;
            Int32 count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Index: {index,4}; Count: {count,4}\n\n");
                List<Guid> idsPage = ids.GetRange(index, count);
                ClientOf1C.GetContracorsByIds(idsPage).ShowOneCObjects();
                input = Console.ReadKey();
                index += count;
            } while (input.Key != ConsoleKey.Escape);
        }

        public static void GetGetContactInfo()
        {
            ClientOf1C.GetContactInfo().ShowOneCObjects();
        }

        public static void GetContractorByCodesOfEdrpo()
        {
            ClientOf1C.GetContractorsByCodeOfEdrpo("40623794").ShowOneCObjects();
        }

        public static void GetContractorsByCodesOfEdrpo()
        {
            List<String> codesOfEdrpo = new List<string>
            {
                "40623794",
                "37652846",
                "39137820",
                "37630010",
                "40792278"
            };
            ClientOf1C.GetContractorsByCodeOfEdrpo(codesOfEdrpo).ShowOneCObjects();
        }

        public static void GetContractorsByIdentifier()
        {
            ClientOf1C.GetContracorsByIds(new Guid("848b5acf-83ed-11e6-80ba-00155d65b717")).ShowOneCObjects();
        }

        public static void GetContractorsByIdentifiers()
        {
            List<Guid> identifiers = new List<Guid>
            {
                new Guid("848b5acf-83ed-11e6-80ba-00155d65b717"),
                new Guid("059ee0c6-7ff2-11e6-80ba-00155d65b717"),
                new Guid("ffe137d9-7018-11e6-80b9-00155d65b717"),
                new Guid("3c2d8c7f-7670-11e6-80ba-00155d65b717"),
                new Guid("86569aec-7f0d-11e6-80ba-00155d65b717"),
            };
            ClientOf1C.GetContracorsByIds(identifiers).ShowOneCObjects();
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
