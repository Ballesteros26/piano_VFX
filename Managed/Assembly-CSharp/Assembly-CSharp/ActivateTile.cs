using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class ActivateTile : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	private void Update()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.transform.position.y < 10f && transform.transform.position.y > -10f && !transform.transform.gameObject.activeSelf)
			{
				transform.transform.gameObject.SetActive(true);
			}
			else if ((transform.transform.position.y + transform.transform.localScale.y * 5f < -10f && transform.transform.gameObject.activeSelf) || (transform.transform.position.y > 10f && transform.transform.gameObject.activeSelf))
			{
				if (transform.tag == "pedal")
				{
					transform.transform.gameObject.SetActive(false);
				}
				else if (transform.transform.GetComponent<SpawnEffect>().canDeactivate)
				{
					transform.transform.gameObject.SetActive(false);
				}
			}
			if (this.setNotesPlayed)
			{
				if (this.moveDown)
				{
					if (transform.transform.position.y < -2.8f && transform.tag == "tile")
					{
						this.notesPlayed++;
					}
				}
				else if (transform.transform.position.y > -2.8f && transform.tag == "tile")
				{
					this.notesPlayed++;
				}
			}
		}
		if (this.setNotesPlayed)
		{
			this.setNotesPlayed = false;
			this.hudController.GetComponent<HUDController>().SetNotesPlayed(this.notesPlayed);
			this.notesPlayed = 0;
		}
	}

	// Token: 0x04000002 RID: 2
	public bool moveDown;

	// Token: 0x04000003 RID: 3
	public GameObject hudController;

	// Token: 0x04000004 RID: 4
	private int notesPlayed;

	// Token: 0x04000005 RID: 5
	public bool setNotesPlayed;
}
