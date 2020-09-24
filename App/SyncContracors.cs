using App.Auxilary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OneS = DatEx.OneS.DataModel;

namespace App
{
    public partial class Program
    {
        public static void SyncContractors(SyncSettings settings, List<String> codesOfEdrpo)
        {
            Task<SyncObjs_SyncContractors> oneS_Contractors_GetSyncObjs = new Task<SyncObjs_SyncContractors>(() => OneS_GetSyncObjs_ContractorsInfo(settings, codesOfEdrpo));
            Task[] tasks = new Task[]
            {
                new Task(() => Creatio_Contractors_ClearSyncObjs(codesOfEdrpo)),
                oneS_Contractors_GetSyncObjs
            }.StartAndWaitForAll();

            SyncObjs_SyncContractors syncObjs = oneS_Contractors_GetSyncObjs.Result;

            Creatio_Contractors_FirstSyncWithOneS(syncObjs, settings);
        }
    }

    /// <summary> Работа с Creatio </summary>
    public partial class Program
    {
        public static void Creatio_Contractors_ClearSyncObjs(List<String> codesOfEdrpo)
        {
            
        }


        public static void Creatio_Contractors_FirstSyncWithOneS(SyncObjs_SyncContractors syncObjs, SyncSettings settings)
        {
            Console.WriteLine("НАЧАЛО СИНХРОНИЗАЦИИ");

            Console.WriteLine("Синхронизация контрагентов");
            Creatio_SyncContractors(syncObjs, settings);

            Console.WriteLine("СИНХРОНИЗАЦИЯ ЗАВЕРШЕНА");
        }


        public static void Creatio_SyncContractors(SyncObjs_SyncContractors syncObjs, SyncSettings settings)
        {
            //TODO Синхронизация контрагентов
        }
    }

    /// <summary> Работа с 1С </summary>
    public partial class Program
    {
        public static SyncObjs_SyncContractors OneS_GetSyncObjs_ContractorsInfo(SyncSettings settings, List<String> codesOfEdrpo)
        {
            SyncObjs_SyncContractors syncObjs = new SyncObjs_SyncContractors();

            Console.WriteLine("Получение контрагентов");
            OneS_GetSyncObjs_Contractors(settings, syncObjs, codesOfEdrpo);

            return syncObjs;
        }

        private static void OneS_GetSyncObjs_Contractors(SyncSettings settings, SyncObjs_SyncContractors syncObjs, List<String> codesOfEdrpo)
        {
            //TODO Получение контрагентов из 1С

            List<OneS.Contractor> contractors = HttpClientOfOneS.GetContractorsByCodeOfEdrpo(codesOfEdrpo);

        }
    }
}
