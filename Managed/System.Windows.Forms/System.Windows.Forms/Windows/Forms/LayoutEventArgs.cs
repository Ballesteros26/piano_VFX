using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Layout" /> event. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000200 RID: 512
	public sealed class LayoutEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutEventArgs" /> class with the specified control and property affected.</summary>
		/// <param name="affectedControl">The <see cref="T:System.Windows.Forms.Control" /> affected by the layout change.</param>
		/// <param name="affectedProperty">The property affected by the layout change.</param>
		// Token: 0x06001F91 RID: 8081 RVA: 0x00076390 File Offset: 0x00074590
		public LayoutEventArgs(Control affectedControl, string affectedProperty)
		{
			this.affected_control = affectedControl;
			this.affected_property = affectedProperty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutEventArgs" /> class with the specified component and property affected.</summary>
		/// <param name="affectedComponent">The <see cref="T:System.ComponentModel.Component" /> affected by the layout change. </param>
		/// <param name="affectedProperty">The property affected by the layout change. </param>
		// Token: 0x06001F92 RID: 8082 RVA: 0x000763A8 File Offset: 0x000745A8
		public LayoutEventArgs(IComponent affectedComponent, string affectedProperty)
		{
			this.affected_component = affectedComponent;
			this.affected_property = affectedProperty;
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Component" /> affected by the layout change.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> representing the <see cref="T:System.ComponentModel.Component" /> affected by the layout change.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000763C0 File Offset: 0x000745C0
		public IComponent AffectedComponent
		{
			get
			{
				return this.affected_component;
			}
		}

		/// <summary>Gets the child control affected by the change.</summary>
		/// <returns>The child <see cref="T:System.Windows.Forms.Control" /> affected by the change.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x000763C8 File Offset: 0x000745C8
		public Control AffectedControl
		{
			get
			{
				return this.affected_control;
			}
		}

		/// <summary>Gets the property affected by the change.</summary>
		/// <returns>The property affected by the change.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x000763D0 File Offset: 0x000745D0
		public string AffectedProperty
		{
			get
			{
				return this.affected_property;
			}
		}

		// Token: 0x04001140 RID: 4416
		private Control affected_control;

		// Token: 0x04001141 RID: 4417
		private string affected_property;

		// Token: 0x04001142 RID: 4418
		private IComponent affected_component;
	}
}
