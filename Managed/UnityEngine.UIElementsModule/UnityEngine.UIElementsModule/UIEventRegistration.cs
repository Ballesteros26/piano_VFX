using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006E RID: 110
	internal static class UIEventRegistration
	{
		// Token: 0x06000295 RID: 661 RVA: 0x000099E4 File Offset: 0x00007BE4
		static UIEventRegistration()
		{
			GUIUtility.takeCapture = (Action)Delegate.Combine(GUIUtility.takeCapture, delegate
			{
				UIEventRegistration.TakeCapture();
			});
			GUIUtility.releaseCapture = (Action)Delegate.Combine(GUIUtility.releaseCapture, delegate
			{
				UIEventRegistration.ReleaseCapture();
			});
			GUIUtility.processEvent = (Func<int, IntPtr, bool>)Delegate.Combine(GUIUtility.processEvent, (int i, IntPtr ptr) => UIEventRegistration.ProcessEvent(i, ptr));
			GUIUtility.cleanupRoots = (Action)Delegate.Combine(GUIUtility.cleanupRoots, delegate
			{
				UIEventRegistration.CleanupRoots();
			});
			GUIUtility.endContainerGUIFromException = (Func<Exception, bool>)Delegate.Combine(GUIUtility.endContainerGUIFromException, (Exception exception) => UIEventRegistration.EndContainerGUIFromException(exception));
			GUIUtility.guiChanged = (Action)Delegate.Combine(GUIUtility.guiChanged, delegate
			{
				UIEventRegistration.MakeCurrentIMGUIContainerDirty();
			});
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009AD4 File Offset: 0x00007CD4
		internal static void RegisterUIElementSystem(IUIElementsUtility utility)
		{
			UIEventRegistration.s_Utilities.Insert(0, utility);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009AE4 File Offset: 0x00007CE4
		private static void TakeCapture()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.TakeCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009B40 File Offset: 0x00007D40
		private static void ReleaseCapture()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.ReleaseCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009B9C File Offset: 0x00007D9C
		private static bool EndContainerGUIFromException(Exception exception)
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.EndContainerGUIFromException(exception);
				if (flag)
				{
					return true;
				}
			}
			return GUIUtility.ShouldRethrowException(exception);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009C08 File Offset: 0x00007E08
		private static bool ProcessEvent(int instanceID, IntPtr nativeEventPtr)
		{
			bool flag = false;
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag2 = iuielementsUtility.ProcessEvent(instanceID, nativeEventPtr, ref flag);
				if (flag2)
				{
					return flag;
				}
			}
			return false;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009C78 File Offset: 0x00007E78
		private static void CleanupRoots()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.CleanupRoots();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009CD4 File Offset: 0x00007ED4
		internal static void MakeCurrentIMGUIContainerDirty()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.MakeCurrentIMGUIContainerDirty();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009D30 File Offset: 0x00007F30
		internal static void UpdateSchedulers()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				iuielementsUtility.UpdateSchedulers();
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009D88 File Offset: 0x00007F88
		internal static void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				iuielementsUtility.RequestRepaintForPanels(repaintCallback);
			}
		}

		// Token: 0x0400014F RID: 335
		private static List<IUIElementsUtility> s_Utilities = new List<IUIElementsUtility>();
	}
}
