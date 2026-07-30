using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Mono.WebBrowser;

namespace System.Windows.Forms
{
	/// <summary>Enables the user to navigate Web pages inside your form. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003A8 RID: 936
	[ClassInterface(1)]
	[Docking(DockingBehavior.AutoDock)]
	[DefaultEvent("DocumentCompleted")]
	[DefaultProperty("Url")]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.WebBrowserDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class WebBrowser : WebBrowserBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowser" /> class.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.WebBrowser" /> control is hosted inside Internet Explorer.</exception>
		// Token: 0x0600441F RID: 17439 RVA: 0x0010C210 File Offset: 0x0010A410
		[MonoTODO("WebBrowser control is only supported on Linux/Windows. No support for OSX.")]
		public WebBrowser()
		{
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.CanGoBack" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400043A RID: 1082
		// (add) Token: 0x06004420 RID: 17440 RVA: 0x0010C228 File Offset: 0x0010A428
		// (remove) Token: 0x06004421 RID: 17441 RVA: 0x0010C244 File Offset: 0x0010A444
		[Browsable(false)]
		public event EventHandler CanGoBackChanged;

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.CanGoForward" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400043B RID: 1083
		// (add) Token: 0x06004422 RID: 17442 RVA: 0x0010C260 File Offset: 0x0010A460
		// (remove) Token: 0x06004423 RID: 17443 RVA: 0x0010C27C File Offset: 0x0010A47C
		[Browsable(false)]
		public event EventHandler CanGoForwardChanged;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control finishes loading a document.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400043C RID: 1084
		// (add) Token: 0x06004424 RID: 17444 RVA: 0x0010C298 File Offset: 0x0010A498
		// (remove) Token: 0x06004425 RID: 17445 RVA: 0x0010C2B4 File Offset: 0x0010A4B4
		public event WebBrowserDocumentCompletedEventHandler DocumentCompleted;

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.DocumentTitle" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400043D RID: 1085
		// (add) Token: 0x06004426 RID: 17446 RVA: 0x0010C2D0 File Offset: 0x0010A4D0
		// (remove) Token: 0x06004427 RID: 17447 RVA: 0x0010C2EC File Offset: 0x0010A4EC
		[Browsable(false)]
		public event EventHandler DocumentTitleChanged;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to or away from a Web site that uses encryption.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400043E RID: 1086
		// (add) Token: 0x06004428 RID: 17448 RVA: 0x0010C308 File Offset: 0x0010A508
		// (remove) Token: 0x06004429 RID: 17449 RVA: 0x0010C324 File Offset: 0x0010A524
		[Browsable(false)]
		public event EventHandler EncryptionLevelChanged;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control downloads a file.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400043F RID: 1087
		// (add) Token: 0x0600442A RID: 17450 RVA: 0x0010C340 File Offset: 0x0010A540
		// (remove) Token: 0x0600442B RID: 17451 RVA: 0x0010C35C File Offset: 0x0010A55C
		public event EventHandler FileDownload;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated to a new document and has begun loading it.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000440 RID: 1088
		// (add) Token: 0x0600442C RID: 17452 RVA: 0x0010C378 File Offset: 0x0010A578
		// (remove) Token: 0x0600442D RID: 17453 RVA: 0x0010C394 File Offset: 0x0010A594
		public event WebBrowserNavigatedEventHandler Navigated;

		/// <summary>Occurs before the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to a new document.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000441 RID: 1089
		// (add) Token: 0x0600442E RID: 17454 RVA: 0x0010C3B0 File Offset: 0x0010A5B0
		// (remove) Token: 0x0600442F RID: 17455 RVA: 0x0010C3CC File Offset: 0x0010A5CC
		public event WebBrowserNavigatingEventHandler Navigating;

		/// <summary>Occurs before a new browser window is opened.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000442 RID: 1090
		// (add) Token: 0x06004430 RID: 17456 RVA: 0x0010C3E8 File Offset: 0x0010A5E8
		// (remove) Token: 0x06004431 RID: 17457 RVA: 0x0010C404 File Offset: 0x0010A604
		public event CancelEventHandler NewWindow;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control has updated information on the download progress of a document it is navigating to.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000443 RID: 1091
		// (add) Token: 0x06004432 RID: 17458 RVA: 0x0010C420 File Offset: 0x0010A620
		// (remove) Token: 0x06004433 RID: 17459 RVA: 0x0010C43C File Offset: 0x0010A63C
		public event WebBrowserProgressChangedEventHandler ProgressChanged;

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.StatusText" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000444 RID: 1092
		// (add) Token: 0x06004434 RID: 17460 RVA: 0x0010C458 File Offset: 0x0010A658
		// (remove) Token: 0x06004435 RID: 17461 RVA: 0x0010C474 File Offset: 0x0010A674
		[Browsable(false)]
		public event EventHandler StatusTextChanged;

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.WebBrowser.Padding" /> property changes.</summary>
		// Token: 0x14000445 RID: 1093
		// (add) Token: 0x06004436 RID: 17462 RVA: 0x0010C490 File Offset: 0x0010A690
		// (remove) Token: 0x06004437 RID: 17463 RVA: 0x0010C4AC File Offset: 0x0010A6AC
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new event EventHandler PaddingChanged;

		/// <summary>Gets or sets a value indicating whether the control can navigate to another page after its initial page has been loaded.</summary>
		/// <returns>true if the control can navigate to another page; otherwise, false.</returns>
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x0010C4C8 File Offset: 0x0010A6C8
		// (set) Token: 0x06004439 RID: 17465 RVA: 0x0010C4D0 File Offset: 0x0010A6D0
		[DefaultValue(true)]
		public bool AllowNavigation
		{
			get
			{
				return this.allowNavigation;
			}
			set
			{
				this.allowNavigation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to documents that are dropped onto it.</summary>
		/// <returns>true if the control accepts documents that are dropped onto it; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x0010C4DC File Offset: 0x0010A6DC
		// (set) Token: 0x0600443B RID: 17467 RVA: 0x0010C4E4 File Offset: 0x0010A6E4
		[DefaultValue(true)]
		public bool AllowWebBrowserDrop
		{
			get
			{
				return this.allowWebBrowserDrop;
			}
			set
			{
				this.allowWebBrowserDrop = value;
			}
		}

		/// <summary>Gets a value indicating whether a previous page in navigation history is available, which allows the <see cref="M:System.Windows.Forms.WebBrowser.GoBack" /> method to succeed.</summary>
		/// <returns>true if the control can navigate backward; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x0600443C RID: 17468 RVA: 0x0010C4F0 File Offset: 0x0010A6F0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool CanGoBack
		{
			get
			{
				return base.WebHost.Navigation.CanGoBack;
			}
		}

		/// <summary>Gets a value indicating whether a subsequent page in navigation history is available, which allows the <see cref="M:System.Windows.Forms.WebBrowser.GoForward" /> method to succeed.</summary>
		/// <returns>true if the control can navigate forward; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x0010C504 File Offset: 0x0010A704
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool CanGoForward
		{
			get
			{
				return base.WebHost.Navigation.CanGoForward;
			}
		}

		/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlDocument" /> representing the Web page currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlDocument" /> representing the current page, or null if no page is loaded.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x0010C518 File Offset: 0x0010A718
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public HtmlDocument Document
		{
			get
			{
				if (this.document == null && this.documentReady)
				{
					this.document = new HtmlDocument(this, base.WebHost);
				}
				return this.document;
			}
		}

		/// <summary>Gets or sets a stream containing the contents of the Web page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the contents of the current Web page, or null if no page is loaded. The default is null.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x0600443F RID: 17471 RVA: 0x0010C55C File Offset: 0x0010A75C
		// (set) Token: 0x06004440 RID: 17472 RVA: 0x0010C594 File Offset: 0x0010A794
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Stream DocumentStream
		{
			get
			{
				if (base.WebHost.Document == null || base.WebHost.Document.DocumentElement == null)
				{
					return null;
				}
				return null;
			}
			set
			{
				if (this.allowNavigation)
				{
					return;
				}
				this.Url = new Uri("about:blank");
				this.data = value;
				this.isStreamSet = true;
			}
		}

		/// <summary>Gets or sets the HTML contents of the page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The HTML text of the displayed page, or the empty string ("") if no document is loaded.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x0010C5CC File Offset: 0x0010A7CC
		// (set) Token: 0x06004442 RID: 17474 RVA: 0x0010C61C File Offset: 0x0010A81C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string DocumentText
		{
			get
			{
				if (base.WebHost.Document == null || base.WebHost.Document.DocumentElement == null)
				{
					return string.Empty;
				}
				return base.WebHost.Document.DocumentElement.OuterHTML;
			}
			set
			{
				if (base.WebHost.Document != null && base.WebHost.Document.DocumentElement != null)
				{
					base.WebHost.Document.DocumentElement.OuterHTML = value;
				}
			}
		}

		/// <summary>Gets the title of the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The title of the current document, or the empty string ("") if no document is loaded.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x0010C664 File Offset: 0x0010A864
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public string DocumentTitle
		{
			get
			{
				if (this.document != null)
				{
					return this.document.Title;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the type of the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The type of the current document.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x0010C694 File Offset: 0x0010A894
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public string DocumentType
		{
			get
			{
				if (this.document != null)
				{
					return this.document.DocType;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a value indicating the encryption method used by the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.WebBrowserEncryptionLevel" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x0010C6C4 File Offset: 0x0010A8C4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public WebBrowserEncryptionLevel EncryptionLevel
		{
			get
			{
				return this.securityLevel;
			}
		}

		/// <summary>Gets a value indicating whether the control or any of its child windows has input focus.</summary>
		/// <returns>true if the control or any of its child windows has input focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x0010C6CC File Offset: 0x0010A8CC
		public override bool Focused
		{
			get
			{
				return base.Focused;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control is currently loading a new document.</summary>
		/// <returns>true if the control is busy loading a document; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06004447 RID: 17479 RVA: 0x0010C6D4 File Offset: 0x0010A8D4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool IsBusy
		{
			get
			{
				return !this.documentReady;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control is in offline mode.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.WebBrowser" /> control is in offline mode; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x0010C6E0 File Offset: 0x0010A8E0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool IsOffline
		{
			get
			{
				return base.WebHost.Offline;
			}
		}

		/// <summary>Gets or a sets a value indicating whether the shortcut menu of the <see cref="T:System.Windows.Forms.WebBrowser" /> control is enabled.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.WebBrowser" /> control shortcut menu is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x0010C6F0 File Offset: 0x0010A8F0
		// (set) Token: 0x0600444A RID: 17482 RVA: 0x0010C6F8 File Offset: 0x0010A8F8
		[DefaultValue(true)]
		[MonoTODO("Stub, not implemented")]
		public bool IsWebBrowserContextMenuEnabled
		{
			get
			{
				return this.isWebBrowserContextMenuEnabled;
			}
			set
			{
				this.isWebBrowserContextMenuEnabled = value;
			}
		}

		/// <summary>Gets or sets an object that can be accessed by scripting code that is contained within a Web page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The object being made available to the scripting code.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is an instance of a non-public type.-or-The specified value when setting this property is an instance of a type that is not COM-visible. For more information, see <see cref="M:System.Runtime.InteropServices.Marshal.IsTypeVisibleFromCom(System.Type)" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x0010C704 File Offset: 0x0010A904
		// (set) Token: 0x0600444C RID: 17484 RVA: 0x0010C70C File Offset: 0x0010A90C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[MonoTODO("Stub, not implemented")]
		public object ObjectForScripting
		{
			get
			{
				return this.objectForScripting;
			}
			set
			{
				this.objectForScripting = value;
			}
		}

		/// <summary>Gets a value indicating the current state of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.WebBrowserReadyState" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x0010C718 File Offset: 0x0010A918
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public WebBrowserReadyState ReadyState
		{
			get
			{
				return this.readyState;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> displays dialog boxes such as script error messages.</summary>
		/// <returns>true if the control does not display its dialog boxes; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x0010C720 File Offset: 0x0010A920
		// (set) Token: 0x0600444F RID: 17487 RVA: 0x0010C728 File Offset: 0x0010A928
		[DefaultValue(false)]
		public bool ScriptErrorsSuppressed
		{
			get
			{
				return base.SuppressDialogs;
			}
			set
			{
				base.SuppressDialogs = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether scroll bars are displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>true if scroll bars are displayed in the control; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x0010C734 File Offset: 0x0010A934
		// (set) Token: 0x06004451 RID: 17489 RVA: 0x0010C73C File Offset: 0x0010A93C
		[DefaultValue(true)]
		public bool ScrollBarsEnabled
		{
			get
			{
				return this.scrollbarsEnabled;
			}
			set
			{
				this.scrollbarsEnabled = value;
				if (this.document != null)
				{
					this.SetScrollbars();
				}
			}
		}

		/// <summary>Gets the status text of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The status text.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x0010C75C File Offset: 0x0010A95C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public virtual string StatusText
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Gets or sets the URL of the current document.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the URL of the current document.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x0010C764 File Offset: 0x0010A964
		// (set) Token: 0x06004454 RID: 17492 RVA: 0x0010C7C4 File Offset: 0x0010A9C4
		[TypeConverter(typeof(WebBrowserUriTypeConverter))]
		[DefaultValue(null)]
		[Bindable(true)]
		public Uri Url
		{
			get
			{
				if (this.url != null)
				{
					return new Uri(this.url);
				}
				if (base.WebHost.Document != null && base.WebHost.Document.Url != null)
				{
					return new Uri(base.WebHost.Document.Url);
				}
				return null;
			}
			set
			{
				this.url = null;
				this.Navigate(value);
			}
		}

		/// <summary>Gets the version of Internet Explorer installed.</summary>
		/// <returns>A <see cref="T:System.Version" /> object representing the version of Internet Explorer installed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x0010C7D4 File Offset: 0x0010A9D4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Version Version
		{
			get
			{
				Assembly assembly = base.WebHost.GetType().Assembly;
				return assembly.GetName().Version;
			}
		}

		/// <summary>Gets or sets a value indicating whether keyboard shortcuts are enabled within the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>true if keyboard shortcuts are enabled within the control; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x0010C800 File Offset: 0x0010AA00
		// (set) Token: 0x06004457 RID: 17495 RVA: 0x0010C808 File Offset: 0x0010AA08
		[MonoTODO("Stub, not implemented")]
		[DefaultValue(true)]
		public bool WebBrowserShortcutsEnabled
		{
			get
			{
				return this.webBrowserShortcutsEnabled;
			}
			set
			{
				this.webBrowserShortcutsEnabled = value;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>Gets the default size of the control.</returns>
		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x0010C814 File Offset: 0x0010AA14
		protected override Size DefaultSize
		{
			get
			{
				return base.DefaultSize;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" />
		/// </returns>
		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x0010C81C File Offset: 0x0010AA1C
		// (set) Token: 0x0600445A RID: 17498 RVA: 0x0010C824 File Offset: 0x0010AA24
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the previous page in the navigation history, if one is available.</summary>
		/// <returns>true if the navigation succeeds; false if a previous page in the navigation history is not available.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600445B RID: 17499 RVA: 0x0010C830 File Offset: 0x0010AA30
		public bool GoBack()
		{
			this.documentReady = false;
			this.document = null;
			return base.WebHost.Navigation.Back();
		}

		/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the next page in the navigation history, if one is available.</summary>
		/// <returns>true if the navigation succeeds; false if a subsequent page in the navigation history is not available.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600445C RID: 17500 RVA: 0x0010C850 File Offset: 0x0010AA50
		public bool GoForward()
		{
			this.documentReady = false;
			this.document = null;
			return base.WebHost.Navigation.Forward();
		}

		/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the home page of the current user.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600445D RID: 17501 RVA: 0x0010C870 File Offset: 0x0010AA70
		public void GoHome()
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Home();
		}

		/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the previous document.</summary>
		/// <param name="urlString">The URL of the document to load.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600445E RID: 17502 RVA: 0x0010C890 File Offset: 0x0010AA90
		public void Navigate(string urlString)
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Go(urlString);
		}

		/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the previous document.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load. </param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x0600445F RID: 17503 RVA: 0x0010C8B4 File Offset: 0x0010AAB4
		public void Navigate(Uri url)
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Go(url.ToString());
		}

		/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into a new browser window or into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <param name="urlString">The URL of the document to load.</param>
		/// <param name="newWindow">true to load the document into a new browser window; false to load the document into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004460 RID: 17504 RVA: 0x0010C8E8 File Offset: 0x0010AAE8
		public void Navigate(string urlString, bool newWindow)
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Go(urlString);
		}

		/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the contents of the Web page frame with the specified name.</summary>
		/// <param name="urlString">The URL of the document to load.</param>
		/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004461 RID: 17505 RVA: 0x0010C90C File Offset: 0x0010AB0C
		public void Navigate(string urlString, string targetFrameName)
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Go(urlString);
		}

		/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into a new browser window or into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
		/// <param name="newWindow">true to load the document into a new browser window; false to load the document into the <see cref="T:System.Windows.Forms.WebBrowser" /> control. </param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x06004462 RID: 17506 RVA: 0x0010C930 File Offset: 0x0010AB30
		public void Navigate(Uri url, bool newWindow)
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Go(url.ToString());
		}

		/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the contents of the Web page frame with the specified name.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
		/// <param name="targetFrameName">The name of the frame in which to load the document. </param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x06004463 RID: 17507 RVA: 0x0010C964 File Offset: 0x0010AB64
		public void Navigate(Uri url, string targetFrameName)
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Go(url.ToString());
		}

		/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, requesting it using the specified HTTP data and replacing the contents of the Web page frame with the specified name.</summary>
		/// <param name="urlString">The URL of the document to load.</param>
		/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
		/// <param name="postData">HTTP POST data such as form data.</param>
		/// <param name="additionalHeaders">HTTP headers to add to the default headers.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		// Token: 0x06004464 RID: 17508 RVA: 0x0010C998 File Offset: 0x0010AB98
		public void Navigate(string urlString, string targetFrameName, byte[] postData, string additionalHeaders)
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Go(urlString);
		}

		/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, requesting it using the specified HTTP data and replacing the contents of the Web page frame with the specified name.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
		/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
		/// <param name="postData">HTTP POST data such as form data.</param>
		/// <param name="additionalHeaders">HTTP headers to add to the default headers.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x06004465 RID: 17509 RVA: 0x0010C9BC File Offset: 0x0010ABBC
		public void Navigate(Uri url, string targetFrameName, byte[] postData, string additionalHeaders)
		{
			this.documentReady = false;
			this.document = null;
			base.WebHost.Navigation.Go(url.ToString());
		}

		/// <summary>Reloads the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control by checking the server for an updated version.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004466 RID: 17510 RVA: 0x0010C9F0 File Offset: 0x0010ABF0
		public override void Refresh()
		{
			this.Refresh(WebBrowserRefreshOption.IfExpired);
		}

		/// <summary>Reloads the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control using the specified refresh options.</summary>
		/// <param name="opt">One of the <see cref="T:System.Windows.Forms.WebBrowserRefreshOption" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004467 RID: 17511 RVA: 0x0010C9FC File Offset: 0x0010ABFC
		public void Refresh(WebBrowserRefreshOption opt)
		{
			this.documentReady = false;
			this.document = null;
			switch (opt)
			{
			case WebBrowserRefreshOption.Normal:
				base.WebHost.Navigation.Reload(1);
				break;
			case WebBrowserRefreshOption.IfExpired:
				base.WebHost.Navigation.Reload(0);
				break;
			case WebBrowserRefreshOption.Completely:
				base.WebHost.Navigation.Reload(2);
				break;
			}
		}

		/// <summary>Cancels any pending navigation and stops any dynamic page elements, such as background sounds and animations.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004468 RID: 17512 RVA: 0x0010CA78 File Offset: 0x0010AC78
		public void Stop()
		{
			base.WebHost.Navigation.Stop();
		}

		/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the default search page of the current user.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004469 RID: 17513 RVA: 0x0010CA8C File Offset: 0x0010AC8C
		public void GoSearch()
		{
			string text = "http://www.google.com";
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\Search Page");
				if (registryKey != null)
				{
					object value = registryKey.GetValue("Default_Search_URL");
					Uri uri;
					if (value != null && value is string && Uri.TryCreate(value as string, 1, ref uri))
					{
						text = uri.ToString();
					}
				}
			}
			catch
			{
			}
			this.Navigate(text);
		}

		/// <summary>Prints the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control using the current print and page settings.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600446A RID: 17514 RVA: 0x0010CB1C File Offset: 0x0010AD1C
		public void Print()
		{
			throw new NotImplementedException();
		}

		/// <summary>Opens the Internet Explorer Page Setup dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600446B RID: 17515 RVA: 0x0010CB24 File Offset: 0x0010AD24
		public void ShowPageSetupDialog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Opens the Internet Explorer Print dialog box without setting header and footer values.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600446C RID: 17516 RVA: 0x0010CB2C File Offset: 0x0010AD2C
		public void ShowPrintDialog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Opens the Internet Explorer Print Preview dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600446D RID: 17517 RVA: 0x0010CB34 File Offset: 0x0010AD34
		public void ShowPrintPreviewDialog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Opens the Internet Explorer Properties dialog box for the current document.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600446E RID: 17518 RVA: 0x0010CB3C File Offset: 0x0010AD3C
		public void ShowPropertiesDialog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Opens the Internet Explorer Save Web Page dialog box or the Save dialog box of the hosted document if it is not an HTML page.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600446F RID: 17519 RVA: 0x0010CB44 File Offset: 0x0010AD44
		public void ShowSaveAsDialog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the control when the underlying ActiveX control is created.</summary>
		/// <param name="nativeActiveXObject">An object that represents the underlying ActiveX control.</param>
		// Token: 0x06004470 RID: 17520 RVA: 0x0010CB4C File Offset: 0x0010AD4C
		[MonoTODO("Stub, not implemented")]
		protected override void AttachInterfaces(object nativeActiveXObject)
		{
			base.AttachInterfaces(nativeActiveXObject);
		}

		/// <summary>Associates the underlying ActiveX control with a client that can handle control events.</summary>
		// Token: 0x06004471 RID: 17521 RVA: 0x0010CB58 File Offset: 0x0010AD58
		[MonoTODO("Stub, not implemented")]
		protected override void CreateSink()
		{
			base.CreateSink();
		}

		/// <summary>Returns a reference to the unmanaged WebBrowser ActiveX control site, which you can extend to customize the managed <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> that represents the WebBrowser ActiveX control site.</returns>
		// Token: 0x06004472 RID: 17522 RVA: 0x0010CB60 File Offset: 0x0010AD60
		[MonoTODO("Stub, not implemented")]
		protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
		{
			return base.CreateWebBrowserSiteBase();
		}

		/// <summary>Called by the control when the underlying ActiveX control is discarded.</summary>
		// Token: 0x06004473 RID: 17523 RVA: 0x0010CB68 File Offset: 0x0010AD68
		[MonoTODO("Stub, not implemented")]
		protected override void DetachInterfaces()
		{
			base.DetachInterfaces();
		}

		/// <summary>Releases the event-handling client attached in the <see cref="M:System.Windows.Forms.WebBrowser.CreateSink" /> method from the underlying ActiveX control.</summary>
		// Token: 0x06004474 RID: 17524 RVA: 0x0010CB70 File Offset: 0x0010AD70
		[MonoTODO("Stub, not implemented")]
		protected override void DetachSink()
		{
			base.DetachSink();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.WebBrowser" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06004475 RID: 17525 RVA: 0x0010CB78 File Offset: 0x0010AD78
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <param name="m">The windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06004476 RID: 17526 RVA: 0x0010CB84 File Offset: 0x0010AD84
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.CanGoBackChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004477 RID: 17527 RVA: 0x0010CB90 File Offset: 0x0010AD90
		protected virtual void OnCanGoBackChanged(EventArgs e)
		{
			if (this.CanGoBackChanged != null)
			{
				this.CanGoBackChanged.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.CanGoForwardChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004478 RID: 17528 RVA: 0x0010CBAC File Offset: 0x0010ADAC
		protected virtual void OnCanGoForwardChanged(EventArgs e)
		{
			if (this.CanGoForwardChanged != null)
			{
				this.CanGoForwardChanged.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.DocumentCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserDocumentCompletedEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		// Token: 0x06004479 RID: 17529 RVA: 0x0010CBC8 File Offset: 0x0010ADC8
		protected virtual void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			if (this.DocumentCompleted != null)
			{
				this.DocumentCompleted(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.DocumentTitleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600447A RID: 17530 RVA: 0x0010CBE4 File Offset: 0x0010ADE4
		protected virtual void OnDocumentTitleChanged(EventArgs e)
		{
			if (this.DocumentTitleChanged != null)
			{
				this.DocumentTitleChanged.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.EncryptionLevelChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600447B RID: 17531 RVA: 0x0010CC00 File Offset: 0x0010AE00
		protected virtual void OnEncryptionLevelChanged(EventArgs e)
		{
			if (this.EncryptionLevelChanged != null)
			{
				this.EncryptionLevelChanged.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.FileDownload" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x0600447C RID: 17532 RVA: 0x0010CC1C File Offset: 0x0010AE1C
		protected virtual void OnFileDownload(EventArgs e)
		{
			if (this.FileDownload != null)
			{
				this.FileDownload.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.Navigated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserNavigatedEventArgs" /> that contains the event data. </param>
		// Token: 0x0600447D RID: 17533 RVA: 0x0010CC38 File Offset: 0x0010AE38
		protected virtual void OnNavigated(WebBrowserNavigatedEventArgs e)
		{
			if (this.Navigated != null)
			{
				this.Navigated(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.Navigating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserNavigatingEventArgs" /> that contains the event data. </param>
		// Token: 0x0600447E RID: 17534 RVA: 0x0010CC54 File Offset: 0x0010AE54
		protected virtual void OnNavigating(WebBrowserNavigatingEventArgs e)
		{
			if (this.Navigating != null)
			{
				this.Navigating(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.NewWindow" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x0600447F RID: 17535 RVA: 0x0010CC70 File Offset: 0x0010AE70
		protected virtual void OnNewWindow(CancelEventArgs e)
		{
			if (this.NewWindow != null)
			{
				this.NewWindow.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.ProgressChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserProgressChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06004480 RID: 17536 RVA: 0x0010CC8C File Offset: 0x0010AE8C
		protected virtual void OnProgressChanged(WebBrowserProgressChangedEventArgs e)
		{
			if (this.ProgressChanged != null)
			{
				this.ProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.StatusTextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004481 RID: 17537 RVA: 0x0010CCA8 File Offset: 0x0010AEA8
		protected virtual void OnStatusTextChanged(EventArgs e)
		{
			if (this.StatusTextChanged != null)
			{
				this.StatusTextChanged.Invoke(this, e);
			}
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x0010CCC4 File Offset: 0x0010AEC4
		internal override bool OnNewWindowInternal()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnNewWindow(cancelEventArgs);
			return cancelEventArgs.Cancel;
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x0010CCE4 File Offset: 0x0010AEE4
		internal override void OnWebHostLoadStarted(object sender, LoadStartedEventArgs e)
		{
			this.documentReady = false;
			this.document = null;
			this.readyState = WebBrowserReadyState.Loading;
			WebBrowserNavigatingEventArgs webBrowserNavigatingEventArgs = new WebBrowserNavigatingEventArgs(new Uri(e.Uri), e.FrameName);
			this.OnNavigating(webBrowserNavigatingEventArgs);
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x0010CD24 File Offset: 0x0010AF24
		internal override void OnWebHostLoadCommited(object sender, LoadCommitedEventArgs e)
		{
			this.readyState = WebBrowserReadyState.Loaded;
			this.url = e.Uri;
			this.SetScrollbars();
			WebBrowserNavigatedEventArgs webBrowserNavigatedEventArgs = new WebBrowserNavigatedEventArgs(new Uri(e.Uri));
			this.OnNavigated(webBrowserNavigatedEventArgs);
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x0010CD64 File Offset: 0x0010AF64
		internal override void OnWebHostProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.readyState = WebBrowserReadyState.Interactive;
			WebBrowserProgressChangedEventArgs webBrowserProgressChangedEventArgs = new WebBrowserProgressChangedEventArgs((long)e.Progress, (long)e.MaxProgress);
			this.OnProgressChanged(webBrowserProgressChangedEventArgs);
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x0010CD94 File Offset: 0x0010AF94
		internal override void OnWebHostLoadFinished(object sender, LoadFinishedEventArgs e)
		{
			this.url = null;
			this.documentReady = true;
			this.readyState = WebBrowserReadyState.Complete;
			if (this.isStreamSet)
			{
				byte[] array = new byte[this.data.Length];
				long length = this.data.Length;
				this.data.Position = 0L;
				int num;
				do
				{
					num = this.data.Read(array, (int)this.data.Position, (int)(length - this.data.Position));
				}
				while (num > 0);
				base.WebHost.Render(array);
				this.data = null;
				this.isStreamSet = false;
			}
			this.SetScrollbars();
			WebBrowserDocumentCompletedEventArgs webBrowserDocumentCompletedEventArgs = new WebBrowserDocumentCompletedEventArgs(new Uri(e.Uri));
			this.OnDocumentCompleted(webBrowserDocumentCompletedEventArgs);
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x0010CE54 File Offset: 0x0010B054
		internal override void OnWebHostSecurityChanged(object sender, SecurityChangedEventArgs e)
		{
			switch (e.State)
			{
			case 1:
				this.securityLevel = WebBrowserEncryptionLevel.Insecure;
				break;
			case 2:
				this.securityLevel = WebBrowserEncryptionLevel.Mixed;
				break;
			case 3:
				this.securityLevel = WebBrowserEncryptionLevel.Bit56;
				break;
			}
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x0010CEA8 File Offset: 0x0010B0A8
		internal override void OnWebHostContextMenuShown(object sender, ContextMenuEventArgs e)
		{
			if (!this.isWebBrowserContextMenuEnabled)
			{
				return;
			}
			ContextMenu contextMenu = new ContextMenu();
			MenuItem menuItem = new MenuItem("Back", delegate
			{
				this.GoBack();
			});
			menuItem.Enabled = this.CanGoBack;
			contextMenu.MenuItems.Add(menuItem);
			menuItem = new MenuItem("Forward", delegate
			{
				this.GoForward();
			});
			menuItem.Enabled = this.CanGoForward;
			contextMenu.MenuItems.Add(menuItem);
			menuItem = new MenuItem("Refresh", delegate
			{
				this.Refresh();
			});
			contextMenu.MenuItems.Add(menuItem);
			contextMenu.MenuItems.Add(new MenuItem("-"));
			contextMenu.Show(this, base.PointToClient(Control.MousePosition));
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x0010CF74 File Offset: 0x0010B174
		internal override void OnWebHostStatusChanged(object sender, StatusChangedEventArgs e)
		{
			this.status = e.Message;
			this.OnStatusTextChanged(null);
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x0010CF8C File Offset: 0x0010B18C
		private void SetScrollbars()
		{
		}

		// Token: 0x04001C95 RID: 7317
		private bool allowNavigation;

		// Token: 0x04001C96 RID: 7318
		private bool allowWebBrowserDrop = true;

		// Token: 0x04001C97 RID: 7319
		private bool isWebBrowserContextMenuEnabled;

		// Token: 0x04001C98 RID: 7320
		private object objectForScripting;

		// Token: 0x04001C99 RID: 7321
		private bool webBrowserShortcutsEnabled;

		// Token: 0x04001C9A RID: 7322
		private bool scrollbarsEnabled = true;

		// Token: 0x04001C9B RID: 7323
		private WebBrowserReadyState readyState;

		// Token: 0x04001C9C RID: 7324
		private HtmlDocument document;

		// Token: 0x04001C9D RID: 7325
		private WebBrowserEncryptionLevel securityLevel;

		// Token: 0x04001C9E RID: 7326
		private Stream data;

		// Token: 0x04001C9F RID: 7327
		private bool isStreamSet;

		// Token: 0x04001CA0 RID: 7328
		private string url;

		/// <summary>Represents the host window of a <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		// Token: 0x020003A9 RID: 937
		[MonoTODO("Stub, not implemented")]
		[ComVisible(false)]
		protected class WebBrowserSite : WebBrowserSiteBase
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> class. </summary>
			/// <param name="host">The <see cref="T:System.Windows.Forms.WebBrowser" /></param>
			// Token: 0x0600448E RID: 17550 RVA: 0x0010CFB0 File Offset: 0x0010B1B0
			[MonoTODO("Stub, not implemented")]
			public WebBrowserSite(WebBrowser host)
			{
			}
		}
	}
}
