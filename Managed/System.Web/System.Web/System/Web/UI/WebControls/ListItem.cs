using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a data item in a data-bound list control. This class cannot be inherited.</summary>
	// Token: 0x020003C1 RID: 961
	[ControlBuilder(typeof(ListItemControlBuilder))]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true, "Text")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ListItem : IAttributeAccessor, IParserAccessor, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text, value, and enabled data.</summary>
		/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />.</param>
		/// <param name="value">The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />.</param>
		/// <param name="enabled">Indicates whether the <see cref="T:System.Web.UI.WebControls.ListItem" /> is enabled.</param>
		// Token: 0x060027D5 RID: 10197 RVA: 0x00067A09 File Offset: 0x00065C09
		public ListItem(string text, string value, bool enabled)
			: this(text, value)
		{
			this.enabled = enabled;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text and value data.</summary>
		/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		/// <param name="value">The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		// Token: 0x060027D6 RID: 10198 RVA: 0x00067A1A File Offset: 0x00065C1A
		public ListItem(string text, string value)
		{
			this.text = text;
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text data.</summary>
		/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		// Token: 0x060027D7 RID: 10199 RVA: 0x00067A37 File Offset: 0x00065C37
		public ListItem(string text)
			: this(text, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class.</summary>
		// Token: 0x060027D8 RID: 10200 RVA: 0x00067A41 File Offset: 0x00065C41
		public ListItem()
			: this(null, null)
		{
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.ListItem" /> from the specified text.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that represents the text specified by the <paramref name="s" /> parameter.</returns>
		/// <param name="s">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		// Token: 0x060027D9 RID: 10201 RVA: 0x00067A4B File Offset: 0x00065C4B
		public static ListItem FromString(string s)
		{
			return new ListItem(s);
		}

		/// <summary>Determines whether the specified object has the same value and text as the current list item.</summary>
		/// <returns>true if the specified object is equivalent to the current list item; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current list item.</param>
		// Token: 0x060027DA RID: 10202 RVA: 0x00067A54 File Offset: 0x00065C54
		public override bool Equals(object o)
		{
			ListItem listItem = o as ListItem;
			return listItem != null && listItem.Text == this.Text && listItem.Value == this.Value;
		}

		/// <summary>Serves as a hash function for a particular type, and is suitable for use in hashing algorithms and data structures like a hash table.</summary>
		// Token: 0x060027DB RID: 10203 RVA: 0x00067A93 File Offset: 0x00065C93
		public override int GetHashCode()
		{
			return this.Text.GetHashCode() ^ this.Value.GetHashCode();
		}

		/// <summary>Returns the attribute value of the list item control having the specified attribute name.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="name">The name component of an attribute's name/value pair. </param>
		// Token: 0x060027DC RID: 10204 RVA: 0x00067AAC File Offset: 0x00065CAC
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this.attrs == null)
			{
				return null;
			}
			return this.Attributes[key];
		}

		/// <summary>Sets an attribute of the list item control with the specified name and value.</summary>
		/// <param name="name">The name component of the attribute's name/value pair. </param>
		/// <param name="value">The value component of the attribute's name/value pair. </param>
		// Token: 0x060027DD RID: 10205 RVA: 0x00067AC4 File Offset: 0x00065CC4
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		/// <summary>Allows the <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property to be persisted as inner content.</summary>
		/// <param name="obj">The specified object that is parsed. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="obj" /> is a <see cref="T:System.Web.UI.DataBoundLiteralControl" />.- or -<paramref name="obj" /> is not a <see cref="T:System.Web.UI.LiteralControl" />. </exception>
		// Token: 0x060027DE RID: 10206 RVA: 0x00067AD4 File Offset: 0x00065CD4
		void IParserAccessor.AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl == null)
			{
				throw new HttpException("'ListItem' cannot have children of type " + obj.GetType());
			}
			this.Text = literalControl.Text;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control. </param>
		// Token: 0x060027DF RID: 10207 RVA: 0x00067B0D File Offset: 0x00065D0D
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x00067B18 File Offset: 0x00065D18
		internal void LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			if (array[0] != null)
			{
				this.sb = new StateBag(true);
				this.sb.LoadViewState(array[0]);
				this.sb.SetDirty(true);
			}
			if (array[1] != null)
			{
				this.text = (string)array[1];
			}
			if (array[2] != null)
			{
				this.value = (string)array[2];
			}
			if (array[3] != null)
			{
				this.selected = (bool)array[3];
			}
			if (array[4] != null)
			{
				this.enabled = (bool)array[4];
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x060027E1 RID: 10209 RVA: 0x00067BA7 File Offset: 0x00065DA7
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x00067BB0 File Offset: 0x00065DB0
		internal object SaveViewState()
		{
			if (!this.dirty)
			{
				return null;
			}
			return new object[]
			{
				(this.sb != null) ? this.sb.SaveViewState() : null,
				this.text,
				this.value,
				this.selected,
				this.enabled
			};
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
		// Token: 0x060027E3 RID: 10211 RVA: 0x00067C14 File Offset: 0x00065E14
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x00067C1C File Offset: 0x00065E1C
		internal void TrackViewState()
		{
			this.tracking = true;
			if (this.sb != null)
			{
				this.sb.TrackViewState();
				this.sb.SetDirty(true);
			}
		}

		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060027E5 RID: 10213 RVA: 0x00067C44 File Offset: 0x00065E44
		public override string ToString()
		{
			return this.Text;
		}

		/// <summary>Gets a collection of attribute name and value pairs for the <see cref="T:System.Web.UI.WebControls.ListItem" /> that are not directly supported by the class.</summary>
		/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> that contains a collection of name and value pairs.</returns>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x00067C4C File Offset: 0x00065E4C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this.attrs != null)
				{
					return this.attrs;
				}
				if (this.sb == null)
				{
					this.sb = new StateBag(true);
					if (this.tracking)
					{
						this.sb.TrackViewState();
					}
				}
				return this.attrs = new AttributeCollection(this.sb);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>true if view state is being tracked; otherwise false.  The default is true.</returns>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x00067CA3 File Offset: 0x00065EA3
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is selected.</summary>
		/// <returns>true if the item is selected; otherwise, false. The default is false.</returns>
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x00067CAB File Offset: 0x00065EAB
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x00067CB3 File Offset: 0x00065EB3
		[TypeConverter("System.Web.UI.MinimizableAttributeTypeConverter")]
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
				if (this.tracking)
				{
					this.SetDirty();
				}
			}
		}

		/// <summary>Gets or sets the text displayed in a list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />.</summary>
		/// <returns>The text displayed in a list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x00067CCC File Offset: 0x00065ECC
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x00067CF4 File Offset: 0x00065EF4
		[PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				string empty = this.text;
				if (empty == null)
				{
					empty = this.value;
				}
				if (empty == null)
				{
					empty = string.Empty;
				}
				return empty;
			}
			set
			{
				this.text = value;
				if (this.tracking)
				{
					this.SetDirty();
				}
			}
		}

		/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />.</summary>
		/// <returns>The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x00067D0C File Offset: 0x00065F0C
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x00067D34 File Offset: 0x00065F34
		[DefaultValue("")]
		[Localizable(true)]
		public string Value
		{
			get
			{
				string empty = this.value;
				if (empty == null)
				{
					empty = this.text;
				}
				if (empty == null)
				{
					empty = string.Empty;
				}
				return empty;
			}
			set
			{
				this.value = value;
				if (this.tracking)
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x00067D4B File Offset: 0x00065F4B
		internal void SetDirty()
		{
			this.dirty = true;
		}

		/// <summary>Gets or sets a value indicating whether the list item is enabled.</summary>
		/// <returns>true if the list item is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x00067D54 File Offset: 0x00065F54
		// (set) Token: 0x060027F0 RID: 10224 RVA: 0x00067D5C File Offset: 0x00065F5C
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				if (this.tracking)
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x00067D73 File Offset: 0x00065F73
		internal bool HasAttributes
		{
			get
			{
				return this.attrs != null && this.attrs.Count > 0;
			}
		}

		// Token: 0x04001A65 RID: 6757
		private string text;

		// Token: 0x04001A66 RID: 6758
		private string value;

		// Token: 0x04001A67 RID: 6759
		private bool selected;

		// Token: 0x04001A68 RID: 6760
		private bool dirty;

		// Token: 0x04001A69 RID: 6761
		private bool enabled = true;

		// Token: 0x04001A6A RID: 6762
		private bool tracking;

		// Token: 0x04001A6B RID: 6763
		private StateBag sb;

		// Token: 0x04001A6C RID: 6764
		private AttributeCollection attrs;
	}
}
