using System;
using System.ComponentModel;
using System.Drawing;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for all zone controls that act as containers for <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or other server or user) controls.</summary>
	// Token: 0x020006C4 RID: 1732
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartZoneBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class WebPartZoneBase : WebZone, IPostBackEventHandler
	{
		/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x06004988 RID: 18824 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected WebPartZoneBase()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a value that indicates whether the layout of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls can be changed within a zone.</summary>
		/// <returns>true if the layout can be changed; otherwise, false. The default is true.</returns>
		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x000CA460 File Offset: 0x000C8660
		// (set) Token: 0x0600498A RID: 18826 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool AllowLayoutChange
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

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to close the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to close <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb CloseVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to create connections between <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that creates a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb ConnectVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to delete the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to delete <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb DeleteVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the current value of the text being used as the title for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone when the zone itself is visible.</summary>
		/// <returns>A string that contains the title text for a zone. The default is the value of the base <see cref="P:System.Web.UI.WebControls.WebParts.WebZone.HeaderText" /> property.</returns>
		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string DisplayTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls can be dragged into or out of a zone.</summary>
		/// <returns>A Boolean value that indicates whether controls can be dragged. </returns>
		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x000CA47C File Offset: 0x000C867C
		protected internal bool DragDropEnabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the color around the border of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone and its drop-cue regions when a user is dragging a control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that contains the highlight color. The default color is <see cref="P:System.Drawing.Color.Blue" />.</returns>
		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x000CA498 File Offset: 0x000C8698
		// (set) Token: 0x06004991 RID: 18833 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual Color DragHighlightColor
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Color);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to edit <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to edit <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06004992 RID: 18834 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb EditVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a message that appears when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> control contains no <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A string containing the message that appears in an empty zone. A default culture-specific string is supplied by the .NET Framework.</returns>
		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06004993 RID: 18835 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004994 RID: 18836 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string EmptyZoneText
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

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to export an XML definition file for each <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to export a definition file for <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb ExportVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value indicating whether a zone has a footer area.</summary>
		/// <returns>true if the zone has a footer area; otherwise, false. The default value is false.</returns>
		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x000CA4B4 File Offset: 0x000C86B4
		protected override bool HasFooter
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value indicating whether a zone has a header area.</summary>
		/// <returns>true if the zone has a header area; otherwise, false. The default value is false when the page is in normal <see cref="F:System.Web.UI.WebControls.WebParts.WebPartManager.BrowseDisplayMode" />.</returns>
		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06004997 RID: 18839 RVA: 0x000CA4D0 File Offset: 0x000C86D0
		protected override bool HasHeader
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object used to access Help content for <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables users to access Help content on the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</returns>
		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06004998 RID: 18840 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb HelpVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether controls in a zone are arranged vertically or horizontally.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.Orientation" /> value that determines how controls in a zone are arranged. The default orientation is <see cref="F:System.Web.UI.WebControls.Orientation.Vertical" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not one of the enumerated <see cref="T:System.Web.UI.WebControls.Orientation" /> values.</exception>
		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06004999 RID: 18841 RVA: 0x000CA4EC File Offset: 0x000C86EC
		// (set) Token: 0x0600499A RID: 18842 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual Orientation LayoutOrientation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return Orientation.Horizontal;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets style attributes that are applied to the check mark image that appears on a verbs menu next to the selected verb text.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style attributes for check mark images in a verbs menu. </returns>
		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x0600499B RID: 18843 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style MenuCheckImageStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the URL to an image used as a check mark in the verbs menu of each <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <returns>A string that represents the URL to an image used as a check mark. The default value is an empty string ("").</returns>
		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x0600499C RID: 18844 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x0600499D RID: 18845 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string MenuCheckImageUrl
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

		/// <summary>Gets style attributes that are applied to the label of a verbs menu in the title bar of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control when a user positions the mouse pointer over the label.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style attributes for the label in a verbs menu. </returns>
		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x0600499E RID: 18846 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style MenuLabelHoverStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets style information for the label of the verbs drop-down menu that appears in the title bar of each <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style information for the label of the verbs menu. </returns>
		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style MenuLabelStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the value that serves as a label for the verbs drop-down menu in the title bar of each <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <returns>A string containing the text that appears in the label for the verbs menu. The default is an empty string ("").</returns>
		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060049A1 RID: 18849 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string MenuLabelText
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

		/// <summary>Gets or sets the URL to an image that opens the verbs drop-down menu in the title bar of each <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <returns>A string that represents the URL to an image used to open the verbs drop-down menu. The default value is an empty string ("").</returns>
		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x060049A2 RID: 18850 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060049A3 RID: 18851 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string MenuPopupImageUrl
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

		/// <summary>Gets style attributes for the drop-down verbs menu that appears on <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartMenuStyle" /> that contains style attributes for the verbs menu.</returns>
		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x060049A4 RID: 18852 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartMenuStyle MenuPopupStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets style information for the appearance of a verb in a verbs drop-down menu when an end user positions the mouse pointer over the verb.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style information for a verb when a user positions the mouse pointer over the verb.</returns>
		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style MenuVerbHoverStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets style information for the appearance of a verb in a verbs drop-down menu when the menu is displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style information for a verb displayed in a verbs menu.</returns>
		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x060049A6 RID: 18854 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style MenuVerbStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to minimize <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to minimize <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb MinimizeVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to restore <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone to normal size.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to restore <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb RestoreVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets style information for the appearance of a selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style information for the selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style SelectedPartChromeStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether title icons are displayed in the title bar of each <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <returns>true if title icons are displayed; otherwise, false. The default is true.</returns>
		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x000CA508 File Offset: 0x000C8708
		// (set) Token: 0x060049AB RID: 18859 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool ShowTitleIcons
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

		/// <summary>Gets or sets the type of button used for the verbs in the title bar of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> that indicates what type of button is used for the verbs in the title bar of a control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value for the property is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x000CA524 File Offset: 0x000C8724
		// (set) Token: 0x060049AD RID: 18861 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual ButtonType TitleBarVerbButtonType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ButtonType.Button;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets style attributes for verbs in the title bar of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for verbs.</returns>
		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x060049AE RID: 18862 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style TitleBarVerbStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the kind of button associated with the verbs that exist in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone when accessed with an older browser. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> that determines what kind of button is associated with the verbs in a zone. </returns>
		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x000CA540 File Offset: 0x000C8740
		// (set) Token: 0x060049B0 RID: 18864 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override ButtonType VerbButtonType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ButtonType.Button;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" /> object that determines the peripheral rendering for <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in the zone. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" /> that determines rendering for controls in the zone.</returns>
		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x060049B1 RID: 18865 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartChrome WebPartChrome
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the collection of Web Parts controls contained within a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCollection" /> that contains references to all the Web Parts controls in a zone.</returns>
		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartCollection WebParts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value indicating how the verbs should be rendered on <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in the zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbRenderMode" /> enumeration value indicating how verbs should be rendered on <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. The default value is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartVerbRenderMode.Menu" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value for the property is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbRenderMode" /> values.</exception>
		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x000CA55C File Offset: 0x000C875C
		// (set) Token: 0x060049B4 RID: 18868 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual WebPartVerbRenderMode WebPartVerbRenderMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return WebPartVerbRenderMode.Menu;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the verbs are created for a zone that derives from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> class.</summary>
		// Token: 0x1400012C RID: 300
		// (add) Token: 0x060049B5 RID: 18869 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x060049B6 RID: 18870 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event WebPartVerbsEventHandler CreateVerbs
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

		/// <summary>Closes a selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  control to be closed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049B7 RID: 18871 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void CloseWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initiates the process of creating a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  control that initiates the connection with another selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  control. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049B8 RID: 18872 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void ConnectWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Enables derived zones to substitute a custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" /> object to change the appearance of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartChrome" /> that determines how <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone are rendered.</returns>
		// Token: 0x060049B9 RID: 18873 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual WebPartChrome CreateWebPartChrome()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Deletes a selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to be deleted. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049BA RID: 18874 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void DeleteWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initiates the process of editing a selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to be edited. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049BB RID: 18875 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void EditWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> value that contains the kind of border that currently frames Web Parts controls contained by a zone. </returns>
		/// <param name="part">A <see cref="T:System.Web.UI.WebControls.WebParts.Part" /> control for which the zone needs to retrieve the current <see cref="P:System.Web.UI.WebControls.WebParts.Part.ChromeType" /> setting.</param>
		// Token: 0x060049BC RID: 18876 RVA: 0x000CA578 File Offset: 0x000C8778
		public override PartChromeType GetEffectiveChromeType(Part part)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return PartChromeType.Default;
		}

		/// <summary>Gets an initial collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls contained in a zone, based on a template or some storage medium.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCollection" /> that contains the initial set of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls that belong in a zone.</returns>
		// Token: 0x060049BD RID: 18877
		protected internal abstract WebPartCollection GetInitialWebParts();

		/// <summary>Minimizes a selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a zone.</summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to be minimized. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049BE RID: 18878 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void MinimizeWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartZoneBase.CreateVerbs" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060049BF RID: 18879 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnCreateVerbs(WebPartVerbsEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the status of the current collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone, based on the changes that have occurred since the most recent postback event.</summary>
		/// <param name="eventArgument">The postback argument. </param>
		// Token: 0x060049C0 RID: 18880 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides the base method to render the body area of a zone derived from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> class.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the zone's body content. </param>
		// Token: 0x060049C1 RID: 18881 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void RenderBody(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders user interface (UI) elements to indicate to an end user where a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is being dragged can be dropped within a zone.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the UI elements that indicate where a control can be dropped. </param>
		// Token: 0x060049C2 RID: 18882 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RenderDropCue(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides the base method to render the header of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone that contains <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the content of the header. </param>
		// Token: 0x060049C3 RID: 18883 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void RenderHeader(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Restores a selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that was minimized to normal state.</summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to be restored. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		// Token: 0x060049C4 RID: 18884 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RestoreWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
		/// <param name="eventArgument">The postback argument.</param>
		// Token: 0x060049C5 RID: 18885 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
