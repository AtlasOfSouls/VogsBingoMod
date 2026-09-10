/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.IO;
using System.Reflection;

namespace VogsBingoMod
{
    internal static class Resources
    {
        internal static byte[]? GetResourceAsByteArray(string fileName)
        {
            Assembly executeAssembly = Assembly.GetExecutingAssembly();
            Stream stream = executeAssembly.GetManifestResourceStream($"{fileName}");
            if (stream == null)
            {
                VogsBingoModPlugin.LogError($"Could not find the resource \"{fileName}\".");
                return null;
            }
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            stream.Dispose();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Dispose();
            return bytes;
        }
    }
}