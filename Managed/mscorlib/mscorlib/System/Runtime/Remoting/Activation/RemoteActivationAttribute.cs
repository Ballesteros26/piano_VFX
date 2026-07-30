using System;
using System.Collections;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020007C2 RID: 1986
	internal class RemoteActivationAttribute : Attribute, IContextAttribute
	{
		// Token: 0x06005038 RID: 20536 RVA: 0x00002180 File Offset: 0x00000380
		public RemoteActivationAttribute()
		{
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0011F5C0 File Offset: 0x0011D7C0
		public RemoteActivationAttribute(IList contextProperties)
		{
			this._contextProperties = contextProperties;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsContextOK(Context ctx, IConstructionCallMessage ctor)
		{
			return false;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0011F5D0 File Offset: 0x0011D7D0
		public void GetPropertiesForNewContext(IConstructionCallMessage ctor)
		{
			if (this._contextProperties != null)
			{
				foreach (object obj in this._contextProperties)
				{
					ctor.ContextProperties.Add(obj);
				}
			}
		}

		// Token: 0x04002A72 RID: 10866
		private IList _contextProperties;
	}
}
