using System;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000067 RID: 103
	public class BaseInput : UIBehaviour
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00017549 File Offset: 0x00015749
		public virtual string compositionString
		{
			get
			{
				return Input.compositionString;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00017550 File Offset: 0x00015750
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00017557 File Offset: 0x00015757
		public virtual IMECompositionMode imeCompositionMode
		{
			get
			{
				return Input.imeCompositionMode;
			}
			set
			{
				Input.imeCompositionMode = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001755F File Offset: 0x0001575F
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00017566 File Offset: 0x00015766
		public virtual Vector2 compositionCursorPos
		{
			get
			{
				return Input.compositionCursorPos;
			}
			set
			{
				Input.compositionCursorPos = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001756E File Offset: 0x0001576E
		public virtual bool mousePresent
		{
			get
			{
				return Input.mousePresent;
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00017575 File Offset: 0x00015775
		public virtual bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001757D File Offset: 0x0001577D
		public virtual bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00017585 File Offset: 0x00015785
		public virtual bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001758D File Offset: 0x0001578D
		public virtual Vector2 mousePosition
		{
			get
			{
				return MultipleDisplayUtilities.GetMousePositionRelativeToMainDisplayResolution();
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00017594 File Offset: 0x00015794
		public virtual Vector2 mouseScrollDelta
		{
			get
			{
				return Input.mouseScrollDelta;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001759B File Offset: 0x0001579B
		public virtual bool touchSupported
		{
			get
			{
				return Input.touchSupported;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000175A2 File Offset: 0x000157A2
		public virtual int touchCount
		{
			get
			{
				return Input.touchCount;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000175A9 File Offset: 0x000157A9
		public virtual Touch GetTouch(int index)
		{
			return Input.GetTouch(index);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000175B1 File Offset: 0x000157B1
		public virtual float GetAxisRaw(string axisName)
		{
			return Input.GetAxisRaw(axisName);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000175B9 File Offset: 0x000157B9
		public virtual bool GetButtonDown(string buttonName)
		{
			return Input.GetButtonDown(buttonName);
		}
	}
}
