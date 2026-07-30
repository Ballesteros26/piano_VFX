using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004C RID: 76
	public class AxisEventData : BaseEventData
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00016415 File Offset: 0x00014615
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001641D File Offset: 0x0001461D
		public Vector2 moveVector { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00016426 File Offset: 0x00014626
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0001642E File Offset: 0x0001462E
		public MoveDirection moveDir { get; set; }

		// Token: 0x060004E1 RID: 1249 RVA: 0x00016437 File Offset: 0x00014637
		public AxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.moveVector = Vector2.zero;
			this.moveDir = MoveDirection.None;
		}
	}
}
