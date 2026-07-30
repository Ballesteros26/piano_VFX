using System;
using Unity;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents the internal data of a graphics container. This class is used when saving the state of a <see cref="T:System.Drawing.Graphics" /> object using the <see cref="M:System.Drawing.Graphics.BeginContainer" /> and <see cref="M:System.Drawing.Graphics.EndContainer(System.Drawing.Drawing2D.GraphicsContainer)" /> methods. This class cannot be inherited.</summary>
	// Token: 0x0200014F RID: 335
	public sealed class GraphicsContainer : MarshalByRefObject
	{
		// Token: 0x06000E21 RID: 3617 RVA: 0x0001F0C3 File Offset: 0x0001D2C3
		internal GraphicsContainer(uint state)
		{
			this.nativeState = state;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0001F0D2 File Offset: 0x0001D2D2
		internal uint NativeObject
		{
			get
			{
				return this.nativeState;
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal GraphicsContainer()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B55 RID: 2901
		private uint nativeState;
	}
}
