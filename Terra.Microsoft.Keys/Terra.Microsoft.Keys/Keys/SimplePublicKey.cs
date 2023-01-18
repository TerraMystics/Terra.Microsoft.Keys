using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using Terra.Microsoft.Extensions.ProtoBufs;
using Terra.Microsoft.Extensions.StringExt;
using Terra.Microsoft.ProtoBufs.third_party.proto.cosmos.crypto.secp256k1;
using Terra.Microsoft.Keys.Constants;
using Terra.Microsoft.Keys.Extensions;
using Terra.Microsoft.Extensions.Security;
using Terra.Microsoft.ProtoBufs.proto.keys;
using Terra.Microsoft.Extensions.Extension.Bech32;

namespace Terra.Microsoft.Keys
{
    public class SimplePublicKey
    {
        public readonly byte[] key;
        public SimplePublicKey(byte[] key)
        {
            this.key = key;
        }

        public Any PackAny()
        {
            return new Any()
            {
                TypeUrl = CosmosKeys.SECP256K1_SIMP_PUBKEY,
                Value = this.ToProto()
            };
        }

        public static SimplePublicKey UnPackAny(Any msgAny)
        {
            return FromProto(ProtoExtensions.DeserialiseFromBytes<PubKey>(msgAny.Value));
        }

        public SimplePublicKeyDataArgs ToData()
        {
            return new SimplePublicKeyDataArgs()
            {
                Type = CosmosKeys.SECP256K1_SIMP_PUBKEY,
                Key = this.key
            };
        }

        public PubKey ToProtoWithType()
        {
            return new PubKey()
            {
                Key = this.key
            };
        }

        public byte[] ToProto()
        {
            return ProtoExtensions.SerialiseFromData(this.ToProtoWithType());
        }


        public KeysDto ToProtoDto(bool amino = false)
        {
            return new KeysDto()
            {
                Key = TerraStringExtensions.GetBase64FromBytes(key),
                RawPublicKey = key,
                TypeUrl = amino ? TendermintKeys.TENDERMINT_SIMPLE_PUBKEY : CosmosKeys.SECP256K1_SIMP_PUBKEY
            };
        }

        public SimplePublicKeyAminoArgs ToAmino()
        {
            return new SimplePublicKeyAminoArgs()
            {
                Key = this.key
            };
        }

        public static SimplePublicKey FromAmino(SimplePublicKeyAminoArgs data)
        {
            return new SimplePublicKey(data.Key);
        }

        public static SimplePublicKey FromData(SimplePublicKeyDataArgs data)
        {
            return new SimplePublicKey(data.Key);
        }

        public static SimplePublicKey FromProto(PubKey data)
        {
            return new SimplePublicKey(data.Key);
        }

        public byte[] EncodeAminoPubkey()
        {
            return PublicKeyExtensions.pubkeyAminoPrefixSecp256k1.MergeDataArrays(this.key);
        }

        public byte[] RawAddress()
        {
            return HashExtensions.Ripemd(HashExtensions.Sha256(this.key));
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
                TypeUrl = CosmosKeys.SECP256K1_SIMP_PUBKEY,
                RawPublicKey = key,
                Key = TerraStringExtensions.GetBase64FromBytes(key),
            };
        }
    }

    public class SimplePublicKeyAminoArgs : SimplePublicKeyCommonArgs
    {
        public SimplePublicKeyAminoArgs()
        {
            Type = TendermintKeys.TENDERMINT_SIMPLE_PUBKEY;
        }
    }

    public class SimplePublicKeyDataArgs : SimplePublicKeyCommonArgs
    {
        public SimplePublicKeyDataArgs()
        {
            Type = CosmosKeys.SECP256K1_SIMP_PUBKEY;
        }
    }

    public class SimplePublicKeyCommonArgs
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public byte[] Key { get; set; }
    }
}
