using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunSwaCli
{
    internal class BatchBuilder
    {
        public string? SolutionPath { get; set; }
        public string? ReactUrl { get; set; }
        public string? ApiUrl { get; set; }

        public string Build()
        {
            var lines = new List<string>();
            lines.Add($"cd /d {SolutionPath}");
            lines.Add($"swa start {ReactUrl} --api-location {ApiUrl} --open");
            return string.Join("\n", lines.ToArray());
        }
    }
}
