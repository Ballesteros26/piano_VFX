using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies settings that apply to a single, printed page.</summary>
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public class PageSettings : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PageSettings" /> class using the default printer.</summary>
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00017500 File Offset: 0x00015700
		public PageSettings()
			: this(new PrinterSettings())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PageSettings" /> class using a specified printer.</summary>
		/// <param name="printerSettings">The <see cref="T:System.Drawing.Printing.PrinterSettings" /> that describes the printer to use. </param>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00017510 File Offset: 0x00015710
		public PageSettings(PrinterSettings printerSettings)
		{
			this.margins = new Margins();
			base..ctor();
			this.PrinterSettings = printerSettings;
			this.color = printerSettings.DefaultPageSettings.color;
			this.landscape = printerSettings.DefaultPageSettings.landscape;
			this.paperSize = printerSettings.DefaultPageSettings.paperSize;
			this.paperSource = printerSettings.DefaultPageSettings.paperSource;
			this.printerResolution = printerSettings.DefaultPageSettings.printerResolution;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001758A File Offset: 0x0001578A
		internal PageSettings(PrinterSettings printerSettings, bool color, bool landscape, PaperSize paperSize, PaperSource paperSource, PrinterResolution printerResolution)
		{
			this.margins = new Margins();
			base..ctor();
			this.PrinterSettings = printerSettings;
			this.color = color;
			this.landscape = landscape;
			this.paperSize = paperSize;
			this.paperSource = paperSource;
			this.printerResolution = printerResolution;
		}

		/// <summary>Gets the size of the page, taking into account the page orientation specified by the <see cref="P:System.Drawing.Printing.PageSettings.Landscape" /> property.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the length and width, in hundredths of an inch, of the page.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x000175CC File Offset: 0x000157CC
		public Rectangle Bounds
		{
			get
			{
				int num = this.paperSize.Width;
				int num2 = this.paperSize.Height;
				num -= this.margins.Left + this.margins.Right;
				num2 -= this.margins.Top + this.margins.Bottom;
				if (this.landscape)
				{
					int num3 = num;
					num = num2;
					num2 = num3;
				}
				return new Rectangle(this.margins.Left, this.margins.Top, num, num2);
			}
		}

		/// <summary>Gets or sets a value indicating whether the page should be printed in color.</summary>
		/// <returns>true if the page should be printed in color; otherwise, false. The default is determined by the printer.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0001764E File Offset: 0x0001584E
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0001766F File Offset: 0x0001586F
		public bool Color
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the page is printed in landscape or portrait orientation.</summary>
		/// <returns>true if the page should be printed in landscape orientation; otherwise, false. The default is determined by the printer.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00017678 File Offset: 0x00015878
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x00017699 File Offset: 0x00015899
		public bool Landscape
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.landscape;
			}
			set
			{
				this.landscape = value;
			}
		}

		/// <summary>Gets or sets the margins for this page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.Margins" /> that represents the margins, in hundredths of an inch, for the page. The default is 1-inch margins on all sides.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x000176A2 File Offset: 0x000158A2
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x000176C3 File Offset: 0x000158C3
		public Margins Margins
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.margins;
			}
			set
			{
				this.margins = value;
			}
		}

		/// <summary>Gets or sets the paper size for the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PaperSize" /> that represents the size of the paper. The default is the printer's default paper size.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x000176CC File Offset: 0x000158CC
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x000176ED File Offset: 0x000158ED
		public PaperSize PaperSize
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.paperSize;
			}
			set
			{
				if (value != null)
				{
					this.paperSize = value;
				}
			}
		}

		/// <summary>Gets or sets the page's paper source; for example, the printer's upper tray.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PaperSource" /> that specifies the source of the paper. The default is the printer's default paper source.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x000176F9 File Offset: 0x000158F9
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0001771A File Offset: 0x0001591A
		public PaperSource PaperSource
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.paperSource;
			}
			set
			{
				if (value != null)
				{
					this.paperSource = value;
				}
			}
		}

		/// <summary>Gets or sets the printer resolution for the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterResolution" /> that specifies the printer resolution for the page. The default is the printer's default resolution.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00017726 File Offset: 0x00015926
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x00017747 File Offset: 0x00015947
		public PrinterResolution PrinterResolution
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.printerResolution;
			}
			set
			{
				if (value != null)
				{
					this.printerResolution = value;
				}
			}
		}

		/// <summary>Gets or sets the printer settings associated with the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that represents the printer settings associated with the page.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00017753 File Offset: 0x00015953
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0001775B File Offset: 0x0001595B
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printerSettings;
			}
			set
			{
				this.printerSettings = value;
			}
		}

		/// <summary>Gets the x-coordinate, in hundredths of an inch, of the hard margin at the left of the page.</summary>
		/// <returns>The x-coordinate, in hundredths of an inch, of the left-hand hard margin.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00017764 File Offset: 0x00015964
		public float HardMarginX
		{
			get
			{
				return this.hardMarginX;
			}
		}

		/// <summary>Gets the y-coordinate, in hundredths of an inch, of the hard margin at the top of the page.</summary>
		/// <returns>The y-coordinate, in hundredths of an inch, of the hard margin at the top of the page.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0001776C File Offset: 0x0001596C
		public float HardMarginY
		{
			get
			{
				return this.hardMarginY;
			}
		}

		/// <summary>Gets the bounds of the printable area of the page for the printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> representing the length and width, in hundredths of an inch, of the area the printer is capable of printing in.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00017774 File Offset: 0x00015974
		public RectangleF PrintableArea
		{
			get
			{
				return this.printableArea;
			}
		}

		/// <summary>Creates a copy of this <see cref="T:System.Drawing.Printing.PageSettings" />.</summary>
		/// <returns>A copy of this object.</returns>
		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001777C File Offset: 0x0001597C
		public object Clone()
		{
			PrinterResolution printerResolution = new PrinterResolution(this.printerResolution.Kind, this.printerResolution.X, this.printerResolution.Y);
			PaperSource paperSource = new PaperSource(this.paperSource.Kind, this.paperSource.SourceName);
			PaperSize paperSize = new PaperSize(this.paperSize.PaperName, this.paperSize.Width, this.paperSize.Height);
			paperSize.RawKind = (int)this.paperSize.Kind;
			return new PageSettings(this.printerSettings, this.color, this.landscape, paperSize, paperSource, printerResolution)
			{
				Margins = (Margins)this.margins.Clone()
			};
		}

		/// <summary>Copies the relevant information from the <see cref="T:System.Drawing.Printing.PageSettings" /> to the specified DEVMODE structure.</summary>
		/// <param name="hdevmode">The handle to a Win32 DEVMODE structure. </param>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AC7 RID: 2759 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PageSettings.CopyToHdevmode")]
		public void CopyToHdevmode(IntPtr hdevmode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies relevant information to the <see cref="T:System.Drawing.Printing.PageSettings" /> from the specified DEVMODE structure.</summary>
		/// <param name="hdevmode">The handle to a Win32 DEVMODE structure. </param>
		/// <exception cref="T:System.ArgumentException">The printer handle is not valid. </exception>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PageSettings.SetHdevmode")]
		public void SetHdevmode(IntPtr hdevmode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the <see cref="T:System.Drawing.Printing.PageSettings" /> to string form.</summary>
		/// <returns>A string showing the various property settings for the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00017834 File Offset: 0x00015A34
		public override string ToString()
		{
			return string.Format("[PageSettings: Color={0}" + ", Landscape={1}" + ", Margins={2}" + ", PaperSize={3}" + ", PaperSource={4}" + ", PrinterResolution={5}" + "]", new object[] { this.color, this.landscape, this.margins, this.paperSize, this.paperSource, this.printerResolution });
		}

		// Token: 0x040006FE RID: 1790
		internal bool color;

		// Token: 0x040006FF RID: 1791
		internal bool landscape;

		// Token: 0x04000700 RID: 1792
		internal PaperSize paperSize;

		// Token: 0x04000701 RID: 1793
		internal PaperSource paperSource;

		// Token: 0x04000702 RID: 1794
		internal PrinterResolution printerResolution;

		// Token: 0x04000703 RID: 1795
		private Margins margins;

		// Token: 0x04000704 RID: 1796
		private float hardMarginX;

		// Token: 0x04000705 RID: 1797
		private float hardMarginY;

		// Token: 0x04000706 RID: 1798
		private RectangleF printableArea;

		// Token: 0x04000707 RID: 1799
		private PrinterSettings printerSettings;
	}
}
