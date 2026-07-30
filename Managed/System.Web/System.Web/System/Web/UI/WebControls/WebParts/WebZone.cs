using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for all controls that act as containers for server controls (including Web Parts <see cref="T:System.Web.UI.WebControls.WebParts.Part" /> controls, server controls, and user controls) in Web Parts applications. </summary>
	// Token: 0x020006C5 RID: 1733
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Bindable(false)]
	public abstract class WebZone : CompositeControl
	{
		// Token: 0x060049C6 RID: 18886 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WebZone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the URL to a background image for a zone.</summary>
		/// <returns>A string that represents the URL to an image used as a background image for the zone. The default value is an empty string ("").</returns>
		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060049C8 RID: 18888 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string BackImageUrl
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

		/// <summary>Gets or sets a message that appears when a zone contains no controls.</summary>
		/// <returns>A string containing the message that appears in an empty zone. A default culture-specific string is supplied by the .NET Framework.</returns>
		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060049CA RID: 18890 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string EmptyZoneText
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

		/// <summary>Gets the style attributes for the placeholder text in an empty zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the text assigned to a zone's <see cref="P:System.Web.UI.WebControls.WebParts.WebZone.EmptyZoneText" /> property.</returns>
		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style EmptyZoneTextStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the style attributes for rendering the error message that is displayed if a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control cannot be loaded or created.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the error message.</returns>
		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style ErrorStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the style attributes for the contents of a zone's footer area.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.TitleStyle" /> that contains style attributes for the contents of a zone's footer area.</returns>
		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public TitleStyle FooterStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value indicating whether a zone has a footer area.</summary>
		/// <returns>true if the zone has a footer area; otherwise, false. The default value is true.</returns>
		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x000CA594 File Offset: 0x000C8794
		protected virtual bool HasFooter
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value indicating whether a zone has a header area.</summary>
		/// <returns>true if the zone has a header area; otherwise, false. The default value is true.</returns>
		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x000CA5B0 File Offset: 0x000C87B0
		protected virtual bool HasHeader
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the style attributes for the contents of a zone's header area.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.TitleStyle" /> that contains style attributes for the contents of a zone's header area.</returns>
		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x060049D0 RID: 18896 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public TitleStyle HeaderStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the text for the header area of a zone.</summary>
		/// <returns>A string that contains the header text for the zone. The default is an empty string ("").</returns>
		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x060049D1 RID: 18897 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060049D2 RID: 18898 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string HeaderText
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

		/// <summary>Gets or sets the cell padding attributes on the table that contains the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</summary>
		/// <returns>The number of pixels for the padding between items and their cell boundaries in the table rendered for a zone. The default is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a value less than -1.</exception>
		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x000CA5CC File Offset: 0x000C87CC
		// (set) Token: 0x060049D4 RID: 18900 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual int Padding
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the distance between the contents of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control and the border of the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that indicates the type of measurement and the amount of padding. The default padding for a zone is 5 pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned to the property is a negative number.</exception>
		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x000CA5E8 File Offset: 0x000C87E8
		// (set) Token: 0x060049D6 RID: 18902 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public Unit PartChromePadding
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Unit);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets style characteristics that apply to the borders of Web Parts controls contained by a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the borders that surround Web Parts controls contained by a zone.</returns>
		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style PartChromeStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the type of border that frames Web Parts controls contained by a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> that determines the type of border that frames Web Parts controls contained by a zone. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. </exception>
		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x000CA604 File Offset: 0x000C8804
		// (set) Token: 0x060049D9 RID: 18905 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual PartChromeType PartChromeType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return PartChromeType.Default;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets style characteristics that apply to the border and contents of each Web Parts control contained by a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableStyle" /> that contains style attributes for the Web Parts controls in the zone.</returns>
		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public TableStyle PartStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets style attributes for the title bar content for each Web Parts control contained by a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.TitleStyle" /> that contains style attributes for the title bar content for each Web Parts control in the zone.</returns>
		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x060049DB RID: 18907 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public TitleStyle PartTitleStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether to render client script on a Web Parts page.</summary>
		/// <returns>true if the zone renders client script when the page is in a given display mode; otherwise, false. The default value is false.</returns>
		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x000CA620 File Offset: 0x000C8820
		protected internal bool RenderClientScript
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets what kind of button is used to represent verbs in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> that indicates what kind of button will be visible to represent verbs in the user interface (UI). The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Link" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is not set to a <see cref="T:System.Web.UI.WebControls.ButtonType" /> value.</exception>
		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x000CA63C File Offset: 0x000C883C
		// (set) Token: 0x060049DE RID: 18910 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual ButtonType VerbButtonType
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

		/// <summary>Gets the style attributes for the user interface (UI) verbs associated with Web Parts controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the verbs associated with Web Parts controls contained by a zone.</returns>
		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x060049DF RID: 18911 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Style VerbStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebZone" /> control instance on a Web Parts page.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control that is associated with a zone.</returns>
		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns the actual or effective current <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> value of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, given the <see cref="P:System.Web.UI.WebControls.WebParts.WebZone.PartChromeType" /> property of the zone and the current display mode of the Web Parts page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> value that contains the kind of border that currently frames Web Parts controls contained by a zone. </returns>
		/// <param name="part">A part control within the current zone.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="part" /> is null.</exception>
		// Token: 0x060049E1 RID: 18913 RVA: 0x000CA658 File Offset: 0x000C8858
		public virtual PartChromeType GetEffectiveChromeType(Part part)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return PartChromeType.Default;
		}

		/// <summary>Overrides rendering for the body of a zone control. </summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x060049E2 RID: 18914 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RenderBody(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides rendering for the footer of a zone control.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x060049E3 RID: 18915 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RenderFooter(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides rendering for the header of a zone control.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x060049E4 RID: 18916 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void RenderHeader(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
