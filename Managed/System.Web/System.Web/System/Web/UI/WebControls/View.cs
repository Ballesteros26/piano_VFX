using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a control that acts as a container for a group of controls within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	// Token: 0x0200043C RID: 1084
	[Designer("System.Web.UI.Design.WebControls.ViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxData("<{0}:View runat=\"server\"></{0}:View>")]
	[ParseChildren(false)]
	public class View : Control
	{
		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is false.</returns>
		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x00070DE4 File Offset: 0x0006EFE4
		// (set) Token: 0x060031F5 RID: 12789 RVA: 0x00070DEC File Offset: 0x0006EFEC
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.View" /> class. </summary>
		// Token: 0x060031F6 RID: 12790 RVA: 0x000859BD File Offset: 0x00083BBD
		public View()
		{
			base.Visible = false;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000859CC File Offset: 0x00083BCC
		internal void NotifyActivation(bool activated)
		{
			if (activated)
			{
				this.OnActivate(EventArgs.Empty);
				return;
			}
			this.OnDeactivate(EventArgs.Empty);
		}

		/// <summary>Occurs when the current <see cref="T:System.Web.UI.WebControls.View" /> control becomes the active view.</summary>
		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x060031F8 RID: 12792 RVA: 0x000859E8 File Offset: 0x00083BE8
		// (remove) Token: 0x060031F9 RID: 12793 RVA: 0x000859FB File Offset: 0x00083BFB
		public event EventHandler Activate
		{
			add
			{
				base.Events.AddHandler(View.ActivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(View.ActivateEvent, value);
			}
		}

		/// <summary>Occurs when the current active <see cref="T:System.Web.UI.WebControls.View" /> control becomes inactive.</summary>
		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x060031FA RID: 12794 RVA: 0x00085A0E File Offset: 0x00083C0E
		// (remove) Token: 0x060031FB RID: 12795 RVA: 0x00085A21 File Offset: 0x00083C21
		public event EventHandler Deactivate
		{
			add
			{
				base.Events.AddHandler(View.DeactivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(View.DeactivateEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.View.Activate" /> event of the <see cref="T:System.Web.UI.WebControls.View" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060031FC RID: 12796 RVA: 0x00085A34 File Offset: 0x00083C34
		protected virtual void OnActivate(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[View.ActivateEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.View.Deactivate" /> event of the <see cref="T:System.Web.UI.WebControls.View" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060031FD RID: 12797 RVA: 0x00085A6C File Offset: 0x00083C6C
		protected virtual void OnDeactivate(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[View.DeactivateEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x0003784A File Offset: 0x00035A4A
		// (set) Token: 0x060031FF RID: 12799 RVA: 0x00037852 File Offset: 0x00035A52
		internal bool VisibleInternal
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.View" /> control is visible. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.View" /> control is visible; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set this property at run time.</exception>
		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x0003784A File Offset: 0x00035A4A
		// (set) Token: 0x06003201 RID: 12801 RVA: 0x00085AA2 File Offset: 0x00083CA2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				throw new InvalidOperationException("The Visible property of a View control can only be set by setting the active View of a MultiView.");
			}
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x00085AAE File Offset: 0x00083CAE
		// Note: this type is marked as 'beforefieldinit'.
		static View()
		{
			View.ActivateEvent = new object();
			View.DeactivateEvent = new object();
		}
	}
}
