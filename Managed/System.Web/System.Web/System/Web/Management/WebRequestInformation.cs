using System;
using System.Security.Principal;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about the current Web request.</summary>
	// Token: 0x020006E5 RID: 1765
	public sealed class WebRequestInformation
	{
		// Token: 0x06004AC1 RID: 19137 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WebRequestInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the instance of the managed-code principal associated with the Web request.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> instance that is associated with the request event.</returns>
		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06004AC2 RID: 19138 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IPrincipal Principal
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the physical path of the Web request.</summary>
		/// <returns>The physical path of the request.</returns>
		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06004AC3 RID: 19139 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string RequestPath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the logical path of the request.</summary>
		/// <returns>The logical path of the request.</returns>
		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06004AC4 RID: 19140 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string RequestUrl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a string that represents the Windows logon name of the user on whose behalf the code is being run.</summary>
		/// <returns>The Windows logon name of the user on whose behalf the code is being run.</returns>
		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string ThreadAccountName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the user host address. </summary>
		/// <returns>The user host address.</returns>
		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string UserHostAddress
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Formats the Web-request information.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06004AC7 RID: 19143 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void FormatToString(WebEventFormatter formatter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
