using System;
using UnityEngine;
using UnityEngine.UI;

namespace LetterboxCamera
{
	// Token: 0x02000057 RID: 87
	public class FoodTracker : MonoBehaviour
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00013E17 File Offset: 0x00012017
		private void Start()
		{
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00014BF8 File Offset: 0x00012DF8
		private void Update()
		{
			for (int i = 0; i < this.textPieces.Length; i++)
			{
				this.textPieces[i].text = (this.dragon.GetFruit() + this.dragon.GetMeat()).ToString();
			}
		}

		// Token: 0x0400040B RID: 1035
		public DragonClass dragon;

		// Token: 0x0400040C RID: 1036
		public Text[] textPieces;
	}
}
