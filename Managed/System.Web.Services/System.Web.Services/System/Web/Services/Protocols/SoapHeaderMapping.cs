using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.Services.Protocols
{
	/// <summary>The <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> class represents a SOAP header mapping.</summary>
	// Token: 0x02000068 RID: 104
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class SoapHeaderMapping
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000210F File Offset: 0x0000030F
		internal SoapHeaderMapping()
		{
		}

		/// <summary>Gets a <see cref="T:System.Type" /> that represents the type of the SOAP header mapping.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the SOAP header mapping.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000C29A File Offset: 0x0000A49A
		public Type HeaderType
		{
			get
			{
				return this.headerType;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the SOAP header mapping repeats.</summary>
		/// <returns>true if the SOAP header mapping repeats; otherwise, false.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000C2A2 File Offset: 0x0000A4A2
		public bool Repeats
		{
			get
			{
				return this.repeats;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the SOAP header mapping is custom-defined.</summary>
		/// <returns>true if the SOAP header mapping is custom-defined; otherwise, false.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000C2AA File Offset: 0x0000A4AA
		public bool Custom
		{
			get
			{
				return this.custom;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP header mapping.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP header mapping.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000C2B2 File Offset: 0x0000A4B2
		public SoapHeaderDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MemberInfo" /> associated with the SOAP header mapping.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> associated with the SOAP header mapping.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000C2BA File Offset: 0x0000A4BA
		public MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		// Token: 0x04000284 RID: 644
		internal Type headerType;

		// Token: 0x04000285 RID: 645
		internal bool repeats;

		// Token: 0x04000286 RID: 646
		internal bool custom;

		// Token: 0x04000287 RID: 647
		internal SoapHeaderDirection direction;

		// Token: 0x04000288 RID: 648
		internal MemberInfo memberInfo;
	}
}
