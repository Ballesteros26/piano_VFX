using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200000C RID: 12
	internal class ControlBindingsConverter : TypeConverter
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002468 File Offset: 0x00000668
		[MonoTODO]
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			ControlBindingsCollection controlBindingsCollection = value as ControlBindingsCollection;
			object bindableComponent = controlBindingsCollection.BindableComponent;
			if (controlBindingsCollection != null && bindableComponent != null)
			{
				foreach (object obj in TypeDescriptor.GetProperties(bindableComponent, attributes))
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if (((BindableAttribute)propertyDescriptor.Attributes[typeof(BindableAttribute)]).Bindable)
					{
						propertyDescriptorCollection.Add(new ControlBindingsConverter.DataBindingPropertyDescriptor(propertyDescriptor, attributes, true));
					}
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000023D8 File Offset: 0x000005D8
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002510 File Offset: 0x00000710
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000252E File Offset: 0x0000072E
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0200000D RID: 13
		[MonoTODO]
		private class DataBindingPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x0600006C RID: 108 RVA: 0x00002554 File Offset: 0x00000754
			[MonoTODO]
			public DataBindingPropertyDescriptor(PropertyDescriptor property, Attribute[] attrs, bool readOnly)
				: base(property.Name, attrs)
			{
				this._readOnly = readOnly;
			}

			// Token: 0x0600006D RID: 109 RVA: 0x0000256A File Offset: 0x0000076A
			[MonoTODO]
			public override object GetValue(object component)
			{
				return null;
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00002432 File Offset: 0x00000632
			[MonoTODO]
			public override void SetValue(object component, object value)
			{
			}

			// Token: 0x0600006F RID: 111 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public override void ResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000070 RID: 112 RVA: 0x0000241E File Offset: 0x0000061E
			[MonoTODO]
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x06000071 RID: 113 RVA: 0x0000241E File Offset: 0x0000061E
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000072 RID: 114 RVA: 0x0000256D File Offset: 0x0000076D
			[MonoTODO]
			public override Type PropertyType
			{
				get
				{
					return typeof(ControlBindingsConverter.DataBindingPropertyDescriptor);
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000073 RID: 115 RVA: 0x0000256A File Offset: 0x0000076A
			[MonoTODO]
			public override TypeConverter Converter
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000074 RID: 116 RVA: 0x00002579 File Offset: 0x00000779
			public override Type ComponentType
			{
				get
				{
					return typeof(ControlBindingsCollection);
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000075 RID: 117 RVA: 0x00002585 File Offset: 0x00000785
			public override bool IsReadOnly
			{
				get
				{
					return this._readOnly;
				}
			}

			// Token: 0x0400001E RID: 30
			private bool _readOnly;
		}
	}
}
