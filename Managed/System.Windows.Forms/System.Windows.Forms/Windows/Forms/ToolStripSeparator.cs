using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a line used to group items of a <see cref="T:System.Windows.Forms.ToolStrip" /> or the drop-down items of a <see cref="T:System.Windows.Forms.MenuStrip" /> or <see cref="T:System.Windows.Forms.ContextMenuStrip" /> or other <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000378 RID: 888
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripSeparator : ToolStripItem
	{
		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140003EC RID: 1004
		// (add) Token: 0x06003FFA RID: 16378 RVA: 0x000FF264 File Offset: 0x000FD464
		// (remove) Token: 0x06003FFB RID: 16379 RVA: 0x000FF270 File Offset: 0x000FD470
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler DisplayStyleChanged
		{
			add
			{
				base.DisplayStyleChanged += value;
			}
			remove
			{
				base.DisplayStyleChanged -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140003ED RID: 1005
		// (add) Token: 0x06003FFC RID: 16380 RVA: 0x000FF27C File Offset: 0x000FD47C
		// (remove) Token: 0x06003FFD RID: 16381 RVA: 0x000FF288 File Offset: 0x000FD488
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140003EE RID: 1006
		// (add) Token: 0x06003FFE RID: 16382 RVA: 0x000FF294 File Offset: 0x000FD494
		// (remove) Token: 0x06003FFF RID: 16383 RVA: 0x000FF2A0 File Offset: 0x000FD4A0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false. </returns>
		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x000FF2AC File Offset: 0x000FD4AC
		// (set) Token: 0x06004001 RID: 16385 RVA: 0x000FF2B4 File Offset: 0x000FD4B4
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new bool AutoToolTip
		{
			get
			{
				return base.AutoToolTip;
			}
			set
			{
				base.AutoToolTip = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x000FF2C0 File Offset: 0x000FD4C0
		// (set) Token: 0x06004003 RID: 16387 RVA: 0x000FF2C8 File Offset: 0x000FD4C8
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000FF2D4 File Offset: 0x000FD4D4
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x000FF2DC File Offset: 0x000FD4DC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> can be selected. </summary>
		/// <returns>true if the component using the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is in design mode; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x000FF2E8 File Offset: 0x000FD4E8
		public override bool CanSelect
		{
			get
			{
				return false;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x000FF2EC File Offset: 0x000FD4EC
		// (set) Token: 0x06004008 RID: 16392 RVA: 0x000FF2F4 File Offset: 0x000FD4F4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return base.DisplayStyle;
			}
			set
			{
				base.DisplayStyle = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false. </returns>
		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x000FF300 File Offset: 0x000FD500
		// (set) Token: 0x0600400A RID: 16394 RVA: 0x000FF308 File Offset: 0x000FD508
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new bool DoubleClickEnabled
		{
			get
			{
				return base.DoubleClickEnabled;
			}
			set
			{
				base.DoubleClickEnabled = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x000FF314 File Offset: 0x000FD514
		// (set) Token: 0x0600400C RID: 16396 RVA: 0x000FF31C File Offset: 0x000FD51C
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x0600400D RID: 16397 RVA: 0x000FF328 File Offset: 0x000FD528
		// (set) Token: 0x0600400E RID: 16398 RVA: 0x000FF330 File Offset: 0x000FD530
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x000FF33C File Offset: 0x000FD53C
		// (set) Token: 0x06004010 RID: 16400 RVA: 0x000FF344 File Offset: 0x000FD544
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Image Image
		{
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.ContentAlignment" /> value.</returns>
		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x000FF350 File Offset: 0x000FD550
		// (set) Token: 0x06004012 RID: 16402 RVA: 0x000FF358 File Offset: 0x000FD558
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
			set
			{
				base.ImageAlign = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Int32" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06004013 RID: 16403 RVA: 0x000FF364 File Offset: 0x000FD564
		// (set) Token: 0x06004014 RID: 16404 RVA: 0x000FF36C File Offset: 0x000FD56C
		[RefreshProperties(2)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new int ImageIndex
		{
			get
			{
				return base.ImageIndex;
			}
			set
			{
				base.ImageIndex = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x000FF378 File Offset: 0x000FD578
		// (set) Token: 0x06004016 RID: 16406 RVA: 0x000FF380 File Offset: 0x000FD580
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new string ImageKey
		{
			get
			{
				return base.ImageKey;
			}
			set
			{
				base.ImageKey = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemImageScaling" /> value.</returns>
		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06004017 RID: 16407 RVA: 0x000FF38C File Offset: 0x000FD58C
		// (set) Token: 0x06004018 RID: 16408 RVA: 0x000FF394 File Offset: 0x000FD594
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return base.ImageScaling;
			}
			set
			{
				base.ImageScaling = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06004019 RID: 16409 RVA: 0x000FF3A0 File Offset: 0x000FD5A0
		// (set) Token: 0x0600401A RID: 16410 RVA: 0x000FF3A8 File Offset: 0x000FD5A8
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Color ImageTransparentColor
		{
			get
			{
				return base.ImageTransparentColor;
			}
			set
			{
				base.ImageTransparentColor = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x000FF3B4 File Offset: 0x000FD5B4
		// (set) Token: 0x0600401C RID: 16412 RVA: 0x000FF3BC File Offset: 0x000FD5BC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x000FF3C8 File Offset: 0x000FD5C8
		// (set) Token: 0x0600401E RID: 16414 RVA: 0x000FF3D0 File Offset: 0x000FD5D0
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.ContentAlignment" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x000FF3DC File Offset: 0x000FD5DC
		// (set) Token: 0x06004020 RID: 16416 RVA: 0x000FF3E4 File Offset: 0x000FD5E4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> value.</returns>
		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x000FF3F0 File Offset: 0x000FD5F0
		// (set) Token: 0x06004022 RID: 16418 RVA: 0x000FF3F8 File Offset: 0x000FD5F8
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
			set
			{
				base.TextDirection = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TextImageRelation" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x000FF404 File Offset: 0x000FD604
		// (set) Token: 0x06004024 RID: 16420 RVA: 0x000FF40C File Offset: 0x000FD60C
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new TextImageRelation TextImageRelation
		{
			get
			{
				return base.TextImageRelation;
			}
			set
			{
				base.TextImageRelation = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A string.</returns>
		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x000FF418 File Offset: 0x000FD618
		// (set) Token: 0x06004026 RID: 16422 RVA: 0x000FF420 File Offset: 0x000FD620
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new string ToolTipText
		{
			get
			{
				return base.ToolTipText;
			}
			set
			{
				base.ToolTipText = value;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x000FF42C File Offset: 0x000FD62C
		protected internal override Padding DefaultMargin
		{
			get
			{
				return default(Padding);
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x000FF444 File Offset: 0x000FD644
		protected override Size DefaultSize
		{
			get
			{
				return new Size(6, 6);
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> can be fitted.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, in pixels.</returns>
		/// <param name="constrainingSize">A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, in pixels.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004029 RID: 16425 RVA: 0x000FF450 File Offset: 0x000FD650
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return new Size(6, 6);
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000FF45C File Offset: 0x000FD65C
		[EditorBrowsable(2)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripItem.ToolStripItemAccessibleObject(this)
			{
				default_action = "Press",
				role = AccessibleRole.Separator,
				state = AccessibleStates.None
			};
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600402B RID: 16427 RVA: 0x000FF48C File Offset: 0x000FD68C
		[EditorBrowsable(1)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x0600402C RID: 16428 RVA: 0x000FF498 File Offset: 0x000FD698
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner != null)
			{
				if (base.IsOnDropDown)
				{
					base.Owner.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(e.Graphics, this, base.Owner.Orientation != Orientation.Horizontal));
				}
				else
				{
					base.Owner.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(e.Graphics, this, base.Owner.Orientation == Orientation.Horizontal));
				}
			}
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> specifying the size and location of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		// Token: 0x0600402D RID: 16429 RVA: 0x000FF530 File Offset: 0x000FD730
		protected internal override void SetBounds(Rectangle rect)
		{
			base.SetBounds(rect);
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x0600402E RID: 16430 RVA: 0x000FF53C File Offset: 0x000FD73C
		internal override ToolStripTextDirection DefaultTextDirection
		{
			get
			{
				return ToolStripTextDirection.Horizontal;
			}
		}
	}
}
