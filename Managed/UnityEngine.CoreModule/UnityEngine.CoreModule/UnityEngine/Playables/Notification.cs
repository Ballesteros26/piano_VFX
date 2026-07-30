using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200039A RID: 922
	public class Notification : INotification
	{
		// Token: 0x06001FFE RID: 8190 RVA: 0x000365AF File Offset: 0x000347AF
		public Notification(string name)
		{
			this.id = new PropertyName(name);
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x000365C5 File Offset: 0x000347C5
		public PropertyName id { get; }
	}
}
