using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
	/// <summary>Provides methods and data for the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
	// Token: 0x02000A8B RID: 2699
	public sealed class ContractFailedEventArgs : EventArgs
	{
		/// <summary>Provides data for the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <param name="failureKind">One of the enumeration values that specifies the contract that failed.</param>
		/// <param name="message">The message for the event.</param>
		/// <param name="condition">The condition for the event.</param>
		/// <param name="originalException">The exception that caused the event.</param>
		// Token: 0x06006242 RID: 25154 RVA: 0x00140FC4 File Offset: 0x0013F1C4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public ContractFailedEventArgs(ContractFailureKind failureKind, string message, string condition, Exception originalException)
		{
			this._failureKind = failureKind;
			this._message = message;
			this._condition = condition;
			this._originalException = originalException;
		}

		/// <summary>Gets the message that describes the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <returns>The message that describes the event.</returns>
		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06006243 RID: 25155 RVA: 0x00140FE9 File Offset: 0x0013F1E9
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets the condition for the failure of the contract.</summary>
		/// <returns>The condition for the failure.</returns>
		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06006244 RID: 25156 RVA: 0x00140FF1 File Offset: 0x0013F1F1
		public string Condition
		{
			get
			{
				return this._condition;
			}
		}

		/// <summary>Gets the type of contract that failed.</summary>
		/// <returns>One of the enumeration values that specifies the type of contract that failed.</returns>
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06006245 RID: 25157 RVA: 0x00140FF9 File Offset: 0x0013F1F9
		public ContractFailureKind FailureKind
		{
			get
			{
				return this._failureKind;
			}
		}

		/// <summary>Gets the original exception that caused the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <returns>The exception that caused the event.</returns>
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06006246 RID: 25158 RVA: 0x00141001 File Offset: 0x0013F201
		public Exception OriginalException
		{
			get
			{
				return this._originalException;
			}
		}

		/// <summary>Indicates whether the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event has been handled.</summary>
		/// <returns>true if the event has been handled; otherwise, false. </returns>
		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06006247 RID: 25159 RVA: 0x00141009 File Offset: 0x0013F209
		public bool Handled
		{
			get
			{
				return this._handled;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Handled" /> property to true.</summary>
		// Token: 0x06006248 RID: 25160 RVA: 0x00141011 File Offset: 0x0013F211
		[SecurityCritical]
		public void SetHandled()
		{
			this._handled = true;
		}

		/// <summary>Indicates whether the code contract escalation policy should be applied.</summary>
		/// <returns>true to apply the escalation policy; otherwise, false. The default is false.</returns>
		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06006249 RID: 25161 RVA: 0x0014101A File Offset: 0x0013F21A
		public bool Unwind
		{
			get
			{
				return this._unwind;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Unwind" /> property to true.</summary>
		// Token: 0x0600624A RID: 25162 RVA: 0x00141022 File Offset: 0x0013F222
		[SecurityCritical]
		public void SetUnwind()
		{
			this._unwind = true;
		}

		// Token: 0x040030FA RID: 12538
		private ContractFailureKind _failureKind;

		// Token: 0x040030FB RID: 12539
		private string _message;

		// Token: 0x040030FC RID: 12540
		private string _condition;

		// Token: 0x040030FD RID: 12541
		private Exception _originalException;

		// Token: 0x040030FE RID: 12542
		private bool _handled;

		// Token: 0x040030FF RID: 12543
		private bool _unwind;

		// Token: 0x04003100 RID: 12544
		internal Exception thrownDuringHandler;
	}
}
