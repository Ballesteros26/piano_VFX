using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x0200005E RID: 94
	public class LoopingTerrain : MonoBehaviour
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00013E17 File Offset: 0x00012017
		private void Start()
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000152D8 File Offset: 0x000134D8
		private void Update()
		{
			Vector3 position = this.upperTerrainDummy.transform.position;
			if (this.lowerTerrainDummy.transform.position.x <= position.x)
			{
				base.gameObject.transform.position = base.gameObject.transform.position + this.jumpPosition;
			}
		}

		// Token: 0x04000425 RID: 1061
		public GameObject upperTerrainDummy;

		// Token: 0x04000426 RID: 1062
		public GameObject lowerTerrainDummy;

		// Token: 0x04000427 RID: 1063
		public Vector3 jumpPosition;
	}
}
