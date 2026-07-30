using System;

namespace System.Windows.Forms
{
	/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for users with impairments.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200034F RID: 847
	public class ToolStripDropDownItemAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItemAccessibleObject" /> class with the specified <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> that owns this <see cref="T:System.Windows.Forms.ToolStripDropDownItemAccessibleObject" />.</param>
		// Token: 0x06003CC8 RID: 15560 RVA: 0x000F44F8 File Offset: 0x000F26F8
		public ToolStripDropDownItemAccessibleObject(ToolStripDropDownItem item)
			: base(item)
		{
		}

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000F4504 File Offset: 0x000F2704
		public override AccessibleRole Role
		{
			get
			{
				return base.Role;
			}
		}

		/// <summary>Performs the default action associated with this accessible object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003CCA RID: 15562 RVA: 0x000F450C File Offset: 0x000F270C
		public override void DoDefaultAction()
		{
			base.DoDefaultAction();
		}

		/// <summary>Retrieves the accessible child control corresponding to the specified index.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child control corresponding to the specified index.</returns>
		/// <param name="index">The zero-based index of the accessible child control.</param>
		// Token: 0x06003CCB RID: 15563 RVA: 0x000F4514 File Offset: 0x000F2714
		public override AccessibleObject GetChild(int index)
		{
			return (this.owner_item as ToolStripDropDownItem).DropDownItems[index].AccessibilityObject;
		}

		/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
		/// <returns>The number of children belonging to an accessible object.</returns>
		// Token: 0x06003CCC RID: 15564 RVA: 0x000F4534 File Offset: 0x000F2734
		public override int GetChildCount()
		{
			return (this.owner_item as ToolStripDropDownItem).DropDownItems.Count;
		}
	}
}
