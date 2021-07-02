using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tracker.Manager
{
    public static class FileManger
    {
        private const string Root = "wwwroot/Content/FootPrint.txt";

        public static async Task Save(object footPrint)
        {
            await using StreamWriter file = new(Root, append: true);
            await file.WriteLineAsync(JsonSerializer.Serialize(footPrint));
        }
    }
}