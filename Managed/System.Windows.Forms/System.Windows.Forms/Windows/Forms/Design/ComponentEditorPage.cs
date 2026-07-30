using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a base implementation for a <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" />.</summary>
	// Token: 0x02000013 RID: 19
	[ComVisible(true)]
	[ClassInterface(1)]
	public abstract class ComponentEditorPage : Panel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" /> class.</summary>
		// Token: 0x0600007F RID: 127 RVA: 0x00004060 File Offset: 0x00002260
		public ComponentEditorPage()
		{
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000080 RID: 128 RVA: 0x00004070 File Offset: 0x00002270
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x0000407C File Offset: 0x0000227C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004088 File Offset: 0x00002288
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004090 File Offset: 0x00002290
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new virtual bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Specifies whether the editor should apply its changes before it is deactivated.</summary>
		/// <returns>true if the editor should apply its changes; otherwise, false.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000409C File Offset: 0x0000229C
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000040A4 File Offset: 0x000022A4
		public bool CommitOnDeactivate
		{
			get
			{
				return this.commitOnDeactivate;
			}
			set
			{
				this.commitOnDeactivate = value;
			}
		}

		/// <summary>Gets or sets the component to edit.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> this page allows you to edit.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000040B0 File Offset: 0x000022B0
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000040B8 File Offset: 0x000022B8
		protected IComponent Component
		{
			get
			{
				return this.component;
			}
			set
			{
				this.component = value;
			}
		}

		/// <summary>Gets the creation parameters for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that indicates the creation parameters for the control.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000040C4 File Offset: 0x000022C4
		[MonoTODO("Find out what this does.")]
		protected override CreateParams CreateParams
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the page is being activated for the first time.</summary>
		/// <returns>true if the page has not previously been activated; otherwise, false.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000040CC File Offset: 0x000022CC
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000040D4 File Offset: 0x000022D4
		protected bool FirstActivate
		{
			get
			{
				return this.firstActivate;
			}
			set
			{
				this.firstActivate = value;
			}
		}

		/// <summary>Gets or sets the icon for the page.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> used to represent the page.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000040E0 File Offset: 0x000022E0
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000040E8 File Offset: 0x000022E8
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		/// <summary>Indicates how many load dependencies remain until loading has been completed.</summary>
		/// <returns>The number of remaining load dependencies.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000040F4 File Offset: 0x000022F4
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000040FC File Offset: 0x000022FC
		protected int Loading
		{
			get
			{
				return this.loading;
			}
			set
			{
				this.loading = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a component must be loaded before editing can occur.</summary>
		/// <returns>true if a component must be loaded before editing can occur; otherwise, false.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004108 File Offset: 0x00002308
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004110 File Offset: 0x00002310
		protected bool LoadRequired
		{
			get
			{
				return this.loadRequired;
			}
			set
			{
				this.loadRequired = value;
			}
		}

		/// <summary>Gets or sets the page site.</summary>
		/// <returns>The page site.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000411C File Offset: 0x0000231C
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004124 File Offset: 0x00002324
		protected IComponentEditorPageSite PageSite
		{
			get
			{
				return this.pageSite;
			}
			set
			{
				this.pageSite = value;
			}
		}

		/// <summary>Gets the title of the page.</summary>
		/// <returns>The title of the page.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004130 File Offset: 0x00002330
		public virtual string Title
		{
			get
			{
				return base.Text;
			}
		}

		/// <summary>Activates and displays the page.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000094 RID: 148 RVA: 0x00004138 File Offset: 0x00002338
		public virtual void Activate()
		{
			base.Visible = true;
			this.firstActivate = false;
			if (this.loadRequired)
			{
				this.EnterLoadingMode();
				this.LoadComponent();
				this.ExitLoadingMode();
			}
		}

		/// <summary>Applies changes to all the components being edited.</summary>
		// Token: 0x06000095 RID: 149 RVA: 0x00004168 File Offset: 0x00002368
		public virtual void ApplyChanges()
		{
			this.SaveComponent();
		}

		/// <summary>Deactivates and hides the page.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000096 RID: 150 RVA: 0x00004170 File Offset: 0x00002370
		public virtual void Deactivate()
		{
			base.Visible = false;
		}

		/// <summary>Increments the loading counter.</summary>
		// Token: 0x06000097 RID: 151 RVA: 0x0000417C File Offset: 0x0000237C
		protected void EnterLoadingMode()
		{
			this.loading++;
		}

		/// <summary>Decrements the loading counter.</summary>
		// Token: 0x06000098 RID: 152 RVA: 0x0000418C File Offset: 0x0000238C
		protected void ExitLoadingMode()
		{
			this.loading--;
		}

		/// <summary>Gets the control that represents the window for this page.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the window for this page.</returns>
		// Token: 0x06000099 RID: 153 RVA: 0x0000419C File Offset: 0x0000239C
		public virtual Control GetControl()
		{
			return this;
		}

		/// <summary>Gets the component that is to be edited.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that is to be edited.</returns>
		// Token: 0x0600009A RID: 154 RVA: 0x000041A0 File Offset: 0x000023A0
		protected IComponent GetSelectedComponent()
		{
			return this.component;
		}

		/// <summary>Gets a value indicating whether the page is being activated for the first time.</summary>
		/// <returns>true if this is the first time the page is being activated; otherwise, false.</returns>
		// Token: 0x0600009B RID: 155 RVA: 0x000041A8 File Offset: 0x000023A8
		protected bool IsFirstActivate()
		{
			return this.firstActivate;
		}

		/// <summary>Gets a value indicating whether the page is being loaded.</summary>
		/// <returns>true if the page is being loaded; otherwise, false.</returns>
		// Token: 0x0600009C RID: 156 RVA: 0x000041B0 File Offset: 0x000023B0
		protected bool IsLoading()
		{
			return this.loading != 0;
		}

		/// <summary>Processes messages that could be handled by the page.</summary>
		/// <returns>true if the page processed the message; otherwise, false.</returns>
		/// <param name="msg">The message to process. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600009D RID: 157 RVA: 0x000041C0 File Offset: 0x000023C0
		public virtual bool IsPageMessage(ref Message msg)
		{
			return this.PreProcessMessage(ref msg);
		}

		/// <summary>Loads the component into the page user interface (UI).</summary>
		// Token: 0x0600009E RID: 158
		protected abstract void LoadComponent();

		/// <summary>Called when the page and any sibling pages have applied their changes.</summary>
		// Token: 0x0600009F RID: 159 RVA: 0x000041CC File Offset: 0x000023CC
		[MonoTODO("Find out what this does.")]
		public virtual void OnApplyComplete()
		{
		}

		/// <summary>Reloads the component for the page.</summary>
		// Token: 0x060000A0 RID: 160 RVA: 0x000041D0 File Offset: 0x000023D0
		protected virtual void ReloadComponent()
		{
			this.loadRequired = true;
		}

		/// <summary>Saves the component from the page user interface (UI).</summary>
		// Token: 0x060000A1 RID: 161
		protected abstract void SaveComponent();

		/// <summary>Sets the component to be edited.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to be edited. </param>
		// Token: 0x060000A2 RID: 162 RVA: 0x000041DC File Offset: 0x000023DC
		public virtual void SetComponent(IComponent component)
		{
			this.component = component;
			this.ReloadComponent();
		}

		/// <summary>Sets the page as changed since the last load or save.</summary>
		// Token: 0x060000A3 RID: 163 RVA: 0x000041EC File Offset: 0x000023EC
		[MonoTODO("Find out what this does.")]
		protected virtual void SetDirty()
		{
		}

		/// <summary>Sets the site for this page.</summary>
		/// <param name="site">The site for this page. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060000A4 RID: 164 RVA: 0x000041F0 File Offset: 0x000023F0
		public virtual void SetSite(IComponentEditorPageSite site)
		{
			this.pageSite = site;
			this.pageSite.GetControl().Controls.Add(this);
		}

		/// <summary>Shows Help information if the page supports Help information.</summary>
		// Token: 0x060000A5 RID: 165 RVA: 0x00004210 File Offset: 0x00002410
		public virtual void ShowHelp()
		{
		}

		/// <summary>Gets a value indicating whether the editor supports Help.</summary>
		/// <returns>true if the editor supports Help; otherwise, false. The default implementation returns false.</returns>
		// Token: 0x060000A6 RID: 166 RVA: 0x00004214 File Offset: 0x00002414
		public virtual bool SupportsHelp()
		{
			return false;
		}

		// Token: 0x04000045 RID: 69
		private bool commitOnDeactivate;

		// Token: 0x04000046 RID: 70
		private IComponent component;

		// Token: 0x04000047 RID: 71
		private bool firstActivate = true;

		// Token: 0x04000048 RID: 72
		private Icon icon;

		// Token: 0x04000049 RID: 73
		private int loading;

		// Token: 0x0400004A RID: 74
		private bool loadRequired;

		// Token: 0x0400004B RID: 75
		private IComponentEditorPageSite pageSite;
	}
}
