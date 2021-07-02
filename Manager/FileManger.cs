using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tracker.Models;

namespace Tracker.Manager
{
    public static class FileManger
    {
        private const string Root = "wwwroot/Content/FootPrint.txt";

        public static async Task Save(object footPrint)
        {
            await using StreamWriter file = new(Root, append: true);
            await file.WriteLineAsync(JsonConvert.SerializeObject(footPrint));
        }

        public static async Task<IEnumerable<FootPrint>> GetFootPrints()
        {
            using var reader = new StreamReader(Root);
            var data = await reader.ReadToEndAsync();
            var splited = data.Split("\r\n");
            var footPrints = splited.Select(x => JsonConvert.DeserializeObject<FootPrint>(x))
                .Where(x => x != null).Reverse();
            return footPrints;
        }
    }
}