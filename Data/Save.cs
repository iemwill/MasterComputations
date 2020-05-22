using System;
using ProtoBuf;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using MasterComputations.Classes;

namespace MasterComputations.Data
{
    public class Save
    {
        public static bool options(List<Option> input)
        {
            try
            {
                var path = Application.StartupPath + "\\data\\";
                Directory.CreateDirectory(path);
                var path2 = path + "bitcoin.options";
                using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    Serializer.Serialize(fs, input);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool currencies(List<Currency> input)
        {
            try
            {
                var path = Application.StartupPath + "\\data\\";
                Directory.CreateDirectory(path);
                var path2 = path + "currencies.deribit";
                using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    Serializer.Serialize(fs, input);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public static bool activeOptionsBTC(List<Instrument> input)
        //{
        //    try
        //    {
        //        var path = Application.StartupPath + "\\data\\";
        //        Directory.CreateDirectory(path);
        //        var path2 = path + "activeOptionsBTC.will";
        //        using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        //            Serializer.Serialize(fs, input);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static bool inactiveOptionsBTC(List<Instrument> input)
        //{
        //    try
        //    {
        //        var path = Application.StartupPath + "\\data\\";
        //        Directory.CreateDirectory(path);
        //        var path2 = path + "inactiveOptionsBTC.will";
        //        using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        //            Serializer.Serialize(fs, input);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static bool book(Dictionary<string, List<Book>> input)
        //{
        //    try
        //    {
        //        var path = Application.StartupPath + "\\data\\";
        //        Directory.CreateDirectory(path);
        //        var path2 = path + "orderBook.will";
        //        using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        //            Serializer.Serialize(fs, input);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static bool chartData(Dictionary<string, ChartData> input)
        //{
        //    try
        //    {
        //        var path = Application.StartupPath + "\\data\\";
        //        Directory.CreateDirectory(path);
        //        var path2 = path + "chartData.will";
        //        using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        //            Serializer.Serialize(fs, input);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
