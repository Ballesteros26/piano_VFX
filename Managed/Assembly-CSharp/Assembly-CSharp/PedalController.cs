using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class PedalController : MonoBehaviour
{
	// Token: 0x060000E7 RID: 231 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "RedBar")
		{
			if (this.pedalDown)
			{
				this.soundObj.GetComponent<PlayMidiSound>().PlayMidiEvent(64, 127, EventName.VoiceControlChange);
				this.hudController.GetComponent<HUDController>().SetPedalState("ON");
				return;
			}
			this.soundObj.GetComponent<PlayMidiSound>().PlayMidiEvent(64, 0, EventName.VoiceControlChange);
			this.hudController.GetComponent<HUDController>().SetPedalState("OFF");
		}
	}

	// Token: 0x04000260 RID: 608
	public GameObject soundObj;

	// Token: 0x04000261 RID: 609
	public bool pedalDown = true;

	// Token: 0x04000262 RID: 610
	public GameObject hudController;
}
