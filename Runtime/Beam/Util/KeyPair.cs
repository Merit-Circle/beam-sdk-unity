using Beam.Extensions;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Model;
using Nethereum.Signer;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Account = Nethereum.Web3.Accounts.Account;

namespace Beam.Util
{
    public class KeyPair
    {
        private static readonly X9ECParameters Curve = ECNamedCurveTable.GetByName("secp256k1");

        private static readonly ECDomainParameters DomainParams =
            new(Curve.Curve, Curve.G, Curve.N, Curve.H, Curve.GetSeed());

        private readonly ECPublicKeyParameters _public;
        private readonly ECPrivateKeyParameters _private;

        public Account Account => new(PrivateHex);

        public string PublicHex => ByteExtensions.ToHex(_public.Q.GetEncoded(true));
        public string PrivateHex => _private.D.ToHex();

        internal static KeyPair Generate()
        {
            var secureRandom = new SecureRandom();
            var keyParams = new ECKeyGenerationParameters(DomainParams, secureRandom);

            var generator = new ECKeyPairGenerator("ECDSA");
            generator.Init(keyParams);
            return new KeyPair(generator.GenerateKeyPair());
        }

        internal static KeyPair Load(string savedPrivateKey)
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

        /// <summary>
        /// Signs message as a regular Ethereum message
        /// </summary>
        /// <param name="message">Message/hash to sign</param>
        /// <returns></returns>
        internal string SignMessage(string message)
        {
            var signer = new EthereumMessageSigner();
            var ethEcKey = new EthECKey(_private.D.ToByteArray(), true);
            var msgBytes = message.HexToByteArray();
            var signature = signer.Sign(msgBytes, ethEcKey);

            return signature;
        }

        /// <summary>
        /// Signs an Ethereum of a Marketplace Listing related Typed Data. Skips adding Ethereum message prefix.
        /// </summary>
        /// <param name="hash">Ethereum valid hash of TypedData</param>
        /// <returns></returns>
        internal string SignMarketplaceTransactionHash(string hash)
        {
            var ethEcKey = new EthECKey(_private.D.ToByteArray(), true);
            var msgBytes = hash.HexToByteArray();
            var signature = ethEcKey.SignAndCalculateV(msgBytes).CreateStringSignature();

            return signature;
        }

        private KeyPair(AsymmetricCipherKeyPair keyPair)
        {
            _public = keyPair.Public as ECPublicKeyParameters;
            _private = keyPair.Private as ECPrivateKeyParameters;
        }
    }
}