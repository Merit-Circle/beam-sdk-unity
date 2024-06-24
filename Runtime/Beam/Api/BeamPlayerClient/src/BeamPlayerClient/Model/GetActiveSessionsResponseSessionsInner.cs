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
    /// GetActiveSessionsResponseSessionsInner
    /// </summary>
    [DataContract(Name = "GetActiveSessionsResponse_sessions_inner")]
    public partial class GetActiveSessionsResponseSessionsInner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActiveSessionsResponseSessionsInner" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GetActiveSessionsResponseSessionsInner() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActiveSessionsResponseSessionsInner" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="isActive">isActive (required).</param>
        /// <param name="startTime">startTime (required).</param>
        /// <param name="endTime">endTime (required).</param>
        /// <param name="sessionAddress">sessionAddress (required).</param>
        public GetActiveSessionsResponseSessionsInner(string id = default(string), bool isActive = default(bool), string startTime = default(string), string endTime = default(string), string sessionAddress = default(string))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new ArgumentNullException("id is a required property for GetActiveSessionsResponseSessionsInner and cannot be null");
            }
            this.Id = id;
            this.IsActive = isActive;
            // to ensure "startTime" is required (not null)
            if (startTime == null)
            {
                throw new ArgumentNullException("startTime is a required property for GetActiveSessionsResponseSessionsInner and cannot be null");
            }
            this.StartTime = startTime;
            // to ensure "endTime" is required (not null)
            if (endTime == null)
            {
                throw new ArgumentNullException("endTime is a required property for GetActiveSessionsResponseSessionsInner and cannot be null");
            }
            this.EndTime = endTime;
            // to ensure "sessionAddress" is required (not null)
            if (sessionAddress == null)
            {
                throw new ArgumentNullException("sessionAddress is a required property for GetActiveSessionsResponseSessionsInner and cannot be null");
            }
            this.SessionAddress = sessionAddress;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets IsActive
        /// </summary>
        [DataMember(Name = "isActive", IsRequired = true, EmitDefaultValue = true)]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or Sets StartTime
        /// </summary>
        [DataMember(Name = "startTime", IsRequired = true, EmitDefaultValue = true)]
        public string StartTime { get; set; }

        /// <summary>
        /// Gets or Sets EndTime
        /// </summary>
        [DataMember(Name = "endTime", IsRequired = true, EmitDefaultValue = true)]
        public string EndTime { get; set; }

        /// <summary>
        /// Gets or Sets SessionAddress
        /// </summary>
        [DataMember(Name = "sessionAddress", IsRequired = true, EmitDefaultValue = true)]
        public string SessionAddress { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetActiveSessionsResponseSessionsInner {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IsActive: ").Append(IsActive).Append("\n");
            sb.Append("  StartTime: ").Append(StartTime).Append("\n");
            sb.Append("  EndTime: ").Append(EndTime).Append("\n");
            sb.Append("  SessionAddress: ").Append(SessionAddress).Append("\n");
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
