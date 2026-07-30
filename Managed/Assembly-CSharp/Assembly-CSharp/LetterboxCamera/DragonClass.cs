using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000054 RID: 84
	public class DragonClass : MonoBehaviour
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x00014885 File Offset: 0x00012A85
		public int GetMeat()
		{
			return this.meat;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001488D File Offset: 0x00012A8D
		public int GetFruit()
		{
			return this.fruit;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00014898 File Offset: 0x00012A98
		public void ApplyScore(int IncomingScore, Feed Diet)
		{
			this.nutritionalValue += IncomingScore;
			if (Diet == Feed.Meat)
			{
				this.meat++;
			}
			if (Diet == Feed.Fruit)
			{
				this.fruit++;
			}
			if (this.munchAudio != null)
			{
				this.munchAudio.pitch = global::UnityEngine.Random.Range(2.5f, 3.5f);
				this.munchAudio.Play();
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00014909 File Offset: 0x00012B09
		private void Start()
		{
			this.munchAudio = base.GetComponent<AudioSource>();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00013E17 File Offset: 0x00012017
		private void Update()
		{
		}

		// Token: 0x040003FD RID: 1021
		private AudioSource munchAudio;

		// Token: 0x040003FE RID: 1022
		private int meat;

		// Token: 0x040003FF RID: 1023
		private int fruit;

		// Token: 0x04000400 RID: 1024
		private int nutritionalValue;
	}
}
