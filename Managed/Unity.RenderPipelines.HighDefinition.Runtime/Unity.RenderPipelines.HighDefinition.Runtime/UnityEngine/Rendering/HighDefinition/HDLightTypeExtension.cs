using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000054 RID: 84
	public static class HDLightTypeExtension
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000EE8E File Offset: 0x0000D08E
		public static bool IsSpot(this HDLightTypeAndShape type)
		{
			return type == HDLightTypeAndShape.BoxSpot || type == HDLightTypeAndShape.PyramidSpot || type == HDLightTypeAndShape.ConeSpot;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000EE9E File Offset: 0x0000D09E
		public static bool IsArea(this HDLightTypeAndShape type)
		{
			return type == HDLightTypeAndShape.TubeArea || type == HDLightTypeAndShape.RectangleArea || type == HDLightTypeAndShape.DiscArea;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000EEAE File Offset: 0x0000D0AE
		public static bool SupportsRuntimeOnly(this HDLightTypeAndShape type)
		{
			return type != HDLightTypeAndShape.DiscArea;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000EEB7 File Offset: 0x0000D0B7
		public static bool SupportsBakedOnly(this HDLightTypeAndShape type)
		{
			return type != HDLightTypeAndShape.TubeArea;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		public static bool SupportsMixed(this HDLightTypeAndShape type)
		{
			return type != HDLightTypeAndShape.TubeArea && type != HDLightTypeAndShape.DiscArea;
		}
	}
}
