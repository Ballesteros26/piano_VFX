using System;

namespace System.Web.Util
{
	// Token: 0x0200014A RID: 330
	internal sealed class SimpleWebObjectFactory : IWebObjectFactory
	{
		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002A758 File Offset: 0x00028958
		public SimpleWebObjectFactory(Type type)
		{
			this.type = type;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0002A767 File Offset: 0x00028967
		public object CreateInstance()
		{
			if (this.type == null)
			{
				return null;
			}
			return Activator.CreateInstance(this.type);
		}

		// Token: 0x04001220 RID: 4640
		private Type type;
	}
}
