using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents state machines that are generated for asynchronous methods. This type is intended for compiler use only.</summary>
	// Token: 0x0200084B RID: 2123
	public interface IAsyncStateMachine
	{
		/// <summary>Moves the state machine to its next state.</summary>
		// Token: 0x060053F7 RID: 21495
		void MoveNext();

		/// <summary>Configures the state machine with a heap-allocated replica.</summary>
		/// <param name="stateMachine">The heap-allocated replica.</param>
		// Token: 0x060053F8 RID: 21496
		void SetStateMachine(IAsyncStateMachine stateMachine);
	}
}
