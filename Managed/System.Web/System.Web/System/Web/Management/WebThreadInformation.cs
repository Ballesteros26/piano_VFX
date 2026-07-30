using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about the state of a thread in an ASP.NET process.</summary>
	// Token: 0x020006E6 RID: 1766
	public sealed class WebThreadInformation
	{
		// Token: 0x06004AC8 RID: 19144 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WebThreadInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the current thread-impersonation mode.</summary>
		/// <returns>true if the thread is executing in impersonation mode; otherwise, false.</returns>
		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06004AC9 RID: 19145 RVA: 0x000CA8A4 File Offset: 0x000C8AA4
		public bool IsImpersonating
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the current thread-managed stack trace.</summary>
		/// <returns>The thread-managed stack trace.</returns>
		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06004ACA RID: 19146 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string StackTrace
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the thread account name.</summary>
		/// <returns>The thread account name.</returns>
		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06004ACB RID: 19147 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string ThreadAccountName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the current thread identifier.</summary>
		/// <returns>The current thread identifier.</returns>
		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06004ACC RID: 19148 RVA: 0x000CA8C0 File Offset: 0x000C8AC0
		public int ThreadID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Formats thread-related information.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06004ACD RID: 19149 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void FormatToString(WebEventFormatter formatter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
