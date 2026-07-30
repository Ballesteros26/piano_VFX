using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Defines evidence that represents permission requests. This class cannot be inherited.</summary>
	// Token: 0x02000573 RID: 1395
	[ComVisible(true)]
	[Serializable]
	public sealed class PermissionRequestEvidence : EvidenceBase, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> class with the permission request of a code assembly.</summary>
		/// <param name="request">The minimum permissions the code requires to run. </param>
		/// <param name="optional">The permissions the code can use if they are granted, but that are not required. </param>
		/// <param name="denied">The permissions the code explicitly asks not to be granted. </param>
		// Token: 0x06003E76 RID: 15990 RVA: 0x000DFA42 File Offset: 0x000DDC42
		public PermissionRequestEvidence(PermissionSet request, PermissionSet optional, PermissionSet denied)
		{
			if (request != null)
			{
				this.requested = new PermissionSet(request);
			}
			if (optional != null)
			{
				this.optional = new PermissionSet(optional);
			}
			if (denied != null)
			{
				this.denied = new PermissionSet(denied);
			}
		}

		/// <summary>Gets the permissions the code explicitly asks not to be granted.</summary>
		/// <returns>The permissions the code explicitly asks not to be granted.</returns>
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003E77 RID: 15991 RVA: 0x000DFA77 File Offset: 0x000DDC77
		public PermissionSet DeniedPermissions
		{
			get
			{
				return this.denied;
			}
		}

		/// <summary>Gets the permissions the code can use if they are granted, but are not required.</summary>
		/// <returns>The permissions the code can use if they are granted, but are not required.</returns>
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x000DFA7F File Offset: 0x000DDC7F
		public PermissionSet OptionalPermissions
		{
			get
			{
				return this.optional;
			}
		}

		/// <summary>Gets the minimum permissions the code requires to run.</summary>
		/// <returns>The minimum permissions the code requires to run.</returns>
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003E79 RID: 15993 RVA: 0x000DFA87 File Offset: 0x000DDC87
		public PermissionSet RequestedPermissions
		{
			get
			{
				return this.requested;
			}
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</summary>
		/// <returns>An equivalent copy of the current <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</returns>
		// Token: 0x06003E7A RID: 15994 RVA: 0x000DFA8F File Offset: 0x000DDC8F
		public PermissionRequestEvidence Copy()
		{
			return new PermissionRequestEvidence(this.requested, this.optional, this.denied);
		}

		/// <summary>Gets a string representation of the state of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</summary>
		/// <returns>A representation of the state of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003E7B RID: 15995 RVA: 0x000DFAA8 File Offset: 0x000DDCA8
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.PermissionRequestEvidence");
			securityElement.AddAttribute("version", "1");
			if (this.requested != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Request");
				securityElement2.AddChild(this.requested.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.optional != null)
			{
				SecurityElement securityElement3 = new SecurityElement("Optional");
				securityElement3.AddChild(this.optional.ToXml());
				securityElement.AddChild(securityElement3);
			}
			if (this.denied != null)
			{
				SecurityElement securityElement4 = new SecurityElement("Denied");
				securityElement4.AddChild(this.denied.ToXml());
				securityElement.AddChild(securityElement4);
			}
			return securityElement.ToString();
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x000DFB58 File Offset: 0x000DDD58
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			int num = (verbose ? 3 : 1);
			if (this.requested != null)
			{
				int num2 = this.requested.ToXml().ToString().Length + (verbose ? 5 : 0);
				num += num2;
			}
			if (this.optional != null)
			{
				int num3 = this.optional.ToXml().ToString().Length + (verbose ? 5 : 0);
				num += num3;
			}
			if (this.denied != null)
			{
				int num4 = this.denied.ToXml().ToString().Length + (verbose ? 5 : 0);
				num += num4;
			}
			return num;
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x04001FE7 RID: 8167
		private PermissionSet requested;

		// Token: 0x04001FE8 RID: 8168
		private PermissionSet optional;

		// Token: 0x04001FE9 RID: 8169
		private PermissionSet denied;
	}
}
