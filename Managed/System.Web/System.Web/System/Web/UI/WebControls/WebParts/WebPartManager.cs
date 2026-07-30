using System;
using System.ComponentModel;
using System.Security;
using System.Xml;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the central class of the Web Parts control set, managing all the Web Parts controls, functionality, and events that occur on a Web page. </summary>
	// Token: 0x020006B7 RID: 1719
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartManagerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[NonVisualControl]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ViewStateModeById]
	public class WebPartManager : Control, INamingContainer, IPersonalizable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> class.</summary>
		// Token: 0x0600487F RID: 18559 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartManager()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects that are available for use in creating Web Parts connections between server controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> that contains a set of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</returns>
		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public TransformerTypeCollection AvailableTransformers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a warning that is displayed when a user closes a control that is acting as a provider to other controls in a connection.</summary>
		/// <returns>A string that contains the warning message. The default is a culture-specific message supplied by the .NET Framework.</returns>
		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004882 RID: 18562 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string CloseProviderWarning
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to the collection of all current connections on a Web page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionCollection" /> that contains a set of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> objects.</returns>
		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartConnectionCollection Connections
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a custom warning message displayed to end users when they delete a control.</summary>
		/// <returns>A string that contains the text of the warning message. The default value is a localized warning message.</returns>
		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06004884 RID: 18564 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004885 RID: 18565 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string DeleteWarning
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the active display mode for a Web page that contains Web Parts controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> that determines a page's display mode.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> object being assigned to the property is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> object being assigned to the property is not one of the supported display modes.- or - The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> object being assigned to the property is disabled.</exception>
		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06004886 RID: 18566 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004887 RID: 18567 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual WebPartDisplayMode DisplayMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a read-only collection of all display modes that are associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection" /> that contains the set of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> objects associated with the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control. </returns>
		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06004888 RID: 18568 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartDisplayModeCollection DisplayModes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the collection of all dynamic connections that currently exist on a Web page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionCollection" /> that contains references to all dynamic connections on a page.</returns>
		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal WebPartConnectionCollection DynamicConnections
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that determines whether client-side scripting is enabled on the Web page that contains a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <returns>A Boolean value that indicates whether client script can run on the page. The default value is true.</returns>
		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x0600488A RID: 18570 RVA: 0x000C9F74 File Offset: 0x000C8174
		// (set) Token: 0x0600488B RID: 18571 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool EnableClientScript
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a warning message that is displayed when a user attempts to export sensitive state data from a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the warning message. The default message is a culture-specific value supplied by the .NET Framework.</returns>
		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x0600488C RID: 18572 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x0600488D RID: 18573 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ExportSensitiveDataWarning
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManagerInternals" /> class, which is used to combine and separate a set of methods that are actually implemented in the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> class, but are mostly useful for control developers.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManagerInternals" />, through which a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> can reference the various methods that have been separated into the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManagerInternals" />.</returns>
		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x0600488E RID: 18574 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPartManagerInternals Internals
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether personalization changes have been made that affect page-level personalization details controlled by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control. </summary>
		/// <returns>A Boolean value that indicates whether personalization changes have been made. The default value is false.</returns>
		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x000C9F90 File Offset: 0x000C8190
		protected virtual bool IsCustomPersonalizationStateDirty
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.PermissionSet" /> object that allows only <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> permission and <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</summary>
		/// <returns>A <see cref="T:System.Security.PermissionSet" /> object that allows only <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> permission and <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</returns>
		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06004890 RID: 18576 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual PermissionSet MediumPermissionSet
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.PermissionSet" /> object that allows only <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> permission and <see cref="F:System.Web.AspNetHostingPermissionLevel.Minimal" /> permission.</summary>
		/// <returns>Gets a <see cref="T:System.Security.PermissionSet" /> object that allows only <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> permission and <see cref="F:System.Web.AspNetHostingPermissionLevel.Minimal" /> permission.</returns>
		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06004891 RID: 18577 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual PermissionSet MinimalPermissionSet
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to an object that contains personalization data for a Web page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> that contains personalization data.</returns>
		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartPersonalization Personalization
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control that is currently selected for editing or for creating a connection with another control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is currently selected for editing or forming a connection.</returns>
		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06004893 RID: 18579 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPart SelectedWebPart
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the collection of all <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> objects on a Web page that are defined as static connections.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionCollection" /> that contains all the static connections on the page.</returns>
		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06004894 RID: 18580 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartConnectionCollection StaticConnections
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a read-only collection of all display modes that are available on a particular Web page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection" /> that contains the set of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> objects available on a specific Web page.</returns>
		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06004895 RID: 18581 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartDisplayModeCollection SupportedDisplayModes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x000C9FAC File Offset: 0x000C81AC
		bool IPersonalizable.get_IsDirty()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Gets a reference to all <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls tracked by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control on a Web page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCollection" /> that contains references to a set of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartCollection WebParts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a collection of all the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zones on a Web page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneCollection" /> that references a set of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zones.</returns>
		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartZoneCollection Zones
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Occurs when the <see cref="Overload:System.Web.UI.WebControls.WebParts.WebPartManager.IsAuthorized" /> method is called to determine whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control can be added to a page.</summary>
		// Token: 0x14000119 RID: 281
		// (add) Token: 0x06004899 RID: 18585 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x0600489A RID: 18586 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartAuthorizationEventHandler AuthorizeWebPart
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after all the current Web Parts connections on a page are not only connected, but have also begun actively sharing data between the consumer and provider controls involved in each connection.</summary>
		// Token: 0x1400011A RID: 282
		// (add) Token: 0x0600489B RID: 18587 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x0600489C RID: 18588 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event EventHandler ConnectionsActivated
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs during the process of activating all the established Web Parts connections on a Web page.</summary>
		// Token: 0x1400011B RID: 283
		// (add) Token: 0x0600489D RID: 18589 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x0600489E RID: 18590 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event EventHandler ConnectionsActivating
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after the current display mode on a Web Parts page has changed.</summary>
		// Token: 0x1400011C RID: 284
		// (add) Token: 0x0600489F RID: 18591 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048A0 RID: 18592 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartDisplayModeEventHandler DisplayModeChanged
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after a user clicks a verb on a Web page that begins the process of changing to a different display mode.</summary>
		// Token: 0x1400011D RID: 285
		// (add) Token: 0x060048A1 RID: 18593 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048A2 RID: 18594 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartDisplayModeCancelEventHandler DisplayModeChanging
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after the selection of one <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has changed and moved to another control on a Web page.</summary>
		// Token: 0x1400011E RID: 286
		// (add) Token: 0x060048A3 RID: 18595 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048A4 RID: 18596 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartEventHandler SelectedWebPartChanged
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs during the process of changing which <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control is currently selected on a Web page.</summary>
		// Token: 0x1400011F RID: 287
		// (add) Token: 0x060048A5 RID: 18597 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048A6 RID: 18598 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartCancelEventHandler SelectedWebPartChanging
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after a dynamic <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control has been added to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone, to indicate that the control was added successfully.</summary>
		// Token: 0x14000120 RID: 288
		// (add) Token: 0x060048A7 RID: 18599 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048A8 RID: 18600 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartEventHandler WebPartAdded
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs during the process of adding a dynamic <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		// Token: 0x14000121 RID: 289
		// (add) Token: 0x060048A9 RID: 18601 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048AA RID: 18602 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartAddingEventHandler WebPartAdding
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control (or server or user control) is removed from a page.</summary>
		// Token: 0x14000122 RID: 290
		// (add) Token: 0x060048AB RID: 18603 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048AC RID: 18604 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartEventHandler WebPartClosed
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs during the process of removing a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control (or server or user control) from a page.</summary>
		// Token: 0x14000123 RID: 291
		// (add) Token: 0x060048AD RID: 18605 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048AE RID: 18606 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartCancelEventHandler WebPartClosing
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control has been deleted from a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		// Token: 0x14000124 RID: 292
		// (add) Token: 0x060048AF RID: 18607 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048B0 RID: 18608 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartEventHandler WebPartDeleted
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs during the process of permanently deleting an instance of a dynamic <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control from a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		// Token: 0x14000125 RID: 293
		// (add) Token: 0x060048B1 RID: 18609 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048B2 RID: 18610 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartCancelEventHandler WebPartDeleting
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control has been moved to a different location on a Web page.</summary>
		// Token: 0x14000126 RID: 294
		// (add) Token: 0x060048B3 RID: 18611 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048B4 RID: 18612 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartEventHandler WebPartMoved
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs during the process of moving a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control that is contained in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		// Token: 0x14000127 RID: 295
		// (add) Token: 0x060048B5 RID: 18613 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048B6 RID: 18614 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartMovingEventHandler WebPartMoving
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after a specific connection has been established between <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls (or server or user controls).</summary>
		// Token: 0x14000128 RID: 296
		// (add) Token: 0x060048B7 RID: 18615 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048B8 RID: 18616 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartConnectionsEventHandler WebPartsConnected
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs during the process of creating a connection between <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls (or server or user controls placed in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone).</summary>
		// Token: 0x14000129 RID: 297
		// (add) Token: 0x060048B9 RID: 18617 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048BA RID: 18618 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartConnectionsCancelEventHandler WebPartsConnecting
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs after a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server controls has been terminated.</summary>
		// Token: 0x1400012A RID: 298
		// (add) Token: 0x060048BB RID: 18619 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048BC RID: 18620 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartConnectionsEventHandler WebPartsDisconnected
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs during the process of ending the connection between previously connected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server controls.</summary>
		// Token: 0x1400012B RID: 299
		// (add) Token: 0x060048BD RID: 18621 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060048BE RID: 18622 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartConnectionsCancelEventHandler WebPartsDisconnecting
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Makes active all connections on a Web page that are currently inactive.</summary>
		// Token: 0x060048BF RID: 18623 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void ActivateConnections()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides the standard programmatic method for adding <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls to a Web page. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that was added to the page.</returns>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) to be added to a Web page or opened on a page. </param>
		/// <param name="zone">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> that <paramref name="webPart" /> is being added to.</param>
		/// <param name="zoneIndex">An integer that represents the ordinal position that <paramref name="webPart" /> occupies in <paramref name="zone" />, relative to other controls in <paramref name="zone" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.- or - <paramref name="zone" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="zone" /> is not registered in the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's collection of zones.- or - <paramref name="webPart" /> is already in <paramref name="zone" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="zoneIndex" /> is less than zero.</exception>
		// Token: 0x060048C0 RID: 18624 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPart AddWebPart(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Starts the process of connecting two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. </summary>
		/// <param name="webPart">The control for which the connection is being formed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current display mode on the page is not <see cref="F:System.Web.UI.WebControls.WebParts.WebPartManager.ConnectDisplayMode" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> is closed.- or - <paramref name="webPart" /> is not part of the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.Controls" /> collection.-or -<paramref name="webPart" /> is equal to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPart" /> control.</exception>
		// Token: 0x060048C1 RID: 18625 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void BeginWebPartConnecting(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Starts the process of editing a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <param name="webPart">The control to be edited. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current display mode on the page is not <see cref="F:System.Web.UI.WebControls.WebParts.WebPartManager.EditDisplayMode" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> is closed.- or - <paramref name="webPart" /> is not part of the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.Controls" /> collection.-or -<paramref name="webPart" /> is equal to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPart" /> control.</exception>
		// Token: 0x060048C2 RID: 18626 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void BeginWebPartEditing(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Checks the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls that will be participating in a connection to determine whether they are capable of being connected, when the consumer and provider controls have compatible interfaces and a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> object is not needed.</summary>
		/// <returns>A Boolean value that indicates whether <paramref name="provider" /> and <paramref name="consumer" /> can be connected.</returns>
		/// <param name="provider">The control that provides data to <paramref name="consumer" /> when the controls are connected.</param>
		/// <param name="providerConnectionPoint">A <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" /> that enables <paramref name="provider" /> to participate in a connection.</param>
		/// <param name="consumer">The control that receives data from <paramref name="provider" /> when the controls are connected.</param>
		/// <param name="consumerConnectionPoint">A <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" /> that acts as a callback method so that <paramref name="consumer" /> can participate in a connection.</param>
		// Token: 0x060048C3 RID: 18627 RVA: 0x000C9FC8 File Offset: 0x000C81C8
		public bool CanConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Checks the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls that will be participating in a connection to determine whether they are capable of being connected, and uses a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> object to create the connection between an incompatible consumer and provider.</summary>
		/// <returns>A Boolean value that indicates whether <paramref name="provider" /> and <paramref name="consumer" /> can form a connection.</returns>
		/// <param name="provider">The control that provides data to <paramref name="consumer" /> when the controls are connected.</param>
		/// <param name="providerConnectionPoint">A <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" /> that acts as a callback method so that <paramref name="provider" /> can participate in a connection.</param>
		/// <param name="consumer">The control that receives data from <paramref name="provider" /> when the controls are connected.</param>
		/// <param name="consumerConnectionPoint">A <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" /> that acts as a callback method so that <paramref name="consumer" /> can participate in a connection.</param>
		/// <param name="transformer">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> that enables an incompatible <paramref name="provider" /> and <paramref name="consumer" /> to connect. </param>
		// Token: 0x060048C4 RID: 18628 RVA: 0x000C9FE4 File Offset: 0x000C81E4
		public virtual bool CanConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartTransformer transformer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Checks the capabilities of the browser making the request, and the value of the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.EnableClientScript" /> property, to determine whether to render client script.</summary>
		/// <returns>A Boolean value that indicates whether to render client script.  </returns>
		// Token: 0x060048C5 RID: 18629 RVA: 0x000CA000 File Offset: 0x000C8200
		protected virtual bool CheckRenderClientScript()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Closes a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in such a way that it is not rendered on a Web page, but can be reopened.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control that is being closed in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> is not in the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.Controls" /> collection.- or -<paramref name="webPart" /> is a shared control and has already been closed by another user.</exception>
		// Token: 0x060048C6 RID: 18630 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CloseWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> controls using only the references to the controls and their specified <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> that contains the various information about the provider and the consumer needed for a connection.</returns>
		/// <param name="provider">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that has the role of furnishing data to another connected control. </param>
		/// <param name="providerConnectionPoint">A method that serves as a callback method for the connection. As implemented in the Web Parts control set, this is a public method in <paramref name="provider" /> that is marked with a ConnectionProvider metadata attribute. </param>
		/// <param name="consumer">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that has the role of receiving data from <paramref name="provider" />, and then processing or displaying it. </param>
		/// <param name="consumerConnectionPoint">A method that connects with <paramref name="providerConnectionPoint" /> to receive the data for the connection. As implemented in the Web Parts control set, this is a public method in <paramref name="consumer" /> that is marked with a ConnectionConsumer metadata attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's collection of dynamic collections is read-only. </exception>
		// Token: 0x060048C7 RID: 18631 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartConnection ConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Creates a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> controls using the references to the controls, their specified <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" /> objects, and a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> that contains the information about the provider, consumer, and transformer needed for a connection.</returns>
		/// <param name="provider">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that has the role of furnishing data to another connected control. </param>
		/// <param name="providerConnectionPoint">A public method in <paramref name="provider" /> that is marked with a ConnectionProvider metadata attribute, and serves as a callback method for the connection. </param>
		/// <param name="consumer">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that has the role of receiving data from <paramref name="provider" /> or <paramref name="transformer" />, and then processing or displaying it. </param>
		/// <param name="consumerConnectionPoint">A public method in <paramref name="consumer" /> that is marked with a ConnectionConsumer metadata attribute, and connects with <paramref name="providerConnectionPoint" /> to receive the data for the connection. </param>
		/// <param name="transformer">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> that enables a connection between two controls by converting the data from <paramref name="provider" /> to a format that <paramref name="consumer" /> can process. </param>
		/// <exception cref="T:System.InvalidOperationException">Connections have already been activated in <see cref="E:System.Web.UI.Control.PreRender" />.</exception>
		// Token: 0x060048C8 RID: 18632 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartConnection ConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartTransformer transformer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Used by the Web Parts control set to create a copy of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control for the purpose of adding the control to a Web page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> to be added to a page.</returns>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control to be copied. </param>
		// Token: 0x060048C9 RID: 18633 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual WebPart CopyWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Creates a set of transformers specified in a Web site's configuration file and adds them to the collection of transformers referenced by the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.AvailableTransformers" /> property.</summary>
		/// <returns>The collection of transformers specified in a Web site's configuration file.</returns>
		// Token: 0x060048CA RID: 18634 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual TransformerTypeCollection CreateAvailableTransformers()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Creates the set of all possible display modes for a Web Parts application.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection" /> that contains all the display modes that are supported.</returns>
		// Token: 0x060048CB RID: 18635 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual WebPartDisplayModeCollection CreateDisplayModes()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a unique value to serve as an ID for a dynamic connection.</summary>
		/// <returns>A string that contains a unique ID for a connection.</returns>
		// Token: 0x060048CC RID: 18636 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual string CreateDynamicConnectionID()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Generates a unique ID for a dynamic <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the unique ID for a control. </returns>
		/// <param name="webPartType">The <see cref="T:System.Type" /> of the control for which an ID is being generated. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPartType" /> is null.</exception>
		// Token: 0x060048CD RID: 18637 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual string CreateDynamicWebPartID(Type webPartType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Creates a special control that is inserted into a page and displayed for end users, when an attempt to load or create a dynamic <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control fails for some reason.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.ErrorWebPart" /> that is inserted into a page in place of a control that failed to be loaded or created.</returns>
		/// <param name="originalID">A string that is the ID of the failing control. If a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> is involved in the failure, the ID is the ID of its child server control.</param>
		/// <param name="originalTypeName">A string that is the name of the <see cref="T:System.Type" /> of the failed control. If a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> is involved in the failure, the type name is the type of its child server control. </param>
		/// <param name="originalPath">A string that contains the path to a user control, if a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> that contains a child user control is involved in the failure.</param>
		/// <param name="genericWebPartID">A string that returns the ID of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" />, if that type of control was involved in the failure to load or create a control.</param>
		/// <param name="errorMessage">A string that contains the error message to display on the page.</param>
		// Token: 0x060048CE RID: 18638 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual ErrorWebPart CreateErrorWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID, string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a personalization object to contain a user's personalization data for the current Web page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> to contain a user's personalization data.</returns>
		// Token: 0x060048CF RID: 18639 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual WebPartPersonalization CreatePersonalization()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Wraps a server control that is not a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control with a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> object, so that the control can have Web Parts functionality.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> that wraps <paramref name="control" /> and enables it to function as a true <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		/// <param name="control">A server control that is not a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. </param>
		// Token: 0x060048D0 RID: 18640 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual GenericWebPart CreateWebPart(Control control)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Permanently removes a dynamic instance of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control from a Web page.</summary>
		/// <param name="webPart">The server control to be deleted.</param>
		// Token: 0x060048D1 RID: 18641 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void DeleteWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control that is being closed or deleted from any connections it is participating in.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is to be disconnected.  </param>
		// Token: 0x060048D2 RID: 18642 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void DisconnectWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Carries out the process of disconnecting server controls that are connected on a Web page.</summary>
		/// <param name="connection">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> that represents the connection between server controls. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="connection" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="connection" /> is not contained in either <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.StaticConnections" /> or <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.DynamicConnections" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.StaticConnections" /> is read-only.- or -<paramref name="connection" /> has already been disconnected from <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.StaticConnections" />.- or -<see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.DynamicConnections" /> is read-only.- or -<paramref name="connection" /> has already been disconnected from <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.DynamicConnections" />.</exception>
		// Token: 0x060048D3 RID: 18643 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void DisconnectWebParts(WebPartConnection connection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Completes the process of connecting a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to another control.</summary>
		/// <exception cref="T:System.InvalidOperationException">The control referenced by the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPart" /> property is null.</exception>
		// Token: 0x060048D4 RID: 18644 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void EndWebPartConnecting()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Completes the process of editing a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <exception cref="T:System.InvalidOperationException">The control referenced by the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPart" /> property is null.</exception>
		// Token: 0x060048D5 RID: 18645 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void EndWebPartEditing()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates an XML description file that contains state and property data for a server control.</summary>
		/// <param name="webPart">The control from which data will be exported. </param>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> that writes the exported data from <paramref name="webPart" /> to an XML description file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.- or -<paramref name="writer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> is not contained in the collection of controls referenced in <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.Controls" />.- or -The <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.ExportMode" /> property of <paramref name="webPart" /> is set to a value of <see cref="F:System.Web.UI.WebControls.WebParts.WebPartExportMode.None" />, which means that export is disabled for <paramref name="webPart" />.</exception>
		// Token: 0x060048D6 RID: 18646 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void ExportWebPart(WebPart webPart, XmlWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the collection of <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> objects that can act as connection points from a server control that is acting as a consumer within a Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" /> that contains all connection points in the consumer.</returns>
		/// <param name="webPart">A server control that is acting as a consumer in a connection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060048D7 RID: 18647 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ConsumerConnectionPointCollection GetConsumerConnectionPoints(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves a reference to the current instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control on a page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> that references the current instance of the control on a page.</returns>
		/// <param name="page">The Web page that contains an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="page" /> is null.</exception>
		// Token: 0x060048D8 RID: 18648 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static WebPartManager GetCurrentWebPartManager(Page page)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a string containing the value for the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.DisplayTitle" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the calculated value of <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.DisplayTitle" /> for <paramref name="webPart" />.</returns>
		/// <param name="webPart">The control for which the method returns the value of <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.DisplayTitle" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> is not in the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.Controls" /> collection.</exception>
		// Token: 0x060048D9 RID: 18649 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal virtual string GetDisplayTitle(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the relative virtual path and the query string that are part of the request when a user attempts to export a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. </summary>
		/// <returns>A string that contains the relative virtual path and the query string that together form the request to export a control.</returns>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is being exported. </param>
		// Token: 0x060048DA RID: 18650 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetExportUrl(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a reference to the instance of the <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control that contains a server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> that wraps <paramref name="control" /> as a child control. The method returns null if <paramref name="control" /> is not contained in a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" />.</returns>
		/// <param name="control">A server control that exists in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> and is wrapped as a child control of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> at run time. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		// Token: 0x060048DB RID: 18651 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public GenericWebPart GetGenericWebPart(Control control)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves the collection of <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects that can act as connection points from a server control that is acting as a provider within a Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" /> that contains all connection points in the provider.</returns>
		/// <param name="webPart">A server control that is acting as a provider in a connection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060048DC RID: 18652 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ProviderConnectionPointCollection GetProviderConnectionPoints(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Imports an XML description file that contains state and property data for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, and applies the data to the control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or a server control that is wrapped by a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> and thus treated as a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />) that is referenced in the imported XML description file.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> that reads the state and property data from the XML description file that is being imported.</param>
		/// <param name="errorMessage">A <see cref="T:System.String" /> that is displayed to the user if an error is encountered during import. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null.</exception>
		/// <exception cref="T:System.IO.EndOfStreamException">
		///   <paramref name="reader" /> could not read the file.- or -<paramref name="reader" /> needed to display an import error message but did not find one in the file.- or - <paramref name="reader" /> reached the end of the file without finding the XML element that contains the exported data.</exception>
		// Token: 0x060048DD RID: 18653 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPart ImportWebPart(XmlReader reader, out string errorMessage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Carries out the final steps in determining whether a control is authorized to be added to a page.</summary>
		/// <returns>A Boolean value that indicates whether a control is authorized to be added to a page.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the control being checked for authorization. </param>
		/// <param name="path">The relative application path to the source file for the control being authorized, if the control is a user control. </param>
		/// <param name="authorizationFilter">An arbitrary string value assigned to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, used to authorize whether a control can be added to a page. </param>
		/// <param name="isShared">Indicates whether the control being checked for authorization is a shared control, meaning that it is visible to many or all users of the application, and its <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsShared" /> property value is set to true. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is a user control, and <paramref name="path" /> is either null or an empty string ("").- or -<paramref name="type" /> is not a user control, and <paramref name="path" /> has a value assigned to it.</exception>
		// Token: 0x060048DE RID: 18654 RVA: 0x000CA01C File Offset: 0x000C821C
		public virtual bool IsAuthorized(Type type, string path, string authorizationFilter, bool isShared)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Carries out the initial steps in determining whether a control is authorized to be added to a page.</summary>
		/// <returns>A Boolean value that indicates whether <paramref name="webPart" /> can be added to a page.</returns>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control being checked for authorization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060048DF RID: 18655 RVA: 0x000CA038 File Offset: 0x000C8238
		public bool IsAuthorized(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Stores the custom personalization data that has been passed to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control by the personalization objects to be used later during the initialization process.</summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> that contains the state data to be loaded. </param>
		// Token: 0x060048E0 RID: 18656 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void LoadCustomPersonalizationState(PersonalizationDictionary state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Moves a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control from one <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone to another, or to a new position within the same zone. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control being moved. </param>
		/// <param name="zone">The target <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> to which <paramref name="webPart" /> is being moved. </param>
		/// <param name="zoneIndex">An integer that indicates the index of <paramref name="webPart" /> relative to other controls within <paramref name="zone" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> is not contained in the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.Controls" /> collection of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.- or -<paramref name="zone" /> is not contained in the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.Zones" /> collection of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.- or -The zone referenced by the <paramref name="webPart" /> control's <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.Zone" /> property is null, which means that <paramref name="webPart" /> is not currently contained in a zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> or <paramref name="zone" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="zoneIndex" /> is less than zero.</exception>
		// Token: 0x060048E1 RID: 18657 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void MoveWebPart(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.AuthorizeWebPart" /> event and invokes a handler for the event, if one exists.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartAuthorizationEventArgs" />  that contains event data. </param>
		// Token: 0x060048E2 RID: 18658 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnAuthorizeWebPart(WebPartAuthorizationEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.ConnectionsActivated" /> event to indicate that a page and its controls are loaded, and connections on the page have been activated to begin sharing data.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060048E3 RID: 18659 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnConnectionsActivated(EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.ConnectionsActivating" /> event to indicate that a page and its controls have loaded, and the process of activating connections can begin.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.  </param>
		// Token: 0x060048E4 RID: 18660 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnConnectionsActivating(EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event to indicate that the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control has completed the process of switching from one display mode to another on a Web page.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> that contains event data associated with a changed display mode.</param>
		// Token: 0x060048E5 RID: 18661 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnDisplayModeChanged(WebPartDisplayModeEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanging" /> event to indicate that the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control is in the process of switching from one display mode to another on a Web page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data associated with a changing display mode. </param>
		// Token: 0x060048E6 RID: 18662 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnDisplayModeChanging(WebPartDisplayModeCancelEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event, which occurs after a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has either been newly selected or had its selection cleared.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data. </param>
		// Token: 0x060048E7 RID: 18663 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnSelectedWebPartChanged(WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanging" /> event, which occurs during the process of changing which <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is currently selected. </summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060048E8 RID: 18664 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnSelectedWebPartChanging(WebPartCancelEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdded" /> event, which occurs after a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has been added to a page.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data. </param>
		// Token: 0x060048E9 RID: 18665 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartAdded(WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdding" /> event, which occurs during the process of adding a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control (or a server or user control) to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartAddingEventArgs" /> that contains the event data. </param>
		// Token: 0x060048EA RID: 18666 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartAdding(WebPartAddingEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosed" /> event to signal that a control has been removed from a page.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data. </param>
		// Token: 0x060048EB RID: 18667 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartClosed(WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosing" /> event, which occurs during the process of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control being removed from a page.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060048EC RID: 18668 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartClosing(WebPartCancelEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleted" /> event, which occurs after a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has been permanently deleted from a page.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data.  </param>
		// Token: 0x060048ED RID: 18669 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartDeleted(WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleting" /> event, which indicates that a dynamic <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control (or server or user control that is contained in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone) is in the process of being deleted.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCancelEventArgs" /> that contains the event data.</param>
		// Token: 0x060048EE RID: 18670 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartDeleting(WebPartCancelEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoved" /> event, which occurs after a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has been moved to a different location on a page.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data. </param>
		// Token: 0x060048EF RID: 18671 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartMoved(WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoving" /> event, which indicates that a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server or user control in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone is in the process of being moved.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartMovingEventArgs" /> that contains the event data. </param>
		// Token: 0x060048F0 RID: 18672 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartMoving(WebPartMovingEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnected" /> event, which occurs after a connection has been established between <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsEventArgs" /> that contains the event data. </param>
		// Token: 0x060048F1 RID: 18673 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartsConnected(WebPartConnectionsEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsConnecting" /> event, which occurs during the process of establishing a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server or user controls contained in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060048F2 RID: 18674 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartsConnecting(WebPartConnectionsCancelEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnected" /> event, which occurs after a connection between <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls has ended.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsEventArgs" /> that contains the event data.  </param>
		// Token: 0x060048F3 RID: 18675 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartsDisconnected(WebPartConnectionsEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartsDisconnecting" /> event, which indicates that two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server or user controls in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone are in the process of ending a connection.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionsCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060048F4 RID: 18676 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnWebPartsDisconnecting(WebPartConnectionsCancelEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Enables the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control to emit client-side script that is used for various personalization features, such as dragging <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a Web page.</summary>
		// Token: 0x060048F5 RID: 18677 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RegisterClientScript()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves custom personalization state data maintained by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control, so that this data can be reloaded whenever the page is reloaded.</summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> that contains the state data to be loaded. </param>
		// Token: 0x060048F6 RID: 18678 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void SaveCustomPersonalizationState(PersonalizationDictionary state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets a flag indicating that custom personalization data for the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control has changed.</summary>
		// Token: 0x060048F7 RID: 18679 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void SetPersonalizationDirty()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPart" /> property value equal to the currently selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control that has been selected. </param>
		// Token: 0x060048F8 RID: 18680 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void SetSelectedWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns previously saved custom personalization state data that needs to be loaded to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> that contains the state data to be loaded.</param>
		// Token: 0x060048F9 RID: 18681 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IPersonalizable.Load(PersonalizationDictionary state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves custom personalization state data that is managed by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> that contains the state data to be saved. </param>
		// Token: 0x060048FA RID: 18682 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IPersonalizable.Save(PersonalizationDictionary state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Represents the default display mode for pages that contain Web Parts controls. This field is read-only. </summary>
		// Token: 0x040025D6 RID: 9686
		public static readonly WebPartDisplayMode BrowseDisplayMode;

		/// <summary>Represents the display mode used for adding server controls from a catalog of controls to a Web page. This field is read-only.</summary>
		// Token: 0x040025D7 RID: 9687
		public static readonly WebPartDisplayMode CatalogDisplayMode;

		/// <summary>Represents the display mode used for displaying a special user interface (UI) for users to manage connections between <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. This field is read-only.</summary>
		// Token: 0x040025D8 RID: 9688
		public static readonly WebPartDisplayMode ConnectDisplayMode;

		/// <summary>Represents the display mode used for changing the layout of Web pages that contain Web Parts controls. This field is read-only.</summary>
		// Token: 0x040025D9 RID: 9689
		public static readonly WebPartDisplayMode DesignDisplayMode;

		/// <summary>Represents the display mode in which end users can edit and modify server controls. This field is read-only.</summary>
		// Token: 0x040025DA RID: 9690
		public static readonly WebPartDisplayMode EditDisplayMode;
	}
}
