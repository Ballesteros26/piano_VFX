using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A8 RID: 1192
	internal class EventHandler
	{
		// Token: 0x06004BDC RID: 19420 RVA: 0x0012DCCC File Offset: 0x0012BECC
		internal static int EventCallback(IntPtr callref, IntPtr eventref, IntPtr handle)
		{
			uint eventClass = EventHandler.GetEventClass(eventref);
			uint eventKind = EventHandler.GetEventKind(eventref);
			MSG msg = default(MSG);
			uint num = eventClass;
			IEventHandler eventHandler;
			if (num != 1634758764U)
			{
				if (num != 1668183148U)
				{
					if (num != 1751740258U)
					{
						if (num != 1801812322U)
						{
							if (num == 1836021107U)
							{
								eventHandler = EventHandler.Driver.MouseHandler;
								goto IL_00D6;
							}
							if (num != 1952807028U)
							{
								if (num != 2003398244U)
								{
									return 0;
								}
								eventHandler = EventHandler.Driver.WindowHandler;
								goto IL_00D6;
							}
						}
						eventHandler = EventHandler.Driver.KeyboardHandler;
					}
					else
					{
						eventHandler = EventHandler.Driver.HIObjectHandler;
					}
				}
				else
				{
					eventHandler = EventHandler.Driver.ControlHandler;
				}
			}
			else
			{
				eventHandler = EventHandler.Driver.ApplicationHandler;
			}
			IL_00D6:
			if (eventHandler.ProcessEvent(callref, eventref, handle, eventKind, ref msg))
			{
				EventHandler.Driver.EnqueueMessage(msg);
				return -9874;
			}
			return 0;
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0012DDD4 File Offset: 0x0012BFD4
		internal static bool TranslateMessage(ref MSG msg)
		{
			bool flag = false;
			if (!flag)
			{
				flag = EventHandler.Driver.KeyboardHandler.TranslateMessage(ref msg);
			}
			if (!flag)
			{
				flag = EventHandler.Driver.MouseHandler.TranslateMessage(ref msg);
			}
			return flag;
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0012DE14 File Offset: 0x0012C014
		internal static void InstallApplicationHandler()
		{
			EventHandler.InstallEventHandler(EventHandler.GetApplicationEventTarget(), EventHandler.EventHandlerDelegate, (uint)EventHandler.ApplicationEvents.Length, EventHandler.ApplicationEvents, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0012DE48 File Offset: 0x0012C048
		internal static void InstallControlHandler(IntPtr control)
		{
			EventHandler.InstallEventHandler(EventHandler.GetControlEventTarget(control), EventHandler.EventHandlerDelegate, (uint)EventHandler.ControlEvents.Length, EventHandler.ControlEvents, control, IntPtr.Zero);
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0012DE70 File Offset: 0x0012C070
		internal static void InstallWindowHandler(IntPtr window)
		{
			EventHandler.InstallEventHandler(EventHandler.GetWindowEventTarget(window), EventHandler.EventHandlerDelegate, (uint)EventHandler.WindowEvents.Length, EventHandler.WindowEvents, window, IntPtr.Zero);
		}

		// Token: 0x06004BE1 RID: 19425
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr GetApplicationEventTarget();

		// Token: 0x06004BE2 RID: 19426
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetControlEventTarget(IntPtr control);

		// Token: 0x06004BE3 RID: 19427
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetWindowEventTarget(IntPtr window);

		// Token: 0x06004BE4 RID: 19428
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern uint GetEventClass(IntPtr eventref);

		// Token: 0x06004BE5 RID: 19429
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern uint GetEventKind(IntPtr eventref);

		// Token: 0x06004BE6 RID: 19430
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int InstallEventHandler(IntPtr window, EventDelegate event_handler, uint count, EventTypeSpec[] types, IntPtr user_data, IntPtr handlerref);

		// Token: 0x040028CB RID: 10443
		internal const int EVENT_NOT_HANDLED = 0;

		// Token: 0x040028CC RID: 10444
		internal const int EVENT_HANDLED = -9874;

		// Token: 0x040028CD RID: 10445
		internal const uint kEventClassMouse = 1836021107U;

		// Token: 0x040028CE RID: 10446
		internal const uint kEventClassKeyboard = 1801812322U;

		// Token: 0x040028CF RID: 10447
		internal const uint kEventClassTextInput = 1952807028U;

		// Token: 0x040028D0 RID: 10448
		internal const uint kEventClassApplication = 1634758764U;

		// Token: 0x040028D1 RID: 10449
		internal const uint kEventClassAppleEvent = 1701867619U;

		// Token: 0x040028D2 RID: 10450
		internal const uint kEventClassMenu = 1835363957U;

		// Token: 0x040028D3 RID: 10451
		internal const uint kEventClassWindow = 2003398244U;

		// Token: 0x040028D4 RID: 10452
		internal const uint kEventClassControl = 1668183148U;

		// Token: 0x040028D5 RID: 10453
		internal const uint kEventClassCommand = 1668113523U;

		// Token: 0x040028D6 RID: 10454
		internal const uint kEventClassTablet = 1952607348U;

		// Token: 0x040028D7 RID: 10455
		internal const uint kEventClassVolume = 1987013664U;

		// Token: 0x040028D8 RID: 10456
		internal const uint kEventClassAppearance = 1634758765U;

		// Token: 0x040028D9 RID: 10457
		internal const uint kEventClassService = 1936028278U;

		// Token: 0x040028DA RID: 10458
		internal const uint kEventClassToolbar = 1952604530U;

		// Token: 0x040028DB RID: 10459
		internal const uint kEventClassToolbarItem = 1952606580U;

		// Token: 0x040028DC RID: 10460
		internal const uint kEventClassAccessibility = 1633903461U;

		// Token: 0x040028DD RID: 10461
		internal const uint kEventClassHIObject = 1751740258U;

		// Token: 0x040028DE RID: 10462
		internal static EventDelegate EventHandlerDelegate = new EventDelegate(EventHandler.EventCallback);

		// Token: 0x040028DF RID: 10463
		internal static XplatUICarbon Driver;

		// Token: 0x040028E0 RID: 10464
		internal static EventTypeSpec[] HIObjectEvents = new EventTypeSpec[]
		{
			new EventTypeSpec(1751740258U, 1U),
			new EventTypeSpec(1751740258U, 2U),
			new EventTypeSpec(1751740258U, 3U)
		};

		// Token: 0x040028E1 RID: 10465
		internal static EventTypeSpec[] ControlEvents = new EventTypeSpec[]
		{
			new EventTypeSpec(1668183148U, 154U),
			new EventTypeSpec(1668183148U, 4U),
			new EventTypeSpec(1668183148U, 18U),
			new EventTypeSpec(1668183148U, 19U),
			new EventTypeSpec(1668183148U, 20U),
			new EventTypeSpec(1668183148U, 21U),
			new EventTypeSpec(1668183148U, 8U),
			new EventTypeSpec(1668183148U, 1000U),
			new EventTypeSpec(1668183148U, 157U)
		};

		// Token: 0x040028E2 RID: 10466
		internal static EventTypeSpec[] ApplicationEvents = new EventTypeSpec[]
		{
			new EventTypeSpec(1634758764U, 1U),
			new EventTypeSpec(1634758764U, 2U)
		};

		// Token: 0x040028E3 RID: 10467
		private static EventTypeSpec[] WindowEvents = new EventTypeSpec[]
		{
			new EventTypeSpec(1836021107U, 5U),
			new EventTypeSpec(1836021107U, 6U),
			new EventTypeSpec(1836021107U, 1U),
			new EventTypeSpec(1836021107U, 2U),
			new EventTypeSpec(1836021107U, 10U),
			new EventTypeSpec(1836021107U, 11U),
			new EventTypeSpec(2003398244U, 6U),
			new EventTypeSpec(2003398244U, 5U),
			new EventTypeSpec(2003398244U, 6U),
			new EventTypeSpec(2003398244U, 67U),
			new EventTypeSpec(2003398244U, 86U),
			new EventTypeSpec(2003398244U, 70U),
			new EventTypeSpec(2003398244U, 87U),
			new EventTypeSpec(2003398244U, 27U),
			new EventTypeSpec(2003398244U, 28U),
			new EventTypeSpec(2003398244U, 29U),
			new EventTypeSpec(2003398244U, 72U),
			new EventTypeSpec(2003398244U, 24U),
			new EventTypeSpec(1801812322U, 4U),
			new EventTypeSpec(1801812322U, 1U),
			new EventTypeSpec(1801812322U, 2U),
			new EventTypeSpec(1801812322U, 3U),
			new EventTypeSpec(1952807028U, 2U)
		};
	}
}
