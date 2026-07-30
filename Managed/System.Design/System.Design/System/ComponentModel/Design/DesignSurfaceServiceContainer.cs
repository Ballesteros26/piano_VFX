using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x0200010C RID: 268
	internal sealed class DesignSurfaceServiceContainer : ServiceContainer
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x0000D56B File Offset: 0x0000B76B
		public DesignSurfaceServiceContainer()
			: this(null)
		{
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000D574 File Offset: 0x0000B774
		public DesignSurfaceServiceContainer(IServiceProvider parentProvider)
			: base(parentProvider)
		{
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0000D57D File Offset: 0x0000B77D
		internal void AddNonReplaceableService(Type serviceType, object instance)
		{
			if (this._nonRemoveableServices == null)
			{
				this._nonRemoveableServices = new Hashtable();
			}
			this._nonRemoveableServices[serviceType] = serviceType;
			base.AddService(serviceType, instance);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0000D5A7 File Offset: 0x0000B7A7
		internal void RemoveNonReplaceableService(Type serviceType, object instance)
		{
			if (this._nonRemoveableServices != null)
			{
				this._nonRemoveableServices.Remove(serviceType);
			}
			base.RemoveService(serviceType);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public override void RemoveService(Type serviceType, bool promote)
		{
			if (serviceType != null && this._nonRemoveableServices != null && this._nonRemoveableServices.ContainsKey(serviceType))
			{
				throw new InvalidOperationException("Cannot remove non-replaceable service: " + serviceType.AssemblyQualifiedName);
			}
			base.RemoveService(serviceType, promote);
		}

		// Token: 0x040001AB RID: 427
		private Hashtable _nonRemoveableServices;
	}
}
