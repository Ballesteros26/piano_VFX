using System;
using Unity;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates the data that makes up a <see cref="T:System.Drawing.Region" /> object. This class cannot be inherited.</summary>
	// Token: 0x0200014A RID: 330
	public sealed class RegionData
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x0001EFFD File Offset: 0x0001D1FD
		internal RegionData(byte[] data)
		{
			this.Data = data;
		}

		/// <summary>Gets or sets an array of bytes that specify the <see cref="T:System.Drawing.Region" /> object.</summary>
		/// <returns>An array of bytes that specify the <see cref="T:System.Drawing.Region" /> object.</returns>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0001F00C File Offset: 0x0001D20C
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x0001F014 File Offset: 0x0001D214
		public byte[] Data { get; set; }

		// Token: 0x06000E1B RID: 3611 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal RegionData()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
