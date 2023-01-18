using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using Terra.Microsoft.Extensions.ProtoBufs;
using Terra.Microsoft.Extensions.StringExt;
using Terra.Microsoft.ProtoBufs.third_party.proto.cosmos.crypto.ed25519;
using Terra.Microsoft.Keys.Extensions;
using Terra.Microsoft.Keys.Constants;
using Terra.Microsoft.Extensions.Extension.Bech32;
using Terra.Microsoft.Extensions.Security;
using Terra.Microsoft.ProtoBufs.proto.keys;
using Terra.Microsoft.Rest.Staking;

namespace Terra.Microsoft.Keys
{
    public class ValConsPublicKey
    {
        public readonly string key;
        public ValConsPublicKey(string key)
        {
            this.key = key;
        }

        public ValConsPublicKeyAminoArgs ToAmino()
        {
            return new ValConsPublicKeyAminoArgs()
            {
                Key = this.key
            };
        }

        public ValConsPublicKeyDataArgs ToData()
        {
            return new ValConsPublicKeyDataArgs()
            {
                Key = this.key
            };
        }

        public PubKey ToProtoWithType()
        {
            return new PubKey()
            {
                Key = TerraStringExtensions.GetBase64BytesFromString(this.key)
            };
        }

        public byte[] ToProto()
        {
            return ProtoExtensions.SerialiseFromData(this.ToProtoWithType());
        }

        public static ValConsPublicKey FromAmino(ValConsPublicKeyAminoArgs data)
        {
            return new ValConsPublicKey(data.Key);
        }

        public static ValConsPublicKey FromData(ValConsPublicKeyDataArgs data)
        {
            return new ValConsPublicKey(data.Key);
        }

        public static ValConsPublicKey FromProto(PubKey data)
        {
            return new ValConsPublicKey(TerraStringExtensions.GetBase64FromBytes(data.Key));
        }

        public static ValConsPublicKey FromJSON(ValConsPublicKeyCommonArgsJSON data)
        {
            return new ValConsPublicKey(data.key);
        }

        public static ValConsPublicKey UnPackAny(Any msgAny)
        {
            return FromProto(ProtoExtensions.DeserialiseFromBytes<PubKey>(msgAny.Value));
        }

        public Any PackAny()
        {
            return new Any()
            {
                TypeUrl = CosmosKeys.ED25519_VAL_CONS_PUBKEY,
                Value = this.ToProto()
            };
        }

        public byte[] EncodeAminoPubkey()
        {
            var base64Data = PublicKeyExtensions.GetBase64DataFromKey(this.key);
            return PublicKeyExtensions.pubkeyAminoPrefixEd25519.MergeDataArrays(base64Data);
        }


        public byte[] RawAddress()
        {
            return HashExtensions.Ripemd(HashExtensions.Sha256(TerraStringExtensions.GetBase64BytesFromString(this.key)));
        }

        public string Address()
        {
            return Bech32Extensions.GetBech32Address(TerraPubKeys.TERRA_PUBLIC_KEYNAME, this.RawAddress());
        }

        public string PubKeyAddress()
        {
            return Bech32Extensions.GetBech32Address(TerraPubKeys.TERRA_PUB, this.RawAddress());
        }

        public KeysDto ToKeyProto()
        {
            return new KeysDto()
            {
                TypeUrl = CosmosKeys.ED25519_VAL_CONS_PUBKEY,
                RawPublicKey = TerraStringExtensions.GetBase64BytesFromString(key),
                Key = key,
            };
        }
    }

    public class ValConsPublicKeyAminoArgs : ValConsPublicKeyCommonArgs
    {
        public ValConsPublicKeyAminoArgs()
        {
            Type = TendermintKeys.TENDERMINT_VALCONS_PUBKEY;
        }
    }

    public class ValConsPublicKeyDataArgs : ValConsPublicKeyCommonArgs
    {
        public ValConsPublicKeyDataArgs()
        {
            Type = CosmosKeys.ED25519_VAL_CONS_PUBKEY;
        }
    }

    public class ValConsPublicKeyCommonArgs
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string Key { get; set; }
    }
}