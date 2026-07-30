using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000066 RID: 102
	public static class ExecuteEvents
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x00016FF1 File Offset: 0x000151F1
		public static T ValidateEventData<T>(BaseEventData data) where T : class
		{
			if (!(data is T))
			{
				throw new ArgumentException(string.Format("Invalid type: {0} passed to event expecting {1}", data.GetType(), typeof(T)));
			}
			return data as T;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00017030 File Offset: 0x00015230
		private static void Execute(IPointerEnterHandler handler, BaseEventData eventData)
		{
			handler.OnPointerEnter(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001703E File Offset: 0x0001523E
		private static void Execute(IPointerExitHandler handler, BaseEventData eventData)
		{
			handler.OnPointerExit(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001704C File Offset: 0x0001524C
		private static void Execute(IPointerDownHandler handler, BaseEventData eventData)
		{
			handler.OnPointerDown(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001705A File Offset: 0x0001525A
		private static void Execute(IPointerUpHandler handler, BaseEventData eventData)
		{
			handler.OnPointerUp(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00017068 File Offset: 0x00015268
		private static void Execute(IPointerClickHandler handler, BaseEventData eventData)
		{
			handler.OnPointerClick(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00017076 File Offset: 0x00015276
		private static void Execute(IInitializePotentialDragHandler handler, BaseEventData eventData)
		{
			handler.OnInitializePotentialDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00017084 File Offset: 0x00015284
		private static void Execute(IBeginDragHandler handler, BaseEventData eventData)
		{
			handler.OnBeginDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00017092 File Offset: 0x00015292
		private static void Execute(IDragHandler handler, BaseEventData eventData)
		{
			handler.OnDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000170A0 File Offset: 0x000152A0
		private static void Execute(IEndDragHandler handler, BaseEventData eventData)
		{
			handler.OnEndDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000170AE File Offset: 0x000152AE
		private static void Execute(IDropHandler handler, BaseEventData eventData)
		{
			handler.OnDrop(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000170BC File Offset: 0x000152BC
		private static void Execute(IScrollHandler handler, BaseEventData eventData)
		{
			handler.OnScroll(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000170CA File Offset: 0x000152CA
		private static void Execute(IUpdateSelectedHandler handler, BaseEventData eventData)
		{
			handler.OnUpdateSelected(eventData);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000170D3 File Offset: 0x000152D3
		private static void Execute(ISelectHandler handler, BaseEventData eventData)
		{
			handler.OnSelect(eventData);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000170DC File Offset: 0x000152DC
		private static void Execute(IDeselectHandler handler, BaseEventData eventData)
		{
			handler.OnDeselect(eventData);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000170E5 File Offset: 0x000152E5
		private static void Execute(IMoveHandler handler, BaseEventData eventData)
		{
			handler.OnMove(ExecuteEvents.ValidateEventData<AxisEventData>(eventData));
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000170F3 File Offset: 0x000152F3
		private static void Execute(ISubmitHandler handler, BaseEventData eventData)
		{
			handler.OnSubmit(eventData);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000170FC File Offset: 0x000152FC
		private static void Execute(ICancelHandler handler, BaseEventData eventData)
		{
			handler.OnCancel(eventData);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00017105 File Offset: 0x00015305
		public static ExecuteEvents.EventFunction<IPointerEnterHandler> pointerEnterHandler
		{
			get
			{
				return ExecuteEvents.s_PointerEnterHandler;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001710C File Offset: 0x0001530C
		public static ExecuteEvents.EventFunction<IPointerExitHandler> pointerExitHandler
		{
			get
			{
				return ExecuteEvents.s_PointerExitHandler;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00017113 File Offset: 0x00015313
		public static ExecuteEvents.EventFunction<IPointerDownHandler> pointerDownHandler
		{
			get
			{
				return ExecuteEvents.s_PointerDownHandler;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001711A File Offset: 0x0001531A
		public static ExecuteEvents.EventFunction<IPointerUpHandler> pointerUpHandler
		{
			get
			{
				return ExecuteEvents.s_PointerUpHandler;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00017121 File Offset: 0x00015321
		public static ExecuteEvents.EventFunction<IPointerClickHandler> pointerClickHandler
		{
			get
			{
				return ExecuteEvents.s_PointerClickHandler;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00017128 File Offset: 0x00015328
		public static ExecuteEvents.EventFunction<IInitializePotentialDragHandler> initializePotentialDrag
		{
			get
			{
				return ExecuteEvents.s_InitializePotentialDragHandler;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001712F File Offset: 0x0001532F
		public static ExecuteEvents.EventFunction<IBeginDragHandler> beginDragHandler
		{
			get
			{
				return ExecuteEvents.s_BeginDragHandler;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00017136 File Offset: 0x00015336
		public static ExecuteEvents.EventFunction<IDragHandler> dragHandler
		{
			get
			{
				return ExecuteEvents.s_DragHandler;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001713D File Offset: 0x0001533D
		public static ExecuteEvents.EventFunction<IEndDragHandler> endDragHandler
		{
			get
			{
				return ExecuteEvents.s_EndDragHandler;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00017144 File Offset: 0x00015344
		public static ExecuteEvents.EventFunction<IDropHandler> dropHandler
		{
			get
			{
				return ExecuteEvents.s_DropHandler;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001714B File Offset: 0x0001534B
		public static ExecuteEvents.EventFunction<IScrollHandler> scrollHandler
		{
			get
			{
				return ExecuteEvents.s_ScrollHandler;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00017152 File Offset: 0x00015352
		public static ExecuteEvents.EventFunction<IUpdateSelectedHandler> updateSelectedHandler
		{
			get
			{
				return ExecuteEvents.s_UpdateSelectedHandler;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00017159 File Offset: 0x00015359
		public static ExecuteEvents.EventFunction<ISelectHandler> selectHandler
		{
			get
			{
				return ExecuteEvents.s_SelectHandler;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00017160 File Offset: 0x00015360
		public static ExecuteEvents.EventFunction<IDeselectHandler> deselectHandler
		{
			get
			{
				return ExecuteEvents.s_DeselectHandler;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00017167 File Offset: 0x00015367
		public static ExecuteEvents.EventFunction<IMoveHandler> moveHandler
		{
			get
			{
				return ExecuteEvents.s_MoveHandler;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001716E File Offset: 0x0001536E
		public static ExecuteEvents.EventFunction<ISubmitHandler> submitHandler
		{
			get
			{
				return ExecuteEvents.s_SubmitHandler;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00017175 File Offset: 0x00015375
		public static ExecuteEvents.EventFunction<ICancelHandler> cancelHandler
		{
			get
			{
				return ExecuteEvents.s_CancelHandler;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001717C File Offset: 0x0001537C
		private static void GetEventChain(GameObject root, IList<Transform> eventChain)
		{
			eventChain.Clear();
			if (root == null)
			{
				return;
			}
			Transform transform = root.transform;
			while (transform != null)
			{
				eventChain.Add(transform);
				transform = transform.parent;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000171BC File Offset: 0x000153BC
		public static bool Execute<T>(GameObject target, BaseEventData eventData, ExecuteEvents.EventFunction<T> functor) where T : IEventSystemHandler
		{
			List<IEventSystemHandler> list = ExecuteEvents.s_HandlerListPool.Get();
			ExecuteEvents.GetEventList<T>(target, list);
			int i = 0;
			while (i < list.Count)
			{
				T t;
				try
				{
					t = (T)((object)list[i]);
				}
				catch (Exception ex)
				{
					IEventSystemHandler eventSystemHandler = list[i];
					Debug.LogException(new Exception(string.Format("Type {0} expected {1} received.", typeof(T).Name, eventSystemHandler.GetType().Name), ex));
					goto IL_0074;
				}
				goto IL_0062;
				IL_0074:
				i++;
				continue;
				IL_0062:
				try
				{
					functor(t, eventData);
				}
				catch (Exception ex2)
				{
					Debug.LogException(ex2);
				}
				goto IL_0074;
			}
			int count = list.Count;
			ExecuteEvents.s_HandlerListPool.Release(list);
			return count > 0;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001727C File Offset: 0x0001547C
		public static GameObject ExecuteHierarchy<T>(GameObject root, BaseEventData eventData, ExecuteEvents.EventFunction<T> callbackFunction) where T : IEventSystemHandler
		{
			ExecuteEvents.GetEventChain(root, ExecuteEvents.s_InternalTransformList);
			for (int i = 0; i < ExecuteEvents.s_InternalTransformList.Count; i++)
			{
				Transform transform = ExecuteEvents.s_InternalTransformList[i];
				if (ExecuteEvents.Execute<T>(transform.gameObject, eventData, callbackFunction))
				{
					return transform.gameObject;
				}
			}
			return null;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000172CC File Offset: 0x000154CC
		private static bool ShouldSendToComponent<T>(Component component) where T : IEventSystemHandler
		{
			if (!(component is T))
			{
				return false;
			}
			Behaviour behaviour = component as Behaviour;
			return !(behaviour != null) || behaviour.isActiveAndEnabled;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00017300 File Offset: 0x00015500
		private static void GetEventList<T>(GameObject go, IList<IEventSystemHandler> results) where T : IEventSystemHandler
		{
			if (results == null)
			{
				throw new ArgumentException("Results array is null", "results");
			}
			if (go == null || !go.activeInHierarchy)
			{
				return;
			}
			List<Component> list = ListPool<Component>.Get();
			go.GetComponents<Component>(list);
			for (int i = 0; i < list.Count; i++)
			{
				if (ExecuteEvents.ShouldSendToComponent<T>(list[i]))
				{
					results.Add(list[i] as IEventSystemHandler);
				}
			}
			ListPool<Component>.Release(list);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00017378 File Offset: 0x00015578
		public static bool CanHandleEvent<T>(GameObject go) where T : IEventSystemHandler
		{
			List<IEventSystemHandler> list = ExecuteEvents.s_HandlerListPool.Get();
			ExecuteEvents.GetEventList<T>(go, list);
			int count = list.Count;
			ExecuteEvents.s_HandlerListPool.Release(list);
			return count != 0;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000173AC File Offset: 0x000155AC
		public static GameObject GetEventHandler<T>(GameObject root) where T : IEventSystemHandler
		{
			if (root == null)
			{
				return null;
			}
			Transform transform = root.transform;
			while (transform != null)
			{
				if (ExecuteEvents.CanHandleEvent<T>(transform.gameObject))
				{
					return transform.gameObject;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x040001D6 RID: 470
		private static readonly ExecuteEvents.EventFunction<IPointerEnterHandler> s_PointerEnterHandler = new ExecuteEvents.EventFunction<IPointerEnterHandler>(ExecuteEvents.Execute);

		// Token: 0x040001D7 RID: 471
		private static readonly ExecuteEvents.EventFunction<IPointerExitHandler> s_PointerExitHandler = new ExecuteEvents.EventFunction<IPointerExitHandler>(ExecuteEvents.Execute);

		// Token: 0x040001D8 RID: 472
		private static readonly ExecuteEvents.EventFunction<IPointerDownHandler> s_PointerDownHandler = new ExecuteEvents.EventFunction<IPointerDownHandler>(ExecuteEvents.Execute);

		// Token: 0x040001D9 RID: 473
		private static readonly ExecuteEvents.EventFunction<IPointerUpHandler> s_PointerUpHandler = new ExecuteEvents.EventFunction<IPointerUpHandler>(ExecuteEvents.Execute);

		// Token: 0x040001DA RID: 474
		private static readonly ExecuteEvents.EventFunction<IPointerClickHandler> s_PointerClickHandler = new ExecuteEvents.EventFunction<IPointerClickHandler>(ExecuteEvents.Execute);

		// Token: 0x040001DB RID: 475
		private static readonly ExecuteEvents.EventFunction<IInitializePotentialDragHandler> s_InitializePotentialDragHandler = new ExecuteEvents.EventFunction<IInitializePotentialDragHandler>(ExecuteEvents.Execute);

		// Token: 0x040001DC RID: 476
		private static readonly ExecuteEvents.EventFunction<IBeginDragHandler> s_BeginDragHandler = new ExecuteEvents.EventFunction<IBeginDragHandler>(ExecuteEvents.Execute);

		// Token: 0x040001DD RID: 477
		private static readonly ExecuteEvents.EventFunction<IDragHandler> s_DragHandler = new ExecuteEvents.EventFunction<IDragHandler>(ExecuteEvents.Execute);

		// Token: 0x040001DE RID: 478
		private static readonly ExecuteEvents.EventFunction<IEndDragHandler> s_EndDragHandler = new ExecuteEvents.EventFunction<IEndDragHandler>(ExecuteEvents.Execute);

		// Token: 0x040001DF RID: 479
		private static readonly ExecuteEvents.EventFunction<IDropHandler> s_DropHandler = new ExecuteEvents.EventFunction<IDropHandler>(ExecuteEvents.Execute);

		// Token: 0x040001E0 RID: 480
		private static readonly ExecuteEvents.EventFunction<IScrollHandler> s_ScrollHandler = new ExecuteEvents.EventFunction<IScrollHandler>(ExecuteEvents.Execute);

		// Token: 0x040001E1 RID: 481
		private static readonly ExecuteEvents.EventFunction<IUpdateSelectedHandler> s_UpdateSelectedHandler = new ExecuteEvents.EventFunction<IUpdateSelectedHandler>(ExecuteEvents.Execute);

		// Token: 0x040001E2 RID: 482
		private static readonly ExecuteEvents.EventFunction<ISelectHandler> s_SelectHandler = new ExecuteEvents.EventFunction<ISelectHandler>(ExecuteEvents.Execute);

		// Token: 0x040001E3 RID: 483
		private static readonly ExecuteEvents.EventFunction<IDeselectHandler> s_DeselectHandler = new ExecuteEvents.EventFunction<IDeselectHandler>(ExecuteEvents.Execute);

		// Token: 0x040001E4 RID: 484
		private static readonly ExecuteEvents.EventFunction<IMoveHandler> s_MoveHandler = new ExecuteEvents.EventFunction<IMoveHandler>(ExecuteEvents.Execute);

		// Token: 0x040001E5 RID: 485
		private static readonly ExecuteEvents.EventFunction<ISubmitHandler> s_SubmitHandler = new ExecuteEvents.EventFunction<ISubmitHandler>(ExecuteEvents.Execute);

		// Token: 0x040001E6 RID: 486
		private static readonly ExecuteEvents.EventFunction<ICancelHandler> s_CancelHandler = new ExecuteEvents.EventFunction<ICancelHandler>(ExecuteEvents.Execute);

		// Token: 0x040001E7 RID: 487
		private static readonly ObjectPool<List<IEventSystemHandler>> s_HandlerListPool = new ObjectPool<List<IEventSystemHandler>>(null, delegate(List<IEventSystemHandler> l)
		{
			l.Clear();
		});

		// Token: 0x040001E8 RID: 488
		private static readonly List<Transform> s_InternalTransformList = new List<Transform>(30);

		// Token: 0x020000BD RID: 189
		// (Invoke) Token: 0x060006AE RID: 1710
		public delegate void EventFunction<T1>(T1 handler, BaseEventData eventData);
	}
}
