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
    /// CommonStatsResponseCount
    /// </summary>
    [DataContract(Name = "CommonStatsResponse_count")]
    [UnityEngine.Scripting.Preserve]
    public partial class CommonStatsResponseCount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonStatsResponseCount" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        [UnityEngine.Scripting.Preserve]
        protected CommonStatsResponseCount() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonStatsResponseCount" /> class.
        /// </summary>
        /// <param name="tokens">tokens (required).</param>
        /// <param name="listed">listed (required).</param>
        [UnityEngine.Scripting.Preserve]
        public CommonStatsResponseCount(decimal tokens = default(decimal), decimal listed = default(decimal))
        {
            this.Tokens = tokens;
            this.Listed = listed;
        }

        /// <summary>
        /// Gets or Sets Tokens
        /// </summary>
        [DataMember(Name = "tokens", IsRequired = true, EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public decimal Tokens { get; set; }

        /// <summary>
        /// Gets or Sets Listed
        /// </summary>
        [DataMember(Name = "listed", IsRequired = true, EmitDefaultValue = true)]
        [UnityEngine.Scripting.Preserve]
        public decimal Listed { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        [UnityEngine.Scripting.Preserve]
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CommonStatsResponseCount {\n");
            sb.Append("  Tokens: ").Append(Tokens).Append("\n");
            sb.Append("  Listed: ").Append(Listed).Append("\n");
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