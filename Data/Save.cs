using System;
using ProtoBuf;
using System.IO;
using System.Collections.Generic;

namespace MasterComputations.Data
{
    public class Save
    {
        public static bool currencies(List<Currency> input)
        {
            try
            {
                //var name = DateTime.Now.DayOfYear.ToString().Replace('.', '-').Replace(':', '-');
                var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\MC\\";
                Directory.CreateDirectory(path);
                var path2 = path + "currencies.will";
                using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    Serializer.Serialize(fs, input);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool optionsBTC(List<Instrument> input)
        {
            try
            {
                //var name = DateTime.Now.DayOfYear.ToString().Replace('.', '-').Replace(':', '-');
                var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\MC\\";
                Directory.CreateDirectory(path);
                var path2 = path + "optionsBTC.will";
                using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    Serializer.Serialize(fs, input);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool book(Dictionary<string,List<Book>> input)
        {
            try
            {
                //var name = DateTime.Now.DayOfYear.ToString().Replace('.', '-').Replace(':', '-');
                var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\MC\\";
                Directory.CreateDirectory(path);
                var path2 = path + "orderBook.will";
                using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    Serializer.Serialize(fs, input);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
