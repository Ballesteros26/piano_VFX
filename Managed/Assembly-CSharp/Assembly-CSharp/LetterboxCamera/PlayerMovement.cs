using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x0200005B RID: 91
	public class PlayerMovement : MonoBehaviour
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0001500C File Offset: 0x0001320C
		private void Awake()
		{
			this.rigid = base.GetComponent<Rigidbody2D>();
			if (this.rigid == null)
			{
				Debug.Log("Warning: There is no Rigidbody2D on the Player!");
			}
			if (this.feetMarker == null)
			{
				Debug.Log("Warning: Feet have not been set on the Player so we cannot jump!");
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001504C File Offset: 0x0001324C
		private void FixedUpdate()
		{
			Vector3 vector = this.rigid.velocity;
			float axis = Input.GetAxis("Horizontal");
			if (this.grounded && Input.GetButton("Jump"))
			{
				vector.y = this.jumpSpeed;
			}
			vector.x = this.runSpeed * axis;
			this.rigid.velocity = vector;
			this.grounded = false;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000150C0 File Offset: 0x000132C0
		private void OnCollisionStay2D(Collision2D collision)
		{
			for (int i = 0; i < collision.contacts.Length; i++)
			{
				if (collision.contacts[i].point.y < this.feetMarker.position.y)
				{
					this.grounded = true;
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001510F File Offset: 0x0001330F
		public void OnDrawGizmos()
		{
			if (this.feetMarker != null)
			{
				Gizmos.color = Color.magenta;
				Gizmos.DrawSphere(this.feetMarker.position, 0.1f);
			}
		}

		// Token: 0x0400041D RID: 1053
		public float runSpeed = 7.5f;

		// Token: 0x0400041E RID: 1054
		public float jumpSpeed = 5f;

		// Token: 0x0400041F RID: 1055
		public Transform feetMarker;

		// Token: 0x04000420 RID: 1056
		private Rigidbody2D rigid;

		// Token: 0x04000421 RID: 1057
		private bool grounded;
	}
}
