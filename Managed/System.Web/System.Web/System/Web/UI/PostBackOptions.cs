using System;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Specifies how client-side JavaScript is generated to initiate a postback event.</summary>
	// Token: 0x0200021C RID: 540
	public sealed class PostBackOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified target control data.</summary>
		/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
		// Token: 0x0600162D RID: 5677 RVA: 0x0003B8EC File Offset: 0x00039AEC
		public PostBackOptions(Control targetControl)
			: this(targetControl, null, null, false, false, false, true, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified target control and argument data.</summary>
		/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
		/// <param name="argument">The optional parameter passed during the postback event.</param>
		// Token: 0x0600162E RID: 5678 RVA: 0x0003B908 File Offset: 0x00039B08
		public PostBackOptions(Control targetControl, string argument)
			: this(targetControl, argument, null, false, false, false, true, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified values for the instance's properties.</summary>
		/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
		/// <param name="argument">The optional parameter passed during the postback event.</param>
		/// <param name="actionUrl">The target of the postback.</param>
		/// <param name="autoPostBack">true to automatically post the form back to the server in response to a user action; otherwise, false.</param>
		/// <param name="requiresJavaScriptProtocol">true if the javascript: prefix is required; otherwise, false.</param>
		/// <param name="trackFocus">true if the postback event should return the page to the current scroll position and return focus to the target control; otherwise, false.</param>
		/// <param name="clientSubmit">true if the postback event can be raised by client script; otherwise, false.</param>
		/// <param name="performValidation">true if client-side validation is required before the postback event occurs; otherwise, false.</param>
		/// <param name="validationGroup">The group of controls for which <see cref="T:System.Web.UI.PostBackOptions" /> causes validation when it posts back to the server.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetControl" /> is null.</exception>
		// Token: 0x0600162F RID: 5679 RVA: 0x0003B924 File Offset: 0x00039B24
		public PostBackOptions(Control targetControl, string argument, string actionUrl, bool autoPostBack, bool requiresJavaScriptProtocol, bool trackFocus, bool clientSubmit, bool performValidation, string validationGroup)
		{
			if (targetControl == null)
			{
				throw new ArgumentNullException("targetControl");
			}
			this.control = targetControl;
			this.argument = argument;
			this.actionUrl = actionUrl;
			this.autoPostBack = autoPostBack;
			this.requiresJavaScriptProtocol = requiresJavaScriptProtocol;
			this.trackFocus = trackFocus;
			this.clientSubmit = clientSubmit;
			this.performValidation = performValidation;
			this.validationGroup = validationGroup;
		}

		/// <summary>Gets or sets the target URL for the postback of a Web Forms page.</summary>
		/// <returns>The URL for the postback of a Web Forms page. The default value is an empty string ("").</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0003B98A File Offset: 0x00039B8A
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x0003B992 File Offset: 0x00039B92
		[DefaultValue("")]
		public string ActionUrl
		{
			get
			{
				return this.actionUrl;
			}
			set
			{
				this.actionUrl = value;
			}
		}

		/// <summary>Gets or sets an optional argument that is transferred in the postback event.</summary>
		/// <returns>The optional argument that is transferred in the postback event. The default value is an empty string ("").</returns>
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0003B99B File Offset: 0x00039B9B
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x0003B9A3 File Offset: 0x00039BA3
		[DefaultValue("")]
		public string Argument
		{
			get
			{
				return this.argument;
			}
			set
			{
				this.argument = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the form will automatically post back to the server in response to a user action.</summary>
		/// <returns>true if the form will automatically post back in response to a user action; otherwise, false. The default value is false.</returns>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0003B9AC File Offset: 0x00039BAC
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x0003B9B4 File Offset: 0x00039BB4
		[global::System.MonoTODO("Implement support for this in Page")]
		[DefaultValue(false)]
		public bool AutoPostBack
		{
			get
			{
				return this.autoPostBack;
			}
			set
			{
				this.autoPostBack = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the postback event should occur from client-side script.</summary>
		/// <returns>true if the postback event should occur from client-side script; otherwise, false. The default value is true.</returns>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0003B9BD File Offset: 0x00039BBD
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x0003B9C5 File Offset: 0x00039BC5
		[DefaultValue(true)]
		public bool ClientSubmit
		{
			get
			{
				return this.clientSubmit;
			}
			set
			{
				this.clientSubmit = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether client-side validation is required before the postback event occurs.</summary>
		/// <returns>true if client-side validation is required before the postback event occurs; otherwise, false. The default value is false.</returns>
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0003B9CE File Offset: 0x00039BCE
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x0003B9D6 File Offset: 0x00039BD6
		[DefaultValue(false)]
		public bool PerformValidation
		{
			get
			{
				return this.performValidation;
			}
			set
			{
				this.performValidation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the javascript: prefix is generated for the client-side script. </summary>
		/// <returns>true if the javascript: prefix is generated for the client-side script; otherwise, false. The default value is true.</returns>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0003B9DF File Offset: 0x00039BDF
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0003B9E7 File Offset: 0x00039BE7
		[DefaultValue(true)]
		public bool RequiresJavaScriptProtocol
		{
			get
			{
				return this.requiresJavaScriptProtocol;
			}
			set
			{
				this.requiresJavaScriptProtocol = value;
			}
		}

		/// <summary>Gets the control target that receives the postback event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that represents the control that receives the postback event.</returns>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0003B9F0 File Offset: 0x00039BF0
		[DefaultValue(null)]
		public Control TargetControl
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets or sets a value indicating whether the postback event should return the page to the current scroll position and return focus to the current control.</summary>
		/// <returns>true if the postback event should return the page to the current scroll position and return focus to the target control; otherwise, false. The default value is false.</returns>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0003B9F8 File Offset: 0x00039BF8
		// (set) Token: 0x0600163E RID: 5694 RVA: 0x0003BA00 File Offset: 0x00039C00
		[global::System.MonoTODO("Implement support for this in Page")]
		[DefaultValue(false)]
		public bool TrackFocus
		{
			get
			{
				return this.trackFocus;
			}
			set
			{
				this.trackFocus = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.PostBackOptions" /> object causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.PostBackOptions" /> object causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0003BA09 File Offset: 0x00039C09
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x0003BA11 File Offset: 0x00039C11
		[DefaultValue("")]
		[global::System.MonoTODO("Implement support for this in Page")]
		public string ValidationGroup
		{
			get
			{
				return this.validationGroup;
			}
			set
			{
				this.validationGroup = value;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0003BA1A File Offset: 0x00039C1A
		internal bool RequiresSpecialPostBack
		{
			get
			{
				return this.actionUrl != null || this.validationGroup != null || this.trackFocus || this.autoPostBack || this.argument != null;
			}
		}

		// Token: 0x04001556 RID: 5462
		private Control control;

		// Token: 0x04001557 RID: 5463
		private string argument;

		// Token: 0x04001558 RID: 5464
		private string actionUrl;

		// Token: 0x04001559 RID: 5465
		private bool autoPostBack;

		// Token: 0x0400155A RID: 5466
		private bool requiresJavaScriptProtocol;

		// Token: 0x0400155B RID: 5467
		private bool trackFocus;

		// Token: 0x0400155C RID: 5468
		private bool clientSubmit;

		// Token: 0x0400155D RID: 5469
		private bool performValidation;

		// Token: 0x0400155E RID: 5470
		private string validationGroup;
	}
}
