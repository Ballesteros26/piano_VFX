using System;
using System.Collections;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000443 RID: 1091
	internal class XEventQueue
	{
		// Token: 0x0600462C RID: 17964 RVA: 0x001147A0 File Offset: 0x001129A0
		public XEventQueue(Thread thread)
		{
			this.xqueue = new XEventQueue.XQueue(XEventQueue.InitialXEventSize);
			this.lqueue = new XEventQueue.XQueue(XEventQueue.InitialLXEventSize);
			this.paint = new XEventQueue.PaintQueue(XEventQueue.InitialPaintSize);
			this.timer_list = new ArrayList();
			this.thread = thread;
			this.dispatch_idle = true;
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x00114814 File Offset: 0x00112A14
		public int Count
		{
			get
			{
				XEventQueue.XQueue xqueue = this.lqueue;
				int num;
				lock (xqueue)
				{
					num = this.xqueue.Count + this.lqueue.Count;
				}
				return num;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x0600462F RID: 17967 RVA: 0x00114874 File Offset: 0x00112A74
		public XEventQueue.PaintQueue Paint
		{
			get
			{
				return this.paint;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06004630 RID: 17968 RVA: 0x0011487C File Offset: 0x00112A7C
		public Thread Thread
		{
			get
			{
				return this.thread;
			}
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x00114884 File Offset: 0x00112A84
		public void Enqueue(XEvent xevent)
		{
			if (Thread.CurrentThread != this.thread)
			{
				Console.WriteLine("Hwnd.Queue.Enqueue called from a different thread without locking.");
				Console.WriteLine(Environment.StackTrace);
			}
			this.xqueue.Enqueue(xevent);
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x001148C4 File Offset: 0x00112AC4
		public void EnqueueLocked(XEvent xevent)
		{
			XEventQueue.XQueue xqueue = this.lqueue;
			lock (xqueue)
			{
				this.lqueue.Enqueue(xevent);
			}
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x00114914 File Offset: 0x00112B14
		public XEvent Dequeue()
		{
			if (Thread.CurrentThread != this.thread)
			{
				Console.WriteLine("Hwnd.Queue.Dequeue called from a different thread without locking.");
				Console.WriteLine(Environment.StackTrace);
			}
			if (this.xqueue.Count == 0)
			{
				XEventQueue.XQueue xqueue = this.lqueue;
				lock (xqueue)
				{
					return this.lqueue.Dequeue();
				}
			}
			return this.xqueue.Dequeue();
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x001149A8 File Offset: 0x00112BA8
		public XEvent Peek()
		{
			if (Thread.CurrentThread != this.thread)
			{
				Console.WriteLine("Hwnd.Queue.Peek called from a different thread without locking.");
				Console.WriteLine(Environment.StackTrace);
			}
			if (this.xqueue.Count == 0)
			{
				XEventQueue.XQueue xqueue = this.lqueue;
				lock (xqueue)
				{
					return this.lqueue.Peek();
				}
			}
			return this.xqueue.Peek();
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x00114A3C File Offset: 0x00112C3C
		// (set) Token: 0x06004636 RID: 17974 RVA: 0x00114A44 File Offset: 0x00112C44
		public bool DispatchIdle
		{
			get
			{
				return this.dispatch_idle;
			}
			set
			{
				this.dispatch_idle = value;
			}
		}

		// Token: 0x040022BB RID: 8891
		private XEventQueue.XQueue xqueue;

		// Token: 0x040022BC RID: 8892
		private XEventQueue.XQueue lqueue;

		// Token: 0x040022BD RID: 8893
		private XEventQueue.PaintQueue paint;

		// Token: 0x040022BE RID: 8894
		internal ArrayList timer_list;

		// Token: 0x040022BF RID: 8895
		private Thread thread;

		// Token: 0x040022C0 RID: 8896
		private bool dispatch_idle;

		// Token: 0x040022C1 RID: 8897
		private static readonly int InitialXEventSize = 100;

		// Token: 0x040022C2 RID: 8898
		private static readonly int InitialLXEventSize = 10;

		// Token: 0x040022C3 RID: 8899
		private static readonly int InitialPaintSize = 50;

		// Token: 0x02000444 RID: 1092
		public class PaintQueue
		{
			// Token: 0x06004637 RID: 17975 RVA: 0x00114A50 File Offset: 0x00112C50
			public PaintQueue(int size)
			{
				this.hwnds = new ArrayList(size);
				this.xevent = default(XEvent);
				this.xevent.AnyEvent.type = XEventName.Expose;
			}

			// Token: 0x170011E8 RID: 4584
			// (get) Token: 0x06004638 RID: 17976 RVA: 0x00114A90 File Offset: 0x00112C90
			public int Count
			{
				get
				{
					return this.hwnds.Count;
				}
			}

			// Token: 0x06004639 RID: 17977 RVA: 0x00114AA0 File Offset: 0x00112CA0
			public void Enqueue(Hwnd hwnd)
			{
				this.hwnds.Add(hwnd);
			}

			// Token: 0x0600463A RID: 17978 RVA: 0x00114AB0 File Offset: 0x00112CB0
			public void Remove(Hwnd hwnd)
			{
				if (!hwnd.expose_pending && !hwnd.nc_expose_pending)
				{
					this.hwnds.Remove(hwnd);
				}
			}

			// Token: 0x0600463B RID: 17979 RVA: 0x00114AE0 File Offset: 0x00112CE0
			public XEvent Dequeue()
			{
				if (this.hwnds.Count == 0)
				{
					this.xevent.ExposeEvent.window = IntPtr.Zero;
					return this.xevent;
				}
				IEnumerator enumerator = this.hwnds.GetEnumerator();
				enumerator.MoveNext();
				Hwnd hwnd = (Hwnd)enumerator.Current;
				if (!hwnd.nc_expose_pending || !hwnd.expose_pending)
				{
					this.hwnds.Remove(hwnd);
				}
				if (hwnd.expose_pending)
				{
					this.xevent.ExposeEvent.window = hwnd.client_window;
					return this.xevent;
				}
				this.xevent.ExposeEvent.window = hwnd.whole_window;
				this.xevent.ExposeEvent.x = hwnd.nc_invalid.X;
				this.xevent.ExposeEvent.y = hwnd.nc_invalid.Y;
				this.xevent.ExposeEvent.width = hwnd.nc_invalid.Width;
				this.xevent.ExposeEvent.height = hwnd.nc_invalid.Height;
				return this.xevent;
			}

			// Token: 0x040022C4 RID: 8900
			private ArrayList hwnds;

			// Token: 0x040022C5 RID: 8901
			private XEvent xevent;
		}

		// Token: 0x02000445 RID: 1093
		private class XQueue
		{
			// Token: 0x0600463C RID: 17980 RVA: 0x00114C0C File Offset: 0x00112E0C
			public XQueue(int size)
			{
				this.xevents = new XEvent[size];
			}

			// Token: 0x170011E9 RID: 4585
			// (get) Token: 0x0600463D RID: 17981 RVA: 0x00114C20 File Offset: 0x00112E20
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x0600463E RID: 17982 RVA: 0x00114C28 File Offset: 0x00112E28
			public void Enqueue(XEvent xevent)
			{
				if (this.size == this.xevents.Length)
				{
					this.Grow();
				}
				this.xevents[this.tail] = xevent;
				this.tail = (this.tail + 1) % this.xevents.Length;
				this.size++;
			}

			// Token: 0x0600463F RID: 17983 RVA: 0x00114C8C File Offset: 0x00112E8C
			public XEvent Dequeue()
			{
				if (this.size < 1)
				{
					throw new Exception("Attempt to dequeue empty queue.");
				}
				XEvent xevent = this.xevents[this.head];
				this.head = (this.head + 1) % this.xevents.Length;
				this.size--;
				return xevent;
			}

			// Token: 0x06004640 RID: 17984 RVA: 0x00114CF0 File Offset: 0x00112EF0
			public XEvent Peek()
			{
				if (this.size < 1)
				{
					throw new Exception("Attempt to peek at empty queue");
				}
				return this.xevents[this.head];
			}

			// Token: 0x06004641 RID: 17985 RVA: 0x00114D20 File Offset: 0x00112F20
			private void Grow()
			{
				int num = this.xevents.Length * 2;
				XEvent[] array = new XEvent[num];
				this.xevents.CopyTo(array, 0);
				this.xevents = array;
				this.head = 0;
				this.tail = this.head + this.size;
			}

			// Token: 0x040022C6 RID: 8902
			private XEvent[] xevents;

			// Token: 0x040022C7 RID: 8903
			private int head;

			// Token: 0x040022C8 RID: 8904
			private int tail;

			// Token: 0x040022C9 RID: 8905
			private int size;
		}
	}
}
