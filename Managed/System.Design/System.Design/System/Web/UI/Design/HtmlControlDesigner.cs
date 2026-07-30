using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides basic design-time functionality for ASP.NET server controls.</summary>
	// Token: 0x0200007F RID: 127
	[MonoTODO]
	public class HtmlControlDesigner : ComponentDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.HtmlControlDesigner" /> class.</summary>
		// Token: 0x06000411 RID: 1041 RVA: 0x00009290 File Offset: 0x00007490
		[MonoTODO]
		public HtmlControlDesigner()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.Web.UI.Design.HtmlControlDesigner" /> object and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000412 RID: 1042 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when a behavior is associated with the element.</summary>
		// Token: 0x06000413 RID: 1043 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Use ControlDesigner.Tag instead")]
		protected virtual void OnBehaviorAttached()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when a behavior disassociates from the element.</summary>
		// Token: 0x06000414 RID: 1044 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Use ControlDesigner.Tag instead")]
		protected virtual void OnBehaviorDetaching()
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides a method that can be used to indicate when a data binding has changed.</summary>
		/// <param name="propName">The name of the property that has changed. </param>
		// Token: 0x06000415 RID: 1045 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Use DataBinding.Changed event instead")]
		protected virtual void OnBindingsCollectionChanged(string propName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides a way to perform additional processing when the associated control is attached to a parent control.</summary>
		// Token: 0x06000416 RID: 1046 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnSetParent()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the list of events that are exposed at design-time for the <see cref="T:System.ComponentModel.TypeDescriptor" /> object for the component.</summary>
		/// <param name="events">An <see cref="T:System.Collections.IDictionary" /> that contains the names of the events of the component to expose. </param>
		// Token: 0x06000417 RID: 1047 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void PreFilterEvents(IDictionary events)
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows the designer to expose a specific set of properties through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object at design time.</summary>
		/// <param name="properties">The set of properties to filter for the component. </param>
		// Token: 0x06000418 RID: 1048 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the DHTML behavior that is associated with the designer.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IHtmlControlDesignerBehavior" /> that is associated with the designer.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Use ControlDesigner.Tag instead")]
		[MonoTODO]
		public IHtmlControlDesignerBehavior Behavior
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the data bindings collection for the current control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataBindingCollection" /> that contains the data bindings for the current control.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public DataBindingCollection DataBindings
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the design-time object representing the control that is associated with the <see cref="T:System.Web.UI.Design.HtmlControlDesigner" /> object on the design surface.</summary>
		/// <returns>The design-time object representing the control associated with the <see cref="T:System.Web.UI.Design.HtmlControlDesigner" />.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Use new WebFormsRootDesigner feature instead. It is not used anymore", true)]
		protected object DesignTimeElement
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether to create a field declaration for the control in the code-behind file for the current design document during serialization.</summary>
		/// <returns>true, if a declaration should be created; otherwise, false.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Code serialization is not supported in 2.0 anymore")]
		[MonoTODO]
		public virtual bool ShouldCodeSerialize
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the expression bindings for the current control at design time.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> that contains the expressions strings set for properties in the current control.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000234B File Offset: 0x0000054B
		public ExpressionBindingCollection Expressions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes the designer and sets the component for design.</summary>
		/// <param name="component">The control element for design. </param>
		// Token: 0x06000420 RID: 1056 RVA: 0x0000234B File Offset: 0x0000054B
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
