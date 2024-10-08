/*
 * Player API
 *
 * The Player API is a service to integrate your game with Beam
 *
 * The version of the OpenAPI document: 1.0.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenAPIDateConverter = BeamPlayerClient.Client.OpenAPIDateConverter;

namespace BeamPlayerClient.Model
{
    /// <summary>
    /// GetAssetListingsResponseDataInnerPrice
    /// </summary>
    [DataContract(Name = "GetAssetListingsResponse_data_inner_price")]
    [UnityEngine.Scripting.Preserve]
    public partial class GetAssetListingsResponseDataInnerPrice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAssetListingsResponseDataInnerPrice" /> class.
        /// </summary>
        /// <param name="currency">currency.</param>
        /// <param name="amount">amount.</param>
        /// <param name="netAmount">netAmount.</param>
        [UnityEngine.Scripting.Preserve]
        public GetAssetListingsResponseDataInnerPrice(GetAssetListingsResponseDataInnerPriceCurrency currency = default(GetAssetListingsResponseDataInnerPriceCurrency), GetAssetListingsResponseDataInnerPriceAmount amount = default(GetAssetListingsResponseDataInnerPriceAmount), GetAssetListingsResponseDataInnerPriceAmount netAmount = default(GetAssetListingsResponseDataInnerPriceAmount))
        {
            this.Currency = currency;
            this.Amount = amount;
            this.NetAmount = netAmount;
        }

        /// <summary>
        /// Gets or Sets Currency
        /// </summary>
        [DataMember(Name = "currency", EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public GetAssetListingsResponseDataInnerPriceCurrency Currency { get; set; }

        /// <summary>
        /// Gets or Sets Amount
        /// </summary>
        [DataMember(Name = "amount", EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public GetAssetListingsResponseDataInnerPriceAmount Amount { get; set; }

        /// <summary>
        /// Gets or Sets NetAmount
        /// </summary>
        [DataMember(Name = "netAmount", EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public GetAssetListingsResponseDataInnerPriceAmount NetAmount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        [UnityEngine.Scripting.Preserve]
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetAssetListingsResponseDataInnerPrice {\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  NetAmount: ").Append(NetAmount).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        [UnityEngine.Scripting.Preserve]
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

    }

}
