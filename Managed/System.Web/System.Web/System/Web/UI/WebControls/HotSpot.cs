using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Implements the basic functionality common to all hot spot shapes.</summary>
	// Token: 0x020003AF RID: 943
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HotSpot : IStateManager
	{
		/// <summary>Gets or sets the access key that allows you to quickly navigate to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
		/// <returns>The access key for quick navigation to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified access key is neither null, an empty string (""), nor a single character string. </exception>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x00064C6C File Offset: 0x00062E6C
		// (set) Token: 0x0600267F RID: 9855 RVA: 0x00064C99 File Offset: 0x00062E99
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string AccessKey
		{
			get
			{
				object obj = this.viewState["AccessKey"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (value == null || value.Length < 2)
				{
					this.viewState["AccessKey"] = value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", "AccessKey can only be null, empty or a single character");
			}
		}

		/// <summary>Gets or sets the alternate text to display for a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the image is unavailable or renders to a browser that does not support images.</summary>
		/// <returns>The text displayed in place of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> when the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control's image is unavailable. The default value is an empty string ("").</returns>
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x00064CC8 File Offset: 0x00062EC8
		// (set) Token: 0x06002681 RID: 9857 RVA: 0x00064CF5 File Offset: 0x00062EF5
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Behavior")]
		public virtual string AlternateText
		{
			get
			{
				object obj = this.viewState["AlternateText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["AlternateText"] = value;
			}
		}

		/// <summary>Gets or sets the behavior of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. The default is Default.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. </exception>
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x00064D08 File Offset: 0x00062F08
		// (set) Token: 0x06002683 RID: 9859 RVA: 0x00064D31 File Offset: 0x00062F31
		[NotifyParentProperty(true)]
		[DefaultValue(HotSpotMode.NotSet)]
		[WebCategory("Behavior")]
		public virtual HotSpotMode HotSpotMode
		{
			get
			{
				object obj = this.viewState["HotSpotMode"];
				if (obj == null)
				{
					return HotSpotMode.NotSet;
				}
				return (HotSpotMode)obj;
			}
			set
			{
				if (value < HotSpotMode.NotSet || value > HotSpotMode.Inactive)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.viewState["HotSpotMode"] = value;
			}
		}

		/// <summary>Gets or sets the URL to navigate to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is clicked.</summary>
		/// <returns>The URL to navigate to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is clicked. The default is an empty string ("").</returns>
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x00064D5C File Offset: 0x00062F5C
		// (set) Token: 0x06002685 RID: 9861 RVA: 0x00064D89 File Offset: 0x00062F89
		[DefaultValue("")]
		[Bindable(true)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		public string NavigateUrl
		{
			get
			{
				object obj = this.viewState["NavigateUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to pass in the event data when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked.</summary>
		/// <returns>The name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to pass in the event data when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked. The default is an empty string ("").</returns>
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x00064D9C File Offset: 0x00062F9C
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x00064DC9 File Offset: 0x00062FC9
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		public string PostBackValue
		{
			get
			{
				object obj = this.viewState["PostBackValue"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["PostBackValue"] = value;
			}
		}

		/// <summary>Gets or sets the tab index of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
		/// <returns>The tab index of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region. The default is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified tab index is not between -32768 and 32767. </exception>
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x00064DDC File Offset: 0x00062FDC
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x00064E05 File Offset: 0x00063005
		[DefaultValue(0)]
		[WebCategory("Accessibility")]
		public virtual short TabIndex
		{
			get
			{
				object obj = this.viewState["TabIndex"];
				if (obj == null)
				{
					return 0;
				}
				return (short)obj;
			}
			set
			{
				this.viewState["TabIndex"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content linked to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that navigates to a URL is clicked.</summary>
		/// <returns>The target window or frame in which to load the Web page linked to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that navigates to a URL is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x00064E20 File Offset: 0x00063020
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x00064E4D File Offset: 0x0006304D
		[WebCategory("Behavior")]
		[TypeConverter(typeof(TargetConverter))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string Target
		{
			get
			{
				object obj = this.viewState["Target"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["Target"] = value;
			}
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object across multiple requests for the same page.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.UI.StateBag" /> class that contains the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region's view-state information.</returns>
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x00064E60 File Offset: 0x00063060
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected StateBag ViewState
		{
			get
			{
				return this.viewState;
			}
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's previously saved view state to the object.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to restore. </param>
		// Token: 0x0600268D RID: 9869 RVA: 0x00064E68 File Offset: 0x00063068
		protected virtual void LoadViewState(object savedState)
		{
			this.viewState.LoadViewState(savedState);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state since the time the page was posted back to the server.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state. If no view state is associated with the object, this method returns null.</returns>
		// Token: 0x0600268E RID: 9870 RVA: 0x00064E76 File Offset: 0x00063076
		protected virtual object SaveViewState()
		{
			return this.viewState.SaveViewState();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to track changes to its view state so they can be stored in the object's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x0600268F RID: 9871 RVA: 0x00064E83 File Offset: 0x00063083
		protected virtual void TrackViewState()
		{
			this.viewState.TrackViewState();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x00064E90 File Offset: 0x00063090
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this.viewState.IsTrackingViewState;
			}
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's previously saved view state to the object.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved view state values for the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to restore. </param>
		// Token: 0x06002691 RID: 9873 RVA: 0x00064E9D File Offset: 0x0006309D
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state since the last time the page was posted back to the server.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state.</returns>
		// Token: 0x06002692 RID: 9874 RVA: 0x00064EA6 File Offset: 0x000630A6
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region to track changes to its view state.</summary>
		// Token: 0x06002693 RID: 9875 RVA: 0x00064EAE File Offset: 0x000630AE
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes.  </summary>
		/// <returns>true if a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x00064EB6 File Offset: 0x000630B6
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Returns the <see cref="T:System.String" /> representation of this instance of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Web.UI.WebControls.HotSpot" /> object.</returns>
		// Token: 0x06002695 RID: 9877 RVA: 0x00064EBE File Offset: 0x000630BE
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x00064ECB File Offset: 0x000630CB
		internal void SetDirty()
		{
			this.viewState.SetDirty(true);
		}

		/// <summary>When overridden in a derived class, returns a string that represents the coordinates of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
		/// <returns>A string that represents the coordinates of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</returns>
		// Token: 0x06002697 RID: 9879
		public abstract string GetCoordinates();

		/// <summary>When overridden in a derived class, gets the string representation for the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's shape.</summary>
		/// <returns>A string that represents the name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's shape.</returns>
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002698 RID: 9880
		protected internal abstract string MarkupName { get; }

		// Token: 0x04001A50 RID: 6736
		private StateBag viewState = new StateBag();
	}
}
