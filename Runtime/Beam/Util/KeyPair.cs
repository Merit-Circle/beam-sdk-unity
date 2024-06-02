using System.Text;
using Nethereum.ABI.EIP712;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Beam.Util
{
    internal class KeyPair
    {
        private static readonly X9ECParameters Curve = ECNamedCurveTable.GetByName("secp256k1");

        private static readonly ECDomainParameters DomainParams =
            new ECDomainParameters(Curve.Curve, Curve.G, Curve.N, Curve.H, Curve.GetSeed());

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

        public string SignMessage(string message)
        {
            var signer = new EthereumMessageSigner();
            var ethEcKey = new EthECKey(_private.D.ToByteArray(), true);
            var msgBytes = message.HexToByteArray();
            var signature = signer.Sign(msgBytes, ethEcKey);

            return signature;
        }

        public string SignMarketplaceTransaction(string hash, string accountAddress, int chainId)
        {
            var signer = new EthereumMessageSigner();
            var ethEcKey = new EthECKey(_private.D.ToByteArray(), true);
            var msgBytes = Encoding.UTF8.GetBytes(hash); // todo: figure out why either doesn't work yet
            // var msgBytes = hash.HexToByteArray();
            var signature = signer.Sign(msgBytes, ethEcKey);

            return signature;
        }

        private KeyPair(AsymmetricCipherKeyPair keyPair)
        {
            _public = keyPair.Public as ECPublicKeyParameters;
            _private = keyPair.Private as ECPrivateKeyParameters;
        }
        
        public class BeamDomain : IDomain
        {
            [Parameter("string", "name", 1)]
            public virtual string Name { get; set; }

            [Parameter("string", "version", 2)]
            public virtual string Version { get; set; }

            [Parameter("uint256", "chainId", 3)]
            public virtual int ChainId { get; set; }

            [Parameter("address", "verifyingContract", 4)]
            public virtual string VerifyingContract { get; set; }
        }
    }
}