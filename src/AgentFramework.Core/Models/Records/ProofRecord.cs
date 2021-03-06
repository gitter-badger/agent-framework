﻿using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Stateless;

namespace AgentFramework.Core.Models.Records
{
    /// <summary>
    /// Represents a proof record in the agency wallet
    /// </summary>
    /// <seealso cref="RecordBase" />
    public class ProofRecord : RecordBase
    {
        private ProofState _state;

        public ProofRecord()
        {
            State = ProofState.Requested;
        }

        public ProofRecord ShallowCopy()
        {
            return (ProofRecord)this.MemberwiseClone();
        }

        public ProofRecord DeepCopy()
        {
            return (ProofRecord)this.MemberwiseClone();
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <returns>The type name.</returns>
        public override string TypeName => "AF.ProofRecord";

        /// <summary>
        /// Gets or sets the proof request json.
        /// </summary>
        /// <value>The proof request json.</value>
        public string RequestJson
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the proof json.
        /// </summary>
        /// <value>The proof json.</value>
        public string ProofJson
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the connection identifier associated with this proof request.
        /// </summary>
        /// <value>The connection identifier.</value>
        [JsonIgnore]
        public string ConnectionId
        {
            get => Get();
            set => Set(value);
        }

        #region State Machine Implementation

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public ProofState State
        {
            get => _state;
            set => Set(value, ref _state);
        }

        /// <summary>
        /// Triggers the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="trigger">Trigger.</param>
        public Task TriggerAsync(ProofTrigger trigger) => GetStateMachine().FireAsync(trigger);

        private StateMachine<ProofState, ProofTrigger> GetStateMachine()
        {
            var state = new StateMachine<ProofState, ProofTrigger>(() => State, x => State = x);
            state.Configure(ProofState.Requested).Permit(ProofTrigger.Accept, ProofState.Accepted);
            state.Configure(ProofState.Requested).Permit(ProofTrigger.Reject, ProofState.Rejected);
            return state;
        }
        #endregion
    }

    /// <summary>
    /// Enumeration of possible proof states
    /// </summary>
    public enum ProofState
    {
        Requested = 0,
        Accepted = 1,
        Rejected = 2
    }

    /// <summary>
    /// Enumeration of possible triggers that change the proofs state
    /// </summary>
    public enum ProofTrigger
    {
        Request,
        Accept,
        Reject
    }
}