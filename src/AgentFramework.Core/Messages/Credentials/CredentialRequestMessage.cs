﻿using Newtonsoft.Json;

namespace AgentFramework.Core.Messages.Credentials
{
    /// <summary>
    /// A credential request content message.
    /// </summary>
    public class CredentialRequestMessage : IAgentMessage
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("@type")]
        public string Type { get; set; } = MessageTypes.CredentialRequest;

        /// <summary>
        /// Gets or sets the offer json.
        /// </summary>
        /// <value>
        /// The offer json.
        /// </value>
        public string OfferJson { get; set; }

        /// <summary>
        /// Gets or sets the credential request json.
        /// </summary>
        /// <value>
        /// The credential request json.
        /// </value>
        public string CredentialRequestJson { get; set; }

        /// <summary>
        /// Gets or sets the credential values json.
        /// </summary>
        /// <value>
        /// The credential values json.
        /// </value>
        public string CredentialValuesJson { get; set; }
    }
}
