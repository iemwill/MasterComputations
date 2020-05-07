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
                var name = DateTime.Now.DayOfYear.ToString().Replace('.', '-').Replace(':', '-');
                var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\MC\\";
                Directory.CreateDirectory(path);
                var path2 = path + name + ".will";
                using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    Serializer.Serialize(fs, input);
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
