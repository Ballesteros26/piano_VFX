using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000063 RID: 99
	public class MenuSpin : MonoBehaviour
	{
		// Token: 0x060002FC RID: 764 RVA: 0x000165D8 File Offset: 0x000147D8
		private void Start()
		{
			switch (this.axisRotation)
			{
			case rotAxis.xAxis:
				this.rotationVector = Vector3.right;
				break;
			case rotAxis.yAxis:
				this.rotationVector = Vector3.up;
				break;
			case rotAxis.zAxis:
				this.rotationVector = Vector3.back;
				break;
			}
			if (this.spinDirection == MenuSpin.direc.random)
			{
				if (global::UnityEngine.Random.Range(0, 99) <= 49)
				{
					this.spinDirection = MenuSpin.direc.clockwise;
					return;
				}
				this.spinDirection = MenuSpin.direc.counterclockwise;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001664C File Offset: 0x0001484C
		private void Update()
		{
			if (this.spinDirection == MenuSpin.direc.clockwise)
			{
				base.gameObject.transform.rotation *= Quaternion.AngleAxis(this.spinSpeed * Time.deltaTime, this.rotationVector);
				return;
			}
			base.gameObject.transform.rotation *= Quaternion.AngleAxis(this.spinSpeed * Time.deltaTime, -this.rotationVector);
		}

		// Token: 0x04000453 RID: 1107
		public float spinSpeed;

		// Token: 0x04000454 RID: 1108
		public MenuSpin.direc spinDirection;

		// Token: 0x04000455 RID: 1109
		public rotAxis axisRotation = rotAxis.yAxis;

		// Token: 0x04000456 RID: 1110
		private Vector3 rotationVector;

		// Token: 0x02000090 RID: 144
		public enum direc
		{
			// Token: 0x040004D3 RID: 1235
			clockwise,
			// Token: 0x040004D4 RID: 1236
			counterclockwise,
			// Token: 0x040004D5 RID: 1237
			random
		}
	}
}
