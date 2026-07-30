using System;

namespace System.Diagnostics
{
	// Token: 0x020001B0 RID: 432
	internal class FilterElement : TypedElement
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x0003E014 File Offset: 0x0003C214
		public FilterElement()
			: base(typeof(TraceFilter))
		{
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0003E026 File Offset: 0x0003C226
		public TraceFilter GetRuntimeObject()
		{
			TraceFilter traceFilter = (TraceFilter)base.BaseGetRuntimeObject();
			traceFilter.initializeData = base.InitData;
			return traceFilter;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0003E03F File Offset: 0x0003C23F
		internal TraceFilter RefreshRuntimeObject(TraceFilter filter)
		{
			if (Type.GetType(this.TypeName) != filter.GetType() || base.InitData != filter.initializeData)
			{
				this._runtimeObject = null;
				return this.GetRuntimeObject();
			}
			return filter;
		}
	}
}
