using Beam.Marketplace;
using Nethereum.ABI.EIP712;
using Nethereum.RPC.AccountSigning;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Nethereum.Signer;
using Nethereum.Signer.EIP712;
using Nethereum.Web3.Accounts;

namespace Beam
{
    public class KeyPair
    {
        private static readonly X9ECParameters Curve = ECNamedCurveTable.GetByName("secp256k1");
        private static readonly ECDomainParameters DomainParams = new ECDomainParameters(Curve.Curve, Curve.G, Curve.N, Curve.H, Curve.GetSeed());

        private readonly ECPublicKeyParameters _public;
        private readonly ECPrivateKeyParameters _private;
        
        public Account Account => new Account(PrivateHex);

        public string PublicHex => _public.Q.GetEncoded(true).ToHex();
        public string PrivateHex => _private.D.ToHex();

        public static KeyPair Generate()
        {
            var secureRandom = new SecureRandom();
            var keyParams = new ECKeyGenerationParameters(DomainParams, secureRandom);

            var generator = new ECKeyPairGenerator("ECDSA");
            generator.Init(keyParams);
            return new KeyPair(generator.GenerateKeyPair());
        }

        public static KeyPair Load(string savedPrivateKey)
        {
            BigInteger privateKeyValue;
            try
            {
                privateKeyValue = new BigInteger(savedPrivateKey.TrimHexPrefix(), 16);
            }
            catch
            {
                return null;
            }
            var privateKey = new ECPrivateKeyParameters(privateKeyValue, DomainParams);

            var q = privateKey.Parameters.G.Multiply(privateKey.D);
            var publicKey = new ECPublicKeyParameters(privateKey.AlgorithmName, q, SecObjectIdentifiers.SecP256k1);
            var keyPair = new AsymmetricCipherKeyPair(publicKey, privateKey);

            return new KeyPair(keyPair);
        }

        public string Sign(byte[] msgBytes)
        {
            var signer = new EthereumMessageSigner();
            var ethEcKey = new EthECKey(_private.D.ToByteArray(), true);
            return signer.Sign(msgBytes, ethEcKey);
        }

        public string Sign(string message)
        {
            var signer = new EthereumMessageSigner();
            var ethEcKey = new EthECKey(_private.D.ToByteArray(), true);
            return signer.EncodeUTF8AndSign(message, ethEcKey);
        }
        
        public string SignTypedData(dynamic data)
        {
            var signer = new Eip712TypedDataSigner();
            var ethEcKey = new EthECKey(_private.D.ToByteArray(), true);
            return signer.SignTypedDataV4<SeaportTypedData>(data as TypedData<SeaportTypedData>, ethEcKey);
        }

        private KeyPair(AsymmetricCipherKeyPair keyPair)
        {
            _public = keyPair.Public as ECPublicKeyParameters;
            _private = keyPair.Private as ECPrivateKeyParameters;
        }
    }
}