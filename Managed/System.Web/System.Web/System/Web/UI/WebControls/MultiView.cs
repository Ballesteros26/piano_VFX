using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a control that acts as a container for a group of <see cref="T:System.Web.UI.WebControls.View" /> controls.</summary>
	// Token: 0x020003DB RID: 987
	[Designer("System.Web.UI.Design.WebControls.MultiViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("ActiveViewChanged")]
	[ParseChildren(typeof(View))]
	[ControlBuilder(typeof(MultiViewControlBuilder))]
	[ToolboxData("<{0}:MultiView runat=\"server\"></{0}:MultiView>")]
	public class MultiView : Control
	{
		/// <summary>Occurs when the active <see cref="T:System.Web.UI.WebControls.View" /> control of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control changes between posts to the server.</summary>
		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06002A80 RID: 10880 RVA: 0x00070C74 File Offset: 0x0006EE74
		// (remove) Token: 0x06002A81 RID: 10881 RVA: 0x00070C87 File Offset: 0x0006EE87
		public event EventHandler ActiveViewChanged
		{
			add
			{
				base.Events.AddHandler(MultiView.ActiveViewChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MultiView.ActiveViewChangedEvent, value);
			}
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.WebControls.MultiView" /> control that an XML or HTML element was parsed, and adds the element to the <see cref="T:System.Web.UI.WebControls.ViewCollection" /> collection of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
		/// <exception cref="T:System.Web.HttpException">The specified <see cref="T:System.Object" /> is not a <see cref="T:System.Web.UI.WebControls.View" /> control. </exception>
		// Token: 0x06002A82 RID: 10882 RVA: 0x00070C9A File Offset: 0x0006EE9A
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is View)
			{
				this.Controls.Add(obj as View);
			}
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.ControlCollection" /> to hold the child controls of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ViewCollection" /> to contain the <see cref="T:System.Web.UI.WebControls.View" /> controls of the current <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</returns>
		// Token: 0x06002A83 RID: 10883 RVA: 0x00070CB5 File Offset: 0x0006EEB5
		protected override ControlCollection CreateControlCollection()
		{
			return new ViewCollection(this);
		}

		/// <summary>Returns the current active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.View" /> control that represents the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</returns>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Web.UI.WebControls.MultiView.ActiveViewIndex" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.View" /> control within the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </exception>
		// Token: 0x06002A84 RID: 10884 RVA: 0x00070CBD File Offset: 0x0006EEBD
		public View GetActiveView()
		{
			if (this.viewIndex < 0 || this.viewIndex >= this.Controls.Count)
			{
				throw new HttpException("The ActiveViewIndex is not set to a valid View control");
			}
			return this.Controls[this.viewIndex] as View;
		}

		/// <summary>Sets the specified <see cref="T:System.Web.UI.WebControls.View" /> control to the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="view">A <see cref="T:System.Web.UI.WebControls.View" /> control to set as the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </param>
		/// <exception cref="T:System.Web.HttpException">The specified <paramref name="view" /> parameter value was not contained in the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </exception>
		// Token: 0x06002A85 RID: 10885 RVA: 0x00070CFC File Offset: 0x0006EEFC
		public void SetActiveView(View view)
		{
			int num = this.Controls.IndexOf(view);
			if (num == -1)
			{
				throw new HttpException("The provided view is not contained in the MultiView control.");
			}
			this.ActiveViewIndex = num;
		}

		/// <summary>Gets or sets the index of the active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>The zero-based index of the active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. The default is -1, indicating that no view is set as active.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index was set to less than -1, or greater than or equal to the number of items on the list. </exception>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x00070D2C File Offset: 0x0006EF2C
		// (set) Token: 0x06002A87 RID: 10887 RVA: 0x00070D48 File Offset: 0x0006EF48
		[DefaultValue(-1)]
		public virtual int ActiveViewIndex
		{
			get
			{
				if (this.Controls.Count == 0)
				{
					return this.initialIndex;
				}
				return this.viewIndex;
			}
			set
			{
				if (this.Controls.Count == 0)
				{
					this.initialIndex = value;
					return;
				}
				if (value < -1 || value >= this.Controls.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.viewIndex != -1)
				{
					((View)this.Controls[this.viewIndex]).NotifyActivation(false);
				}
				this.viewIndex = value;
				if (this.viewIndex != -1)
				{
					((View)this.Controls[this.viewIndex]).NotifyActivation(true);
				}
				this.UpdateViewVisibility();
				this.OnActiveViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether themes apply to the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>true if themes are to be used; otherwise, false. The default is false.</returns>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x00070DE4 File Offset: 0x0006EFE4
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x00070DEC File Offset: 0x0006EFEC
		[Browsable(true)]
		public new virtual bool EnableTheming
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

		/// <summary>Gets the collection of <see cref="T:System.Web.UI.WebControls.View" /> controls in the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ViewCollection" /> that represents a collection of <see cref="T:System.Web.UI.WebControls.View" /> controls within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. The default is null.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00070DF5 File Offset: 0x0006EFF5
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		public virtual ViewCollection Views
		{
			get
			{
				return this.Controls as ViewCollection;
			}
		}

		/// <summary>Determines whether the event for the <see cref="T:System.Web.UI.WebControls.MultiView" /> control is passed to the page's UI server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.MultiView" /> control cannot find the <see cref="T:System.Web.UI.WebControls.View" /> specified in the <see cref="P:System.Web.UI.WebControls.CommandEventArgs.CommandArgument" /> property of <paramref name="e" />.</exception>
		/// <exception cref="T:System.FormatException">The <see cref="P:System.Web.UI.WebControls.CommandEventArgs.CommandArgument" /> property of <paramref name="e" /> cannot be parsed as an integer.</exception>
		// Token: 0x06002A8B RID: 10891 RVA: 0x00070E04 File Offset: 0x0006F004
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null)
			{
				string commandName = commandEventArgs.CommandName;
				if (!(commandName == "NextView"))
				{
					if (!(commandName == "PrevView"))
					{
						if (!(commandName == "SwitchViewByID"))
						{
							if (!(commandName == "SwitchViewByIndex"))
							{
								return false;
							}
						}
						else
						{
							using (IEnumerator enumerator = this.Controls.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									View view = (View)obj;
									if (view.ID == (string)commandEventArgs.CommandArgument)
									{
										this.SetActiveView(view);
										break;
									}
								}
								return false;
							}
						}
						int num = (int)Convert.ChangeType(commandEventArgs.CommandArgument, typeof(int));
						this.ActiveViewIndex = num;
					}
					else if (this.viewIndex > 0)
					{
						this.ActiveViewIndex = this.viewIndex - 1;
					}
				}
				else if (this.viewIndex < this.Controls.Count - 1 && this.Controls.Count > 0)
				{
					this.ActiveViewIndex = this.viewIndex + 1;
				}
			}
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002A8C RID: 10892 RVA: 0x00070F48 File Offset: 0x0006F148
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.RegisterRequiresControlState(this);
			if (this.initialIndex != -1)
			{
				this.ActiveViewIndex = this.initialIndex;
				this.initialIndex = -1;
			}
			base.OnInit(e);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00070F7C File Offset: 0x0006F17C
		private void UpdateViewVisibility()
		{
			for (int i = 0; i < this.Views.Count; i++)
			{
				this.Views[i].VisibleInternal = i == this.viewIndex;
			}
		}

		/// <summary>Called after a <see cref="T:System.Web.UI.WebControls.View" /> control is removed from the <see cref="P:System.Web.UI.Control.Controls" /> collection of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="ctl">The <see cref="T:System.Web.UI.WebControls.View" /> control that has been removed. </param>
		// Token: 0x06002A8E RID: 10894 RVA: 0x00070FB9 File Offset: 0x0006F1B9
		protected internal override void RemovedControl(Control ctl)
		{
			if (this.viewIndex >= this.Controls.Count)
			{
				this.viewIndex = this.Controls.Count - 1;
				this.UpdateViewVisibility();
			}
			base.RemovedControl(ctl);
		}

		/// <summary>Loads the current state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </param>
		// Token: 0x06002A8F RID: 10895 RVA: 0x00070FEE File Offset: 0x0006F1EE
		protected internal override void LoadControlState(object state)
		{
			if (state != null)
			{
				this.viewIndex = (int)state;
				this.UpdateViewVisibility();
				return;
			}
			this.viewIndex = -1;
		}

		/// <summary>Saves the current state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. If there is no state associated with the <see cref="T:System.Web.UI.WebControls.MultiView" /> control, this method returns null.</returns>
		// Token: 0x06002A90 RID: 10896 RVA: 0x0007100D File Offset: 0x0006F20D
		protected internal override object SaveControlState()
		{
			if (this.viewIndex != -1)
			{
				return this.viewIndex;
			}
			return null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.MultiView.ActiveViewChanged" /> event of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002A91 RID: 10897 RVA: 0x00071028 File Offset: 0x0006F228
		protected virtual void OnActiveViewChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[MultiView.ActiveViewChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.MultiView" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002A92 RID: 10898 RVA: 0x0007105E File Offset: 0x0006F25E
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Controls.Count == 0 && this.initialIndex != -1)
			{
				this.viewIndex = this.initialIndex;
			}
			if (this.viewIndex != -1)
			{
				this.GetActiveView().Render(writer);
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000710AD File Offset: 0x0006F2AD
		// Note: this type is marked as 'beforefieldinit'.
		static MultiView()
		{
			MultiView.ActiveViewChangedEvent = new object();
		}

		/// <summary>Represents the command name associated with the next <see cref="T:System.Web.UI.WebControls.View" /> control to display in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. This field is read-only.</summary>
		// Token: 0x04001ADF RID: 6879
		public static readonly string NextViewCommandName = "NextView";

		/// <summary>Represents the command name associated with the previous <see cref="T:System.Web.UI.WebControls.View" /> control to display in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. This field is read-only.</summary>
		// Token: 0x04001AE0 RID: 6880
		public static readonly string PreviousViewCommandName = "PrevView";

		/// <summary>Represents the command name associated with changing the active <see cref="T:System.Web.UI.WebControls.View" /> control in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control, based on a specified <see cref="T:System.Web.UI.WebControls.View" /> id. This field is read-only.</summary>
		// Token: 0x04001AE1 RID: 6881
		public static readonly string SwitchViewByIDCommandName = "SwitchViewByID";

		/// <summary>Represents the command name associated with changing the active <see cref="T:System.Web.UI.WebControls.View" /> control in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control based on a specified <see cref="T:System.Web.UI.WebControls.View" /> index. This field is read-only.</summary>
		// Token: 0x04001AE2 RID: 6882
		public static readonly string SwitchViewByIndexCommandName = "SwitchViewByIndex";

		// Token: 0x04001AE4 RID: 6884
		private int viewIndex = -1;

		// Token: 0x04001AE5 RID: 6885
		private int initialIndex = -1;
	}
}
