using System;
using System.Collections;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000144 RID: 324
	internal class WindowCollection : DOMObject, IWindowCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00009F92 File Offset: 0x00008192
		public WindowCollection(WebBrowser control, nsIDOMWindowCollection windowCol)
			: base(control)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedWindows = nsDOMWindowCollection.GetProxy(control, windowCol);
				return;
			}
			this.unmanagedWindows = windowCol;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00009FBE File Offset: 0x000081BE
		public WindowCollection(WebBrowser control)
			: base(control)
		{
			this.windows = new Window[0];
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00009FD3 File Offset: 0x000081D3
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.Clear();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00009FF0 File Offset: 0x000081F0
		protected void Clear()
		{
			if (this.windows != null)
			{
				for (int i = 0; i < this.windowCount; i++)
				{
					this.windows[i] = null;
				}
				this.windowCount = 0;
				this.windows = null;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0000A030 File Offset: 0x00008230
		internal void Load()
		{
			this.Clear();
			uint num;
			this.unmanagedWindows.getLength(out num);
			Window[] array = new Window[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				nsIDOMWindow nsIDOMWindow;
				this.unmanagedWindows.item((uint)num2, out nsIDOMWindow);
				Window[] array2 = array;
				int num3 = this.windowCount;
				this.windowCount = num3 + 1;
				array2[num3] = new Window(this.control, nsIDOMWindow);
				num2++;
			}
			this.windows = new Window[this.windowCount];
			Array.Copy(array, this.windows, this.windowCount);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0000A0BA File Offset: 0x000082BA
		public IEnumerator GetEnumerator()
		{
			return new WindowCollection.WindowEnumerator(this);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0000A0C2 File Offset: 0x000082C2
		public void CopyTo(Array dest, int index)
		{
			if (this.windows != null)
			{
				Array.Copy(this.windows, 0, dest, index, this.windowCount);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0000A0E0 File Offset: 0x000082E0
		public int Count
		{
			get
			{
				if (this.unmanagedWindows != null && this.windows == null)
				{
					this.Load();
				}
				return this.windowCount;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0000A0FE File Offset: 0x000082FE
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0000A101 File Offset: 0x00008301
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0000A104 File Offset: 0x00008304
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0000A107 File Offset: 0x00008307
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0000A10A File Offset: 0x0000830A
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0000A114 File Offset: 0x00008314
		public void RemoveAt(int index)
		{
			if (index > this.windowCount || index < 0)
			{
				return;
			}
			Array.Copy(this.windows, index + 1, this.windows, index, this.windowCount - index - 1);
			this.windowCount--;
			this.windows[this.windowCount] = null;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000A16A File Offset: 0x0000836A
		public void Remove(IWindow window)
		{
			this.RemoveAt(this.IndexOf(window));
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0000A179 File Offset: 0x00008379
		void IList.Remove(object window)
		{
			this.Remove(window as IWindow);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0000A188 File Offset: 0x00008388
		public void Insert(int index, IWindow value)
		{
			if (index > this.windowCount)
			{
				index = this.windowCount;
			}
			IWindow[] array = new Window[this.windowCount + 1];
			if (index > 0)
			{
				Array.Copy(this.windows, 0, array, 0, index);
			}
			array[index] = value;
			if (index < this.windowCount)
			{
				Array.Copy(this.windows, index, array, index + 1, this.windowCount - index);
			}
			this.windows = array;
			this.windowCount++;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0000A201 File Offset: 0x00008401
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as IWindow);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0000A210 File Offset: 0x00008410
		public int IndexOf(IWindow window)
		{
			return Array.IndexOf<IWindow>(this.windows, window);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0000A21E File Offset: 0x0000841E
		int IList.IndexOf(object window)
		{
			return this.IndexOf(window as IWindow);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0000A22C File Offset: 0x0000842C
		public bool Contains(IWindow window)
		{
			return this.IndexOf(window) != -1;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0000A23B File Offset: 0x0000843B
		bool IList.Contains(object window)
		{
			return this.Contains(window as IWindow);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0000A249 File Offset: 0x00008449
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0000A251 File Offset: 0x00008451
		public int Add(IWindow window)
		{
			this.Insert(this.windowCount + 1, window);
			return this.windowCount - 1;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000A26A File Offset: 0x0000846A
		int IList.Add(object window)
		{
			return this.Add(window as IWindow);
		}

		// Token: 0x1700011B RID: 283
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value as IWindow;
			}
		}

		// Token: 0x1700011C RID: 284
		public IWindow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.windowCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.windows[index];
			}
			set
			{
				if (index < 0 || index >= this.windowCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.windows[index] = value;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0000A2D5 File Offset: 0x000084D5
		public override int GetHashCode()
		{
			if (this.unmanagedWindows != null)
			{
				return this.unmanagedWindows.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x04000136 RID: 310
		protected nsIDOMWindowCollection unmanagedWindows;

		// Token: 0x04000137 RID: 311
		protected IWindow[] windows;

		// Token: 0x04000138 RID: 312
		protected int windowCount;

		// Token: 0x0200014D RID: 333
		internal class WindowEnumerator : IEnumerator
		{
			// Token: 0x06000A79 RID: 2681 RVA: 0x0000A3A9 File Offset: 0x000085A9
			public WindowEnumerator(WindowCollection collection)
			{
				this.collection = collection;
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0000A3BF File Offset: 0x000085BF
			public object Current
			{
				get
				{
					if (this.index == -1)
					{
						return null;
					}
					return this.collection[this.index];
				}
			}

			// Token: 0x06000A7B RID: 2683 RVA: 0x0000A3DD File Offset: 0x000085DD
			public bool MoveNext()
			{
				if (this.index + 1 >= this.collection.Count)
				{
					return false;
				}
				this.index++;
				return true;
			}

			// Token: 0x06000A7C RID: 2684 RVA: 0x0000A405 File Offset: 0x00008605
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04000171 RID: 369
			private WindowCollection collection;

			// Token: 0x04000172 RID: 370
			private int index = -1;
		}
	}
}
