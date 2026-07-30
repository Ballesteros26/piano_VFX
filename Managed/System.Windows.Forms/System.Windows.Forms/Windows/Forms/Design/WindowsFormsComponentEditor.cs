using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a base class for editors that use a modal dialog to display a properties page similar to an ActiveX control's property page.</summary>
	// Token: 0x0200001A RID: 26
	public abstract class WindowsFormsComponentEditor : ComponentEditor
	{
		/// <summary>Creates an editor window that allows the user to edit the specified component, using the specified context information.</summary>
		/// <returns>true if the component was changed during editing; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="component">The component to edit. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060000D5 RID: 213 RVA: 0x00004490 File Offset: 0x00002690
		public override bool EditComponent(ITypeDescriptorContext context, object component)
		{
			return this.EditComponent(context, component, null);
		}

		/// <summary>Creates an editor window that allows the user to edit the specified component.</summary>
		/// <returns>true if the component was changed during editing; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="component">The component to edit. </param>
		/// <param name="owner">An <see cref="T:System.Windows.Forms.IWin32Window" /> that the component belongs to. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060000D6 RID: 214 RVA: 0x0000449C File Offset: 0x0000269C
		public virtual bool EditComponent(ITypeDescriptorContext context, object component, IWin32Window owner)
		{
			ComponentEditorForm componentEditorForm = new ComponentEditorForm(component, this.GetComponentEditorPages());
			return componentEditorForm.ShowForm(owner, this.GetInitialComponentEditorPageIndex()) == DialogResult.OK;
		}

		/// <summary>Creates an editor window that allows the user to edit the specified component, using the specified window that owns the component.</summary>
		/// <returns>true if the component was changed during editing; otherwise, false.</returns>
		/// <param name="component">The component to edit. </param>
		/// <param name="owner">An <see cref="T:System.Windows.Forms.IWin32Window" /> that the component belongs to. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060000D7 RID: 215 RVA: 0x000044CC File Offset: 0x000026CC
		public bool EditComponent(object component, IWin32Window owner)
		{
			return this.EditComponent(null, component, owner);
		}

		/// <summary>Gets the component editor pages associated with the component editor.</summary>
		/// <returns>An array of component editor pages.</returns>
		// Token: 0x060000D8 RID: 216 RVA: 0x000044D8 File Offset: 0x000026D8
		protected virtual Type[] GetComponentEditorPages()
		{
			return null;
		}

		/// <summary>Gets the index of the initial component editor page for the component editor to display.</summary>
		/// <returns>The index of the component editor page that the component editor will initially display.</returns>
		// Token: 0x060000D9 RID: 217 RVA: 0x000044DC File Offset: 0x000026DC
		protected virtual int GetInitialComponentEditorPageIndex()
		{
			return 0;
		}
	}
}
