using System.Threading.Tasks;
using Hyperledger.Indy.WalletApi;
using Streetcred.Sdk.Model;
using Streetcred.Sdk.Model.Routes;

namespace Streetcred.Sdk.Contracts
{
    /// <summary>
    /// Router service.
    /// </summary>
    public interface IRouterService
    {
        /// <summary>
        /// Forwards the asynchronous.
        /// </summary>
        /// <param name="envelope">The content.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns>
        /// The async.
        /// </returns>
        Task SendMessageAsync(IEnvelopeMessage envelope, AgentEndpoint endpoint);

        /// <summary>
        /// Processes the add route message.
        /// </summary>
        /// <param name="wallet">Wallet.</param>
        /// <param name="route">Route message.</param>
        /// <returns>
        /// The async.
        /// </returns>
        /// TODO rename this to ProcessRouteMessage once we do it consistently through the SDK
        Task ProcessRoute(Wallet wallet, Route route);
    }
}
