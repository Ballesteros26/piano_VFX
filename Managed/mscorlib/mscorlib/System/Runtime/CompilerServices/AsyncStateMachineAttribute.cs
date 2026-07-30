using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates whether a method is marked with either the Async (Visual Basic) or async (C# Reference) modifier.</summary>
	// Token: 0x02000845 RID: 2117
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class AsyncStateMachineAttribute : StateMachineAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.AsyncStateMachineAttribute" /> class.</summary>
		/// <param name="stateMachineType">The type object for the underlying state machine type that's used to implement a state machine method.</param>
		// Token: 0x060053EC RID: 21484 RVA: 0x00126D90 File Offset: 0x00124F90
		public AsyncStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}
