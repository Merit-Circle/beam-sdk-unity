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
    /// CreateAssetOfferRequestInput
    /// </summary>
    [DataContract(Name = "CreateAssetOfferRequestInput")]
    public partial class CreateAssetOfferRequestInput
    {
        /// <summary>
        /// Defines Currency
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CurrencyEnum
        {
            /// <summary>
            /// Enum WBEAM for value: WBEAM
            /// </summary>
            [EnumMember(Value = "WBEAM")]
            WBEAM = 1,

            /// <summary>
            /// Enum USDC for value: USDC
            /// </summary>
            [EnumMember(Value = "USDC")]
            USDC = 2,

            /// <summary>
            /// Enum USDT for value: USDT
            /// </summary>
            [EnumMember(Value = "USDT")]
            USDT = 3
        }


        /// <summary>
        /// Gets or Sets Currency
        /// </summary>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
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
        public OperationProcessingEnum? OperationProcessing { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAssetOfferRequestInput" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CreateAssetOfferRequestInput() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAssetOfferRequestInput" /> class.
        /// </summary>
        /// <param name="assetAddress">assetAddress (required).</param>
        /// <param name="assetId">assetId (required).</param>
        /// <param name="quantity">quantity (required).</param>
        /// <param name="price">price (required).</param>
        /// <param name="startTime">startTime.</param>
        /// <param name="endTime">endTime.</param>
        /// <param name="currency">currency (default to CurrencyEnum.WBEAM).</param>
        /// <param name="chainId">chainId (default to 13337M).</param>
        /// <param name="operationProcessing">operationProcessing (default to OperationProcessingEnum.Execute).</param>
        /// <param name="operationId">operationId.</param>
        public CreateAssetOfferRequestInput(string assetAddress = default(string), string assetId = default(string), decimal quantity = default(decimal), string price = default(string), DateTime startTime = default(DateTime), DateTime endTime = default(DateTime), CurrencyEnum? currency = CurrencyEnum.WBEAM, decimal chainId = 13337M, OperationProcessingEnum? operationProcessing = OperationProcessingEnum.Execute, string operationId = default(string))
        {
            // to ensure "assetAddress" is required (not null)
            if (assetAddress == null)
            {
                throw new ArgumentNullException("assetAddress is a required property for CreateAssetOfferRequestInput and cannot be null");
            }
            this.AssetAddress = assetAddress;
            // to ensure "assetId" is required (not null)
            if (assetId == null)
            {
                throw new ArgumentNullException("assetId is a required property for CreateAssetOfferRequestInput and cannot be null");
            }
            this.AssetId = assetId;
            this.Quantity = quantity;
            // to ensure "price" is required (not null)
            if (price == null)
            {
                throw new ArgumentNullException("price is a required property for CreateAssetOfferRequestInput and cannot be null");
            }
            this.Price = price;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Currency = currency;
            this.ChainId = chainId;
            this.OperationProcessing = operationProcessing;
            this.OperationId = operationId;
        }

        /// <summary>
        /// Gets or Sets AssetAddress
        /// </summary>
        [DataMember(Name = "assetAddress", IsRequired = true, EmitDefaultValue = true)]
        public string AssetAddress { get; set; }

        /// <summary>
        /// Gets or Sets AssetId
        /// </summary>
        [DataMember(Name = "assetId", IsRequired = true, EmitDefaultValue = true)]
        public string AssetId { get; set; }

        /// <summary>
        /// Gets or Sets Quantity
        /// </summary>
        [DataMember(Name = "quantity", IsRequired = true, EmitDefaultValue = true)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        [DataMember(Name = "price", IsRequired = true, EmitDefaultValue = true)]
        public string Price { get; set; }

        /// <summary>
        /// Gets or Sets StartTime
        /// </summary>
        [DataMember(Name = "startTime", EmitDefaultValue = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or Sets EndTime
        /// </summary>
        [DataMember(Name = "endTime", EmitDefaultValue = false)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or Sets ChainId
        /// </summary>
        [DataMember(Name = "chainId", EmitDefaultValue = false)]
        public decimal ChainId { get; set; }

        /// <summary>
        /// Gets or Sets OperationId
        /// </summary>
        [DataMember(Name = "operationId", EmitDefaultValue = true)]
        public string OperationId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateAssetOfferRequestInput {\n");
            sb.Append("  AssetAddress: ").Append(AssetAddress).Append("\n");
            sb.Append("  AssetId: ").Append(AssetId).Append("\n");
            sb.Append("  Quantity: ").Append(Quantity).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  StartTime: ").Append(StartTime).Append("\n");
            sb.Append("  EndTime: ").Append(EndTime).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
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
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

    }

}
