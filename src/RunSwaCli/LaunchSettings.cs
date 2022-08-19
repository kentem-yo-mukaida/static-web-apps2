using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunSwaCli
{
    public class LaunchSettings
    {
        public Profiles? Profiles { get; set; }
    }

    public class Profiles
    {
        public Api? Api { get; set; }
        public FunctionApp? FunctionApp1 { get; set; }
    }

    public class Api
    {
        public string? ApplicationUrl { get; set; }
    }

    public class FunctionApp
    {
        public string? CommandLineArgs { get; set; }
    }
}
