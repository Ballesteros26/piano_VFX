using System;
using Unity;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents the state of a <see cref="T:System.Drawing.Graphics" /> object. This object is returned by a call to the <see cref="M:System.Drawing.Graphics.Save" /> methods. This class cannot be inherited.</summary>
	// Token: 0x0200013C RID: 316
	public sealed class GraphicsState : MarshalByRefObject
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x0001EE90 File Offset: 0x0001D090
		internal GraphicsState(int nativeState)
		{
			this.nativeState = nativeState;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal GraphicsState()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AC4 RID: 2756
		internal int nativeState;
	}
}
