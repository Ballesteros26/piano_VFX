using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005FF RID: 1535
	internal class Result : CapabilitiesResult
	{
		// Token: 0x06004292 RID: 17042 RVA: 0x000AFA0C File Offset: 0x000ADC0C
		internal Result(IDictionary items)
			: base(items)
		{
			this.AdapterTypeMap = new Dictionary<Type, Type>();
			this.Track = new StringCollection();
			this.MarkupTextWriter = typeof(HtmlTextWriter);
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x000AFA3B File Offset: 0x000ADC3B
		internal void AddTrack(string track)
		{
			this.Track.Add(track);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x000AFA4A File Offset: 0x000ADC4A
		internal void AddAdapter(Type controlType, Type adapterType)
		{
			this.AdapterTypeMap[controlType] = adapterType;
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000AFA59 File Offset: 0x000ADC59
		public StringCollection Tracks
		{
			get
			{
				return this.Track;
			}
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x000AFA61 File Offset: 0x000ADC61
		internal override Type GetTagWriter()
		{
			return this.MarkupTextWriter;
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x000AFA69 File Offset: 0x000ADC69
		internal override IDictionary GetAdapters()
		{
			return this.AdapterTypeMap;
		}

		// Token: 0x040023A5 RID: 9125
		private Dictionary<Type, Type> AdapterTypeMap;

		// Token: 0x040023A6 RID: 9126
		private StringCollection Track;

		// Token: 0x040023A7 RID: 9127
		internal Type MarkupTextWriter;
	}
}
