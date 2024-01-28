using System.Data;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace ITE_Decoder_Tool
{
    public static class VersionInfo
    {
        public const string VersionString = "1.2.0";
    }

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }

    internal class Config
    {
        public bool Base64 { get; set; }
        public bool SakuraDecoding { get; set; }
        public bool SakuraLicensing { get; set; }
        public bool SakuraActions { get; set; }
        public Config(bool base64, bool sakuradecoding, bool sakuralicensing, bool sakuraactions)
        {
            Base64 = base64;
            SakuraDecoding = sakuradecoding;
            SakuraLicensing = sakuralicensing;
            SakuraActions = sakuraactions;
        }
    }

    internal static class ConfigOperations
    {
        internal static string[] splitParts(string filename)
        {
            string configRaw = File.ReadAllText(@filename);
            string[] parts = configRaw.Split("{\"__appconfig__\"}", StringSplitOptions.RemoveEmptyEntries);
            return parts;
        }
        public static Config? readConfig(string filename)
        {
            string appConfig = splitParts(@filename)[1];
            return JsonSerializer.Deserialize<Config>(appConfig);
        }
        public static void writeConfig(string filename, Config config)
        {
            string[] parts = splitParts(@filename);
            File.WriteAllText(filename, parts[0] + "{\"__appconfig__\"}" + JsonSerializer.Serialize(config));
        }
    }

    internal static class Utils
    {
        public static string unB64(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string B64(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                //msi.CopyTo(gs);
                CopyTo(msi, gs);
            }
            return mso.ToArray();
        }

        public static string unZip(byte[] bytes)
        {
            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            {
                //gs.CopyTo(mso);
                CopyTo(gs, mso);
            }
            return Encoding.UTF8.GetString(mso.ToArray());
        }
    }
}