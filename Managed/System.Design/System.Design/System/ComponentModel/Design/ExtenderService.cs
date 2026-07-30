using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x02000126 RID: 294
	internal sealed class ExtenderService : IExtenderProviderService, IExtenderListService, IDisposable
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		public ExtenderService()
		{
			this._extenderProviders = new ArrayList();
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000F0F7 File Offset: 0x0000D2F7
		public void AddExtenderProvider(IExtenderProvider provider)
		{
			if (this._extenderProviders != null && !this._extenderProviders.Contains(provider))
			{
				this._extenderProviders.Add(provider);
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0000F11C File Offset: 0x0000D31C
		public void RemoveExtenderProvider(IExtenderProvider provider)
		{
			if (this._extenderProviders != null && this._extenderProviders.Contains(provider))
			{
				this._extenderProviders.Remove(provider);
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000F140 File Offset: 0x0000D340
		public IExtenderProvider[] GetExtenderProviders()
		{
			if (this._extenderProviders != null)
			{
				IExtenderProvider[] array = new IExtenderProvider[this._extenderProviders.Count];
				this._extenderProviders.CopyTo(array, 0);
				return array;
			}
			return null;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000F176 File Offset: 0x0000D376
		public void Dispose()
		{
			if (this._extenderProviders != null)
			{
				this._extenderProviders.Clear();
				this._extenderProviders = null;
			}
		}

		// Token: 0x040001F5 RID: 501
		private ArrayList _extenderProviders;
	}
}
