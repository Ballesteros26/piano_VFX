using System;

namespace System.Web.UI.Design
{
	/// <summary>Represents an editable content region within the design-time markup for the associated control.</summary>
	// Token: 0x02000078 RID: 120
	public class EditableDesignerRegion : DesignerRegion
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> class using the given owner and name.</summary>
		/// <param name="owner">A <see cref="T:System.Web.UI.Design.ControlDesigner" /> object, or a designer that derives from <see cref="T:System.Web.UI.Design.ControlDesigner" />.</param>
		/// <param name="name">The name of the region.</param>
		// Token: 0x060003E5 RID: 997 RVA: 0x00009056 File Offset: 0x00007256
		[MonoNotSupported("")]
		public EditableDesignerRegion(ControlDesigner owner, string name)
			: base(owner, name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> class using the given owner and name and the initial value of the <see cref="P:System.Web.UI.Design.EditableDesignerRegion.ServerControlsOnly" /> property.</summary>
		/// <param name="owner">A <see cref="T:System.Web.UI.Design.ControlDesigner" /> object, or a designer that derives from <see cref="T:System.Web.UI.Design.ControlDesigner" />.</param>
		/// <param name="name">The name of the region.</param>
		/// <param name="serverControlsOnly">true to have the region accept only Web server controls for content; otherwise, false.</param>
		// Token: 0x060003E6 RID: 998 RVA: 0x00009065 File Offset: 0x00007265
		[MonoNotSupported("")]
		public EditableDesignerRegion(ControlDesigner owner, string name, bool serverControlsOnly)
			: base(owner, name, serverControlsOnly)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Web.UI.Design.ViewRendering" /> object containing the design-time HTML markup for the given control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.ViewRendering" /> object.</returns>
		/// <param name="control">The control for which to get the <see cref="T:System.Web.UI.Design.ViewRendering" /> object for the current region.</param>
		// Token: 0x060003E7 RID: 999 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual ViewRendering GetChildViewRendering(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the HTML markup for the content of the region.</summary>
		/// <returns>HTML markup representing the content of the <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> object.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual string Content
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

		/// <summary>Gets or sets a value indicating whether the region can accept only Web server controls.</summary>
		/// <returns>true if the region can contain only Web server controls; otherwise, false.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool ServerControlsOnly
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

		/// <summary>Gets or sets a value indicating whether the region can be bound to a data source.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> content supports binding to a data source; otherwise, false.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool SupportsDataBinding
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
	}
}
