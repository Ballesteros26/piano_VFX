using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F7 RID: 503
	public class UxmlChildElementDescription
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x000387CC File Offset: 0x000369CC
		public UxmlChildElementDescription(Type t)
		{
			bool flag = t == null;
			if (flag)
			{
				throw new ArgumentNullException("t");
			}
			this.elementName = t.Name;
			this.elementNamespace = t.Namespace;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003880F File Offset: 0x00036A0F
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x00038817 File Offset: 0x00036A17
		public string elementName { get; protected set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00038820 File Offset: 0x00036A20
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x00038828 File Offset: 0x00036A28
		public string elementNamespace { get; protected set; }
	}
}
