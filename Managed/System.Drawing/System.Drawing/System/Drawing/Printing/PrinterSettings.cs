using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Imaging;

namespace System.Drawing.Printing
{
	/// <summary>Specifies information about how a document is printed, including the printer that prints it, when printing from a Windows Forms application.</summary>
	// Token: 0x020000CF RID: 207
	[Serializable]
	public class PrinterSettings : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings" /> class.</summary>
		// Token: 0x06000B03 RID: 2819 RVA: 0x00017EF6 File Offset: 0x000160F6
		public PrinterSettings()
			: this(SysPrn.CreatePrintingService())
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00017F03 File Offset: 0x00016103
		internal PrinterSettings(PrintingServices printing_services)
		{
			this.printing_services = printing_services;
			this.printer_name = printing_services.DefaultPrinter;
			this.ResetToDefaults();
			printing_services.LoadPrinterSettings(this.printer_name, this);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00017F31 File Offset: 0x00016131
		private void ResetToDefaults()
		{
			this.printer_resolutions = null;
			this.paper_sizes = null;
			this.paper_sources = null;
			this.default_pagesettings = null;
			this.maximum_page = 9999;
			this.copies = 1;
			this.collate = true;
		}

		/// <summary>Gets a value indicating whether the printer supports double-sided printing.</summary>
		/// <returns>true if the printer supports double-sided printing; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00017F68 File Offset: 0x00016168
		public bool CanDuplex
		{
			get
			{
				return this.can_duplex;
			}
		}

		/// <summary>Gets or sets a value indicating whether the printed document is collated.</summary>
		/// <returns>true if the printed document is collated; otherwise, false. The default is false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00017F70 File Offset: 0x00016170
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00017F78 File Offset: 0x00016178
		public bool Collate
		{
			get
			{
				return this.collate;
			}
			set
			{
				this.collate = value;
			}
		}

		/// <summary>Gets or sets the number of copies of the document to print.</summary>
		/// <returns>The number of copies to print. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.Copies" /> property is less than zero. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00017F81 File Offset: 0x00016181
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00017F89 File Offset: 0x00016189
		public short Copies
		{
			get
			{
				return this.copies;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("The value of the Copies property is less than zero.");
				}
				this.copies = value;
			}
		}

		/// <summary>Gets the default page settings for this printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PageSettings" /> that represents the default page settings for this printer.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00017FA4 File Offset: 0x000161A4
		public PageSettings DefaultPageSettings
		{
			get
			{
				if (this.default_pagesettings == null)
				{
					this.default_pagesettings = new PageSettings(this, this.SupportsColor, false, new PaperSize("A4", 827, 1169), new PaperSource(PaperSourceKind.FormSource, "Tray"), new PrinterResolution(PrinterResolutionKind.Medium, 200, 200));
				}
				return this.default_pagesettings;
			}
		}

		/// <summary>Gets or sets the printer setting for double-sided printing.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.Duplex" /> values. The default is determined by the printer.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.Duplex" /> property is not one of the <see cref="T:System.Drawing.Printing.Duplex" /> values. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00018003 File Offset: 0x00016203
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0001800B File Offset: 0x0001620B
		public Duplex Duplex
		{
			get
			{
				return this.duplex;
			}
			set
			{
				this.duplex = value;
			}
		}

		/// <summary>Gets or sets the page number of the first page to print.</summary>
		/// <returns>The page number of the first page to print.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> property's value is less than zero. </exception>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00018014 File Offset: 0x00016214
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0001801C File Offset: 0x0001621C
		public int FromPage
		{
			get
			{
				return this.from_page;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("The value of the FromPage property is less than zero");
				}
				this.from_page = value;
			}
		}

		/// <summary>Gets the names of all printers installed on the computer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" /> that represents the names of all printers installed on the computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The available printers could not be enumerated. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00018034 File Offset: 0x00016234
		public static PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				return SysPrn.GlobalService.InstalledPrinters;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates the default printer, except when the user explicitly sets <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" />.</summary>
		/// <returns>true if <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> designates the default printer; otherwise, false.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00018040 File Offset: 0x00016240
		public bool IsDefaultPrinter
		{
			get
			{
				return this.printer_name == this.printing_services.DefaultPrinter;
			}
		}

		/// <summary>Gets a value indicating whether the printer is a plotter.</summary>
		/// <returns>true if the printer is a plotter; false if the printer is a raster.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00018058 File Offset: 0x00016258
		public bool IsPlotter
		{
			get
			{
				return this.is_plotter;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates a valid printer.</summary>
		/// <returns>true if the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates a valid printer; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00018060 File Offset: 0x00016260
		public bool IsValid
		{
			get
			{
				return this.printing_services.IsPrinterValid(this.printer_name);
			}
		}

		/// <summary>Gets the angle, in degrees, that the portrait orientation is rotated to produce the landscape orientation.</summary>
		/// <returns>The angle, in degrees, that the portrait orientation is rotated to produce the landscape orientation.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00018073 File Offset: 0x00016273
		public int LandscapeAngle
		{
			get
			{
				return this.landscape_angle;
			}
		}

		/// <summary>Gets the maximum number of copies that the printer enables the user to print at a time.</summary>
		/// <returns>The maximum number of copies that the printer enables the user to print at a time.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0001807B File Offset: 0x0001627B
		public int MaximumCopies
		{
			get
			{
				return this.maximum_copies;
			}
		}

		/// <summary>Gets or sets the maximum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</summary>
		/// <returns>The maximum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.MaximumPage" /> property is less than zero. </exception>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00018083 File Offset: 0x00016283
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0001808B File Offset: 0x0001628B
		public int MaximumPage
		{
			get
			{
				return this.maximum_page;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("The value of the MaximumPage property is less than zero");
				}
				this.maximum_page = value;
			}
		}

		/// <summary>Gets or sets the minimum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</summary>
		/// <returns>The minimum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.MinimumPage" /> property is less than zero. </exception>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x000180A3 File Offset: 0x000162A3
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x000180AB File Offset: 0x000162AB
		public int MinimumPage
		{
			get
			{
				return this.minimum_page;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("The value of the MaximumPage property is less than zero");
				}
				this.minimum_page = value;
			}
		}

		/// <summary>Gets the paper sizes that are supported by this printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> that represents the paper sizes that are supported by this printer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x000180C3 File Offset: 0x000162C3
		public PrinterSettings.PaperSizeCollection PaperSizes
		{
			get
			{
				if (!this.IsValid)
				{
					throw new InvalidPrinterException(this);
				}
				return this.paper_sizes;
			}
		}

		/// <summary>Gets the paper source trays that are available on the printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> that represents the paper source trays that are available on this printer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x000180DA File Offset: 0x000162DA
		public PrinterSettings.PaperSourceCollection PaperSources
		{
			get
			{
				if (!this.IsValid)
				{
					throw new InvalidPrinterException(this);
				}
				return this.paper_sources;
			}
		}

		/// <summary>Gets or sets the file name, when printing to a file.</summary>
		/// <returns>The file name, when printing to a file.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x000180F1 File Offset: 0x000162F1
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x000180F9 File Offset: 0x000162F9
		public string PrintFileName
		{
			get
			{
				return this.print_filename;
			}
			set
			{
				this.print_filename = value;
			}
		}

		/// <summary>Gets or sets the name of the printer to use.</summary>
		/// <returns>The name of the printer to use.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00018102 File Offset: 0x00016302
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0001810A File Offset: 0x0001630A
		public string PrinterName
		{
			get
			{
				return this.printer_name;
			}
			set
			{
				if (this.printer_name == value)
				{
					return;
				}
				this.printer_name = value;
				this.printing_services.LoadPrinterSettings(this.printer_name, this);
			}
		}

		/// <summary>Gets all the resolutions that are supported by this printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> that represents the resolutions that are supported by this printer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00018134 File Offset: 0x00016334
		public PrinterSettings.PrinterResolutionCollection PrinterResolutions
		{
			get
			{
				if (!this.IsValid)
				{
					throw new InvalidPrinterException(this);
				}
				if (this.printer_resolutions == null)
				{
					this.printer_resolutions = new PrinterSettings.PrinterResolutionCollection(new PrinterResolution[0]);
					this.printing_services.LoadPrinterResolutions(this.printer_name, this);
				}
				return this.printer_resolutions;
			}
		}

		/// <summary>Gets or sets the page numbers that the user has specified to be printed.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrintRange" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.PrintRange" /> property is not one of the <see cref="T:System.Drawing.Printing.PrintRange" /> values. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00018181 File Offset: 0x00016381
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00018189 File Offset: 0x00016389
		public PrintRange PrintRange
		{
			get
			{
				return this.print_range;
			}
			set
			{
				if (value != PrintRange.AllPages && value != PrintRange.Selection && value != PrintRange.SomePages)
				{
					throw new InvalidEnumArgumentException("The value of the PrintRange property is not one of the PrintRange values");
				}
				this.print_range = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the printing output is sent to a file instead of a port.</summary>
		/// <returns>true if the printing output is sent to a file; otherwise, false. The default is false.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x000181A8 File Offset: 0x000163A8
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x000181B0 File Offset: 0x000163B0
		public bool PrintToFile
		{
			get
			{
				return this.print_tofile;
			}
			set
			{
				this.print_tofile = value;
			}
		}

		/// <summary>Gets a value indicating whether this printer supports color printing.</summary>
		/// <returns>true if this printer supports color; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000181B9 File Offset: 0x000163B9
		public bool SupportsColor
		{
			get
			{
				return this.supports_color;
			}
		}

		/// <summary>Gets or sets the number of the last page to print.</summary>
		/// <returns>The number of the last page to print.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> property is less than zero. </exception>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x000181C1 File Offset: 0x000163C1
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x000181C9 File Offset: 0x000163C9
		public int ToPage
		{
			get
			{
				return this.to_page;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("The value of the ToPage property is less than zero");
				}
				this.to_page = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x000181E1 File Offset: 0x000163E1
		internal NameValueCollection PrinterCapabilities
		{
			get
			{
				if (this.printer_capabilities == null)
				{
					this.printer_capabilities = new NameValueCollection();
				}
				return this.printer_capabilities;
			}
		}

		/// <summary>Creates a copy of this <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
		/// <returns>A copy of this object.</returns>
		// Token: 0x06000B29 RID: 2857 RVA: 0x000181FC File Offset: 0x000163FC
		public object Clone()
		{
			return new PrinterSettings(this.printing_services);
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information that is useful when creating a <see cref="T:System.Drawing.Printing.PrintDocument" />. </summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains information from a printer.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000B2A RID: 2858 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.CreateMeasurementGraphics")]
		public Graphics CreateMeasurementGraphics()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information, optionally specifying the origin at the margins.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		/// <param name="honorOriginAtMargins">true to indicate the origin at the margins; otherwise, false. </param>
		// Token: 0x06000B2B RID: 2859 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.CreateMeasurementGraphics")]
		public Graphics CreateMeasurementGraphics(bool honorOriginAtMargins)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information associated with the specified <see cref="T:System.Drawing.Printing.PageSettings" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		/// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> to retrieve a graphics object for.</param>
		// Token: 0x06000B2C RID: 2860 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.CreateMeasurementGraphics")]
		public Graphics CreateMeasurementGraphics(PageSettings pageSettings)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Graphics" /> associated with the specified page settings and optionally specifying the origin at the margins.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		/// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> to retrieve a <see cref="T:System.Drawing.Graphics" /> object for.</param>
		/// <param name="honorOriginAtMargins">true to specify the origin at the margins; otherwise, false. </param>
		// Token: 0x06000B2D RID: 2861 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.CreateMeasurementGraphics")]
		public Graphics CreateMeasurementGraphics(PageSettings pageSettings, bool honorOriginAtMargins)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a handle to a DEVMODE structure that corresponds to the printer settings.</summary>
		/// <returns>A handle to a DEVMODE structure.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The printer's initialization information could not be retrieved. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B2E RID: 2862 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.GetHdevmode")]
		public IntPtr GetHdevmode()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a handle to a DEVMODE structure that corresponds to the printer and the page settings specified through the <paramref name="pageSettings" /> parameter.</summary>
		/// <returns>A handle to a DEVMODE structure.</returns>
		/// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> object that the DEVMODE structure's handle corresponds to. </param>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The printer's initialization information could not be retrieved. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B2F RID: 2863 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.GetHdevmode")]
		public IntPtr GetHdevmode(PageSettings pageSettings)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a handle to a DEVNAMES structure that corresponds to the printer settings.</summary>
		/// <returns>A handle to a DEVNAMES structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B30 RID: 2864 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.GetHdevname")]
		public IntPtr GetHdevnames()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the printer supports printing the specified image file.</summary>
		/// <returns>true if the printer supports printing the specified image; otherwise, false.</returns>
		/// <param name="image">The image to print.</param>
		// Token: 0x06000B31 RID: 2865 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("IsDirectPrintingSupported")]
		public bool IsDirectPrintingSupported(Image image)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a value indicating whether the printer supports printing the specified image format.</summary>
		/// <returns>true if the printer supports printing the specified image format; otherwise, false.</returns>
		/// <param name="imageFormat">An <see cref="T:System.Drawing.Imaging.ImageFormat" /> to print.</param>
		// Token: 0x06000B32 RID: 2866 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("IsDirectPrintingSupported")]
		public bool IsDirectPrintingSupported(ImageFormat imageFormat)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies the relevant information out of the given handle and into the <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
		/// <param name="hdevmode">The handle to a Win32 DEVMODE structure. </param>
		/// <exception cref="T:System.ArgumentException">The printer handle is not valid. </exception>
		// Token: 0x06000B33 RID: 2867 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.SetHdevmode")]
		public void SetHdevmode(IntPtr hdevmode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies the relevant information out of the given handle and into the <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
		/// <param name="hdevnames">The handle to a Win32 DEVNAMES structure. </param>
		/// <exception cref="T:System.ArgumentException">The printer handle is invalid. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B34 RID: 2868 RVA: 0x00005902 File Offset: 0x00003B02
		[MonoTODO("PrinterSettings.SetHdevnames")]
		public void SetHdevnames(IntPtr hdevnames)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PrinterSettings" /> in string form.</summary>
		/// <returns>A string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B35 RID: 2869 RVA: 0x0001820C File Offset: 0x0001640C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Printer [PrinterSettings ",
				this.printer_name,
				" Copies=",
				this.copies,
				" Collate=",
				this.collate.ToString(),
				" Duplex=",
				this.can_duplex.ToString(),
				" FromPage=",
				this.from_page,
				" LandscapeAngle=",
				this.landscape_angle,
				" MaximumCopies=",
				this.maximum_copies,
				" OutputPort= ToPage=",
				this.to_page,
				"]"
			});
		}

		// Token: 0x0400071C RID: 1820
		private string printer_name;

		// Token: 0x0400071D RID: 1821
		private string print_filename;

		// Token: 0x0400071E RID: 1822
		private short copies;

		// Token: 0x0400071F RID: 1823
		private int maximum_page;

		// Token: 0x04000720 RID: 1824
		private int minimum_page;

		// Token: 0x04000721 RID: 1825
		private int from_page;

		// Token: 0x04000722 RID: 1826
		private int to_page;

		// Token: 0x04000723 RID: 1827
		private bool collate;

		// Token: 0x04000724 RID: 1828
		private PrintRange print_range;

		// Token: 0x04000725 RID: 1829
		internal int maximum_copies;

		// Token: 0x04000726 RID: 1830
		internal bool can_duplex;

		// Token: 0x04000727 RID: 1831
		internal bool supports_color;

		// Token: 0x04000728 RID: 1832
		internal int landscape_angle;

		// Token: 0x04000729 RID: 1833
		private bool print_tofile;

		// Token: 0x0400072A RID: 1834
		internal PrinterSettings.PrinterResolutionCollection printer_resolutions;

		// Token: 0x0400072B RID: 1835
		internal PrinterSettings.PaperSizeCollection paper_sizes;

		// Token: 0x0400072C RID: 1836
		internal PrinterSettings.PaperSourceCollection paper_sources;

		// Token: 0x0400072D RID: 1837
		private PageSettings default_pagesettings;

		// Token: 0x0400072E RID: 1838
		private Duplex duplex;

		// Token: 0x0400072F RID: 1839
		internal bool is_plotter;

		// Token: 0x04000730 RID: 1840
		private PrintingServices printing_services;

		// Token: 0x04000731 RID: 1841
		internal NameValueCollection printer_capabilities;

		/// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PaperSource" /> objects.</summary>
		// Token: 0x020000D0 RID: 208
		public class PaperSourceCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> class.</summary>
			/// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PaperSource" />. </param>
			// Token: 0x06000B36 RID: 2870 RVA: 0x000182E0 File Offset: 0x000164E0
			public PaperSourceCollection(PaperSource[] array)
			{
				foreach (PaperSource paperSource in array)
				{
					this._PaperSources.Add(paperSource);
				}
			}

			/// <summary>Gets the number of different paper sources in the collection.</summary>
			/// <returns>The number of different paper sources in the collection.</returns>
			// Token: 0x17000317 RID: 791
			// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001831F File Offset: 0x0001651F
			public int Count
			{
				get
				{
					return this._PaperSources.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			// Token: 0x17000318 RID: 792
			// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001831F File Offset: 0x0001651F
			int ICollection.Count
			{
				get
				{
					return this._PaperSources.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			// Token: 0x17000319 RID: 793
			// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0000915C File Offset: 0x0000735C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			// Token: 0x1700031A RID: 794
			// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00002058 File Offset: 0x00000258
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Adds the specified <see cref="T:System.Drawing.Printing.PaperSource" /> to end of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</summary>
			/// <returns>The zero-based index where the <see cref="T:System.Drawing.Printing.PaperSource" /> was added.</returns>
			/// <param name="paperSource">The <see cref="T:System.Drawing.Printing.PaperSource" /> to add to the collection.</param>
			// Token: 0x06000B3B RID: 2875 RVA: 0x0001832C File Offset: 0x0001652C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(PaperSource paperSource)
			{
				return this._PaperSources.Add(paperSource);
			}

			/// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> to the specified array, starting at the specified index.</summary>
			/// <param name="paperSources">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000B3C RID: 2876 RVA: 0x00005902 File Offset: 0x00003B02
			public void CopyTo(PaperSource[] paperSources, int index)
			{
				throw new NotImplementedException();
			}

			/// <summary>Gets the <see cref="T:System.Drawing.Printing.PaperSource" /> at a specified index.</summary>
			/// <returns>The <see cref="T:System.Drawing.Printing.PaperSource" /> at the specified index.</returns>
			/// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PaperSource" /> to get. </param>
			// Token: 0x1700031B RID: 795
			public virtual PaperSource this[int index]
			{
				get
				{
					return this._PaperSources[index] as PaperSource;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			// Token: 0x06000B3E RID: 2878 RVA: 0x0001834D File Offset: 0x0001654D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._PaperSources.GetEnumerator();
			}

			/// <summary>Returns an enumerator that can iterate through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</returns>
			// Token: 0x06000B3F RID: 2879 RVA: 0x0001834D File Offset: 0x0001654D
			public IEnumerator GetEnumerator()
			{
				return this._PaperSources.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="array">The destination array for the contents of the collection.</param>
			/// <param name="index">The index at which to start the copy operation.</param>
			// Token: 0x06000B40 RID: 2880 RVA: 0x0001835A File Offset: 0x0001655A
			void ICollection.CopyTo(Array array, int index)
			{
				this._PaperSources.CopyTo(array, index);
			}

			// Token: 0x06000B41 RID: 2881 RVA: 0x00018369 File Offset: 0x00016569
			internal void Clear()
			{
				this._PaperSources.Clear();
			}

			// Token: 0x04000732 RID: 1842
			private ArrayList _PaperSources = new ArrayList();
		}

		/// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PaperSize" /> objects.</summary>
		// Token: 0x020000D1 RID: 209
		public class PaperSizeCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> class.</summary>
			/// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PaperSize" />. </param>
			// Token: 0x06000B42 RID: 2882 RVA: 0x00018378 File Offset: 0x00016578
			public PaperSizeCollection(PaperSize[] array)
			{
				foreach (PaperSize paperSize in array)
				{
					this._PaperSizes.Add(paperSize);
				}
			}

			/// <summary>Gets the number of different paper sizes in the collection.</summary>
			/// <returns>The number of different paper sizes in the collection.</returns>
			// Token: 0x1700031C RID: 796
			// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000183B7 File Offset: 0x000165B7
			public int Count
			{
				get
				{
					return this._PaperSizes.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			// Token: 0x1700031D RID: 797
			// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000183B7 File Offset: 0x000165B7
			int ICollection.Count
			{
				get
				{
					return this._PaperSizes.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			// Token: 0x1700031E RID: 798
			// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0000915C File Offset: 0x0000735C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			// Token: 0x1700031F RID: 799
			// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00002058 File Offset: 0x00000258
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Adds a <see cref="T:System.Drawing.Printing.PrinterResolution" /> to the end of the collection.</summary>
			/// <returns>The zero-based index of the newly added item.</returns>
			/// <param name="paperSize">The <see cref="T:System.Drawing.Printing.PaperSize" /> to add to the collection.</param>
			// Token: 0x06000B47 RID: 2887 RVA: 0x000183C4 File Offset: 0x000165C4
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(PaperSize paperSize)
			{
				return this._PaperSizes.Add(paperSize);
			}

			/// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> to the specified array, starting at the specified index.</summary>
			/// <param name="paperSizes">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" />.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000B48 RID: 2888 RVA: 0x00005902 File Offset: 0x00003B02
			public void CopyTo(PaperSize[] paperSizes, int index)
			{
				throw new NotImplementedException();
			}

			/// <summary>Gets the <see cref="T:System.Drawing.Printing.PaperSize" /> at a specified index.</summary>
			/// <returns>The <see cref="T:System.Drawing.Printing.PaperSize" /> at the specified index.</returns>
			/// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PaperSize" /> to get. </param>
			// Token: 0x17000320 RID: 800
			public virtual PaperSize this[int index]
			{
				get
				{
					return this._PaperSizes[index] as PaperSize;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			/// <returns>An enumerator associated with the collection.</returns>
			// Token: 0x06000B4A RID: 2890 RVA: 0x000183E5 File Offset: 0x000165E5
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._PaperSizes.GetEnumerator();
			}

			/// <summary>Returns an enumerator that can iterate through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" />.</returns>
			// Token: 0x06000B4B RID: 2891 RVA: 0x000183E5 File Offset: 0x000165E5
			public IEnumerator GetEnumerator()
			{
				return this._PaperSizes.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="array">A zero-based array that receives the items copied from the collection.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000B4C RID: 2892 RVA: 0x000183F2 File Offset: 0x000165F2
			void ICollection.CopyTo(Array array, int index)
			{
				this._PaperSizes.CopyTo(array, index);
			}

			// Token: 0x06000B4D RID: 2893 RVA: 0x00018401 File Offset: 0x00016601
			internal void Clear()
			{
				this._PaperSizes.Clear();
			}

			// Token: 0x04000733 RID: 1843
			private ArrayList _PaperSizes = new ArrayList();
		}

		/// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PrinterResolution" /> objects.</summary>
		// Token: 0x020000D2 RID: 210
		public class PrinterResolutionCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> class.</summary>
			/// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PrinterResolution" />. </param>
			// Token: 0x06000B4E RID: 2894 RVA: 0x00018410 File Offset: 0x00016610
			public PrinterResolutionCollection(PrinterResolution[] array)
			{
				foreach (PrinterResolution printerResolution in array)
				{
					this._PrinterResolutions.Add(printerResolution);
				}
			}

			/// <summary>Gets the number of available printer resolutions in the collection.</summary>
			/// <returns>The number of available printer resolutions in the collection.</returns>
			// Token: 0x17000321 RID: 801
			// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0001844F File Offset: 0x0001664F
			public int Count
			{
				get
				{
					return this._PrinterResolutions.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0001844F File Offset: 0x0001664F
			int ICollection.Count
			{
				get
				{
					return this._PrinterResolutions.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0000915C File Offset: 0x0000735C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00002058 File Offset: 0x00000258
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Adds a <see cref="T:System.Drawing.Printing.PrinterResolution" /> to the end of the collection.</summary>
			/// <returns>The zero-based index of the newly added item.</returns>
			/// <param name="printerResolution">The <see cref="T:System.Drawing.Printing.PrinterResolution" /> to add to the collection.</param>
			// Token: 0x06000B53 RID: 2899 RVA: 0x0001845C File Offset: 0x0001665C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(PrinterResolution printerResolution)
			{
				return this._PrinterResolutions.Add(printerResolution);
			}

			/// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> to the specified array, starting at the specified index.</summary>
			/// <param name="printerResolutions">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" />.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000B54 RID: 2900 RVA: 0x00005902 File Offset: 0x00003B02
			public void CopyTo(PrinterResolution[] printerResolutions, int index)
			{
				throw new NotImplementedException();
			}

			/// <summary>Gets the <see cref="T:System.Drawing.Printing.PrinterResolution" /> at a specified index.</summary>
			/// <returns>The <see cref="T:System.Drawing.Printing.PrinterResolution" /> at the specified index.</returns>
			/// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PrinterResolution" /> to get. </param>
			// Token: 0x17000325 RID: 805
			public virtual PrinterResolution this[int index]
			{
				get
				{
					return this._PrinterResolutions[index] as PrinterResolution;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			// Token: 0x06000B56 RID: 2902 RVA: 0x0001847D File Offset: 0x0001667D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._PrinterResolutions.GetEnumerator();
			}

			/// <summary>Returns an enumerator that can iterate through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" />.</returns>
			// Token: 0x06000B57 RID: 2903 RVA: 0x0001847D File Offset: 0x0001667D
			public IEnumerator GetEnumerator()
			{
				return this._PrinterResolutions.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="array">The destination array.</param>
			/// <param name="index">The index at which to start the copy operation.</param>
			// Token: 0x06000B58 RID: 2904 RVA: 0x0001848A File Offset: 0x0001668A
			void ICollection.CopyTo(Array array, int index)
			{
				this._PrinterResolutions.CopyTo(array, index);
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x00018499 File Offset: 0x00016699
			internal void Clear()
			{
				this._PrinterResolutions.Clear();
			}

			// Token: 0x04000734 RID: 1844
			private ArrayList _PrinterResolutions = new ArrayList();
		}

		/// <summary>Contains a collection of <see cref="T:System.String" /> objects.</summary>
		// Token: 0x020000D3 RID: 211
		public class StringCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" /> class.</summary>
			/// <param name="array">An array of type <see cref="T:System.String" />. </param>
			// Token: 0x06000B5A RID: 2906 RVA: 0x000184A8 File Offset: 0x000166A8
			public StringCollection(string[] array)
			{
				foreach (string text in array)
				{
					this._Strings.Add(text);
				}
			}

			/// <summary>Gets the number of strings in the collection.</summary>
			/// <returns>The number of strings in the collection.</returns>
			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000184E7 File Offset: 0x000166E7
			public int Count
			{
				get
				{
					return this._Strings.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000184E7 File Offset: 0x000166E7
			int ICollection.Count
			{
				get
				{
					return this._Strings.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0000915C File Offset: 0x0000735C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00002058 File Offset: 0x00000258
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets the <see cref="T:System.String" /> at a specified index.</summary>
			/// <returns>The <see cref="T:System.String" /> at the specified index.</returns>
			/// <param name="index">The index of the <see cref="T:System.String" /> to get. </param>
			// Token: 0x1700032A RID: 810
			public virtual string this[int index]
			{
				get
				{
					return this._Strings[index] as string;
				}
			}

			/// <summary>Adds a string to the end of the collection.</summary>
			/// <returns>The zero-based index of the newly added item.</returns>
			/// <param name="value">The string to add to the collection.</param>
			// Token: 0x06000B60 RID: 2912 RVA: 0x00018507 File Offset: 0x00016707
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(string value)
			{
				return this._Strings.Add(value);
			}

			/// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> to the specified array, starting at the specified index</summary>
			/// <param name="strings">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" />.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000B61 RID: 2913 RVA: 0x00005902 File Offset: 0x00003B02
			public void CopyTo(string[] strings, int index)
			{
				throw new NotImplementedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			// Token: 0x06000B62 RID: 2914 RVA: 0x00018515 File Offset: 0x00016715
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._Strings.GetEnumerator();
			}

			/// <summary>Returns an enumerator that can iterate through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" />.</returns>
			// Token: 0x06000B63 RID: 2915 RVA: 0x00018515 File Offset: 0x00016715
			public IEnumerator GetEnumerator()
			{
				return this._Strings.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="array">The array for items to be copied to.</param>
			/// <param name="index">The starting index.</param>
			// Token: 0x06000B64 RID: 2916 RVA: 0x00018522 File Offset: 0x00016722
			void ICollection.CopyTo(Array array, int index)
			{
				this._Strings.CopyTo(array, index);
			}

			// Token: 0x04000735 RID: 1845
			private ArrayList _Strings = new ArrayList();
		}
	}
}
