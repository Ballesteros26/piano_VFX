using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004F RID: 79
	public class PointerEventData : BaseEventData
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000164A4 File Offset: 0x000146A4
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x000164AC File Offset: 0x000146AC
		public GameObject pointerEnter { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000164B5 File Offset: 0x000146B5
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x000164BD File Offset: 0x000146BD
		public GameObject lastPress { get; private set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000164C6 File Offset: 0x000146C6
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x000164CE File Offset: 0x000146CE
		public GameObject rawPointerPress { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x000164D7 File Offset: 0x000146D7
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x000164DF File Offset: 0x000146DF
		public GameObject pointerDrag { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x000164E8 File Offset: 0x000146E8
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x000164F0 File Offset: 0x000146F0
		public GameObject pointerClick { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x000164F9 File Offset: 0x000146F9
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00016501 File Offset: 0x00014701
		public RaycastResult pointerCurrentRaycast { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001650A File Offset: 0x0001470A
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00016512 File Offset: 0x00014712
		public RaycastResult pointerPressRaycast { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001651B File Offset: 0x0001471B
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00016523 File Offset: 0x00014723
		public bool eligibleForClick { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001652C File Offset: 0x0001472C
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00016534 File Offset: 0x00014734
		public int pointerId { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001653D File Offset: 0x0001473D
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00016545 File Offset: 0x00014745
		public Vector2 position { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001654E File Offset: 0x0001474E
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00016556 File Offset: 0x00014756
		public Vector2 delta { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001655F File Offset: 0x0001475F
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00016567 File Offset: 0x00014767
		public Vector2 pressPosition { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00016570 File Offset: 0x00014770
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00016578 File Offset: 0x00014778
		[Obsolete("Use either pointerCurrentRaycast.worldPosition or pointerPressRaycast.worldPosition")]
		public Vector3 worldPosition { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00016581 File Offset: 0x00014781
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00016589 File Offset: 0x00014789
		[Obsolete("Use either pointerCurrentRaycast.worldNormal or pointerPressRaycast.worldNormal")]
		public Vector3 worldNormal { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00016592 File Offset: 0x00014792
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001659A File Offset: 0x0001479A
		public float clickTime { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000165A3 File Offset: 0x000147A3
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x000165AB File Offset: 0x000147AB
		public int clickCount { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x000165B4 File Offset: 0x000147B4
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x000165BC File Offset: 0x000147BC
		public Vector2 scrollDelta { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000165C5 File Offset: 0x000147C5
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x000165CD File Offset: 0x000147CD
		public bool useDragThreshold { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000165D6 File Offset: 0x000147D6
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x000165DE File Offset: 0x000147DE
		public bool dragging { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000165E7 File Offset: 0x000147E7
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x000165EF File Offset: 0x000147EF
		public PointerEventData.InputButton button { get; set; }

		// Token: 0x06000512 RID: 1298 RVA: 0x000165F8 File Offset: 0x000147F8
		public PointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.eligibleForClick = false;
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.useDragThreshold = true;
			this.dragging = false;
			this.button = PointerEventData.InputButton.Left;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00016678 File Offset: 0x00014878
		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0f;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001669C File Offset: 0x0001489C
		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0f;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000166BE File Offset: 0x000148BE
		public Camera enterEventCamera
		{
			get
			{
				if (!(this.pointerCurrentRaycast.module == null))
				{
					return this.pointerCurrentRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x000166E5 File Offset: 0x000148E5
		public Camera pressEventCamera
		{
			get
			{
				if (!(this.pointerPressRaycast.module == null))
				{
					return this.pointerPressRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001670C File Offset: 0x0001490C
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00016714 File Offset: 0x00014914
		public GameObject pointerPress
		{
			get
			{
				return this.m_PointerPress;
			}
			set
			{
				if (this.m_PointerPress == value)
				{
					return;
				}
				this.lastPress = this.m_PointerPress;
				this.m_PointerPress = value;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00016738 File Offset: 0x00014938
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + this.position);
			stringBuilder.AppendLine("<b>delta</b>: " + this.delta);
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + this.eligibleForClick.ToString());
			stringBuilder.AppendLine("<b>pointerEnter</b>: " + this.pointerEnter);
			stringBuilder.AppendLine("<b>pointerPress</b>: " + this.pointerPress);
			stringBuilder.AppendLine("<b>lastPointerPress</b>: " + this.lastPress);
			stringBuilder.AppendLine("<b>pointerDrag</b>: " + this.pointerDrag);
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + this.useDragThreshold.ToString());
			stringBuilder.AppendLine("<b>Current Raycast:</b>");
			stringBuilder.AppendLine(this.pointerCurrentRaycast.ToString());
			stringBuilder.AppendLine("<b>Press Raycast:</b>");
			stringBuilder.AppendLine(this.pointerPressRaycast.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x040001A0 RID: 416
		private GameObject m_PointerPress;

		// Token: 0x040001A7 RID: 423
		public List<GameObject> hovered = new List<GameObject>();

		// Token: 0x020000B9 RID: 185
		public enum InputButton
		{
			// Token: 0x04000303 RID: 771
			Left,
			// Token: 0x04000304 RID: 772
			Right,
			// Token: 0x04000305 RID: 773
			Middle
		}

		// Token: 0x020000BA RID: 186
		public enum FramePressState
		{
			// Token: 0x04000307 RID: 775
			Pressed,
			// Token: 0x04000308 RID: 776
			Released,
			// Token: 0x04000309 RID: 777
			PressedAndReleased,
			// Token: 0x0400030A RID: 778
			NotChanged
		}
	}
}
