using System;
using Mono.WebBrowser.DOM;

namespace Mono.WebBrowser
{
	// Token: 0x02000004 RID: 4
	public interface IWebBrowser
	{
		// Token: 0x06000008 RID: 8
		bool Load(IntPtr handle, int width, int height);

		// Token: 0x06000009 RID: 9
		void Shutdown();

		// Token: 0x0600000A RID: 10
		void FocusIn(FocusOption focus);

		// Token: 0x0600000B RID: 11
		void FocusOut();

		// Token: 0x0600000C RID: 12
		void Activate();

		// Token: 0x0600000D RID: 13
		void Deactivate();

		// Token: 0x0600000E RID: 14
		void Resize(int width, int height);

		// Token: 0x0600000F RID: 15
		void Render(byte[] data);

		// Token: 0x06000010 RID: 16
		void Render(string html);

		// Token: 0x06000011 RID: 17
		void Render(string html, string uri, string contentType);

		// Token: 0x06000012 RID: 18
		void ExecuteScript(string script);

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19
		bool Initialized { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20
		IWindow Window { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21
		IDocument Document { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22
		// (set) Token: 0x06000017 RID: 23
		bool Offline { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24
		INavigation Navigation { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000019 RID: 25
		// (remove) Token: 0x0600001A RID: 26
		event NodeEventHandler KeyDown;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001B RID: 27
		// (remove) Token: 0x0600001C RID: 28
		event NodeEventHandler KeyPress;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600001D RID: 29
		// (remove) Token: 0x0600001E RID: 30
		event NodeEventHandler KeyUp;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600001F RID: 31
		// (remove) Token: 0x06000020 RID: 32
		event NodeEventHandler MouseClick;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000021 RID: 33
		// (remove) Token: 0x06000022 RID: 34
		event NodeEventHandler MouseDoubleClick;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000023 RID: 35
		// (remove) Token: 0x06000024 RID: 36
		event NodeEventHandler MouseDown;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000025 RID: 37
		// (remove) Token: 0x06000026 RID: 38
		event NodeEventHandler MouseEnter;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000027 RID: 39
		// (remove) Token: 0x06000028 RID: 40
		event NodeEventHandler MouseLeave;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000029 RID: 41
		// (remove) Token: 0x0600002A RID: 42
		event NodeEventHandler MouseMove;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600002B RID: 43
		// (remove) Token: 0x0600002C RID: 44
		event NodeEventHandler MouseUp;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600002D RID: 45
		// (remove) Token: 0x0600002E RID: 46
		event EventHandler Focus;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600002F RID: 47
		// (remove) Token: 0x06000030 RID: 48
		event CreateNewWindowEventHandler CreateNewWindow;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000031 RID: 49
		// (remove) Token: 0x06000032 RID: 50
		event AlertEventHandler Alert;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000033 RID: 51
		// (remove) Token: 0x06000034 RID: 52
		event LoadStartedEventHandler LoadStarted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000035 RID: 53
		// (remove) Token: 0x06000036 RID: 54
		event LoadCommitedEventHandler LoadCommited;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000037 RID: 55
		// (remove) Token: 0x06000038 RID: 56
		event ProgressChangedEventHandler ProgressChanged;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000039 RID: 57
		// (remove) Token: 0x0600003A RID: 58
		event LoadFinishedEventHandler LoadFinished;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600003B RID: 59
		// (remove) Token: 0x0600003C RID: 60
		event StatusChangedEventHandler StatusChanged;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600003D RID: 61
		// (remove) Token: 0x0600003E RID: 62
		event SecurityChangedEventHandler SecurityChanged;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600003F RID: 63
		// (remove) Token: 0x06000040 RID: 64
		event ContextMenuEventHandler ContextMenuShown;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000041 RID: 65
		// (remove) Token: 0x06000042 RID: 66
		event NavigationRequestedEventHandler NavigationRequested;
	}
}
