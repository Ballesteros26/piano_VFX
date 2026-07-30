using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200035A RID: 858
	[UsedByNativeCode]
	internal struct CoreCameraValues : IEquatable<CoreCameraValues>
	{
		// Token: 0x06001D68 RID: 7528 RVA: 0x0003109C File Offset: 0x0002F29C
		public bool Equals(CoreCameraValues other)
		{
			return this.filterMode == other.filterMode && this.cullingMask == other.cullingMask && this.instanceID == other.instanceID && this.renderImmediateObjects == other.renderImmediateObjects;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000310EC File Offset: 0x0002F2EC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CoreCameraValues && this.Equals((CoreCameraValues)obj);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00031124 File Offset: 0x0002F324
		public override int GetHashCode()
		{
			int num = this.filterMode;
			num = (num * 397) ^ (int)this.cullingMask;
			num = (num * 397) ^ this.instanceID;
			return (num * 397) ^ this.renderImmediateObjects;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0003116C File Offset: 0x0002F36C
		public static bool operator ==(CoreCameraValues left, CoreCameraValues right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00031188 File Offset: 0x0002F388
		public static bool operator !=(CoreCameraValues left, CoreCameraValues right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000A3E RID: 2622
		private int filterMode;

		// Token: 0x04000A3F RID: 2623
		private uint cullingMask;

		// Token: 0x04000A40 RID: 2624
		private int instanceID;

		// Token: 0x04000A41 RID: 2625
		private int renderImmediateObjects;
	}
}
