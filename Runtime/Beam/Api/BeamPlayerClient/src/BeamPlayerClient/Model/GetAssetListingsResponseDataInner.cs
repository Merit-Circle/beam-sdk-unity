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
    /// GetAssetListingsResponseDataInner
    /// </summary>
    [DataContract(Name = "GetAssetListingsResponse_data_inner")]
    public partial class GetAssetListingsResponseDataInner
    {
        /// <summary>
        /// Defines Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Active for value: active
            /// </summary>
            [EnumMember(Value = "active")]
            Active = 1,

            /// <summary>
            /// Enum Inactive for value: inactive
            /// </summary>
            [EnumMember(Value = "inactive")]
            Inactive = 2,

            /// <summary>
            /// Enum Expired for value: expired
            /// </summary>
            [EnumMember(Value = "expired")]
            Expired = 3,

            /// <summary>
            /// Enum Canceled for value: canceled
            /// </summary>
            [EnumMember(Value = "canceled")]
            Canceled = 4,

            /// <summary>
            /// Enum Filled for value: filled
            /// </summary>
            [EnumMember(Value = "filled")]
            Filled = 5
        }


        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = true)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAssetListingsResponseDataInner" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GetAssetListingsResponseDataInner() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAssetListingsResponseDataInner" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="side">side (required).</param>
        /// <param name="status">status.</param>
        /// <param name="assetAddress">assetAddress (required).</param>
        /// <param name="assetId">assetId (required).</param>
        /// <param name="contractKind">contractKind (required).</param>
        /// <param name="maker">maker (required).</param>
        /// <param name="price">price.</param>
        /// <param name="validFrom">validFrom.</param>
        /// <param name="validUntil">validUntil.</param>
        /// <param name="quantityFilled">quantityFilled.</param>
        /// <param name="quantityRemaining">quantityRemaining.</param>
        /// <param name="expiresAt">expiresAt.</param>
        /// <param name="createdAt">createdAt.</param>
        /// <param name="updatedAt">updatedAt.</param>
        public GetAssetListingsResponseDataInner(string id = default(string), string side = default(string), StatusEnum? status = default(StatusEnum?), string assetAddress = default(string), string assetId = default(string), string contractKind = default(string), string maker = default(string), GetAssetListingsResponseDataInnerPrice price = default(GetAssetListingsResponseDataInnerPrice), DateTime? validFrom = default(DateTime?), DateTime? validUntil = default(DateTime?), decimal quantityFilled = default(decimal), decimal quantityRemaining = default(decimal), DateTime? expiresAt = default(DateTime?), DateTime? createdAt = default(DateTime?), DateTime? updatedAt = default(DateTime?))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new ArgumentNullException("id is a required property for GetAssetListingsResponseDataInner and cannot be null");
            }
            this.Id = id;
            // to ensure "side" is required (not null)
            if (side == null)
            {
                throw new ArgumentNullException("side is a required property for GetAssetListingsResponseDataInner and cannot be null");
            }
            this.Side = side;
            // to ensure "assetAddress" is required (not null)
            if (assetAddress == null)
            {
                throw new ArgumentNullException("assetAddress is a required property for GetAssetListingsResponseDataInner and cannot be null");
            }
            this.AssetAddress = assetAddress;
            // to ensure "assetId" is required (not null)
            if (assetId == null)
            {
                throw new ArgumentNullException("assetId is a required property for GetAssetListingsResponseDataInner and cannot be null");
            }
            this.AssetId = assetId;
            // to ensure "contractKind" is required (not null)
            if (contractKind == null)
            {
                throw new ArgumentNullException("contractKind is a required property for GetAssetListingsResponseDataInner and cannot be null");
            }
            this.ContractKind = contractKind;
            // to ensure "maker" is required (not null)
            if (maker == null)
            {
                throw new ArgumentNullException("maker is a required property for GetAssetListingsResponseDataInner and cannot be null");
            }
            this.Maker = maker;
            this.Status = status;
            this.Price = price;
            this.ValidFrom = validFrom;
            this.ValidUntil = validUntil;
            this.QuantityFilled = quantityFilled;
            this.QuantityRemaining = quantityRemaining;
            this.ExpiresAt = expiresAt;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Side
        /// </summary>
        [DataMember(Name = "side", IsRequired = true, EmitDefaultValue = true)]
        public string Side { get; set; }

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
        /// Gets or Sets ContractKind
        /// </summary>
        [DataMember(Name = "contractKind", IsRequired = true, EmitDefaultValue = true)]
        public string ContractKind { get; set; }

        /// <summary>
        /// Gets or Sets Maker
        /// </summary>
        [DataMember(Name = "maker", IsRequired = true, EmitDefaultValue = true)]
        public string Maker { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        [DataMember(Name = "price", EmitDefaultValue = true)]
        public GetAssetListingsResponseDataInnerPrice Price { get; set; }

        /// <summary>
        /// Gets or Sets ValidFrom
        /// </summary>
        [DataMember(Name = "validFrom", EmitDefaultValue = true)]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// Gets or Sets ValidUntil
        /// </summary>
        [DataMember(Name = "validUntil", EmitDefaultValue = true)]
        public DateTime? ValidUntil { get; set; }

        /// <summary>
        /// Gets or Sets QuantityFilled
        /// </summary>
        [DataMember(Name = "quantityFilled", EmitDefaultValue = false)]
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// Gets or Sets QuantityRemaining
        /// </summary>
        [DataMember(Name = "quantityRemaining", EmitDefaultValue = false)]
        public decimal QuantityRemaining { get; set; }

        /// <summary>
        /// Gets or Sets ExpiresAt
        /// </summary>
        [DataMember(Name = "expiresAt", EmitDefaultValue = true)]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Gets or Sets CreatedAt
        /// </summary>
        [DataMember(Name = "createdAt", EmitDefaultValue = true)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedAt
        /// </summary>
        [DataMember(Name = "updatedAt", EmitDefaultValue = true)]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetAssetListingsResponseDataInner {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Side: ").Append(Side).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  AssetAddress: ").Append(AssetAddress).Append("\n");
            sb.Append("  AssetId: ").Append(AssetId).Append("\n");
            sb.Append("  ContractKind: ").Append(ContractKind).Append("\n");
            sb.Append("  Maker: ").Append(Maker).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  ValidFrom: ").Append(ValidFrom).Append("\n");
            sb.Append("  ValidUntil: ").Append(ValidUntil).Append("\n");
            sb.Append("  QuantityFilled: ").Append(QuantityFilled).Append("\n");
            sb.Append("  QuantityRemaining: ").Append(QuantityRemaining).Append("\n");
            sb.Append("  ExpiresAt: ").Append(ExpiresAt).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
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
