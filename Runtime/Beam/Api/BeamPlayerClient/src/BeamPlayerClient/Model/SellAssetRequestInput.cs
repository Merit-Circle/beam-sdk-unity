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
    /// SellAssetRequestInput
    /// </summary>
    [DataContract(Name = "SellAssetRequestInput")]
    [UnityEngine.Scripting.Preserve]
    public partial class SellAssetRequestInput
    {
        /// <summary>
        /// Defines SellType
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SellTypeEnum
        {
            /// <summary>
            /// Enum FixedPrice for value: FixedPrice
            /// </summary>
            [EnumMember(Value = "FixedPrice")]
            FixedPrice = 1,

            /// <summary>
            /// Enum DescendingAuction for value: DescendingAuction
            /// </summary>
            [EnumMember(Value = "DescendingAuction")]
            DescendingAuction = 2,

            /// <summary>
            /// Enum AscendingAuction for value: AscendingAuction
            /// </summary>
            [EnumMember(Value = "AscendingAuction")]
            AscendingAuction = 3,

            /// <summary>
            /// Enum NotForSale for value: NotForSale
            /// </summary>
            [EnumMember(Value = "NotForSale")]
            NotForSale = 4
        }


        /// <summary>
        /// Gets or Sets SellType
        /// </summary>
        [DataMember(Name = "sellType", IsRequired = true, EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public SellTypeEnum SellType { get; set; }
        /// <summary>
        /// Defines Currency
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CurrencyEnum
        {
            /// <summary>
            /// Enum BEAM for value: BEAM
            /// </summary>
            [EnumMember(Value = "BEAM")]
            BEAM = 1,

            /// <summary>
            /// Enum IMX for value: IMX
            /// </summary>
            [EnumMember(Value = "IMX")]
            IMX = 2,

            /// <summary>
            /// Enum SOPH for value: SOPH
            /// </summary>
            [EnumMember(Value = "SOPH")]
            SOPH = 3,

            /// <summary>
            /// Enum WBEAM for value: WBEAM
            /// </summary>
            [EnumMember(Value = "WBEAM")]
            WBEAM = 4,

            /// <summary>
            /// Enum WIMX for value: WIMX
            /// </summary>
            [EnumMember(Value = "WIMX")]
            WIMX = 5,

            /// <summary>
            /// Enum WSOPH for value: WSOPH
            /// </summary>
            [EnumMember(Value = "WSOPH")]
            WSOPH = 6,

            /// <summary>
            /// Enum USDC for value: USDC
            /// </summary>
            [EnumMember(Value = "USDC")]
            USDC = 7
        }


        /// <summary>
        /// Gets or Sets Currency
        /// </summary>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        [UnityEngine.Scripting.Preserve]
        public CurrencyEnum? Currency { get; set; }
        /// <summary>
        /// Defines OperationProcessing
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum OperationProcessingEnum
        {
            /// <summary>
            /// Enum SignOnly for value: SignOnly
            /// </summary>
            [EnumMember(Value = "SignOnly")]
            SignOnly = 1,

            /// <summary>
            /// Enum Execute for value: Execute
            /// </summary>
            [EnumMember(Value = "Execute")]
            Execute = 2
        }


        /// <summary>
        /// Gets or Sets OperationProcessing
        /// </summary>
        [DataMember(Name = "operationProcessing", EmitDefaultValue = false)]
        [UnityEngine.Scripting.Preserve]
        public OperationProcessingEnum? OperationProcessing { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SellAssetRequestInput" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        [UnityEngine.Scripting.Preserve]
        protected SellAssetRequestInput() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SellAssetRequestInput" /> class.
        /// </summary>
        /// <param name="assetAddress">assetAddress (required).</param>
        /// <param name="assetId">assetId (required).</param>
        /// <param name="quantity">quantity (required).</param>
        /// <param name="price">price (required).</param>
        /// <param name="startTime">Date time string with YYYY-MM-DDTHH:mm:ss.sssZ format or Unix timestamp in milliseconds.</param>
        /// <param name="endTime">Date time string with YYYY-MM-DDTHH:mm:ss.sssZ format or Unix timestamp in milliseconds.</param>
        /// <param name="sellType">sellType (required).</param>
        /// <param name="currency">currency (default to CurrencyEnum.BEAM).</param>
        /// <param name="sponsor">sponsor (default to true).</param>
        /// <param name="policyId">policyId.</param>
        /// <param name="chainId">chainId (default to 13337M).</param>
        /// <param name="operationProcessing">operationProcessing (default to OperationProcessingEnum.Execute).</param>
        /// <param name="operationId">operationId.</param>
        [UnityEngine.Scripting.Preserve]
        public SellAssetRequestInput(string assetAddress = default(string), string assetId = default(string), decimal quantity = default(decimal), string price = default(string), DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), SellTypeEnum sellType = default(SellTypeEnum), CurrencyEnum? currency = CurrencyEnum.BEAM, bool sponsor = true, string policyId = default(string), decimal chainId = 13337M, OperationProcessingEnum? operationProcessing = OperationProcessingEnum.Execute, string operationId = default(string))
        {
            // to ensure "assetAddress" is required (not null)
            if (assetAddress == null)
            {
                throw new ArgumentNullException("assetAddress is a required property for SellAssetRequestInput and cannot be null");
            }
            this.AssetAddress = assetAddress;
            // to ensure "assetId" is required (not null)
            if (assetId == null)
            {
                throw new ArgumentNullException("assetId is a required property for SellAssetRequestInput and cannot be null");
            }
            this.AssetId = assetId;
            this.Quantity = quantity;
            // to ensure "price" is required (not null)
            if (price == null)
            {
                throw new ArgumentNullException("price is a required property for SellAssetRequestInput and cannot be null");
            }
            this.Price = price;
            this.SellType = sellType;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Currency = currency;
            this.Sponsor = sponsor;
            this.PolicyId = policyId;
            this.ChainId = chainId;
            this.OperationProcessing = operationProcessing;
            this.OperationId = operationId;
        }

        /// <summary>
        /// Gets or Sets AssetAddress
        /// </summary>
        [DataMember(Name = "assetAddress", IsRequired = true, EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public string AssetAddress { get; set; }

        /// <summary>
        /// Gets or Sets AssetId
        /// </summary>
        [DataMember(Name = "assetId", IsRequired = true, EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public string AssetId { get; set; }

        /// <summary>
        /// Gets or Sets Quantity
        /// </summary>
        [DataMember(Name = "quantity", IsRequired = true, EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        [DataMember(Name = "price", IsRequired = true, EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public string Price { get; set; }

        /// <summary>
        /// Date time string with YYYY-MM-DDTHH:mm:ss.sssZ format or Unix timestamp in milliseconds
        /// </summary>
        /// <value>Date time string with YYYY-MM-DDTHH:mm:ss.sssZ format or Unix timestamp in milliseconds</value>
        [DataMember(Name = "startTime", EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Date time string with YYYY-MM-DDTHH:mm:ss.sssZ format or Unix timestamp in milliseconds
        /// </summary>
        /// <value>Date time string with YYYY-MM-DDTHH:mm:ss.sssZ format or Unix timestamp in milliseconds</value>
        [DataMember(Name = "endTime", EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or Sets Sponsor
        /// </summary>
        [DataMember(Name = "sponsor", EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public bool Sponsor { get; set; }

        /// <summary>
        /// Gets or Sets PolicyId
        /// </summary>
        [DataMember(Name = "policyId", EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public string PolicyId { get; set; }

        /// <summary>
        /// Gets or Sets ChainId
        /// </summary>
        [DataMember(Name = "chainId", EmitDefaultValue = false)]
        [UnityEngine.Scripting.Preserve]
        public decimal ChainId { get; set; }

        /// <summary>
        /// Gets or Sets OperationId
        /// </summary>
        [DataMember(Name = "operationId", EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public string OperationId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        [UnityEngine.Scripting.Preserve]
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class SellAssetRequestInput {\n");
            sb.Append("  AssetAddress: ").Append(AssetAddress).Append("\n");
            sb.Append("  AssetId: ").Append(AssetId).Append("\n");
            sb.Append("  Quantity: ").Append(Quantity).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  StartTime: ").Append(StartTime).Append("\n");
            sb.Append("  EndTime: ").Append(EndTime).Append("\n");
            sb.Append("  SellType: ").Append(SellType).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  Sponsor: ").Append(Sponsor).Append("\n");
            sb.Append("  PolicyId: ").Append(PolicyId).Append("\n");
            sb.Append("  ChainId: ").Append(ChainId).Append("\n");
            sb.Append("  OperationProcessing: ").Append(OperationProcessing).Append("\n");
            sb.Append("  OperationId: ").Append(OperationId).Append("\n");
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
