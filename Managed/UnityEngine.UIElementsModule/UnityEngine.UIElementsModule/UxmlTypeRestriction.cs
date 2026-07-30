using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000202 RID: 514
	public abstract class UxmlTypeRestriction : IEquatable<UxmlTypeRestriction>
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x00039374 File Offset: 0x00037574
		public virtual bool Equals(UxmlTypeRestriction other)
		{
			return this == other;
		}
	}
}
