using System.Data;
using System.IO;

namespace MasterComputations.Data
{
    public class Parse
    {
        public static DataTable csv(string path, char seperator)
        {
            DataTable dt = new DataTable();

            using (var fs = File.OpenRead(path))
            {
                using (var reader = new StreamReader(fs))
                {
                    string readHeader = reader.ReadLine();
                    if (readHeader != null)
                    {
                        string[] header = readHeader.Split(seperator);
                        int nSignals = header.GetLength(0);

                        for (int j = 0; j < nSignals; j++)
                            dt.Columns.Add(header[j], typeof(string));

                        while (!reader.EndOfStream)
                        {
                            string sRead = reader.ReadLine();
                            string[] splitArray = sRead.Split(seperator);
                            var row = new object[nSignals];

                            for (int j = 0; j < nSignals; j++)
                                row[j] = splitArray[j];

                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            return dt;
        }
    }
}
