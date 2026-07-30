using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x0200005C RID: 92
	public class PlayerScrollScript : MonoBehaviour
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00013E17 File Offset: 0x00012017
		private void Start()
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001515C File Offset: 0x0001335C
		private void FixedUpdate()
		{
			Vector3 vector = new Vector3(1f, 0f, 0f);
			vector = this.speed * vector;
			base.gameObject.transform.position = base.gameObject.transform.position + vector;
		}

		// Token: 0x04000422 RID: 1058
		public float speed = 1f;
	}
}
