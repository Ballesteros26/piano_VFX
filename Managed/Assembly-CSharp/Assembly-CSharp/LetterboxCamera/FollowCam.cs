using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000056 RID: 86
	public class FollowCam : MonoBehaviour
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00014AC5 File Offset: 0x00012CC5
		private void Awake()
		{
			this.originLocalPosition = base.transform.localPosition;
			if (this.objectToFollow == null)
			{
				Debug.Log("Warning: There is no Object to follow on the Following Camera!");
				return;
			}
			this.targetLocalPosition = base.transform.localPosition;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00014B04 File Offset: 0x00012D04
		private void Update()
		{
			float axis = Input.GetAxis("Horizontal");
			if (axis > 0.05f)
			{
				this.targetLocalPosition = new Vector3(this.localDistanceAheadOfObject, this.originLocalPosition.y, this.originLocalPosition.z);
			}
			else if (axis < -0.05f)
			{
				this.targetLocalPosition = new Vector3(-this.localDistanceAheadOfObject, this.originLocalPosition.y, this.originLocalPosition.z);
			}
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.targetLocalPosition, this.followWeight);
			base.transform.parent.position = Vector3.Lerp(base.transform.parent.position, this.objectToFollow.position, this.followWeight);
		}

		// Token: 0x04000406 RID: 1030
		public Transform objectToFollow;

		// Token: 0x04000407 RID: 1031
		public float localDistanceAheadOfObject = 6f;

		// Token: 0x04000408 RID: 1032
		public float followWeight = 0.1f;

		// Token: 0x04000409 RID: 1033
		private Vector3 targetLocalPosition;

		// Token: 0x0400040A RID: 1034
		private Vector3 originLocalPosition;
	}
}
