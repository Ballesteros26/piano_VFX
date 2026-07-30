using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000271 RID: 625
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ExtendedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06001406 RID: 5126 RVA: 0x00052A08 File Offset: 0x00050C08
		public ExtendedPropertyDescriptor(ReflectPropertyDescriptor extenderInfo, Type receiverType, IExtenderProvider provider, Attribute[] attributes)
			: base(extenderInfo, attributes)
		{
			ArrayList arrayList = new ArrayList(this.AttributeArray);
			arrayList.Add(ExtenderProvidedPropertyAttribute.Create(extenderInfo, receiverType, provider));
			if (extenderInfo.IsReadOnly)
			{
				arrayList.Add(ReadOnlyAttribute.Yes);
			}
			Attribute[] array = new Attribute[arrayList.Count];
			arrayList.CopyTo(array, 0);
			this.AttributeArray = array;
			this.extenderInfo = extenderInfo;
			this.provider = provider;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00052A78 File Offset: 0x00050C78
		public ExtendedPropertyDescriptor(PropertyDescriptor extender, Attribute[] attributes)
			: base(extender, attributes)
		{
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = extender.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
			ReflectPropertyDescriptor reflectPropertyDescriptor = extenderProvidedPropertyAttribute.ExtenderProperty as ReflectPropertyDescriptor;
			this.extenderInfo = reflectPropertyDescriptor;
			this.provider = extenderProvidedPropertyAttribute.Provider;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00052AC7 File Offset: 0x00050CC7
		public override bool CanResetValue(object comp)
		{
			return this.extenderInfo.ExtenderCanResetValue(this.provider, comp);
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00052ADB File Offset: 0x00050CDB
		public override Type ComponentType
		{
			get
			{
				return this.extenderInfo.ComponentType;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00052AE8 File Offset: 0x00050CE8
		public override bool IsReadOnly
		{
			get
			{
				return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00052B09 File Offset: 0x00050D09
		public override Type PropertyType
		{
			get
			{
				return this.extenderInfo.ExtenderGetType(this.provider);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00052B1C File Offset: 0x00050D1C
		public override string DisplayName
		{
			get
			{
				string text = base.DisplayName;
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					ISite site = MemberDescriptor.GetSite(this.provider);
					if (site != null)
					{
						string name = site.Name;
						if (name != null && name.Length > 0)
						{
							text = global::SR.GetString("{0} on {1}", new object[] { text, name });
						}
					}
				}
				return text;
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00052B92 File Offset: 0x00050D92
		public override object GetValue(object comp)
		{
			return this.extenderInfo.ExtenderGetValue(this.provider, comp);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00052BA6 File Offset: 0x00050DA6
		public override void ResetValue(object comp)
		{
			this.extenderInfo.ExtenderResetValue(this.provider, comp, this);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00052BBB File Offset: 0x00050DBB
		public override void SetValue(object component, object value)
		{
			this.extenderInfo.ExtenderSetValue(this.provider, component, value, this);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00052BD1 File Offset: 0x00050DD1
		public override bool ShouldSerializeValue(object comp)
		{
			return this.extenderInfo.ExtenderShouldSerializeValue(this.provider, comp);
		}

		// Token: 0x040012E5 RID: 4837
		private readonly ReflectPropertyDescriptor extenderInfo;

		// Token: 0x040012E6 RID: 4838
		private readonly IExtenderProvider provider;
	}
}
