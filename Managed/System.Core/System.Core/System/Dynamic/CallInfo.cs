using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	/// <summary>Describes arguments in the dynamic binding process.</summary>
	// Token: 0x0200030E RID: 782
	public sealed class CallInfo
	{
		/// <summary>Creates a new PositionalArgumentInfo.</summary>
		/// <param name="argCount">The number of arguments.</param>
		/// <param name="argNames">The argument names.</param>
		// Token: 0x060017A1 RID: 6049 RVA: 0x0004D486 File Offset: 0x0004B686
		public CallInfo(int argCount, params string[] argNames)
			: this(argCount, argNames)
		{
		}

		/// <summary>Creates a new CallInfo that represents arguments in the dynamic binding process.</summary>
		/// <param name="argCount">The number of arguments.</param>
		/// <param name="argNames">The argument names.</param>
		// Token: 0x060017A2 RID: 6050 RVA: 0x0004D490 File Offset: 0x0004B690
		public CallInfo(int argCount, IEnumerable<string> argNames)
		{
			ContractUtils.RequiresNotNull(argNames, "argNames");
			ReadOnlyCollection<string> readOnlyCollection = argNames.ToReadOnly<string>();
			if (argCount < readOnlyCollection.Count)
			{
				throw Error.ArgCntMustBeGreaterThanNameCnt();
			}
			ContractUtils.RequiresNotNullItems<string>(readOnlyCollection, "argNames");
			this.ArgumentCount = argCount;
			this.ArgumentNames = readOnlyCollection;
		}

		/// <summary>The number of arguments.</summary>
		/// <returns>The number of arguments.</returns>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0004D4DD File Offset: 0x0004B6DD
		public int ArgumentCount { get; }

		/// <summary>The argument names.</summary>
		/// <returns>The read-only collection of argument names.</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0004D4E5 File Offset: 0x0004B6E5
		public ReadOnlyCollection<string> ArgumentNames { get; }

		/// <summary>Serves as a hash function for the current <see cref="T:System.Dynamic.CallInfo" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Dynamic.CallInfo" />.</returns>
		// Token: 0x060017A5 RID: 6053 RVA: 0x0004D4ED File Offset: 0x0004B6ED
		public override int GetHashCode()
		{
			return this.ArgumentCount ^ this.ArgumentNames.ListHashCode<string>();
		}

		/// <summary>Determines whether the specified CallInfo instance is considered equal to the current.</summary>
		/// <returns>true if the specified instance is equal to the current one otherwise, false.</returns>
		/// <param name="obj">The instance of <see cref="T:System.Dynamic.CallInfo" /> to compare with the current instance.</param>
		// Token: 0x060017A6 RID: 6054 RVA: 0x0004D504 File Offset: 0x0004B704
		public override bool Equals(object obj)
		{
			CallInfo callInfo = obj as CallInfo;
			return callInfo != null && this.ArgumentCount == callInfo.ArgumentCount && this.ArgumentNames.ListEquals(callInfo.ArgumentNames);
		}
	}
}
