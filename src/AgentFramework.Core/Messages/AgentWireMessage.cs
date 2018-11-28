using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AgentFramework.Core.Messages
{
    public class AgentWireMessage
    {
        [JsonProperty("from")] public string From { get; set; }

        [JsonProperty("to")] public string To { get; set; }

        [JsonProperty("msg")] public string Message { get; set; }
    }

    [JsonConverter(typeof(WireMessageConverter))]
    public class WireMessage_WIP
    {
        // FROM AMES HIPE

        /// <summary>
        /// Gets or sets the additional authenticated message data
        /// </summary>
        /// <value>
        /// The protected.
        /// </value>
        [JsonRequired]
        public WireMessageProtected Protected { get; set; }

        [JsonRequired]
        public IEnumerable<WireMessageRecipient> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the hash of the recipients block base64 URL encoded value
        /// </summary>
        /// <value>
        /// The aad.
        /// </value>
        [JsonRequired]
        public byte[] Aad { get; set; }

        /// <summary>
        /// Gets or sets the base64 URL encoded authenticated encrypted message.
        /// </summary>
        /// <value>
        /// The ciphertext.
        /// </value>
        [JsonRequired]
        public byte[] Ciphertext { get; set; }

        /// <summary>
        /// Gets or sets the initialization vector (random nonce)
        /// </summary>
        /// <value>
        /// The iv.
        /// </value>
        public string Iv { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag { get; set; }
    }

    public class WireMessageConverter : JsonConverter<WireMessage_WIP>
    {
        public override void WriteJson(JsonWriter writer, WireMessage_WIP value, JsonSerializer serializer)
        {
            writer.WritePropertyName("aad");
            writer.WriteToken(JsonToken.String, Base64UriEscape(value.Aad));

            writer.WritePropertyName("ciphertext");
            writer.WriteToken(JsonToken.String, Base64UriEscape(value.Ciphertext));

            writer.WritePropertyName("protected");
            serializer.Serialize(writer, value.Protected);
        }

        private string Base64UriEscape(byte[] value)
        {
            return Uri.EscapeDataString(Convert.ToBase64String(value));
        }

        public override WireMessage_WIP ReadJson(JsonReader reader, Type objectType, WireMessage_WIP existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class WireMessageRecipient
    {
        /// <summary>
        /// Gets or sets the key used for encrypting the ciphertext.
        /// This is encrypted either by authcrypting with the sender key
        /// in the header data or anoncrypted
        /// </summary>
        /// <value>
        /// The encrypted key.
        /// </value>
        [JsonRequired]
        public string EncryptedKey { get; set; }

        /// <summary>
        /// Gets or sets the recipient to whom this message will be sent
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        [JsonRequired]
        public WireMessageRecipientHeader Header { get; set; }
    }

    public class WireMessageRecipientHeader
    {
        /// <summary>
        /// Gets or sets the anoncrypted verification key of the sender
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the DID and key reference of the recipient.
        /// If this field is specified, key MUST be absent
        /// </summary>
        /// <value>
        /// The kid.
        /// </value>
        public string Kid { get; set; }

        /// <summary>
        /// Gets or sets the VerKey of the recipient.
        /// If this field is specified, kid MUST be absent
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }
    }

    public class WireMessageProtected
    {
        /// <summary>
        /// Gets or sets the authenticated encryption algorithm used to encrypt the ciphertext
        /// </summary>
        /// <value>
        /// The enc.
        /// </value>
        [JsonRequired]
        public string Enc { get; set; }

        /// <summary>
        /// Gets or sets the message type. Ex: JWM/1.0
        /// </summary>
        /// <value>
        /// The typ.
        /// </value>
        [JsonRequired]
        public string Typ { get; set; }

        /// <summary>
        /// Gets or sets the algorithm used to hash the recipients data to be put in the aad field
        /// </summary>
        /// <value>
        /// The aad hash alg.
        /// </value>
        [JsonRequired]
        public string AadHashAlg { get; set; }

        public string CekEnc { get; set; }
    }
}
