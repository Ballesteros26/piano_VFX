using System;
using System.EnterpriseServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	/// <summary>Provides a way to wrap a callback method within a transaction boundary.</summary>
	// Token: 0x0200014E RID: 334
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Transactions
	{
		/// <summary>Wraps a specified transaction support around a callback method.</summary>
		/// <param name="callback">The <see cref="T:System.Web.Util.TransactedCallback" /> to be run under the specified transaction support.</param>
		/// <param name="mode">The <see cref="T:System.EnterpriseServices.TransactionOption" /> that specifies the transaction support for the delegate.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.Web.HttpException">The transacted code cannot be executed.</exception>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002A8C4 File Offset: 0x00028AC4
		public static void InvokeTransacted(TransactedCallback callback, TransactionOption mode)
		{
			bool flag = false;
			Transactions.InvokeTransacted(callback, mode, ref flag);
		}

		/// <summary>Wraps a specified transaction support around a callback method and indicates whether the transaction aborted.</summary>
		/// <param name="callback">The <see cref="T:System.Web.Util.TransactedCallback" /> to be run under the specified transaction support.</param>
		/// <param name="mode">The <see cref="T:System.EnterpriseServices.TransactionOption" /> that specifies the transaction support for the delegate.</param>
		/// <param name="transactionAborted">The reference parameter that returns true if the transaction was aborted during the callback method; otherwise, false. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.Web.HttpException">The transacted code cannot be executed.</exception>
		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002A8DC File Offset: 0x00028ADC
		[global::System.MonoTODO("Not implemented, not supported by Mono")]
		public static void InvokeTransacted(TransactedCallback callback, TransactionOption mode, ref bool transactionAborted)
		{
			throw new PlatformNotSupportedException("Not supported on mono");
		}
	}
}
