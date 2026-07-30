using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style of a submenu in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	// Token: 0x02000415 RID: 1045
	public class SubMenuStyle : Style, ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class. </summary>
		// Token: 0x06002F17 RID: 12055 RVA: 0x0006ED8C File Offset: 0x0006CF8C
		public SubMenuStyle()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class with the specified view-state information. </summary>
		/// <param name="bag">The view-state information of the current request.</param>
		// Token: 0x06002F18 RID: 12056 RVA: 0x0006ED94 File Offset: 0x0006CF94
		public SubMenuStyle(StateBag bag)
			: base(bag)
		{
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0007CB32 File Offset: 0x0007AD32
		private bool IsSet(string v)
		{
			return base.ViewState[v] != null;
		}

		/// <summary>Gets or sets the amount of space to the left and right of a submenu.</summary>
		/// <returns>The amount of space to the left and right of the text of a submenu. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of <see cref="P:System.Web.UI.WebControls.Unit.Type" /><see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than 0.</exception>
		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x0007CB43 File Offset: 0x0007AD43
		// (set) Token: 0x06002F1B RID: 12059 RVA: 0x0007CB6D File Offset: 0x0007AD6D
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit HorizontalPadding
		{
			get
			{
				if (this.IsSet("HorizontalPadding"))
				{
					return (Unit)base.ViewState["HorizontalPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["HorizontalPadding"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space above and below a submenu.</summary>
		/// <returns>The amount of space above and below a submenu. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of <see cref="P:System.Web.UI.WebControls.Unit.Type" /><see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than 0.</exception>
		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x0007CB85 File Offset: 0x0007AD85
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x0007CBAF File Offset: 0x0007ADAF
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit VerticalPadding
		{
			get
			{
				if (this.IsSet("VerticalPadding"))
				{
					return (Unit)base.ViewState["VerticalPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["VerticalPadding"] = value;
			}
		}

		/// <summary>Copies the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the current instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> object to copy.</param>
		// Token: 0x06002F1E RID: 12062 RVA: 0x0007CBC8 File Offset: 0x0007ADC8
		public override void CopyFrom(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			base.CopyFrom(s);
			SubMenuStyle subMenuStyle = s as SubMenuStyle;
			if (subMenuStyle == null)
			{
				return;
			}
			if (subMenuStyle.IsSet("HorizontalPadding"))
			{
				this.HorizontalPadding = subMenuStyle.HorizontalPadding;
			}
			if (subMenuStyle.IsSet("VerticalPadding"))
			{
				this.VerticalPadding = subMenuStyle.VerticalPadding;
			}
		}

		/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with those of the current instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> object to combine settings with.</param>
		// Token: 0x06002F1F RID: 12063 RVA: 0x0007CC28 File Offset: 0x0007AE28
		public override void MergeWith(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				SubMenuStyle subMenuStyle = s as SubMenuStyle;
				if (subMenuStyle == null)
				{
					return;
				}
				if (subMenuStyle.IsSet("HorizontalPadding") && !this.IsSet("HorizontalPadding"))
				{
					this.HorizontalPadding = subMenuStyle.HorizontalPadding;
				}
				if (subMenuStyle.IsSet("VerticalPadding") && !this.IsSet("VerticalPadding"))
				{
					this.VerticalPadding = subMenuStyle.VerticalPadding;
				}
			}
		}

		/// <summary>Returns the current instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class to its original state.</summary>
		// Token: 0x06002F20 RID: 12064 RVA: 0x0007CCB0 File Offset: 0x0007AEB0
		public override void Reset()
		{
			if (this.IsSet("HorizontalPadding"))
			{
				base.ViewState.Remove("HorizontalPadding");
			}
			if (this.IsSet("VerticalPadding"))
			{
				base.ViewState.Remove("VerticalPadding");
			}
			base.Reset();
		}

		/// <summary>Adds the style properties of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to the specified <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
		/// <param name="attributes">The <see cref="T:System.Web.UI.CssStyleCollection" /> object to which to add the style properties.</param>
		/// <param name="urlResolver">The <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object that contains the context information for the current location (URL).</param>
		// Token: 0x06002F21 RID: 12065 RVA: 0x0007CD00 File Offset: 0x0007AF00
		protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			base.FillStyleAttributes(attributes, urlResolver);
			if (this.IsSet("HorizontalPadding"))
			{
				attributes.Add(HtmlTextWriterStyle.PaddingLeft, this.HorizontalPadding.ToString());
				attributes.Add(HtmlTextWriterStyle.PaddingRight, this.HorizontalPadding.ToString());
			}
			if (this.IsSet("VerticalPadding"))
			{
				attributes.Add(HtmlTextWriterStyle.PaddingTop, this.VerticalPadding.ToString());
				attributes.Add(HtmlTextWriterStyle.PaddingBottom, this.VerticalPadding.ToString());
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetAttributes" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.</returns>
		// Token: 0x06002F22 RID: 12066 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetClassName" />.</summary>
		/// <returns>The class name of the object, or null if the class does not have a name.</returns>
		// Token: 0x06002F23 RID: 12067 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		string ICustomTypeDescriptor.GetClassName()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetComponentName" />.</summary>
		/// <returns>The name of the object, or null if the object does not have a name.</returns>
		// Token: 0x06002F24 RID: 12068 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		string ICustomTypeDescriptor.GetComponentName()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetConverter" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.</returns>
		// Token: 0x06002F25 RID: 12069 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> object that represents the default event for the object, or null if the object has no events.</returns>
		// Token: 0x06002F26 RID: 12070 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> object that represents the default property for this object, or null if the object does not have properties.</returns>
		// Token: 0x06002F27 RID: 12071 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEditor(System.Type)" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
		// Token: 0x06002F28 RID: 12072 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEvents" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that contains the events for this instance.</returns>
		// Token: 0x06002F29 RID: 12073 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEvents(System.Attribute[])" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that contains the filtered events for this instance.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x06002F2A RID: 12074 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] arr)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetProperties" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties for this instance.</returns>
		// Token: 0x06002F2B RID: 12075 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetProperties(System.Attribute[])" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> object that contains the filtered properties for this instance.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x06002F2C RID: 12076 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] arr)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
		// Token: 0x06002F2D RID: 12077 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001BE5 RID: 7141
		private const string HORZ_PADD = "HorizontalPadding";

		// Token: 0x04001BE6 RID: 7142
		private const string VERT_PADD = "VerticalPadding";
	}
}
