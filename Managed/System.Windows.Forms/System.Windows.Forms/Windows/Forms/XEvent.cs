using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020003F8 RID: 1016
	[StructLayout(2)]
	internal struct XEvent
	{
		// Token: 0x06004624 RID: 17956 RVA: 0x001142F0 File Offset: 0x001124F0
		public override string ToString()
		{
			switch (this.type)
			{
			case XEventName.ButtonPress:
			case XEventName.ButtonRelease:
				return XEvent.ToString(this.ButtonEvent);
			case XEventName.MotionNotify:
				return XEvent.ToString(this.MotionEvent);
			case XEventName.EnterNotify:
			case XEventName.LeaveNotify:
				return XEvent.ToString(this.CrossingEvent);
			case XEventName.FocusIn:
			case XEventName.FocusOut:
				return XEvent.ToString(this.FocusChangeEvent);
			case XEventName.KeymapNotify:
				return XEvent.ToString(this.KeymapEvent);
			case XEventName.Expose:
				return XEvent.ToString(this.ExposeEvent);
			case XEventName.GraphicsExpose:
				return XEvent.ToString(this.GraphicsExposeEvent);
			case XEventName.NoExpose:
				return XEvent.ToString(this.NoExposeEvent);
			case XEventName.VisibilityNotify:
				return XEvent.ToString(this.VisibilityEvent);
			case XEventName.CreateNotify:
				return XEvent.ToString(this.CreateWindowEvent);
			case XEventName.DestroyNotify:
				return XEvent.ToString(this.DestroyWindowEvent);
			case XEventName.UnmapNotify:
				return XEvent.ToString(this.UnmapEvent);
			case XEventName.MapNotify:
				return XEvent.ToString(this.MapEvent);
			case XEventName.MapRequest:
				return XEvent.ToString(this.MapRequestEvent);
			case XEventName.ReparentNotify:
				return XEvent.ToString(this.ReparentEvent);
			case XEventName.ConfigureNotify:
				return XEvent.ToString(this.ConfigureEvent);
			case XEventName.ConfigureRequest:
				return XEvent.ToString(this.ConfigureRequestEvent);
			case XEventName.GravityNotify:
				return XEvent.ToString(this.GravityEvent);
			case XEventName.ResizeRequest:
				return XEvent.ToString(this.ResizeRequestEvent);
			case XEventName.CirculateNotify:
			case XEventName.CirculateRequest:
				return XEvent.ToString(this.CirculateEvent);
			case XEventName.PropertyNotify:
				return XEvent.ToString(this.PropertyEvent);
			case XEventName.SelectionClear:
				return XEvent.ToString(this.SelectionClearEvent);
			case XEventName.SelectionRequest:
				return XEvent.ToString(this.SelectionRequestEvent);
			case XEventName.SelectionNotify:
				return XEvent.ToString(this.SelectionEvent);
			case XEventName.ColormapNotify:
				return XEvent.ToString(this.ColormapEvent);
			case XEventName.ClientMessage:
				return XEvent.ToString(this.ClientMessageEvent);
			case XEventName.MappingNotify:
				return XEvent.ToString(this.MappingEvent);
			default:
				return this.type.ToString();
			}
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x00114568 File Offset: 0x00112768
		public static string ToString(object ev)
		{
			string text = string.Empty;
			Type type = ev.GetType();
			FieldInfo[] fields = type.GetFields(60);
			for (int i = 0; i < fields.Length; i++)
			{
				if (text != string.Empty)
				{
					text += ", ";
				}
				object value = fields[i].GetValue(ev);
				text = text + fields[i].Name + "=" + ((value != null) ? value.ToString() : "<null>");
			}
			return type.Name + " (" + text + ")";
		}

		// Token: 0x04001F32 RID: 7986
		[FieldOffset(0)]
		internal XEventName type;

		// Token: 0x04001F33 RID: 7987
		[FieldOffset(0)]
		internal XAnyEvent AnyEvent;

		// Token: 0x04001F34 RID: 7988
		[FieldOffset(0)]
		internal XKeyEvent KeyEvent;

		// Token: 0x04001F35 RID: 7989
		[FieldOffset(0)]
		internal XButtonEvent ButtonEvent;

		// Token: 0x04001F36 RID: 7990
		[FieldOffset(0)]
		internal XMotionEvent MotionEvent;

		// Token: 0x04001F37 RID: 7991
		[FieldOffset(0)]
		internal XCrossingEvent CrossingEvent;

		// Token: 0x04001F38 RID: 7992
		[FieldOffset(0)]
		internal XFocusChangeEvent FocusChangeEvent;

		// Token: 0x04001F39 RID: 7993
		[FieldOffset(0)]
		internal XExposeEvent ExposeEvent;

		// Token: 0x04001F3A RID: 7994
		[FieldOffset(0)]
		internal XGraphicsExposeEvent GraphicsExposeEvent;

		// Token: 0x04001F3B RID: 7995
		[FieldOffset(0)]
		internal XNoExposeEvent NoExposeEvent;

		// Token: 0x04001F3C RID: 7996
		[FieldOffset(0)]
		internal XVisibilityEvent VisibilityEvent;

		// Token: 0x04001F3D RID: 7997
		[FieldOffset(0)]
		internal XCreateWindowEvent CreateWindowEvent;

		// Token: 0x04001F3E RID: 7998
		[FieldOffset(0)]
		internal XDestroyWindowEvent DestroyWindowEvent;

		// Token: 0x04001F3F RID: 7999
		[FieldOffset(0)]
		internal XUnmapEvent UnmapEvent;

		// Token: 0x04001F40 RID: 8000
		[FieldOffset(0)]
		internal XMapEvent MapEvent;

		// Token: 0x04001F41 RID: 8001
		[FieldOffset(0)]
		internal XMapRequestEvent MapRequestEvent;

		// Token: 0x04001F42 RID: 8002
		[FieldOffset(0)]
		internal XReparentEvent ReparentEvent;

		// Token: 0x04001F43 RID: 8003
		[FieldOffset(0)]
		internal XConfigureEvent ConfigureEvent;

		// Token: 0x04001F44 RID: 8004
		[FieldOffset(0)]
		internal XGravityEvent GravityEvent;

		// Token: 0x04001F45 RID: 8005
		[FieldOffset(0)]
		internal XResizeRequestEvent ResizeRequestEvent;

		// Token: 0x04001F46 RID: 8006
		[FieldOffset(0)]
		internal XConfigureRequestEvent ConfigureRequestEvent;

		// Token: 0x04001F47 RID: 8007
		[FieldOffset(0)]
		internal XCirculateEvent CirculateEvent;

		// Token: 0x04001F48 RID: 8008
		[FieldOffset(0)]
		internal XCirculateRequestEvent CirculateRequestEvent;

		// Token: 0x04001F49 RID: 8009
		[FieldOffset(0)]
		internal XPropertyEvent PropertyEvent;

		// Token: 0x04001F4A RID: 8010
		[FieldOffset(0)]
		internal XSelectionClearEvent SelectionClearEvent;

		// Token: 0x04001F4B RID: 8011
		[FieldOffset(0)]
		internal XSelectionRequestEvent SelectionRequestEvent;

		// Token: 0x04001F4C RID: 8012
		[FieldOffset(0)]
		internal XSelectionEvent SelectionEvent;

		// Token: 0x04001F4D RID: 8013
		[FieldOffset(0)]
		internal XColormapEvent ColormapEvent;

		// Token: 0x04001F4E RID: 8014
		[FieldOffset(0)]
		internal XClientMessageEvent ClientMessageEvent;

		// Token: 0x04001F4F RID: 8015
		[FieldOffset(0)]
		internal XMappingEvent MappingEvent;

		// Token: 0x04001F50 RID: 8016
		[FieldOffset(0)]
		internal XErrorEvent ErrorEvent;

		// Token: 0x04001F51 RID: 8017
		[FieldOffset(0)]
		internal XKeymapEvent KeymapEvent;

		// Token: 0x04001F52 RID: 8018
		[FieldOffset(0)]
		internal XEventPad Pad;
	}
}
