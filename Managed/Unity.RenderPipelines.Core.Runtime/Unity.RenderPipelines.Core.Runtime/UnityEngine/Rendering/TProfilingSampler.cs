using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003A RID: 58
	internal class TProfilingSampler<TEnum> : ProfilingSampler where TEnum : Enum
	{
		// Token: 0x06000167 RID: 359 RVA: 0x000077C8 File Offset: 0x000059C8
		static TProfilingSampler()
		{
			string[] names = Enum.GetNames(typeof(TEnum));
			Array values = Enum.GetValues(typeof(TEnum));
			for (int i = 0; i < names.Length; i++)
			{
				TProfilingSampler<TEnum> tprofilingSampler = new TProfilingSampler<TEnum>(names[i]);
				TProfilingSampler<TEnum>.samples.Add((TEnum)((object)values.GetValue(i)), tprofilingSampler);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000782D File Offset: 0x00005A2D
		public TProfilingSampler(string name)
			: base(name)
		{
		}

		// Token: 0x040000FE RID: 254
		internal static Dictionary<TEnum, TProfilingSampler<TEnum>> samples = new Dictionary<TEnum, TProfilingSampler<TEnum>>();
	}
}
