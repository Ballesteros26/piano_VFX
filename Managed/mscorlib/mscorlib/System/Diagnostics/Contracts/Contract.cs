using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
	/// <summary>Contains static methods for representing program contracts such as preconditions, postconditions, and object invariants.</summary>
	// Token: 0x02000A89 RID: 2697
	public static class Contract
	{
		/// <summary>Instructs code analysis tools to assume that the specified condition is true, even if it cannot be statically proven to always be true.</summary>
		/// <param name="condition">The conditional expression to assume true.</param>
		// Token: 0x06006228 RID: 25128 RVA: 0x00140C84 File Offset: 0x0013EE84
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("DEBUG")]
		public static void Assume(bool condition)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assume, null, null, null);
			}
		}

		/// <summary>Instructs code analysis tools to assume that a condition is true, even if it cannot be statically proven to always be true, and displays a message if the assumption fails.</summary>
		/// <param name="condition">The conditional expression to assume true.</param>
		/// <param name="userMessage">The message to post if the assumption fails.</param>
		// Token: 0x06006229 RID: 25129 RVA: 0x00140C92 File Offset: 0x0013EE92
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		public static void Assume(bool condition, string userMessage)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assume, userMessage, null, null);
			}
		}

		/// <summary>Checks for a condition; if the condition is false, follows the escalation policy set for the analyzer.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x0600622A RID: 25130 RVA: 0x00140CA0 File Offset: 0x0013EEA0
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		public static void Assert(bool condition)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assert, null, null, null);
			}
		}

		/// <summary>Checks for a condition; if the condition is false, follows the escalation policy set by the analyzer and displays the specified message.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">A message to display if the condition is not met.</param>
		// Token: 0x0600622B RID: 25131 RVA: 0x00140CAE File Offset: 0x0013EEAE
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string userMessage)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assert, userMessage, null, null);
			}
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x0600622C RID: 25132 RVA: 0x00140CBC File Offset: 0x0013EEBC
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Requires(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and displays a message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is false.</param>
		// Token: 0x0600622D RID: 25133 RVA: 0x00140CBC File Offset: 0x0013EEBC
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Requires(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and throws an exception if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <typeparam name="TException">The exception to throw if the condition is false.</typeparam>
		// Token: 0x0600622E RID: 25134 RVA: 0x00140CC9 File Offset: 0x0013EEC9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Requires<TException>(bool condition) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and throws an exception with the provided message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is false.</param>
		/// <typeparam name="TException">The exception to throw if the condition is false.</typeparam>
		// Token: 0x0600622F RID: 25135 RVA: 0x00140CC9 File Offset: 0x0013EEC9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Requires<TException>(bool condition, string userMessage) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
		}

		/// <summary>Specifies a postcondition contract for the enclosing method or property.</summary>
		/// <param name="condition">The conditional expression to test. The expression may include <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" />, <see cref="M:System.Diagnostics.Contracts.Contract.ValueAtReturn``1(``0@)" />, and <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> values. </param>
		// Token: 0x06006230 RID: 25136 RVA: 0x00140CD6 File Offset: 0x0013EED6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		public static void Ensures(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
		}

		/// <summary>Specifies a postcondition contract for a provided exit condition and a message to display if the condition is false.</summary>
		/// <param name="condition">The conditional expression to test. The expression may include <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" /> and <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> values. </param>
		/// <param name="userMessage">The message to display if the expression is not true.</param>
		// Token: 0x06006231 RID: 25137 RVA: 0x00140CD6 File Offset: 0x0013EED6
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Ensures(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
		}

		/// <summary>Specifies a postcondition contract for the enclosing method or property, based on the provided exception and condition.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <typeparam name="TException">The type of exception that invokes the postcondition check.</typeparam>
		// Token: 0x06006232 RID: 25138 RVA: 0x00140CE3 File Offset: 0x0013EEE3
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void EnsuresOnThrow<TException>(bool condition) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
		}

		/// <summary>Specifies a postcondition contract and a message to display if the condition is false for the enclosing method or property, based on the provided exception and condition.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the expression is false.</param>
		/// <typeparam name="TException">The type of exception that invokes the postcondition check.</typeparam>
		// Token: 0x06006233 RID: 25139 RVA: 0x00140CE3 File Offset: 0x0013EEE3
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		public static void EnsuresOnThrow<TException>(bool condition, string userMessage) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
		}

		/// <summary>Represents the return value of a method or property.</summary>
		/// <returns>Return value of the enclosing method or property.</returns>
		/// <typeparam name="T">Type of return value of the enclosing method or property.</typeparam>
		// Token: 0x06006234 RID: 25140 RVA: 0x00140CF0 File Offset: 0x0013EEF0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static T Result<T>()
		{
			return default(T);
		}

		/// <summary>Represents the final (output) value of an out parameter when returning from a method.</summary>
		/// <returns>The output value of the out parameter.</returns>
		/// <param name="value">The out parameter.</param>
		/// <typeparam name="T">The type of the out parameter.</typeparam>
		// Token: 0x06006235 RID: 25141 RVA: 0x00140D06 File Offset: 0x0013EF06
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static T ValueAtReturn<T>(out T value)
		{
			value = default(T);
			return value;
		}

		/// <summary>Represents values as they were at the start of a method or property.</summary>
		/// <returns>The value of the parameter or field at the start of a method or property.</returns>
		/// <param name="value">The value to represent (field or parameter).</param>
		/// <typeparam name="T">The type of value.</typeparam>
		// Token: 0x06006236 RID: 25142 RVA: 0x00140D18 File Offset: 0x0013EF18
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static T OldValue<T>(T value)
		{
			return default(T);
		}

		/// <summary>Specifies an invariant contract for the enclosing method or property. </summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x06006237 RID: 25143 RVA: 0x00140D2E File Offset: 0x0013EF2E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		public static void Invariant(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
		}

		/// <summary>Specifies an invariant contract for the enclosing method or property, and displays a message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is false.</param>
		// Token: 0x06006238 RID: 25144 RVA: 0x00140D2E File Offset: 0x0013EF2E
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Invariant(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
		}

		/// <summary>Determines whether a particular condition is valid for all integers in a specified range.</summary>
		/// <returns>true if <paramref name="predicate" /> returns true for all integers starting from <paramref name="fromInclusive" /> to <paramref name="toExclusive" /> - 1.</returns>
		/// <param name="fromInclusive">The first integer to pass to <paramref name="predicate" />.</param>
		/// <param name="toExclusive">One more than the last integer to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for the existence of the integers in the specified range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toExclusive " />is less than <paramref name="fromInclusive" />.</exception>
		// Token: 0x06006239 RID: 25145 RVA: 0x00140D3C File Offset: 0x0013EF3C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool ForAll(int fromInclusive, int toExclusive, Predicate<int> predicate)
		{
			if (fromInclusive > toExclusive)
			{
				throw new ArgumentException("fromInclusive must be less than or equal to toExclusive.");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				if (!predicate(i))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether all the elements in a collection exist within a function.</summary>
		/// <returns>true if and only if <paramref name="predicate" /> returns true for all elements of type <paramref name="T" /> in <paramref name="collection" />.</returns>
		/// <param name="collection">The collection from which elements of type <paramref name="T" /> will be drawn to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for the existence of all the elements in <paramref name="collection" />.</param>
		/// <typeparam name="T">The type that is contained in <paramref name="collection" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600623A RID: 25146 RVA: 0x00140D80 File Offset: 0x0013EF80
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (!predicate(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a specified test is true for any integer within a range of integers.</summary>
		/// <returns>true if <paramref name="predicate" /> returns true for any integer starting from <paramref name="fromInclusive" /> to <paramref name="toExclusive" /> - 1.</returns>
		/// <param name="fromInclusive">The first integer to pass to <paramref name="predicate" />.</param>
		/// <param name="toExclusive">One more than the last integer to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for any value of the integer in the specified range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toExclusive " />is less than <paramref name="fromInclusive" />.</exception>
		// Token: 0x0600623B RID: 25147 RVA: 0x00140DF0 File Offset: 0x0013EFF0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool Exists(int fromInclusive, int toExclusive, Predicate<int> predicate)
		{
			if (fromInclusive > toExclusive)
			{
				throw new ArgumentException("fromInclusive must be less than or equal to toExclusive.");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				if (predicate(i))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether an element within a collection of elements exists within a function.</summary>
		/// <returns>true if and only if <paramref name="predicate" /> returns true for any element of type <paramref name="T" /> in <paramref name="collection" />.</returns>
		/// <param name="collection">The collection from which elements of type <paramref name="T" /> will be drawn to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for an element in <paramref name="collection" />.</param>
		/// <typeparam name="T">The type that is contained in <paramref name="collection" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600623C RID: 25148 RVA: 0x00140E34 File Offset: 0x0013F034
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Marks the end of the contract section when a method's contracts contain only preconditions in the if-then-throw form.</summary>
		// Token: 0x0600623D RID: 25149 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EndContractBlock()
		{
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x00140EA4 File Offset: 0x0013F0A4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DebuggerNonUserCode]
		private static void ReportFailure(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { failureKind }), "failureKind");
			}
			string text = ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
			if (text == null)
			{
				return;
			}
			ContractHelper.TriggerFailure(failureKind, text, userMessage, conditionText, innerException);
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x00140EF8 File Offset: 0x0013F0F8
		[SecuritySafeCritical]
		private static void AssertMustUseRewriter(ContractFailureKind kind, string contractKind)
		{
			if (Contract._assertingMustUseRewriter)
			{
				global::System.Diagnostics.Assert.Fail("Asserting that we must use the rewriter went reentrant.", "Didn't rewrite this mscorlib?");
			}
			Contract._assertingMustUseRewriter = true;
			Assembly assembly = typeof(Contract).Assembly;
			StackTrace stackTrace = new StackTrace();
			Assembly assembly2 = null;
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				Assembly assembly3 = stackTrace.GetFrame(i).GetMethod().DeclaringType.Assembly;
				if (assembly3 != assembly)
				{
					assembly2 = assembly3;
					break;
				}
			}
			if (assembly2 == null)
			{
				assembly2 = assembly;
			}
			string name = assembly2.GetName().Name;
			ContractHelper.TriggerFailure(kind, Environment.GetResourceString("An assembly (probably \"{1}\") must be rewritten using the code contracts binary rewriter (CCRewrite) because it is calling Contract.{0} and the CONTRACTS_FULL symbol is defined.  Remove any explicit definitions of the CONTRACTS_FULL symbol from your project and rebuild.  CCRewrite can be downloaded from http://go.microsoft.com/fwlink/?LinkID=169180. \\r\\nAfter the rewriter is installed, it can be enabled in Visual Studio from the project's Properties page on the Code Contracts pane.  Ensure that \"Perform Runtime Contract Checking\" is enabled, which will define CONTRACTS_FULL.", new object[] { contractKind, name }), null, null, null);
			Contract._assertingMustUseRewriter = false;
		}

		/// <summary>Occurs when a contract fails.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06006240 RID: 25152 RVA: 0x00140FB4 File Offset: 0x0013F1B4
		// (remove) Token: 0x06006241 RID: 25153 RVA: 0x00140FBC File Offset: 0x0013F1BC
		public static event EventHandler<ContractFailedEventArgs> ContractFailed
		{
			[SecurityCritical]
			add
			{
				ContractHelper.InternalContractFailed += value;
			}
			[SecurityCritical]
			remove
			{
				ContractHelper.InternalContractFailed -= value;
			}
		}

		// Token: 0x040030F2 RID: 12530
		[ThreadStatic]
		private static bool _assertingMustUseRewriter;
	}
}
