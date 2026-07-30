using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001CE RID: 462
	internal class TypedElement : ConfigurationElement
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x0004307E File Offset: 0x0004127E
		public TypedElement(Type baseType)
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(TypedElement._propTypeName);
			this._properties.Add(TypedElement._propInitData);
			this._baseType = baseType;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x000430B8 File Offset: 0x000412B8
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x000430CA File Offset: 0x000412CA
		[ConfigurationProperty("initializeData", DefaultValue = "")]
		public string InitData
		{
			get
			{
				return (string)base[TypedElement._propInitData];
			}
			set
			{
				base[TypedElement._propInitData] = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000430D8 File Offset: 0x000412D8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x000430E0 File Offset: 0x000412E0
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x000430F2 File Offset: 0x000412F2
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public virtual string TypeName
		{
			get
			{
				return (string)base[TypedElement._propTypeName];
			}
			set
			{
				base[TypedElement._propTypeName] = value;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00043100 File Offset: 0x00041300
		protected object BaseGetRuntimeObject()
		{
			if (this._runtimeObject == null)
			{
				this._runtimeObject = TraceUtils.GetRuntimeObject(this.TypeName, this._baseType, this.InitData);
			}
			return this._runtimeObject;
		}

		// Token: 0x0400108E RID: 4238
		protected static readonly ConfigurationProperty _propTypeName = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x0400108F RID: 4239
		protected static readonly ConfigurationProperty _propInitData = new ConfigurationProperty("initializeData", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04001090 RID: 4240
		protected ConfigurationPropertyCollection _properties;

		// Token: 0x04001091 RID: 4241
		protected object _runtimeObject;

		// Token: 0x04001092 RID: 4242
		private Type _baseType;
	}
}
