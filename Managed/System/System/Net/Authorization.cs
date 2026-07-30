using System;

namespace System.Net
{
	/// <summary>Contains an authentication message for an Internet server.</summary>
	// Token: 0x0200041B RID: 1051
	public class Authorization
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		// Token: 0x06001FF5 RID: 8181 RVA: 0x0007CCD5 File Offset: 0x0007AED5
		public Authorization(string token)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = true;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message and completion status.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		/// <param name="finished">The completion status of the authorization attempt. true if the authorization attempt is complete; otherwise, false. </param>
		// Token: 0x06001FF6 RID: 8182 RVA: 0x0007CCF0 File Offset: 0x0007AEF0
		public Authorization(string token, bool finished)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = finished;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message, completion status, and connection group identifier.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		/// <param name="finished">The completion status of the authorization attempt. true if the authorization attempt is complete; otherwise, false. </param>
		/// <param name="connectionGroupId">A unique identifier that can be used to create private client-server connections that are bound only to this authentication scheme. </param>
		// Token: 0x06001FF7 RID: 8183 RVA: 0x0007CD0B File Offset: 0x0007AF0B
		public Authorization(string token, bool finished, string connectionGroupId)
			: this(token, finished, connectionGroupId, false)
		{
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0007CD17 File Offset: 0x0007AF17
		internal Authorization(string token, bool finished, string connectionGroupId, bool mutualAuth)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_ConnectionGroupId = ValidationHelper.MakeStringNull(connectionGroupId);
			this.m_Complete = finished;
			this.m_MutualAuth = mutualAuth;
		}

		/// <summary>Gets the message returned to the server in response to an authentication challenge.</summary>
		/// <returns>The message that will be returned to the server in response to an authentication challenge.</returns>
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0007CD46 File Offset: 0x0007AF46
		public string Message
		{
			get
			{
				return this.m_Message;
			}
		}

		/// <summary>Gets a unique identifier for user-specific connections.</summary>
		/// <returns>A unique string that associates a connection with an authenticating entity.</returns>
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x0007CD4E File Offset: 0x0007AF4E
		public string ConnectionGroupId
		{
			get
			{
				return this.m_ConnectionGroupId;
			}
		}

		/// <summary>Gets the completion status of the authorization.</summary>
		/// <returns>true if the authentication process is complete; otherwise, false.</returns>
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0007CD56 File Offset: 0x0007AF56
		public bool Complete
		{
			get
			{
				return this.m_Complete;
			}
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0007CD5E File Offset: 0x0007AF5E
		internal void SetComplete(bool complete)
		{
			this.m_Complete = complete;
		}

		/// <summary>Gets or sets the prefix for Uniform Resource Identifiers (URIs) that can be authenticated with the <see cref="P:System.Net.Authorization.Message" /> property.</summary>
		/// <returns>An array of strings that contains URI prefixes.</returns>
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0007CD67 File Offset: 0x0007AF67
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x0007CD70 File Offset: 0x0007AF70
		public string[] ProtectionRealm
		{
			get
			{
				return this.m_ProtectionRealm;
			}
			set
			{
				string[] array = ValidationHelper.MakeEmptyArrayNull(value);
				this.m_ProtectionRealm = array;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>true if both client and server were authenticated; otherwise, false.</returns>
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0007CD8B File Offset: 0x0007AF8B
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x0007CD9D File Offset: 0x0007AF9D
		public bool MutuallyAuthenticated
		{
			get
			{
				return this.Complete && this.m_MutualAuth;
			}
			set
			{
				this.m_MutualAuth = value;
			}
		}

		// Token: 0x04001BC5 RID: 7109
		private string m_Message;

		// Token: 0x04001BC6 RID: 7110
		private bool m_Complete;

		// Token: 0x04001BC7 RID: 7111
		private string[] m_ProtectionRealm;

		// Token: 0x04001BC8 RID: 7112
		private string m_ConnectionGroupId;

		// Token: 0x04001BC9 RID: 7113
		private bool m_MutualAuth;

		// Token: 0x04001BCA RID: 7114
		internal string ModuleAuthenticationType;
	}
}
