using System;

namespace System.Xml.Schema
{
	// Token: 0x02000397 RID: 919
	internal class UpaException : Exception
	{
		// Token: 0x06002511 RID: 9489 RVA: 0x000E0018 File Offset: 0x000DE218
		public UpaException(object particle1, object particle2)
		{
			this.particle1 = particle1;
			this.particle2 = particle2;
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x000E002E File Offset: 0x000DE22E
		public object Particle1
		{
			get
			{
				return this.particle1;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x000E0036 File Offset: 0x000DE236
		public object Particle2
		{
			get
			{
				return this.particle2;
			}
		}

		// Token: 0x04001920 RID: 6432
		private object particle1;

		// Token: 0x04001921 RID: 6433
		private object particle2;
	}
}
