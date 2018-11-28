using System;
using System.Collections.Generic;
using System.Text;
using AgentFramework.Core.Messages;
using Newtonsoft.Json;
using Xunit;

namespace AgentFramework.Core.Tests
{
    public class WireMessageTests
    {
        [Fact]
        public void SerializeWireMessage()
        {
            var buffer = new byte[] {1};

            var message = new WireMessage_WIP()
            {
                Aad = buffer,
                Ciphertext = buffer,
                Protected = new WireMessageProtected
                {
                    Typ = "JWM/1.0",
                    Enc = "xsalsa20poly1305",
                    AadHashAlg = "BLAKE2b",
                    CekEnc = "authcrypt"
                }
            };

            var serialized = JsonConvert.SerializeObject(message);
        }
    }
}
