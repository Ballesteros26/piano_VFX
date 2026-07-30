using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x0200004E RID: 78
	internal class InternalConfigurationRoot : IInternalConfigRoot
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00003727 File Offset: 0x00001927
		[MonoTODO]
		public IInternalConfigRecord GetConfigRecord(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000822F File Offset: 0x0000642F
		public object GetSection(string section, string configPath)
		{
			return this.GetConfigRecord(configPath).GetSection(section);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007D24 File Offset: 0x00005F24
		[MonoTODO]
		public string GetUniqueConfigPath(string configPath)
		{
			return configPath;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000823E File Offset: 0x0000643E
		[MonoTODO]
		public IInternalConfigRecord GetUniqueConfigRecord(string configPath)
		{
			return this.GetConfigRecord(this.GetUniqueConfigPath(configPath));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000824D File Offset: 0x0000644D
		public void Init(IInternalConfigHost host, bool isDesignTime)
		{
			this.host = host;
			this.isDesignTime = isDesignTime;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000825D File Offset: 0x0000645D
		[MonoTODO]
		public void RemoveConfig(string configPath)
		{
			this.host.DeleteStream(configPath);
			if (this.ConfigRemoved != null)
			{
				this.ConfigRemoved(this, new InternalConfigEventArgs(configPath));
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008285 File Offset: 0x00006485
		public bool IsDesignTime
		{
			get
			{
				return this.isDesignTime;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002A6 RID: 678 RVA: 0x00008290 File Offset: 0x00006490
		// (remove) Token: 0x060002A7 RID: 679 RVA: 0x000082C8 File Offset: 0x000064C8
		public event InternalConfigEventHandler ConfigChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002A8 RID: 680 RVA: 0x00008300 File Offset: 0x00006500
		// (remove) Token: 0x060002A9 RID: 681 RVA: 0x00008338 File Offset: 0x00006538
		public event InternalConfigEventHandler ConfigRemoved;

		// Token: 0x040000FB RID: 251
		private IInternalConfigHost host;

		// Token: 0x040000FC RID: 252
		private bool isDesignTime;
	}
}
