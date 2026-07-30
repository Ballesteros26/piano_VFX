using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Drawing.Printing
{
	/// <summary>Represents the resolution supported by a printer.</summary>
	// Token: 0x020000BF RID: 191
	[Serializable]
	public class PrinterResolution
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterResolution" /> class. </summary>
		// Token: 0x06000A82 RID: 2690 RVA: 0x00016BF1 File Offset: 0x00014DF1
		public PrinterResolution()
		{
			this._kind = PrinterResolutionKind.Custom;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00016C00 File Offset: 0x00014E00
		internal PrinterResolution(PrinterResolutionKind kind, int x, int y)
		{
			this._kind = kind;
			this._x = x;
			this._y = y;
		}

		/// <summary>Gets or sets the printer resolution.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrinterResolutionKind" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not a member of the <see cref="T:System.Drawing.Printing.PrinterResolutionKind" /> enumeration.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00016C1D File Offset: 0x00014E1D
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00016C25 File Offset: 0x00014E25
		public PrinterResolutionKind Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				if (value < PrinterResolutionKind.High || value > PrinterResolutionKind.Custom)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PrinterResolutionKind));
				}
				this._kind = value;
			}
		}

		/// <summary>Gets the horizontal printer resolution, in dots per inch.</summary>
		/// <returns>The horizontal printer resolution, in dots per inch, if <see cref="P:System.Drawing.Printing.PrinterResolution.Kind" /> is set to <see cref="F:System.Drawing.Printing.PrinterResolutionKind.Custom" />; otherwise, a dmPrintQuality value.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00016C4D File Offset: 0x00014E4D
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00016C55 File Offset: 0x00014E55
		public int X
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
			}
		}

		/// <summary>Gets the vertical printer resolution, in dots per inch.</summary>
		/// <returns>The vertical printer resolution, in dots per inch.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00016C5E File Offset: 0x00014E5E
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00016C66 File Offset: 0x00014E66
		public int Y
		{
			get
			{
				return this._y;
			}
			set
			{
				this._y = value;
			}
		}

		/// <summary>This member overrides the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains information about the <see cref="T:System.Drawing.Printing.PrinterResolution" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000A8A RID: 2698 RVA: 0x00016C70 File Offset: 0x00014E70
		public override string ToString()
		{
			if (this._kind != PrinterResolutionKind.Custom)
			{
				return "[PrinterResolution " + this.Kind.ToString() + "]";
			}
			return string.Concat(new string[]
			{
				"[PrinterResolution X=",
				this.X.ToString(CultureInfo.InvariantCulture),
				" Y=",
				this.Y.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x040006E7 RID: 1767
		private int _x;

		// Token: 0x040006E8 RID: 1768
		private int _y;

		// Token: 0x040006E9 RID: 1769
		private PrinterResolutionKind _kind;
	}
}
