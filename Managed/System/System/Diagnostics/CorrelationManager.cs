using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Diagnostics
{
	/// <summary>Correlates traces that are part of a logical transaction.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AB RID: 427
	public class CorrelationManager
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x000020EB File Offset: 0x000002EB
		internal CorrelationManager()
		{
		}

		/// <summary>Gets or sets the identity for a global activity.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that identifies the global activity.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0003D3FC File Offset: 0x0003B5FC
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x0003D423 File Offset: 0x0003B623
		public Guid ActivityId
		{
			get
			{
				object obj = CallContext.LogicalGetData("E2ETrace.ActivityID");
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				CallContext.LogicalSetData("E2ETrace.ActivityID", value);
			}
		}

		/// <summary>Gets the logical operation stack from the call context.</summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> object that represents the logical operation stack for the call context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0003D435 File Offset: 0x0003B635
		public Stack LogicalOperationStack
		{
			get
			{
				return this.GetLogicalOperationStack();
			}
		}

		/// <summary>Starts a logical operation with the specified identity on a thread.</summary>
		/// <param name="operationId">An object identifying the operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="operationId" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C78 RID: 3192 RVA: 0x0003D43D File Offset: 0x0003B63D
		public void StartLogicalOperation(object operationId)
		{
			if (operationId == null)
			{
				throw new ArgumentNullException("operationId");
			}
			this.GetLogicalOperationStack().Push(operationId);
		}

		/// <summary>Starts a logical operation on a thread.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C79 RID: 3193 RVA: 0x0003D459 File Offset: 0x0003B659
		public void StartLogicalOperation()
		{
			this.StartLogicalOperation(Guid.NewGuid());
		}

		/// <summary>Stops the current logical operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property is an empty stack.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C7A RID: 3194 RVA: 0x0003D46B File Offset: 0x0003B66B
		public void StopLogicalOperation()
		{
			this.GetLogicalOperationStack().Pop();
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0003D47C File Offset: 0x0003B67C
		private Stack GetLogicalOperationStack()
		{
			Stack stack = CallContext.LogicalGetData("System.Diagnostics.Trace.CorrelationManagerSlot") as Stack;
			if (stack == null)
			{
				stack = new Stack();
				CallContext.LogicalSetData("System.Diagnostics.Trace.CorrelationManagerSlot", stack);
			}
			return stack;
		}

		// Token: 0x0400100C RID: 4108
		private const string transactionSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x0400100D RID: 4109
		private const string activityIdSlotName = "E2ETrace.ActivityID";
	}
}
