using NBitcoin;
using Terra.Microsoft.ProtoBufs.proto.keys;
using Cryptography.ECDSA;
using System;
using System.Threading.Tasks;
using Terra.Microsoft.Extensions.Security;
using Terra.Microsoft.Keys.Extensions;
using Terra.Microsoft.Extensions.StringExt;
using Terra.Microsoft.Keys.Constants;

namespace Terra.Microsoft.Keys
{
    public class MnemonicKey : Key
    {
        private byte[] privateKey;
        public byte[] privateKeyExposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mnemonicKey"></param>
        public MnemonicKey(string mnemonicKey = "", bool exposePrivateKey = false)
        {
            PrepareMnemonic(mnemonicKey, exposePrivateKey);
        }

        private async void PrepareMnemonic(string mnemonicKey, bool exposePrivateKey)
        {
            if (!string.IsNullOrWhiteSpace(mnemonicKey))
            {
                Mnemonic mnemo = new Mnemonic(mnemonicKey);
                ExtKey hdroot = mnemo.DeriveExtKey();
                var firstprivkey = hdroot.Derive(new KeyPath(DerivationPaths.DEFAULT_LUNA_PATH));

                this.privateKey = firstprivkey.PrivateKey.ToBytes();
                this.publicKey = GetSimpleKey();
            }
            else
            {
                var mns = await MnemonicExtension.GenerateRandomMnemonic();

                Mnemonic mnemo = new Mnemonic(mns.Value);
                ExtKey hdroot = mnemo.DeriveExtKey();
                var firstprivkey = hdroot.Derive(new KeyPath(DerivationPaths.DEFAULT_LUNA_PATH));

                this.privateKey = firstprivkey.PrivateKey.ToBytes();
                this.publicKey = GetSimpleKey();
            }

            if (exposePrivateKey)
            {
                this.privateKeyExposed = this.privateKey;
            }
        }

        public KeysDto GetSimpleKey()
        {
            var pubKey = Secp256K1Manager.GetPublicKey(this.privateKey, true);
            return new KeysDto()
            {
                RawPublicKey = pubKey,
                Key = TerraStringExtensions.GetBase64FromBytes(pubKey),
                TypeUrl = CosmosKeys.SECP256K1_SIMP_PUBKEY,
            };
        }

        public byte[] EcdsaSign(byte[] payload)
        {
            var data = TerraStringExtensions.GetBytesFromString(HashExtensions.HashToHex(TerraStringExtensions.GetBase64FromBytes(payload)));
            return Secp256K1Manager.SignCompressedCompact(data, this.privateKey);
        }

        public override async Task<byte[]> Sign(byte[] payload)
        {
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(payload);

            return this.EcdsaSign(payload);
        }
    }
}
