using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x0200004A RID: 74
	internal class InternalConfigurationSystem : IConfigSystem
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00007D27 File Offset: 0x00005F27
		public void Init(Type typeConfigHost, params object[] hostInitParams)
		{
			this.hostInitParams = hostInitParams;
			this.host = (IInternalConfigHost)Activator.CreateInstance(typeConfigHost);
			this.root = new InternalConfigurationRoot();
			this.root.Init(this.host, false);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007D5E File Offset: 0x00005F5E
		public void InitForConfiguration(ref string locationConfigPath, out string parentConfigPath, out string parentLocationConfigPath)
		{
			this.host.InitForConfiguration(ref locationConfigPath, out parentConfigPath, out parentLocationConfigPath, this.root, this.hostInitParams);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00007D7A File Offset: 0x00005F7A
		public IInternalConfigHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00007D82 File Offset: 0x00005F82
		public IInternalConfigRoot Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x040000F5 RID: 245
		private IInternalConfigHost host;

		// Token: 0x040000F6 RID: 246
		private IInternalConfigRoot root;

		// Token: 0x040000F7 RID: 247
		private object[] hostInitParams;
	}
}
