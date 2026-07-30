using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates whether a method in Visual Basic is marked with the Iterator modifier.</summary>
	// Token: 0x0200084E RID: 2126
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class IteratorStateMachineAttribute : StateMachineAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.IteratorStateMachineAttribute" /> class.</summary>
		/// <param name="stateMachineType">The type object for the underlying state machine type that's used to implement a state machine method.</param>
		// Token: 0x060053FB RID: 21499 RVA: 0x00126D90 File Offset: 0x00124F90
		public IteratorStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}
