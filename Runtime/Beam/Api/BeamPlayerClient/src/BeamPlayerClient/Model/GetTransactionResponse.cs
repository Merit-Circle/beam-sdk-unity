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
    /// GetTransactionResponse
    /// </summary>
    [DataContract(Name = "GetTransactionResponse")]
    public partial class GetTransactionResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionResponse" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GetTransactionResponse() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionResponse" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="createdAt">createdAt (required).</param>
        /// <param name="updatedAt">updatedAt (required).</param>
        /// <param name="chainId">chainId (required).</param>
        /// <param name="intent">intent (required).</param>
        /// <param name="transaction">transaction.</param>
        /// <param name="policy">policy (required).</param>
        /// <param name="user">user (required).</param>
        public GetTransactionResponse(string id = default(string), DateTime createdAt = default(DateTime), DateTime updatedAt = default(DateTime), decimal chainId = default(decimal), GetTransactionsResponseDataInnerIntent intent = default(GetTransactionsResponseDataInnerIntent), GetTransactionsResponseDataInnerTransaction transaction = default(GetTransactionsResponseDataInnerTransaction), GetTransactionResponsePolicy policy = default(GetTransactionResponsePolicy), GetTransactionResponseUser user = default(GetTransactionResponseUser))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new ArgumentNullException("id is a required property for GetTransactionResponse and cannot be null");
            }
            this.Id = id;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.ChainId = chainId;
            // to ensure "intent" is required (not null)
            if (intent == null)
            {
                throw new ArgumentNullException("intent is a required property for GetTransactionResponse and cannot be null");
            }
            this.Intent = intent;
            // to ensure "policy" is required (not null)
            if (policy == null)
            {
                throw new ArgumentNullException("policy is a required property for GetTransactionResponse and cannot be null");
            }
            this.Policy = policy;
            // to ensure "user" is required (not null)
            if (user == null)
            {
                throw new ArgumentNullException("user is a required property for GetTransactionResponse and cannot be null");
            }
            this.User = user;
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
        /// Gets or Sets Policy
        /// </summary>
        [DataMember(Name = "policy", IsRequired = true, EmitDefaultValue = true)]
        public GetTransactionResponsePolicy Policy { get; set; }

        /// <summary>
        /// Gets or Sets User
        /// </summary>
        [DataMember(Name = "user", IsRequired = true, EmitDefaultValue = true)]
        public GetTransactionResponseUser User { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetTransactionResponse {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
            sb.Append("  ChainId: ").Append(ChainId).Append("\n");
            sb.Append("  Intent: ").Append(Intent).Append("\n");
            sb.Append("  Transaction: ").Append(Transaction).Append("\n");
            sb.Append("  Policy: ").Append(Policy).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
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