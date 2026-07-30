using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000620 RID: 1568
	internal class BuilderLocation
	{
		// Token: 0x06004345 RID: 17221 RVA: 0x000B3B58 File Offset: 0x000B1D58
		public BuilderLocation(ControlBuilder builder, ILocation location)
		{
			this.Builder = builder;
			this.Location = new Location(location);
		}

		// Token: 0x040023FC RID: 9212
		public ControlBuilder Builder;

		// Token: 0x040023FD RID: 9213
		public ILocation Location;
	}
}
