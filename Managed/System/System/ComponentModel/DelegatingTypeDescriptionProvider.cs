using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200025C RID: 604
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class DelegatingTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x0005148B File Offset: 0x0004F68B
		internal DelegatingTypeDescriptionProvider(Type type)
		{
			this._type = type;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0005149A File Offset: 0x0004F69A
		internal TypeDescriptionProvider Provider
		{
			get
			{
				return TypeDescriptor.GetProviderRecursive(this._type);
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000514A7 File Offset: 0x0004F6A7
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			return this.Provider.CreateInstance(provider, objectType, argTypes, args);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000514B9 File Offset: 0x0004F6B9
		public override IDictionary GetCache(object instance)
		{
			return this.Provider.GetCache(instance);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000514C7 File Offset: 0x0004F6C7
		public override string GetFullComponentName(object component)
		{
			return this.Provider.GetFullComponentName(component);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000514D5 File Offset: 0x0004F6D5
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return this.Provider.GetExtendedTypeDescriptor(instance);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000514E3 File Offset: 0x0004F6E3
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			return this.Provider.GetExtenderProviders(instance);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000514F1 File Offset: 0x0004F6F1
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return this.Provider.GetReflectionType(objectType, instance);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00051500 File Offset: 0x0004F700
		public override Type GetRuntimeType(Type objectType)
		{
			return this.Provider.GetRuntimeType(objectType);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0005150E File Offset: 0x0004F70E
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return this.Provider.GetTypeDescriptor(objectType, instance);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0005151D File Offset: 0x0004F71D
		public override bool IsSupportedType(Type type)
		{
			return this.Provider.IsSupportedType(type);
		}

		// Token: 0x040012AD RID: 4781
		private Type _type;
	}
}
