using System;
using System.Reflection;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001F7 RID: 503
	internal static class SysTxForGlobalTransactions
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00072102 File Offset: 0x00070302
		public static MethodInfo EnlistPromotableSinglePhase
		{
			get
			{
				return SysTxForGlobalTransactions._enlistPromotableSinglePhase.Value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0007210E File Offset: 0x0007030E
		public static MethodInfo SetDistributedTransactionIdentifier
		{
			get
			{
				return SysTxForGlobalTransactions._setDistributedTransactionIdentifier.Value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0007211A File Offset: 0x0007031A
		public static MethodInfo GetPromotedToken
		{
			get
			{
				return SysTxForGlobalTransactions._getPromotedToken.Value;
			}
		}

		// Token: 0x04000F26 RID: 3878
		private static readonly Lazy<MethodInfo> _enlistPromotableSinglePhase = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("EnlistPromotableSinglePhase", new Type[]
		{
			typeof(IPromotableSinglePhaseNotification),
			typeof(Guid)
		}));

		// Token: 0x04000F27 RID: 3879
		private static readonly Lazy<MethodInfo> _setDistributedTransactionIdentifier = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("SetDistributedTransactionIdentifier", new Type[]
		{
			typeof(IPromotableSinglePhaseNotification),
			typeof(Guid)
		}));

		// Token: 0x04000F28 RID: 3880
		private static readonly Lazy<MethodInfo> _getPromotedToken = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("GetPromotedToken"));
	}
}
