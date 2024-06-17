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
    /// CreateTransactionRequestInputInteractionsInner
    /// </summary>
    [DataContract(Name = "CreateTransactionRequestInput_interactions_inner")]
    public partial class CreateTransactionRequestInputInteractionsInner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTransactionRequestInputInteractionsInner" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CreateTransactionRequestInputInteractionsInner() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTransactionRequestInputInteractionsInner" /> class.
        /// </summary>
        /// <param name="contractAddress">contractAddress (required).</param>
        /// <param name="functionName">functionName (required).</param>
        /// <param name="functionArgs">functionArgs.</param>
        /// <param name="value">value.</param>
        public CreateTransactionRequestInputInteractionsInner(string contractAddress = default(string), string functionName = default(string), List<Object> functionArgs = default(List<Object>), string value = default(string))
        {
            // to ensure "contractAddress" is required (not null)
            if (contractAddress == null)
            {
                throw new ArgumentNullException("contractAddress is a required property for CreateTransactionRequestInputInteractionsInner and cannot be null");
            }
            this.ContractAddress = contractAddress;
            // to ensure "functionName" is required (not null)
            if (functionName == null)
            {
                throw new ArgumentNullException("functionName is a required property for CreateTransactionRequestInputInteractionsInner and cannot be null");
            }
            this.FunctionName = functionName;
            this.FunctionArgs = functionArgs;
            this.Value = value;
        }

        /// <summary>
        /// Gets or Sets ContractAddress
        /// </summary>
        [DataMember(Name = "contractAddress", IsRequired = true, EmitDefaultValue = true)]
        public string ContractAddress { get; set; }

        /// <summary>
        /// Gets or Sets FunctionName
        /// </summary>
        [DataMember(Name = "functionName", IsRequired = true, EmitDefaultValue = true)]
        public string FunctionName { get; set; }

        /// <summary>
        /// Gets or Sets FunctionArgs
        /// </summary>
        [DataMember(Name = "functionArgs", EmitDefaultValue = false)]
        public List<Object> FunctionArgs { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateTransactionRequestInputInteractionsInner {\n");
            sb.Append("  ContractAddress: ").Append(ContractAddress).Append("\n");
            sb.Append("  FunctionName: ").Append(FunctionName).Append("\n");
            sb.Append("  FunctionArgs: ").Append(FunctionArgs).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
