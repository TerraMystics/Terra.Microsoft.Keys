using NBitcoin;
using System;
using System.Threading.Tasks;
using Terra.Microsoft.Extensions.Security;
using Terra.Microsoft.Keys.Extensions;
using Terra.Microsoft.Extensions.StringExt;
using Terra.Microsoft.Keys.Constants;
using NBitcoin.Protocol;
using EllipticCurve;
using System.Diagnostics;
using PemUtils;
using System.IO;
using System.Security.Cryptography;

namespace Terra.Microsoft.Keys
{
    public class MnemonicKey : Key
    {
        private ExtKey logKey;
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
            Mnemonic mnemo;
            if (!string.IsNullOrWhiteSpace(mnemonicKey))
            {
                mnemo = new Mnemonic(mnemonicKey);
            }
            else
            {
                var mns = await MnemonicExtension.GenerateRandomMnemonic();
                mnemo = new Mnemonic(mns.Value);
            }

            ExtKey hdroot = logKey = mnemo.DeriveExtKey();
            var firstprivkey = hdroot.Derive(new KeyPath(DerivationPaths.DEFAULT_LUNA_PATH));

            this.privateKey = firstprivkey.PrivateKey.ToBytes();
            this.publicKey = new SimplePublicKey(firstprivkey.GetPublicKey().ToBytes());

            if (exposePrivateKey)
            {
                this.privateKeyExposed = this.privateKey;
            }
        }

        public string EcdsaSign(string payload)
        {
            var key = PrivateKey.fromString(TerraStringExtensions.GetBase64BytesFromBytes(this.privateKey));
            var signature = Ecdsa.sign(payload, key);
            Ecdsa.verify(payload, signature, key.publicKey());

            Debug.WriteLine("PUB KEY: " + payload);
            return signature.toBase64();
        }


        //    public ecdsaSign(payload: Buffer) : { signature: Uint8Array; recid: number } {
        //const hash = Buffer.from(
        //  SHA256.hash(new Word32Array(payload)).toString(),
        //  'hex'
        //);
        //return secp256k1.ecdsaSign(
        //  Uint8Array.from(hash),
        //  Uint8Array.from(this.privateKey)
        //);
        //    }


        public override async Task<string> Sign(string payload)
        {
            return this.EcdsaSign(payload);
        }
    }
}
