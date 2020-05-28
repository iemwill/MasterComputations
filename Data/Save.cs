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
        public static bool options(Dictionary<string, Option> input)
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
        public static bool bookData(Dictionary<string, Option> input)
        {
            try
            {
                var path = Application.StartupPath + "\\data\\";
                Directory.CreateDirectory(path);
                var path2 = path + "BOP.data";
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
