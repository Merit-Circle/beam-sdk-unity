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
    /// GetTransactionsResponseDataInner
    /// </summary>
    [DataContract(Name = "GetTransactionsResponse_data_inner")]
    public partial class GetTransactionsResponseDataInner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionsResponseDataInner" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GetTransactionsResponseDataInner() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionsResponseDataInner" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="createdAt">createdAt (required).</param>
        /// <param name="updatedAt">updatedAt (required).</param>
        /// <param name="chainId">chainId (required).</param>
        /// <param name="intent">intent (required).</param>
        /// <param name="transaction">transaction.</param>
        public GetTransactionsResponseDataInner(string id = default(string), DateTime createdAt = default(DateTime), DateTime updatedAt = default(DateTime), decimal chainId = default(decimal), GetTransactionsResponseDataInnerIntent intent = default(GetTransactionsResponseDataInnerIntent), GetTransactionsResponseDataInnerTransaction transaction = default(GetTransactionsResponseDataInnerTransaction))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new ArgumentNullException("id is a required property for GetTransactionsResponseDataInner and cannot be null");
            }
            this.Id = id;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.ChainId = chainId;
            // to ensure "intent" is required (not null)
            if (intent == null)
            {
                throw new ArgumentNullException("intent is a required property for GetTransactionsResponseDataInner and cannot be null");
            }
            this.Intent = intent;
            this.Transaction = transaction;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets CreatedAt
        /// </summary>
        [DataMember(Name = "createdAt", IsRequired = true, EmitDefaultValue = true)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedAt
        /// </summary>
        [DataMember(Name = "updatedAt", IsRequired = true, EmitDefaultValue = true)]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or Sets ChainId
        /// </summary>
        [DataMember(Name = "chainId", IsRequired = true, EmitDefaultValue = true)]
        public decimal ChainId { get; set; }

        /// <summary>
        /// Gets or Sets Intent
        /// </summary>
        [DataMember(Name = "intent", IsRequired = true, EmitDefaultValue = true)]
        public GetTransactionsResponseDataInnerIntent Intent { get; set; }

        /// <summary>
        /// Gets or Sets Transaction
        /// </summary>
        [DataMember(Name = "transaction", EmitDefaultValue = true)]
        public GetTransactionsResponseDataInnerTransaction Transaction { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetTransactionsResponseDataInner {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
            sb.Append("  ChainId: ").Append(ChainId).Append("\n");
            sb.Append("  Intent: ").Append(Intent).Append("\n");
            sb.Append("  Transaction: ").Append(Transaction).Append("\n");
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
