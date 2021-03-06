﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AgentFramework.Core.Messages.Proofs;
using AgentFramework.Core.Models.Credentials;
using AgentFramework.Core.Models.Proofs;
using AgentFramework.Core.Models.Records;
using AgentFramework.Core.Models.Records.Search;
using Hyperledger.Indy.PoolApi;
using Hyperledger.Indy.WalletApi;

namespace AgentFramework.Core.Contracts
{
    /// <summary>
    /// Proof Service.
    /// </summary>
    public interface IProofService
    {
        /// <summary>
        /// Sends a proof request.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="wallet">Wallet.</param>
        /// <param name="connectionId">Connection identifier of who the proof request will be sent to.</param>
        /// <param name="proofRequest">Proof request object describing the proof request.</param>
        /// <returns>The response async.</returns>
        Task SendProofRequestAsync(Wallet wallet, string connectionId, ProofRequest proofRequest);

        /// <summary>
        /// Sends a proof request.
        /// </summary>
        /// <param name="wallet">The wallet.</param>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="proofRequestJson">The proof request json.</param>
        /// <returns>The response async.</returns>
        Task SendProofRequestAsync(Wallet wallet, string connectionId, string proofRequestJson);

        /// <summary>
        /// Creates a proof request.
        /// </summary>
        /// <returns>The proof request.</returns>
        /// <param name="wallet">Wallet.</param>
        /// <param name="connectionId">Connection identifier of who the proof request will be sent to.</param>
        /// <param name="proofRequest">An enumeration of attribute we wish the prover to disclose.</param>
        /// <returns>Proof Request message and identfier.</returns>
        Task<(ProofRequestMessage, string)> CreateProofRequestAsync(Wallet wallet, string connectionId,
            ProofRequest proofRequest);

        /// <summary>
        /// Creates a proof request.
        /// </summary>
        /// <returns>The proof request.</returns>
        /// <param name="wallet">Wallet.</param>
        /// <param name="connectionId">Connection identifier of who the proof request will be sent to.</param>
        /// <param name="proofRequestJson">A string representation of proof request json object</param>
        /// <returns>Proof Request message and identfier.</returns>
        Task<(ProofRequestMessage, string)> CreateProofRequestAsync(Wallet wallet, string connectionId, string proofRequestJson);

        /// <summary>
        /// Processes a proof request and stores it for a given connection.
        /// </summary>
        /// <returns>The identifier for the stored proof request.</returns>
        /// <param name="wallet">Wallet.</param>
        /// <param name="proofRequest">A proof request.</param>
        /// <param name="connection">Connection</param>
        /// <returns>Proof identfier.</returns>
        Task<string> ProcessProofRequestAsync(Wallet wallet, ProofRequestMessage proofRequest, ConnectionRecord connection);

        /// <summary>
        /// Processes a proof and stores it for a given connection.
        /// </summary>
        /// <returns>The identifier for the stored proof.</returns>
        /// <param name="wallet">Wallet.</param>
        /// <param name="proof">A proof.</param>
        /// <param name="connection">Connection.</param>
        /// <returns>Proof identfier.</returns>
        Task<string> ProcessProofAsync(Wallet wallet, ProofMessage proof, ConnectionRecord connection);

        /// <summary>
        /// Creates a proof.
        /// </summary>
        /// <param name="wallet">Wallet.</param>
        /// <param name="pool">The pool.</param>
        /// <param name="proofRequestId">Identifier of the proof request.</param>
        /// <param name="requestedCredentials">The requested credentials.</param>
        /// <returns>
        /// The proof.
        /// </returns>
        Task<ProofMessage> CreateProofAsync(Wallet wallet, Pool pool, string proofRequestId,
            RequestedCredentials requestedCredentials);

        /// <summary>
        /// Accepts a proof request by generating a proof and sending it to the requestor.
        /// </summary>
        /// <param name="wallet">Wallet.</param>
        /// <param name="pool">The pool.</param>
        /// <param name="proofRequestId">Identifier of the proof request.</param>
        /// <param name="requestedCredentials">The requested credentials.</param>
        /// <returns>
        /// The proof.
        /// </returns>
        Task AcceptProofRequestAsync(Wallet wallet, Pool pool, string proofRequestId,
            RequestedCredentials requestedCredentials);

        /// <summary>
        /// Rejects a proof request.
        /// </summary>
        /// <returns>The proof.</returns>
        /// <param name="wallet">Wallet.</param>
        /// <param name="proofRequestId">Identifier of the proof request.</param>
        Task RejectProofRequestAsync(Wallet wallet, string proofRequestId);

        /// <summary>
        /// Verifies a proof.
        /// </summary>
        /// <param name="wallet">Wallet.</param>
        /// <param name="pool">The pool.</param>
        /// <param name="proofRecId">Identifier of the proof record.</param>
        /// <returns>Staus indicating validity of proof</returns>
        Task<bool> VerifyProofAsync(Wallet wallet, Pool pool, string proofRecId);

        /// <summary>
        /// Gets an enumeration of proofs stored in the wallet.
        /// </summary>
        /// <param name="wallet">Wallet.</param>
        /// <param name="query">The query.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        /// A list of proofs.
        /// </returns>
        Task<List<ProofRecord>> ListAsync(Wallet wallet, ISearchQuery query = null, int count = 100);

        /// <summary>
        /// Gets a particular proof stored in the wallet.
        /// </summary>
        /// <param name="wallet">Wallet.</param>
        /// <param name="proofRecId">Identifier of the proof record.</param>
        /// <returns>The proof.</returns>
        Task<ProofRecord> GetAsync(Wallet wallet, string proofRecId);
        
        /// <summary>
        /// Lists the credentials available for the given proof request.
        /// </summary>
        /// <param name="wallet">The wallet.</param>
        /// <param name="proofRequest">The proof request object.</param>
        /// <param name="attributeReferent">The attribute referent.</param>
        /// <returns>
        /// A collection of <see cref="CredentialInfo" /> that are available
        /// for building a proof for the given proof request
        /// </returns>
        Task<List<Credential>> ListCredentialsForProofRequestAsync(Wallet wallet,
            ProofRequest proofRequest, string attributeReferent);
    }
}