using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about the worker process that hosts ASP.NET.</summary>
	// Token: 0x020006E4 RID: 1764
	public sealed class WebProcessInformation
	{
		// Token: 0x06004ABC RID: 19132 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WebProcessInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the account name for the worker process.</summary>
		/// <returns>The worker process account name.</returns>
		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06004ABD RID: 19133 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string AccountName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the process identifier.</summary>
		/// <returns>The process identifier.</returns>
		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06004ABE RID: 19134 RVA: 0x000CA888 File Offset: 0x000C8A88
		public int ProcessID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the name of the process.</summary>
		/// <returns>The name of the process.</returns>
		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06004ABF RID: 19135 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string ProcessName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Formats the application information.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06004AC0 RID: 19136 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void FormatToString(WebEventFormatter formatter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
