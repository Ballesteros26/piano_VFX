using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x0200005A RID: 90
	public class Parralax : MonoBehaviour
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00014E10 File Offset: 0x00013010
		private void Start()
		{
			this.camLastPos = this.camToParralaxAgainst.transform.position;
			this.parralaxMagnitude = global::UnityEngine.Random.Range(this.movePercentRange.x, this.movePercentRange.y);
			float num = this.baseZ - this.parralaxMagnitude;
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, num);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00014E94 File Offset: 0x00013094
		private void Update()
		{
			if (this.ignoreThisFrame)
			{
				this.ignoreThisFrame = false;
				this.camLastPos = this.camToParralaxAgainst.transform.position;
				return;
			}
			Vector3 vector = this.camToParralaxAgainst.transform.position - this.camLastPos;
			vector *= this.parralaxMagnitude;
			base.transform.position = base.transform.position + new Vector3(vector.x, vector.y, 0f);
			this.camLastPos = this.camToParralaxAgainst.transform.position;
			if (base.transform.position.x < this.camToParralaxAgainst.transform.position.x - this.maxDistanceToCam)
			{
				this.ResetParralaxObject();
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00014F6C File Offset: 0x0001316C
		private void ResetParralaxObject()
		{
			this.parralaxMagnitude = global::UnityEngine.Random.Range(this.movePercentRange.x, this.movePercentRange.y);
			float num = this.baseZ - this.parralaxMagnitude;
			base.transform.position = new Vector3(this.camToParralaxAgainst.transform.position.x + this.maxDistanceToCam, base.transform.position.y, num);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00014FE5 File Offset: 0x000131E5
		public void IgnoreNextFrame()
		{
			this.ignoreThisFrame = true;
		}

		// Token: 0x04000416 RID: 1046
		public Transform camToParralaxAgainst;

		// Token: 0x04000417 RID: 1047
		public Vector2 movePercentRange;

		// Token: 0x04000418 RID: 1048
		public float baseZ = 50f;

		// Token: 0x04000419 RID: 1049
		protected float maxDistanceToCam = 14f;

		// Token: 0x0400041A RID: 1050
		private bool ignoreThisFrame;

		// Token: 0x0400041B RID: 1051
		private float parralaxMagnitude;

		// Token: 0x0400041C RID: 1052
		private Vector3 camLastPos;
	}
}
