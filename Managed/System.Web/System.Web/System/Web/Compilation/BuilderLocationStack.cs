using System;
using System.Collections;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000621 RID: 1569
	internal class BuilderLocationStack : Stack
	{
		// Token: 0x06004346 RID: 17222 RVA: 0x000B3B73 File Offset: 0x000B1D73
		public override void Push(object o)
		{
			if (!(o is BuilderLocation))
			{
				throw new InvalidOperationException();
			}
			base.Push(o);
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x000B3B8C File Offset: 0x000B1D8C
		public virtual void Push(ControlBuilder builder, ILocation location)
		{
			BuilderLocation builderLocation = new BuilderLocation(builder, location);
			this.Push(builderLocation);
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000B3BA8 File Offset: 0x000B1DA8
		public new BuilderLocation Peek()
		{
			return (BuilderLocation)base.Peek();
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000B3BB5 File Offset: 0x000B1DB5
		public new BuilderLocation Pop()
		{
			return (BuilderLocation)base.Pop();
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x000B3BC2 File Offset: 0x000B1DC2
		public ControlBuilder Builder
		{
			get
			{
				return this.Peek().Builder;
			}
		}
	}
}
