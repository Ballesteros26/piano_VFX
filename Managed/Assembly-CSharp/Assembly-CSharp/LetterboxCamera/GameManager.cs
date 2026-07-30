using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000058 RID: 88
	public class GameManager : MonoBehaviour
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00013E17 File Offset: 0x00012017
		private void Start()
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00014C44 File Offset: 0x00012E44
		private void Update()
		{
			if (this.player.GetMeat() == this.meatToWin || this.player.GetFruit() == this.fruitToWin)
			{
				Debug.Log(string.Concat(new object[]
				{
					"You've Won Meat ",
					this.player.GetMeat(),
					" Fruit ",
					this.player.GetFruit()
				}));
			}
		}

		// Token: 0x0400040D RID: 1037
		public DragonClass player;

		// Token: 0x0400040E RID: 1038
		public int meatToWin = 3;

		// Token: 0x0400040F RID: 1039
		public int fruitToWin = 3;
	}
}
