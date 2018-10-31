using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Streetcred.Sdk.Model.Routes
{
    //TODO need to modify the content message interface and change this route name to be routemessage once we have renamed all
    public class Route
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        public string To { get; set; }

        public AgentEndpoint Endpoint { get; set; }
    }
}
