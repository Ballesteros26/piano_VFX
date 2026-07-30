using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Creates a control that displays an image on a page. When a hot spot region defined within the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control is clicked, the control either generates a postback to the server or navigates to a specified URL.</summary>
	// Token: 0x020003BA RID: 954
	[ParseChildren(true, "HotSpots")]
	[DefaultProperty("HotSpots")]
	[DefaultEvent("Click")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ImageMap : Image, IPostBackEventHandler
	{
		/// <summary>Occurs when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control is clicked.</summary>
		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06002750 RID: 10064 RVA: 0x00066689 File Offset: 0x00064889
		// (remove) Token: 0x06002751 RID: 10065 RVA: 0x0006669C File Offset: 0x0006489C
		[Category("Action")]
		public event ImageMapEventHandler Click
		{
			add
			{
				base.Events.AddHandler(ImageMap.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageMap.ClickEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ImageMap.Click" /> event for the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</summary>
		/// <param name="e">An argument of type <see cref="T:System.Web.UI.WebControls.ImageMapEventArgs" /> that contains the event data. </param>
		// Token: 0x06002752 RID: 10066 RVA: 0x000666B0 File Offset: 0x000648B0
		protected virtual void OnClick(ImageMapEventArgs e)
		{
			if (base.Events != null)
			{
				ImageMapEventHandler imageMapEventHandler = (ImageMapEventHandler)base.Events[ImageMap.ClickEvent];
				if (imageMapEventHandler != null)
				{
					imageMapEventHandler(this, e);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control can respond to user interaction.</summary>
		/// <returns>true if the control is to respond to user clicks; otherwise, false.</returns>
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x00065BAD File Offset: 0x00063DAD
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x00065BB5 File Offset: 0x00063DB5
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		/// <summary>Gets or sets the default behavior for the <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects of an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects are clicked.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. </exception>
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000666E8 File Offset: 0x000648E8
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x00066711 File Offset: 0x00064911
		[DefaultValue(HotSpotMode.NotSet)]
		public virtual HotSpotMode HotSpotMode
		{
			get
			{
				object obj = this.ViewState["HotSpotMode"];
				if (obj == null)
				{
					return HotSpotMode.NotSet;
				}
				return (HotSpotMode)obj;
			}
			set
			{
				this.ViewState["HotSpotMode"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame that displays the Web page content linked to when the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control is clicked.</summary>
		/// <returns>The target window or frame that displays the specified Web page when the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control is clicked. Values must begin with a letter in the range of A through Z (case-insensitive), except for the following special values, which begin with an underscore: _blank Renders the content in a new window without frames. _parent Renders the content in the immediate frameset parent. _searchRenders the content in the search pane._self Renders the content in the frame with focus. _top Renders the content in the full window without frames. NoteCheck your browser documentation to determine if the _search value is supported.  For example, Microsoft Internet Explorer 5.0 and later support the _search target value.The default value is an empty string ("").</returns>
		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x0006672C File Offset: 0x0006492C
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x00046F16 File Offset: 0x00045116
		[DefaultValue("")]
		public virtual string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects that represents the defined hot spot regions in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> object that represents the defined hot spot regions in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</returns>
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x00066759 File Offset: 0x00064959
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[NotifyParentProperty(true)]
		public HotSpotCollection HotSpots
		{
			get
			{
				if (this.spots == null)
				{
					this.spots = new HotSpotCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.spots).TrackViewState();
					}
				}
				return this.spots;
			}
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control so they can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x0600275A RID: 10074 RVA: 0x00066787 File Offset: 0x00064987
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.spots != null)
			{
				((IStateManager)this.spots).TrackViewState();
			}
		}

		/// <summary>Saves any changes to an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control's view-state that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>Returns the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control's current view state. If there is no view state associated with the control, this method returns null.</returns>
		// Token: 0x0600275B RID: 10075 RVA: 0x000667A4 File Offset: 0x000649A4
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = ((this.spots != null) ? ((IStateManager)this.spots).SaveViewState() : null);
			if (obj != null || obj2 != null)
			{
				return new Pair(obj, obj2);
			}
			return null;
		}

		/// <summary>Restores view-state information for the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.ImageMap.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control to restore. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.Control.ViewState" />.</exception>
		// Token: 0x0600275C RID: 10076 RVA: 0x000667E0 File Offset: 0x000649E0
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			((IStateManager)this.HotSpots).LoadViewState(pair.Second);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when a form is posted back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x0600275D RID: 10077 RVA: 0x0006681C File Offset: 0x00064A1C
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			HotSpot hotSpot = this.HotSpots[int.Parse(eventArgument)];
			this.OnClick(new ImageMapEventArgs(hotSpot.PostBackValue));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />. </summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x0600275E RID: 10078 RVA: 0x00066859 File Offset: 0x00064A59
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Adds the HTML attributes and styles of an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x0600275F RID: 10079 RVA: 0x00066862 File Offset: 0x00064A62
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (this.spots != null && this.spots.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Usemap, "#ImageMap" + this.ClientID);
			}
		}

		/// <summary>Sends the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to render on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control content. </param>
		// Token: 0x06002760 RID: 10080 RVA: 0x0006689C File Offset: 0x00064A9C
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (this.spots != null && this.spots.Count > 0)
			{
				bool enabled = this.Enabled;
				writer.AddAttribute(HtmlTextWriterAttribute.Id, "ImageMap" + this.ClientID);
				writer.AddAttribute(HtmlTextWriterAttribute.Name, "ImageMap" + this.ClientID);
				writer.RenderBeginTag(HtmlTextWriterTag.Map);
				for (int i = 0; i < this.spots.Count; i++)
				{
					HotSpot hotSpot = this.spots[i];
					writer.AddAttribute(HtmlTextWriterAttribute.Shape, hotSpot.MarkupName);
					writer.AddAttribute(HtmlTextWriterAttribute.Coords, hotSpot.GetCoordinates());
					writer.AddAttribute(HtmlTextWriterAttribute.Title, hotSpot.AlternateText);
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, hotSpot.AlternateText);
					if (hotSpot.AccessKey.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, hotSpot.AccessKey);
					}
					if (hotSpot.TabIndex != 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, hotSpot.TabIndex.ToString());
					}
					switch ((hotSpot.HotSpotMode != HotSpotMode.NotSet) ? hotSpot.HotSpotMode : this.HotSpotMode)
					{
					case HotSpotMode.Navigate:
					{
						string text = ((hotSpot.Target.Length > 0) ? hotSpot.Target : this.Target);
						if (!string.IsNullOrEmpty(text))
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Target, text);
						}
						if (enabled)
						{
							string text2 = base.ResolveClientUrl(hotSpot.NavigateUrl);
							writer.AddAttribute(HtmlTextWriterAttribute.Href, text2);
						}
						break;
					}
					case HotSpotMode.PostBack:
						writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, i.ToString(), true));
						break;
					case HotSpotMode.Inactive:
						writer.AddAttribute("nohref", "true", false);
						break;
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Area);
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00066A6A File Offset: 0x00064C6A
		// Note: this type is marked as 'beforefieldinit'.
		static ImageMap()
		{
			ImageMap.ClickEvent = new object();
		}

		// Token: 0x04001A5C RID: 6748
		private HotSpotCollection spots;
	}
}
