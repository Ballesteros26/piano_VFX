using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000055 RID: 85
	public class EnemyClass : MonoBehaviour
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00014917 File Offset: 0x00012B17
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0001491F File Offset: 0x00012B1F
		public Rigidbody2D rigid { get; private set; }

		// Token: 0x060002C1 RID: 705 RVA: 0x00014928 File Offset: 0x00012B28
		private void Start()
		{
			this.rigid = base.GetComponent<Rigidbody2D>();
			this.hitAudio = base.GetComponent<AudioSource>();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00014942 File Offset: 0x00012B42
		private void OnMouseDown()
		{
			this.beingDragged = true;
			this.EnableGravity();
			this.PlayHitAudio(1.5f, 2.5f);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00014961 File Offset: 0x00012B61
		private void OnMouseUp()
		{
			if (this.rigid != null)
			{
				this.rigid.velocity = Vector3.zero;
				this.rigid.angularVelocity = 0f;
			}
			this.beingDragged = false;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000149A0 File Offset: 0x00012BA0
		private void OnCollisionEnter2D(Collision2D coll)
		{
			this.EnableGravity();
			if (this.rigid != null && this.rigid.velocity.sqrMagnitude > 1f)
			{
				this.PlayHitAudio(0.5f, 1.5f);
			}
			DragonClass component = coll.gameObject.GetComponent<DragonClass>();
			if (component != null)
			{
				component.ApplyScore(this.nutrition, this.diet);
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00014A1D File Offset: 0x00012C1D
		private void PlayHitAudio(float _lowPitch, float _highPitch)
		{
			if (this.hitAudio != null)
			{
				this.hitAudio.pitch = global::UnityEngine.Random.Range(_lowPitch, _highPitch);
				this.hitAudio.Play();
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00014A4A File Offset: 0x00012C4A
		private void EnableGravity()
		{
			if (this.rigid != null)
			{
				this.rigid.gravityScale = 1f;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00014A6C File Offset: 0x00012C6C
		private void Update()
		{
			if (this.beingDragged)
			{
				Vector3 vector = Input.mousePosition;
				vector.z = -Camera.main.gameObject.transform.position.z;
				vector = Camera.main.ScreenToWorldPoint(vector);
				base.gameObject.transform.position = vector;
			}
		}

		// Token: 0x04000401 RID: 1025
		public int nutrition;

		// Token: 0x04000402 RID: 1026
		public Feed diet;

		// Token: 0x04000403 RID: 1027
		private AudioSource hitAudio;

		// Token: 0x04000404 RID: 1028
		private bool beingDragged;
	}
}
