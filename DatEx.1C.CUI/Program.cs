
namespace DatEx._1C.CUI
{
    using System;
    using System.Collections.Generic;
    using DatEx._1C;
    using DatEx._1C.DataModel;

    class Program
    {
        public static ClientOf1C ClientOf1C;

        static void Main(string[] args)
        {
            SettingsForClientOf1C settings = new SettingsForClientOf1C("http://creatio-dev3:81/dev03_1C/odata/standard.odata/", "Администратор", "");
            ClientOf1C = new ClientOf1C(settings);
            //
            List<Contractor> contractors = ClientOf1C.GetContractors(0, 1);

            foreach(Contractor x in contractors)
            {
                Console.WriteLine(x);
            }
        }
    }
}
