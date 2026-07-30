using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020001A3 RID: 419
	internal class GridEntry : GridItem, ITypeDescriptorContext, IServiceProvider
	{
		// Token: 0x06001B40 RID: 6976 RVA: 0x00069D48 File Offset: 0x00067F48
		protected GridEntry(PropertyGrid propertyGrid, GridEntry parent)
		{
			if (propertyGrid == null)
			{
				throw new ArgumentNullException("propertyGrid");
			}
			this.property_grid = propertyGrid;
			this.plus_minus_bounds = new Rectangle(0, 0, 0, 0);
			this.top = -1;
			this.grid_items = new GridItemCollection();
			this.expanded = false;
			this.parent = parent;
			this.child_griditems_cache = null;
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00069DAC File Offset: 0x00067FAC
		public GridEntry(PropertyGrid propertyGrid, PropertyDescriptor[] properties, GridEntry parent)
			: this(propertyGrid, parent)
		{
			if (properties == null || properties.Length == 0)
			{
				throw new ArgumentNullException("prop_desc");
			}
			this.property_descriptors = properties;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00069DE4 File Offset: 0x00067FE4
		void ITypeDescriptorContext.OnComponentChanged()
		{
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00069DE8 File Offset: 0x00067FE8
		bool ITypeDescriptorContext.OnComponentChanging()
		{
			return false;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x00069DEC File Offset: 0x00067FEC
		IContainer ITypeDescriptorContext.Container
		{
			get
			{
				if (this.PropertyOwner == null)
				{
					return null;
				}
				IComponent component = this.property_grid.SelectedObject as IComponent;
				if (component != null && component.Site != null)
				{
					return component.Site.Container;
				}
				return null;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00069E38 File Offset: 0x00068038
		object ITypeDescriptorContext.Instance
		{
			get
			{
				if (this.ParentEntry != null && this.ParentEntry.PropertyOwner != null)
				{
					return this.ParentEntry.PropertyOwner;
				}
				return this.PropertyOwner;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x00069E74 File Offset: 0x00068074
		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get
			{
				if (this.ParentEntry != null && this.ParentEntry.PropertyDescriptor != null)
				{
					return this.ParentEntry.PropertyDescriptor;
				}
				return this.PropertyDescriptor;
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00069EB0 File Offset: 0x000680B0
		object IServiceProvider.GetService(Type serviceType)
		{
			IComponent component = this.property_grid.SelectedObject as IComponent;
			if (component != null && component.Site != null)
			{
				return component.Site.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x00069EF0 File Offset: 0x000680F0
		public override bool Expandable
		{
			get
			{
				TypeConverter converter = this.GetConverter();
				return converter != null && converter.GetPropertiesSupported(this) && this.GetChildGridItemsCached().Count > 0;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00069F2C File Offset: 0x0006812C
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x00069F34 File Offset: 0x00068134
		public override bool Expanded
		{
			get
			{
				return this.expanded;
			}
			set
			{
				if (this.expanded != value)
				{
					this.expanded = value;
					this.PopulateChildGridItems();
					if (value)
					{
						this.property_grid.OnExpandItem(this);
					}
					else
					{
						this.property_grid.OnCollapseItem(this);
					}
				}
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x00069F80 File Offset: 0x00068180
		public override GridItemCollection GridItems
		{
			get
			{
				this.PopulateChildGridItems();
				return this.grid_items;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x00069F90 File Offset: 0x00068190
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.Property;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x00069F94 File Offset: 0x00068194
		public override string Label
		{
			get
			{
				PropertyDescriptor propertyDescriptor = this.PropertyDescriptor;
				if (propertyDescriptor != null)
				{
					string text = propertyDescriptor.DisplayName;
					ParenthesizePropertyNameAttribute parenthesizePropertyNameAttribute = propertyDescriptor.Attributes[typeof(ParenthesizePropertyNameAttribute)] as ParenthesizePropertyNameAttribute;
					if (parenthesizePropertyNameAttribute != null && parenthesizePropertyNameAttribute.NeedParenthesis)
					{
						text = "(" + text + ")";
					}
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00069FFC File Offset: 0x000681FC
		public override GridItem Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0006A004 File Offset: 0x00068204
		public GridEntry ParentEntry
		{
			get
			{
				if (this.parent != null && this.parent.GridItemType == GridItemType.Category)
				{
					return this.parent.Parent as GridEntry;
				}
				return this.parent as GridEntry;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0006A04C File Offset: 0x0006824C
		public override PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return (this.property_descriptors == null) ? null : this.property_descriptors[0];
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0006A068 File Offset: 0x00068268
		public PropertyDescriptor[] PropertyDescriptors
		{
			get
			{
				return this.property_descriptors;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0006A070 File Offset: 0x00068270
		public object PropertyOwner
		{
			get
			{
				object[] propertyOwners = this.PropertyOwners;
				if (propertyOwners != null)
				{
					return propertyOwners[0];
				}
				return null;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x0006A090 File Offset: 0x00068290
		public object[] PropertyOwners
		{
			get
			{
				if (this.ParentEntry == null)
				{
					return null;
				}
				object[] values = this.ParentEntry.Values;
				PropertyDescriptor[] propertyDescriptors = this.PropertyDescriptors;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] is ICustomTypeDescriptor)
					{
						object propertyOwner = ((ICustomTypeDescriptor)values[i]).GetPropertyOwner(propertyDescriptors[i]);
						if (propertyOwner != null)
						{
							values[i] = propertyOwner;
						}
					}
				}
				return values;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0006A0FC File Offset: 0x000682FC
		public bool HasMergedValue
		{
			get
			{
				if (!this.IsMerged)
				{
					return false;
				}
				object[] values = this.Values;
				int num = 0;
				while (num + 1 < values.Length)
				{
					if (!object.Equals(values[num], values[num + 1]))
					{
						return false;
					}
					num++;
				}
				return true;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0006A148 File Offset: 0x00068348
		public virtual bool IsMerged
		{
			get
			{
				return this.PropertyDescriptors != null && this.PropertyDescriptors.Length > 1;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x0006A164 File Offset: 0x00068364
		public virtual object[] Values
		{
			get
			{
				if (this.PropertyDescriptor == null || this.PropertyOwners == null)
				{
					return null;
				}
				if (this.IsMerged)
				{
					object[] propertyOwners = this.PropertyOwners;
					PropertyDescriptor[] propertyDescriptors = this.PropertyDescriptors;
					object[] array = new object[propertyOwners.Length];
					for (int i = 0; i < propertyOwners.Length; i++)
					{
						array[i] = propertyDescriptors[i].GetValue(propertyOwners[i]);
					}
					return array;
				}
				return new object[] { this.Value };
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0006A1E0 File Offset: 0x000683E0
		public override object Value
		{
			get
			{
				if (this.PropertyDescriptor == null || this.PropertyOwner == null)
				{
					return null;
				}
				return this.PropertyDescriptor.GetValue(this.PropertyOwner);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0006A218 File Offset: 0x00068418
		public string ValueText
		{
			get
			{
				string text = null;
				try
				{
					text = this.ConvertToString(this.Value);
					if (text == null)
					{
						text = string.Empty;
					}
				}
				catch
				{
					text = string.Empty;
				}
				return text;
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0006A270 File Offset: 0x00068470
		public override bool Select()
		{
			this.property_grid.SelectedGridItem = this;
			return true;
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0006A280 File Offset: 0x00068480
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x0006A288 File Offset: 0x00068488
		internal int Top
		{
			get
			{
				return this.top;
			}
			set
			{
				if (this.top != value)
				{
					this.top = value;
				}
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0006A2A0 File Offset: 0x000684A0
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0006A2A8 File Offset: 0x000684A8
		internal Rectangle PlusMinusBounds
		{
			get
			{
				return this.plus_minus_bounds;
			}
			set
			{
				this.plus_minus_bounds = value;
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0006A2B4 File Offset: 0x000684B4
		public void SetParent(GridItem parent)
		{
			this.parent = parent;
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0006A2C0 File Offset: 0x000684C0
		public ICollection AcceptedValues
		{
			get
			{
				TypeConverter converter = this.GetConverter();
				if (this.PropertyDescriptor != null && converter != null && converter.GetStandardValuesSupported(this))
				{
					ArrayList arrayList = new ArrayList();
					ICollection standardValues = converter.GetStandardValues(this);
					if (standardValues != null)
					{
						foreach (object obj in standardValues)
						{
							string text = this.ConvertToString(obj);
							if (text != null)
							{
								arrayList.Add(text);
							}
						}
					}
					return (arrayList.Count <= 0) ? null : arrayList;
				}
				return null;
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0006A38C File Offset: 0x0006858C
		private string ConvertToString(object value)
		{
			if (value is string)
			{
				return (string)value;
			}
			if (this.PropertyDescriptor != null && this.PropertyDescriptor.Converter != null && this.PropertyDescriptor.Converter.CanConvertTo(this, typeof(string)))
			{
				try
				{
					return this.PropertyDescriptor.Converter.ConvertToString(this, value);
				}
				catch
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0006A430 File Offset: 0x00068630
		public bool HasCustomEditor
		{
			get
			{
				return this.EditorStyle != 1;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0006A440 File Offset: 0x00068640
		public UITypeEditorEditStyle EditorStyle
		{
			get
			{
				UITypeEditor editor = this.GetEditor();
				if (editor != null)
				{
					try
					{
						return editor.GetEditStyle(this);
					}
					catch
					{
					}
					return 1;
				}
				return 1;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0006A494 File Offset: 0x00068694
		public bool EditorResizeable
		{
			get
			{
				if (this.EditorStyle == 3)
				{
					UITypeEditor editor = this.GetEditor();
					if (editor != null && editor.IsDropDownResizable)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0006A4C8 File Offset: 0x000686C8
		public bool EditValue(IWindowsFormsEditorService service)
		{
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			ServiceContainer serviceContainer2;
			if (serviceContainer != null)
			{
				serviceContainer2 = new ServiceContainer(serviceContainer);
			}
			else
			{
				serviceContainer2 = new ServiceContainer();
			}
			serviceContainer2.AddService(typeof(IWindowsFormsEditorService), service);
			UITypeEditor editor = this.GetEditor();
			if (editor != null)
			{
				try
				{
					object obj = editor.EditValue(this, serviceContainer2, this.Value);
					string text = null;
					return this.SetValue(obj, out text);
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0006A584 File Offset: 0x00068784
		private UITypeEditor GetEditor()
		{
			if (this.PropertyDescriptor != null)
			{
				try
				{
					if (this.PropertyDescriptor != null)
					{
						return (UITypeEditor)this.PropertyDescriptor.GetEditor(typeof(UITypeEditor));
					}
				}
				catch
				{
				}
			}
			return null;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0006A5F4 File Offset: 0x000687F4
		private TypeConverter GetConverter()
		{
			if (this.PropertyDescriptor != null)
			{
				return this.PropertyDescriptor.Converter;
			}
			return null;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0006A610 File Offset: 0x00068810
		public bool ToggleValue()
		{
			if (this.IsReadOnly || (this.IsMerged && !this.HasMergedValue))
			{
				return false;
			}
			bool flag = false;
			string text = null;
			object value = this.Value;
			if (this.PropertyDescriptor.PropertyType == typeof(bool))
			{
				flag = this.SetValue(!(bool)value, out text);
			}
			else
			{
				TypeConverter converter = this.GetConverter();
				if (converter != null && converter.GetStandardValuesSupported(this))
				{
					TypeConverter.StandardValuesCollection standardValues = converter.GetStandardValues(this);
					if (standardValues != null)
					{
						for (int i = 0; i < standardValues.Count; i++)
						{
							if (value != null && value.Equals(standardValues[i]))
							{
								if (i < standardValues.Count - 1)
								{
									flag = this.SetValue(standardValues[i + 1], out text);
								}
								else
								{
									flag = this.SetValue(standardValues[0], out text);
								}
								break;
							}
						}
					}
				}
			}
			if (!flag && text != null)
			{
				this.property_grid.ShowError(text);
			}
			return flag;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0006A73C File Offset: 0x0006893C
		public bool SetValue(object value, out string error)
		{
			error = null;
			if (this.IsReadOnly)
			{
				return false;
			}
			if (this.SetValueCore(value, out error))
			{
				this.InvalidateChildGridItemsCache();
				this.property_grid.OnPropertyValueChangedInternal(this, this.Value);
				return true;
			}
			return false;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0006A784 File Offset: 0x00068984
		protected virtual bool SetValueCore(object value, out string error)
		{
			error = null;
			TypeConverter converter = this.GetConverter();
			Type type = ((value == null) ? null : value.GetType());
			if (type != null && this.PropertyDescriptor.PropertyType != null && !this.PropertyDescriptor.PropertyType.IsAssignableFrom(type))
			{
				bool flag = false;
				try
				{
					if (converter != null && converter.CanConvertFrom(this, type))
					{
						value = converter.ConvertFrom(this, CultureInfo.CurrentCulture, value);
					}
					else
					{
						flag = true;
					}
				}
				catch (Exception ex)
				{
					error = ex.Message;
					flag = true;
				}
				if (flag)
				{
					string text = this.ConvertToString(value);
					string text2;
					if (text != null)
					{
						text2 = string.Concat(new string[]
						{
							"Property value '",
							text,
							"' of '",
							this.PropertyDescriptor.Name,
							"' is not convertible to type '",
							this.PropertyDescriptor.PropertyType.Name,
							"'"
						});
					}
					else
					{
						text2 = string.Concat(new string[]
						{
							"Property value of '",
							this.PropertyDescriptor.Name,
							"' is not convertible to type '",
							this.PropertyDescriptor.PropertyType.Name,
							"'"
						});
					}
					error = text2 + Environment.NewLine + Environment.NewLine + error;
					return false;
				}
			}
			bool flag2 = false;
			bool flag3 = false;
			object[] propertyOwners = this.PropertyOwners;
			PropertyDescriptor[] propertyDescriptors = this.PropertyDescriptors;
			for (int i = 0; i < propertyOwners.Length; i++)
			{
				object value2 = propertyDescriptors[i].GetValue(propertyOwners[i]);
				flag3 = false;
				if (!object.Equals(value2, value))
				{
					if (this.ShouldCreateParentInstance)
					{
						Hashtable hashtable = new Hashtable();
						PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(propertyOwners[i]);
						foreach (object obj in properties)
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
							if (propertyDescriptor.Name == propertyDescriptors[i].Name)
							{
								hashtable[propertyDescriptor.Name] = value;
							}
							else
							{
								hashtable[propertyDescriptor.Name] = propertyDescriptor.GetValue(propertyOwners[i]);
							}
						}
						object obj2 = this.ParentEntry.PropertyDescriptor.Converter.CreateInstance(this, hashtable);
						if (obj2 != null)
						{
							flag3 = this.ParentEntry.SetValueCore(obj2, out error);
						}
					}
					else
					{
						try
						{
							propertyDescriptors[i].SetValue(propertyOwners[i], value);
						}
						catch
						{
							return false;
						}
						if (this.IsValueType(this.ParentEntry))
						{
							flag3 = this.ParentEntry.SetValueCore(propertyOwners[i], out error);
						}
						else
						{
							flag3 = object.Equals(propertyDescriptors[i].GetValue(propertyOwners[i]), value);
						}
					}
				}
				if (flag3)
				{
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0006AAD8 File Offset: 0x00068CD8
		private bool IsValueType(GridEntry item)
		{
			return item != null && item.PropertyDescriptor != null && (item.PropertyDescriptor.PropertyType.IsValueType || item.PropertyDescriptor.PropertyType.IsPrimitive);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0006AB24 File Offset: 0x00068D24
		public bool ResetValue()
		{
			if (this.IsResetable)
			{
				object[] propertyOwners = this.PropertyOwners;
				PropertyDescriptor[] propertyDescriptors = this.PropertyDescriptors;
				for (int i = 0; i < propertyOwners.Length; i++)
				{
					propertyDescriptors[i].ResetValue(propertyOwners[i]);
					if (this.IsValueType(this.ParentEntry))
					{
						string text = null;
						if (!this.ParentEntry.SetValueCore(propertyOwners[i], out text) && text != null)
						{
							this.property_grid.ShowError(text);
						}
					}
				}
				this.property_grid.OnPropertyValueChangedInternal(this, this.Value);
				return true;
			}
			return false;
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0006ABBC File Offset: 0x00068DBC
		public bool HasDefaultValue
		{
			get
			{
				return this.PropertyDescriptor != null && !this.PropertyDescriptor.ShouldSerializeValue(this.PropertyOwner);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0006ABEC File Offset: 0x00068DEC
		public virtual bool IsResetable
		{
			get
			{
				return !this.IsReadOnly && this.PropertyDescriptor.CanResetValue(this.PropertyOwner);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0006AC18 File Offset: 0x00068E18
		public virtual bool IsEditable
		{
			get
			{
				TypeConverter converter = this.GetConverter();
				return this.PropertyDescriptor != null && !this.PropertyDescriptor.PropertyType.IsArray && (!this.PropertyDescriptor.IsReadOnly || this.ShouldCreateParentInstance) && converter != null && converter.CanConvertFrom(this, typeof(string)) && (!converter.GetStandardValuesSupported(this) || !converter.GetStandardValuesExclusive(this));
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0006ACA8 File Offset: 0x00068EA8
		public virtual bool IsReadOnly
		{
			get
			{
				TypeConverter converter = this.GetConverter();
				return this.PropertyDescriptor == null || this.PropertyOwner == null || (this.PropertyDescriptor.IsReadOnly && (this.EditorStyle != 2 || this.PropertyDescriptor.PropertyType.IsValueType) && !this.ShouldCreateParentInstance) || (this.PropertyDescriptor.IsReadOnly && TypeDescriptor.GetAttributes(this.PropertyDescriptor.PropertyType)[typeof(ImmutableObjectAttribute)].Equals(ImmutableObjectAttribute.Yes)) || (this.ShouldCreateParentInstance && this.ParentEntry.IsReadOnly) || (!this.HasCustomEditor && converter == null) || (converter != null && !converter.GetStandardValuesSupported(this) && !converter.CanConvertFrom(this, typeof(string)) && !this.HasCustomEditor) || (this.PropertyDescriptor.PropertyType.IsArray && !this.HasCustomEditor);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0006ADDC File Offset: 0x00068FDC
		public bool IsPassword
		{
			get
			{
				return this.PropertyDescriptor != null && this.PropertyDescriptor.Attributes.Contains(PasswordPropertyTextAttribute.Yes);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0006AE0C File Offset: 0x0006900C
		public virtual bool ShouldCreateParentInstance
		{
			get
			{
				if (this.ParentEntry != null && this.ParentEntry.PropertyDescriptor != null)
				{
					TypeConverter converter = this.ParentEntry.GetConverter();
					if (converter != null && converter.GetCreateInstanceSupported(this))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0006AE58 File Offset: 0x00069058
		public virtual bool PaintValueSupported
		{
			get
			{
				UITypeEditor editor = this.GetEditor();
				if (editor != null)
				{
					try
					{
						return editor.GetPaintValueSupported();
					}
					catch
					{
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0006AEA8 File Offset: 0x000690A8
		public virtual void PaintValue(Graphics gfx, Rectangle rect)
		{
			UITypeEditor editor = this.GetEditor();
			if (editor != null)
			{
				try
				{
					editor.PaintValue(this.Value, gfx, rect);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0006AEF8 File Offset: 0x000690F8
		protected void PopulateChildGridItems()
		{
			this.grid_items = this.GetChildGridItemsCached();
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0006AF08 File Offset: 0x00069108
		private void InvalidateChildGridItemsCache()
		{
			if (this.child_griditems_cache != null)
			{
				this.child_griditems_cache = null;
				this.PopulateChildGridItems();
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0006AF24 File Offset: 0x00069124
		private GridItemCollection GetChildGridItemsCached()
		{
			if (this.child_griditems_cache == null)
			{
				this.child_griditems_cache = this.GetChildGridItems();
			}
			return this.child_griditems_cache;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0006AF44 File Offset: 0x00069144
		private GridItemCollection GetChildGridItems()
		{
			object[] values = this.Values;
			string[] mergedPropertyNames = this.GetMergedPropertyNames(values);
			GridItemCollection gridItemCollection = new GridItemCollection();
			foreach (string text in mergedPropertyNames)
			{
				PropertyDescriptor[] array2 = new PropertyDescriptor[values.Length];
				for (int j = 0; j < values.Length; j++)
				{
					array2[j] = this.GetPropertyDescriptor(values[j], text);
				}
				gridItemCollection.Add(new GridEntry(this.property_grid, array2, this));
			}
			return gridItemCollection;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0006AFD0 File Offset: 0x000691D0
		private bool IsPropertyMergeable(PropertyDescriptor property)
		{
			if (property == null)
			{
				return false;
			}
			MergablePropertyAttribute mergablePropertyAttribute = property.Attributes[typeof(MergablePropertyAttribute)] as MergablePropertyAttribute;
			return mergablePropertyAttribute == null || mergablePropertyAttribute.AllowMerge;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0006B014 File Offset: 0x00069214
		private string[] GetMergedPropertyNames(object[] objects)
		{
			if (objects == null || objects.Length == 0)
			{
				return new string[0];
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] != null)
				{
					PropertyDescriptorCollection properties = this.GetProperties(objects[i], this.property_grid.BrowsableAttributes);
					ArrayList arrayList2 = new ArrayList();
					IEnumerable enumerable;
					if (i == 0)
					{
						ICollection collection = properties;
						enumerable = collection;
					}
					else
					{
						enumerable = arrayList;
					}
					foreach (object obj in enumerable)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
						PropertyDescriptor propertyDescriptor2 = ((i != 0) ? properties[propertyDescriptor.Name] : propertyDescriptor);
						if (objects.Length <= 1 || this.IsPropertyMergeable(propertyDescriptor2))
						{
							if (propertyDescriptor2.PropertyType == propertyDescriptor.PropertyType)
							{
								arrayList2.Add(propertyDescriptor2);
							}
						}
					}
					arrayList = arrayList2;
				}
			}
			string[] array = new string[arrayList.Count];
			for (int j = 0; j < arrayList.Count; j++)
			{
				array[j] = ((PropertyDescriptor)arrayList[j]).Name;
			}
			return array;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0006B17C File Offset: 0x0006937C
		private PropertyDescriptor GetPropertyDescriptor(object propertyOwner, string propertyName)
		{
			if (propertyOwner == null || propertyName == null)
			{
				return null;
			}
			PropertyDescriptorCollection properties = this.GetProperties(propertyOwner, this.property_grid.BrowsableAttributes);
			if (properties != null)
			{
				return properties[propertyName];
			}
			return null;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0006B1BC File Offset: 0x000693BC
		private PropertyDescriptorCollection GetProperties(object propertyOwner, AttributeCollection attributes)
		{
			if (propertyOwner == null || this.property_grid.SelectedTab == null)
			{
				return new PropertyDescriptorCollection(null);
			}
			Attribute[] array = new Attribute[attributes.Count];
			attributes.CopyTo(array, 0);
			return this.property_grid.SelectedTab.GetProperties(this, propertyOwner, array);
		}

		// Token: 0x04000F0C RID: 3852
		private PropertyGrid property_grid;

		// Token: 0x04000F0D RID: 3853
		private bool expanded;

		// Token: 0x04000F0E RID: 3854
		private GridItemCollection grid_items;

		// Token: 0x04000F0F RID: 3855
		private GridItem parent;

		// Token: 0x04000F10 RID: 3856
		private PropertyDescriptor[] property_descriptors;

		// Token: 0x04000F11 RID: 3857
		private int top;

		// Token: 0x04000F12 RID: 3858
		private Rectangle plus_minus_bounds;

		// Token: 0x04000F13 RID: 3859
		private GridItemCollection child_griditems_cache;
	}
}
