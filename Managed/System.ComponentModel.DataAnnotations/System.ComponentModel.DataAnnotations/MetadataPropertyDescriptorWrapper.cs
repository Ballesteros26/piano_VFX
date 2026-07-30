using System;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
	// Token: 0x02000021 RID: 33
	internal class MetadataPropertyDescriptorWrapper : PropertyDescriptor
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003860 File Offset: 0x00001A60
		public MetadataPropertyDescriptorWrapper(PropertyDescriptor descriptor, Attribute[] newAttributes)
			: base(descriptor, newAttributes)
		{
			this._descriptor = descriptor;
			ReadOnlyAttribute readOnlyAttribute = newAttributes.OfType<ReadOnlyAttribute>().FirstOrDefault<ReadOnlyAttribute>();
			this._isReadOnly = readOnlyAttribute != null && readOnlyAttribute.IsReadOnly;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000389A File Offset: 0x00001A9A
		public override void AddValueChanged(object component, EventHandler handler)
		{
			this._descriptor.AddValueChanged(component, handler);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000038A9 File Offset: 0x00001AA9
		public override bool CanResetValue(object component)
		{
			return this._descriptor.CanResetValue(component);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000038B7 File Offset: 0x00001AB7
		public override Type ComponentType
		{
			get
			{
				return this._descriptor.ComponentType;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000038C4 File Offset: 0x00001AC4
		public override object GetValue(object component)
		{
			return this._descriptor.GetValue(component);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000038D2 File Offset: 0x00001AD2
		public override bool IsReadOnly
		{
			get
			{
				return this._isReadOnly || this._descriptor.IsReadOnly;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000038E9 File Offset: 0x00001AE9
		public override Type PropertyType
		{
			get
			{
				return this._descriptor.PropertyType;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000038F6 File Offset: 0x00001AF6
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			this._descriptor.RemoveValueChanged(component, handler);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003905 File Offset: 0x00001B05
		public override void ResetValue(object component)
		{
			this._descriptor.ResetValue(component);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003913 File Offset: 0x00001B13
		public override void SetValue(object component, object value)
		{
			this._descriptor.SetValue(component, value);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003922 File Offset: 0x00001B22
		public override bool ShouldSerializeValue(object component)
		{
			return this._descriptor.ShouldSerializeValue(component);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003930 File Offset: 0x00001B30
		public override bool SupportsChangeEvents
		{
			get
			{
				return this._descriptor.SupportsChangeEvents;
			}
		}

		// Token: 0x04000082 RID: 130
		private PropertyDescriptor _descriptor;

		// Token: 0x04000083 RID: 131
		private bool _isReadOnly;
	}
}
