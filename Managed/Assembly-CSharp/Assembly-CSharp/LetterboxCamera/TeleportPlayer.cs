using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x0200005D RID: 93
	public class TeleportPlayer : MonoBehaviour
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00013E17 File Offset: 0x00012017
		private void Start()
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00013E17 File Offset: 0x00012017
		private void Update()
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000151C8 File Offset: 0x000133C8
		public void OnTriggerEnter2D(Collider2D coll)
		{
			DragonClass component = coll.gameObject.GetComponent<DragonClass>();
			if (component != null)
			{
				float num = component.transform.position.x - this.newX;
				component.transform.position = new Vector3(component.transform.position.x - num, component.transform.position.y, component.transform.position.z);
				for (int i = 0; i < this.parralaxObjects.Length; i++)
				{
					this.parralaxObjects[i].IgnoreNextFrame();
					this.parralaxObjects[i].transform.position = new Vector3(this.parralaxObjects[i].transform.position.x - num, this.parralaxObjects[i].transform.position.y, this.parralaxObjects[i].transform.position.z);
				}
			}
		}

		// Token: 0x04000423 RID: 1059
		public float newX = -12.98f;

		// Token: 0x04000424 RID: 1060
		public Parralax[] parralaxObjects;
	}
}
