namespace Streetcred.Sdk.Model.Records
{
    /// <summary>
    /// Represents a route record in the agent wallet.
    /// </summary>
    /// <seealso cref="WalletRecord" />
    public class RouteRecord : WalletRecord
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns>The identifier.</returns>
        public override string GetId() => To;
        
        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <returns>The type name.</returns>
        public override string GetTypeName() => "RouteRecord";

        /// <summary>
        /// Gets or sets the route identifier.
        /// </summary>
        /// <value>Route identifier.</value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the destination endpoint.
        /// </summary>
        /// <value>The destination endpoint.</value>
        public AgentEndpoint Endpoint { get; set; }
    }
}
