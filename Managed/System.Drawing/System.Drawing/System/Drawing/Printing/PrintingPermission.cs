using System;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
	/// <summary>Controls access to printers. This class cannot be inherited.</summary>
	// Token: 0x020000C3 RID: 195
	[Serializable]
	public sealed class PrintingPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermission" /> class with either fully restricted or unrestricted access, as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06000A93 RID: 2707 RVA: 0x00016E70 File Offset: 0x00015070
		public PrintingPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.printingLevel = PrintingPermissionLevel.AllPrinting;
				return;
			}
			if (state == PermissionState.None)
			{
				this.printingLevel = PrintingPermissionLevel.NoPrinting;
				return;
			}
			throw new ArgumentException(SR.Format("Permission state is not valid.", Array.Empty<object>()));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermission" /> class with the level of printing access specified.</summary>
		/// <param name="printingLevel">One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values. </param>
		// Token: 0x06000A94 RID: 2708 RVA: 0x00016EA3 File Offset: 0x000150A3
		public PrintingPermission(PrintingPermissionLevel printingLevel)
		{
			PrintingPermission.VerifyPrintingLevel(printingLevel);
			this.printingLevel = printingLevel;
		}

		/// <summary>Gets or sets the code's level of printing access.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00016EB8 File Offset: 0x000150B8
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00016EC0 File Offset: 0x000150C0
		public PrintingPermissionLevel Level
		{
			get
			{
				return this.printingLevel;
			}
			set
			{
				PrintingPermission.VerifyPrintingLevel(value);
				this.printingLevel = value;
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00016ECF File Offset: 0x000150CF
		private static void VerifyPrintingLevel(PrintingPermissionLevel level)
		{
			if (level < PrintingPermissionLevel.NoPrinting || level > PrintingPermissionLevel.AllPrinting)
			{
				throw new ArgumentException(SR.Format("Permission level is not valid.", Array.Empty<object>()));
			}
		}

		/// <summary>Gets a value indicating whether the permission is unrestricted.</summary>
		/// <returns>true if permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06000A98 RID: 2712 RVA: 0x00016EEE File Offset: 0x000150EE
		public bool IsUnrestricted()
		{
			return this.printingLevel == PrintingPermissionLevel.AllPrinting;
		}

		/// <summary>Determines whether the current permission object is a subset of the specified permission.</summary>
		/// <returns>true if the current permission object is a subset of <paramref name="target" />; otherwise, false.</returns>
		/// <param name="target">A permission object that is to be tested for the subset relationship. This object must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is an object that is not of the same type as the current permission object. </exception>
		// Token: 0x06000A99 RID: 2713 RVA: 0x00016EFC File Offset: 0x000150FC
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.printingLevel == PrintingPermissionLevel.NoPrinting;
			}
			PrintingPermission printingPermission = target as PrintingPermission;
			if (printingPermission == null)
			{
				throw new ArgumentException(SR.Format("Target does not have permission to print.", Array.Empty<object>()));
			}
			return this.printingLevel <= printingPermission.printingLevel;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission object and a target permission object.</summary>
		/// <returns>A new permission object that represents the intersection of the current object and the specified target. This object is null if the intersection is empty.</returns>
		/// <param name="target">A permission object of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is an object that is not of the same type as the current permission object. </exception>
		// Token: 0x06000A9A RID: 2714 RVA: 0x00016F48 File Offset: 0x00015148
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PrintingPermission printingPermission = target as PrintingPermission;
			if (printingPermission == null)
			{
				throw new ArgumentException(SR.Format("Target does not have permission to print.", Array.Empty<object>()));
			}
			PrintingPermissionLevel printingPermissionLevel = ((this.printingLevel < printingPermission.printingLevel) ? this.printingLevel : printingPermission.printingLevel);
			if (printingPermissionLevel == PrintingPermissionLevel.NoPrinting)
			{
				return null;
			}
			return new PrintingPermission(printingPermissionLevel);
		}

		/// <summary>Creates a permission that combines the permission object and the target permission object.</summary>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <param name="target">A permission object of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is an object that is not of the same type as the current permission object. </exception>
		// Token: 0x06000A9B RID: 2715 RVA: 0x00016FA4 File Offset: 0x000151A4
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			PrintingPermission printingPermission = target as PrintingPermission;
			if (printingPermission == null)
			{
				throw new ArgumentException(SR.Format("Target does not have permission to print.", Array.Empty<object>()));
			}
			PrintingPermissionLevel printingPermissionLevel = ((this.printingLevel > printingPermission.printingLevel) ? this.printingLevel : printingPermission.printingLevel);
			if (printingPermissionLevel == PrintingPermissionLevel.NoPrinting)
			{
				return null;
			}
			return new PrintingPermission(printingPermissionLevel);
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000A9C RID: 2716 RVA: 0x00017002 File Offset: 0x00015202
		public override IPermission Copy()
		{
			return new PrintingPermission(this.printingLevel);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06000A9D RID: 2717 RVA: 0x00017010 File Offset: 0x00015210
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Level", Enum.GetName(typeof(PrintingPermissionLevel), this.printingLevel));
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the security object. </param>
		// Token: 0x06000A9E RID: 2718 RVA: 0x000170B8 File Offset: 0x000152B8
		public override void FromXml(SecurityElement esd)
		{
			if (esd == null)
			{
				throw new ArgumentNullException("esd");
			}
			string text = esd.Attribute("class");
			if (text == null || text.IndexOf(base.GetType().FullName) == -1)
			{
				throw new ArgumentException(SR.Format("Class name is not valid.", Array.Empty<object>()));
			}
			string text2 = esd.Attribute("Unrestricted");
			if (text2 != null && string.Equals(text2, "true", StringComparison.OrdinalIgnoreCase))
			{
				this.printingLevel = PrintingPermissionLevel.AllPrinting;
				return;
			}
			this.printingLevel = PrintingPermissionLevel.NoPrinting;
			string text3 = esd.Attribute("Level");
			if (text3 != null)
			{
				this.printingLevel = (PrintingPermissionLevel)Enum.Parse(typeof(PrintingPermissionLevel), text3);
			}
		}

		// Token: 0x040006F5 RID: 1781
		private PrintingPermissionLevel printingLevel;
	}
}
