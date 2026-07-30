using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class MoveTile : MonoBehaviour
{
	// Token: 0x06000097 RID: 151 RVA: 0x0000A1F8 File Offset: 0x000083F8
	private void Update()
	{
		if (this.play)
		{
			if (this.moveDown)
			{
				base.transform.Translate(Vector2.down * Time.deltaTime * this.speed);
			}
			else
			{
				base.transform.Translate(Vector2.down * Time.deltaTime * -this.speed);
			}
			this.soundObj.GetComponent<PlayMidiSound>().UpdateProgress();
		}
	}

	// Token: 0x040001B3 RID: 435
	public float speed;

	// Token: 0x040001B4 RID: 436
	public bool play;

	// Token: 0x040001B5 RID: 437
	public GameObject soundObj;

	// Token: 0x040001B6 RID: 438
	public bool moveDown;
}
