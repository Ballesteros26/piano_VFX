using System;
using System.Security;

namespace System.IO.IsolatedStorage
{
	/// <summary>Provides settings for maintaining the quota size for isolated storage. </summary>
	// Token: 0x020003F1 RID: 1009
	public class IsolatedStorageSecurityState : SecurityState
	{
		// Token: 0x06002F7E RID: 12158 RVA: 0x000A9BDF File Offset: 0x000A7DDF
		internal IsolatedStorageSecurityState()
		{
		}

		/// <summary>Gets the option for managing isolated storage security. </summary>
		/// <returns>The option to increase the isolated quota storage size.</returns>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x00028BDC File Offset: 0x00026DDC
		public IsolatedStorageSecurityOptions Options
		{
			get
			{
				return IsolatedStorageSecurityOptions.IncreaseQuotaForApplication;
			}
		}

		/// <summary>Gets or sets the current size of the quota for isolated storage.</summary>
		/// <returns>The current quota size, in bytes.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x0002126B File Offset: 0x0001F46B
		// (set) Token: 0x06002F81 RID: 12161 RVA: 0x00002194 File Offset: 0x00000394
		public long Quota
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>Gets the current usage size in isolated storage.</summary>
		/// <returns>The current usage size, in bytes.</returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x0002126B File Offset: 0x0001F46B
		public long UsedSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The state is not available.</exception>
		// Token: 0x06002F83 RID: 12163 RVA: 0x0002126B File Offset: 0x0001F46B
		public override void EnsureState()
		{
			throw new NotImplementedException();
		}
	}
}
