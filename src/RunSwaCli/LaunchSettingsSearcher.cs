using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunSwaCli
{
    internal class LaunchSettingsSearcher
    {
        private string _path;

        public LaunchSettingsSearcher(string path)
        {
            _path = path;
        }

        public LaunchSettings? Get()
        {
            var fileName = Path.Combine(_path, "Properties", "launchSettings.json");
            if (File.Exists(fileName))
                return JsonConvert.DeserializeObject<LaunchSettings>(File.ReadAllText(fileName, Encoding.UTF8));
            return null;
        }
    }
}
