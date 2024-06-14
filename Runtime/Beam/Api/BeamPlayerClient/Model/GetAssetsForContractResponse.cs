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
    /// GetAssetsForContractResponse
    /// </summary>
    [DataContract(Name = "GetAssetsForContractResponse")]
    public partial class GetAssetsForContractResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAssetsForContractResponse" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GetAssetsForContractResponse() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAssetsForContractResponse" /> class.
        /// </summary>
        /// <param name="data">data (required).</param>
        /// <param name="continuation">continuation.</param>
        public GetAssetsForContractResponse(List<GetAssetsForContractResponseDataInner> data = default(List<GetAssetsForContractResponseDataInner>), string continuation = default(string))
        {
            // to ensure "data" is required (not null)
            if (data == null)
            {
                throw new ArgumentNullException("data is a required property for GetAssetsForContractResponse and cannot be null");
            }
            this.Data = data;
            this.Continuation = continuation;
        }

        /// <summary>
        /// Gets or Sets Data
        /// </summary>
        [DataMember(Name = "data", IsRequired = true, EmitDefaultValue = true)]
        public List<GetAssetsForContractResponseDataInner> Data { get; set; }

        /// <summary>
        /// Gets or Sets Continuation
        /// </summary>
        [DataMember(Name = "continuation", EmitDefaultValue = true)]
        public string Continuation { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetAssetsForContractResponse {\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  Continuation: ").Append(Continuation).Append("\n");
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
