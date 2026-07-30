using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Resources
{
	// Token: 0x020002A4 RID: 676
	internal class ResourceFallbackManager : IEnumerable<CultureInfo>, IEnumerable
	{
		// Token: 0x06001F0A RID: 7946 RVA: 0x000789B1 File Offset: 0x00076BB1
		internal ResourceFallbackManager(CultureInfo startingCulture, CultureInfo neutralResourcesCulture, bool useParents)
		{
			if (startingCulture != null)
			{
				this.m_startingCulture = startingCulture;
			}
			else
			{
				this.m_startingCulture = CultureInfo.CurrentUICulture;
			}
			this.m_neutralResourcesCulture = neutralResourcesCulture;
			this.m_useParents = useParents;
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000789DE File Offset: 0x00076BDE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000789E6 File Offset: 0x00076BE6
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			bool reachedNeutralResourcesCulture = false;
			CultureInfo currentCulture = this.m_startingCulture;
			while (this.m_neutralResourcesCulture == null || !(currentCulture.Name == this.m_neutralResourcesCulture.Name))
			{
				yield return currentCulture;
				currentCulture = currentCulture.Parent;
				if (!this.m_useParents || currentCulture.HasInvariantCultureName)
				{
					IL_00CE:
					if (!this.m_useParents || this.m_startingCulture.HasInvariantCultureName)
					{
						yield break;
					}
					if (reachedNeutralResourcesCulture)
					{
						yield break;
					}
					yield return CultureInfo.InvariantCulture;
					yield break;
				}
			}
			yield return CultureInfo.InvariantCulture;
			reachedNeutralResourcesCulture = true;
			goto IL_00CE;
		}

		// Token: 0x040010BE RID: 4286
		private CultureInfo m_startingCulture;

		// Token: 0x040010BF RID: 4287
		private CultureInfo m_neutralResourcesCulture;

		// Token: 0x040010C0 RID: 4288
		private bool m_useParents;
	}
}
