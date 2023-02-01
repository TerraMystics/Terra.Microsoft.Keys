using System.Threading.Tasks;
using System;

namespace Terra.Microsoft.Keys
{
    public abstract class Key
    {
        public SimplePublicKey publicKey;

        public Key() { }
        public Key(SimplePublicKey publicKey)
        {
            this.publicKey = publicKey;
        }

        public abstract Task<string> Sign(string payload);

        public string AccAddress
        {
            get
            {
                if (this.publicKey == null)
                {
                    throw new Exception("Could not compute accAddress: missing rawAddress");
                }

                return this.publicKey.Address();

            }
        }

        public string ValAddress
        {
            get
            {
                if (this.publicKey == null)
                {
                    throw new Exception("Could not compute accAddress: missing rawAddress");
                }

                return this.publicKey.PubKeyAddress();
            }
        }
    }
}
