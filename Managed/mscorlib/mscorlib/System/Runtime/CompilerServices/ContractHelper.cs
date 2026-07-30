using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides methods that the binary rewriter uses to handle contract failures.</summary>
	// Token: 0x0200083B RID: 2107
	public static class ContractHelper
	{
		/// <summary>Used by the binary rewriter to activate the default failure behavior.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) if the event was handled and should not trigger a failure; otherwise, returns the localized failure message.</returns>
		/// <param name="failureKind">One of the enumeration values that specifies the type of failure.</param>
		/// <param name="userMessage">Additional user information.</param>
		/// <param name="conditionText">The description of the condition that caused the failure.</param>
		/// <param name="innerException">The inner exception that caused the current exception.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="failureKind" /> is not a valid <see cref="T:System.Diagnostics.Contracts.ContractFailureKind" /> value.</exception>
		// Token: 0x060053AA RID: 21418 RVA: 0x00125CB0 File Offset: 0x00123EB0
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			string text = "Contract failed";
			ContractHelper.RaiseContractFailedEventImplementation(failureKind, userMessage, conditionText, innerException, ref text);
			return text;
		}

		/// <summary>Triggers the default failure behavior.</summary>
		/// <param name="kind">One of the enumeration values that specifies the type of failure.</param>
		/// <param name="displayMessage">The message to display.</param>
		/// <param name="userMessage">Additional user information.</param>
		/// <param name="conditionText">The description of the condition that caused the failure.</param>
		/// <param name="innerException">The inner exception that caused the current exception.</param>
		// Token: 0x060053AB RID: 21419 RVA: 0x00125CCF File Offset: 0x00123ECF
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DebuggerNonUserCode]
		public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			ContractHelper.TriggerFailureImplementation(kind, displayMessage, userMessage, conditionText, innerException);
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x00125CDC File Offset: 0x00123EDC
		[SecuritySafeCritical]
		[DebuggerNonUserCode]
		private static void RaiseContractFailedEventImplementation(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException, ref string resultFailureMessage)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { failureKind }), "failureKind");
			}
			string text = "contract failed.";
			ContractFailedEventArgs contractFailedEventArgs = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			string text2;
			try
			{
				text = ContractHelper.GetDisplayMessage(failureKind, userMessage, conditionText);
				EventHandler<ContractFailedEventArgs> eventHandler = ContractHelper.contractFailedEvent;
				if (eventHandler != null)
				{
					contractFailedEventArgs = new ContractFailedEventArgs(failureKind, text, conditionText, innerException);
					foreach (EventHandler<ContractFailedEventArgs> eventHandler2 in eventHandler.GetInvocationList())
					{
						try
						{
							eventHandler2(null, contractFailedEventArgs);
						}
						catch (Exception ex)
						{
							contractFailedEventArgs.thrownDuringHandler = ex;
							contractFailedEventArgs.SetUnwind();
						}
					}
					if (contractFailedEventArgs.Unwind)
					{
						if (Environment.IsCLRHosted)
						{
							ContractHelper.TriggerCodeContractEscalationPolicy(failureKind, text, conditionText, innerException);
						}
						if (innerException == null)
						{
							innerException = contractFailedEventArgs.thrownDuringHandler;
						}
						throw new ContractException(failureKind, text, userMessage, conditionText, innerException);
					}
				}
			}
			finally
			{
				if (contractFailedEventArgs != null && contractFailedEventArgs.Handled)
				{
					text2 = null;
				}
				else
				{
					text2 = text;
				}
			}
			resultFailureMessage = text2;
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x00125DE8 File Offset: 0x00123FE8
		[SecuritySafeCritical]
		[DebuggerNonUserCode]
		private static void TriggerFailureImplementation(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			if (Environment.IsCLRHosted)
			{
				ContractHelper.TriggerCodeContractEscalationPolicy(kind, displayMessage, conditionText, innerException);
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			if (!Environment.UserInteractive)
			{
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			string resourceString = Environment.GetResourceString(ContractHelper.GetResourceNameForFailure(kind, false));
			Assert.Fail(conditionText, displayMessage, resourceString, -2146233022, StackTrace.TraceFormat.Normal, 2);
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060053AE RID: 21422 RVA: 0x00125E44 File Offset: 0x00124044
		// (remove) Token: 0x060053AF RID: 21423 RVA: 0x00125E9C File Offset: 0x0012409C
		internal static event EventHandler<ContractFailedEventArgs> InternalContractFailed
		{
			[SecurityCritical]
			add
			{
				RuntimeHelpers.PrepareContractedDelegate(value);
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Combine(ContractHelper.contractFailedEvent, value);
				}
			}
			[SecurityCritical]
			remove
			{
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Remove(ContractHelper.contractFailedEvent, value);
				}
			}
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x00125EF0 File Offset: 0x001240F0
		private static string GetResourceNameForFailure(ContractFailureKind failureKind, bool withCondition = false)
		{
			string text;
			switch (failureKind)
			{
			case ContractFailureKind.Precondition:
				text = (withCondition ? "Precondition failed: {0}" : "Precondition failed.");
				break;
			case ContractFailureKind.Postcondition:
				text = (withCondition ? "Postcondition failed: {0}" : "Postcondition failed.");
				break;
			case ContractFailureKind.PostconditionOnException:
				text = (withCondition ? "Postcondition failed after throwing an exception: {0}" : "Postcondition failed after throwing an exception.");
				break;
			case ContractFailureKind.Invariant:
				text = (withCondition ? "Invariant failed: {0}" : "Invariant failed.");
				break;
			case ContractFailureKind.Assert:
				text = (withCondition ? "Assertion failed: {0}" : "Assertion failed.");
				break;
			case ContractFailureKind.Assume:
				text = (withCondition ? "Assumption failed: {0}" : "Assumption failed.");
				break;
			default:
				Contract.Assume(false, "Unreachable code");
				text = "Assumption failed.";
				break;
			}
			return text;
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x00125FA0 File Offset: 0x001241A0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static string GetDisplayMessage(ContractFailureKind failureKind, string userMessage, string conditionText)
		{
			string resourceNameForFailure = ContractHelper.GetResourceNameForFailure(failureKind, !string.IsNullOrEmpty(conditionText));
			string text;
			if (!string.IsNullOrEmpty(conditionText))
			{
				text = Environment.GetResourceString(resourceNameForFailure, new object[] { conditionText });
			}
			else
			{
				text = Environment.GetResourceString(resourceNameForFailure);
			}
			if (!string.IsNullOrEmpty(userMessage))
			{
				return text + "  " + userMessage;
			}
			return text;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00125FF8 File Offset: 0x001241F8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[DebuggerNonUserCode]
		private static void TriggerCodeContractEscalationPolicy(ContractFailureKind failureKind, string message, string conditionText, Exception innerException)
		{
			string text = null;
			if (innerException != null)
			{
				text = innerException.ToString();
			}
			Environment.TriggerCodeContractFailure(failureKind, message, conditionText, text);
		}

		// Token: 0x04002B81 RID: 11137
		private static volatile EventHandler<ContractFailedEventArgs> contractFailedEvent;

		// Token: 0x04002B82 RID: 11138
		private static readonly object lockObject = new object();

		// Token: 0x04002B83 RID: 11139
		internal const int COR_E_CODECONTRACTFAILED = -2146233022;
	}
}
