using System;
using System.Threading.Tasks;
using Terra.Microsoft.Keys.Constants;

namespace Terra.Microsoft.Keys
{
    public class CLIKey : Key
    {
        private string accAddress;
        public readonly CLIKeyParams @params;
        public CLIKey(CLIKeyParams @params) : base(null)
        {
            this.@params = @params;
            if (string.IsNullOrWhiteSpace(this.@params.CliPath))
            {
                this.@params.CliPath = TerraPubKeys.TERRA_KEY_NAME_D;
            }
        }

        public override Task<string> Sign(string payload)
        {
            throw new InvalidOperationException("CLIKey does not use sign() -- use createSignature() directly.");
        }

        public string GenerateCommand(string args)
        {
            return string.Concat(this.@params.CliPath, $" {args} --output json " +
                $"{(!string.IsNullOrWhiteSpace(this.@params.Home) ? $"--home {this.@params.Home}" : String.Empty)}");
        }
    }

    public class CLIKeyParams
    {
        public string KeyName { get; set; }
        public string Multisig { get; set; }
        public string CliPath { get; set; }
        public string Home { get; set; }
    }
}
