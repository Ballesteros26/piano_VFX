using System;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
	/// <summary>Allows declarative printing permission checks.</summary>
	// Token: 0x020000C4 RID: 196
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public sealed class PrintingPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06000A9F RID: 2719 RVA: 0x00017161 File Offset: 0x00015361
		public PrintingPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the type of printing allowed.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not one of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values. </exception>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0001716A File Offset: 0x0001536A
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00017172 File Offset: 0x00015372
		public PrintingPermissionLevel Level
		{
			get
			{
				return this.level;
			}
			set
			{
				if (value < PrintingPermissionLevel.NoPrinting || value > PrintingPermissionLevel.AllPrinting)
				{
					throw new ArgumentException(SR.Format("Permission level must be between PrintingPermissionLevel.NoPrinting and PrintingPermissionLevel.AllPrinting.", Array.Empty<object>()), "value");
				}
				this.level = value;
			}
		}

		/// <summary>Creates the permission based on the requested access levels, which are set through the <see cref="P:System.Drawing.Printing.PrintingPermissionAttribute.Level" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001719D File Offset: 0x0001539D
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PrintingPermission(PermissionState.Unrestricted);
			}
			return new PrintingPermission(this.level);
		}

		// Token: 0x040006F6 RID: 1782
		private PrintingPermissionLevel level;
	}
}
