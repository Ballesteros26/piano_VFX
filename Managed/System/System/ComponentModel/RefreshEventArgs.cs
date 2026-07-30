using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.TypeDescriptor.Refreshed" /> event.</summary>
	// Token: 0x020002CD RID: 717
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class RefreshEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the component that has changed.</summary>
		/// <param name="componentChanged">The component that changed. </param>
		// Token: 0x060016F1 RID: 5873 RVA: 0x0005BB26 File Offset: 0x00059D26
		public RefreshEventArgs(object componentChanged)
		{
			this.componentChanged = componentChanged;
			this.typeChanged = componentChanged.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the type of component that has changed.</summary>
		/// <param name="typeChanged">The <see cref="T:System.Type" /> that changed. </param>
		// Token: 0x060016F2 RID: 5874 RVA: 0x0005BB41 File Offset: 0x00059D41
		public RefreshEventArgs(Type typeChanged)
		{
			this.typeChanged = typeChanged;
		}

		/// <summary>Gets the component that changed its properties, events, or extenders.</summary>
		/// <returns>The component that changed its properties, events, or extenders, or null if all components of the same type have changed.</returns>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0005BB50 File Offset: 0x00059D50
		public object ComponentChanged
		{
			get
			{
				return this.componentChanged;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that changed its properties or events.</summary>
		/// <returns>The <see cref="T:System.Type" /> that changed its properties or events.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0005BB58 File Offset: 0x00059D58
		public Type TypeChanged
		{
			get
			{
				return this.typeChanged;
			}
		}

		// Token: 0x040013DF RID: 5087
		private object componentChanged;

		// Token: 0x040013E0 RID: 5088
		private Type typeChanged;
	}
}
