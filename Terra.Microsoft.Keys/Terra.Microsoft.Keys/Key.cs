using Terra.Microsoft.ProtoBufs.third_party.proto.cosmos.tx.signing.v1beta1;
using Terra.Microsoft.ProtoBufs.proto.keys;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Terra.Microsoft.Extensions.StringExt;

namespace Terra.Microsoft.Keys
{
    public abstract class Key
    {
        public KeysDto publicKey;

        public Key() { }
        public Key(KeysDto publicKey)
        {
            this.publicKey = publicKey;
        }

        public abstract Task<byte[]> Sign(byte[] payload);
    }
}
