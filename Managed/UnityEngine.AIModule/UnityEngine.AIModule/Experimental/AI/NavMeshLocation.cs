using System;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x0200001D RID: 29
	public struct NavMeshLocation
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000031A7 File Offset: 0x000013A7
		public PolygonId polygon { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000031AF File Offset: 0x000013AF
		public Vector3 position { get; }

		// Token: 0x0600016F RID: 367 RVA: 0x000031B7 File Offset: 0x000013B7
		internal NavMeshLocation(Vector3 position, PolygonId polygon)
		{
			this.position = position;
			this.polygon = polygon;
		}
	}
}
