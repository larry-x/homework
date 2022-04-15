using System.IO;

namespace CUSC_larry
{
    public static class Utilities
    {
        public static byte[] LoadDatFile(string filename)
        {
            byte[] data;

            using (FileStream fs = File.OpenRead(filename))
            using (BinaryReader br = new BinaryReader(fs))
            {
                return data = br.ReadBytes((int)fs.Length);
            }
        }
    }
}