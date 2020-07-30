
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
            List<Contractor> contractors = ClientOf1C.GetContractors(10, 5);

            foreach(Contractor x in contractors)
            {
                Console.WriteLine($"{x}\n\n");
            }
        }
    }
}
