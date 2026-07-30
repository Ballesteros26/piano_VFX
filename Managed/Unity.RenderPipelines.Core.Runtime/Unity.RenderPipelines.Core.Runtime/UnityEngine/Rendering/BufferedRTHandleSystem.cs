using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000046 RID: 70
	public class BufferedRTHandleSystem : IDisposable
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public int maxWidth
		{
			get
			{
				return this.m_RTHandleSystem.GetMaxWidth();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007AF5 File Offset: 0x00005CF5
		public int maxHeight
		{
			get
			{
				return this.m_RTHandleSystem.GetMaxHeight();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007B02 File Offset: 0x00005D02
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_RTHandleSystem.rtHandleProperties;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007B0F File Offset: 0x00005D0F
		public RTHandle GetFrameRT(int bufferId, int frameIndex)
		{
			if (!this.m_RTHandles.ContainsKey(bufferId))
			{
				return null;
			}
			return this.m_RTHandles[bufferId][frameIndex];
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007B30 File Offset: 0x00005D30
		public void AllocBuffer(int bufferId, Func<RTHandleSystem, int, RTHandle> allocator, int bufferCount)
		{
			RTHandle[] array = new RTHandle[bufferCount];
			this.m_RTHandles.Add(bufferId, array);
			array[0] = allocator(this.m_RTHandleSystem, 0);
			int i = 1;
			int num = array.Length;
			while (i < num)
			{
				array[i] = allocator(this.m_RTHandleSystem, i);
				this.m_RTHandleSystem.SwitchResizeMode(array[i], RTHandleSystem.ResizeMode.OnDemand);
				i++;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007B90 File Offset: 0x00005D90
		public void ReleaseBuffer(int bufferId)
		{
			RTHandle[] array;
			if (this.m_RTHandles.TryGetValue(bufferId, out array))
			{
				foreach (RTHandle rthandle in array)
				{
					this.m_RTHandleSystem.Release(rthandle);
				}
			}
			this.m_RTHandles.Remove(bufferId);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007BDA File Offset: 0x00005DDA
		public void SwapAndSetReferenceSize(int width, int height, MSAASamples msaaSamples)
		{
			this.Swap();
			this.m_RTHandleSystem.SetReferenceSize(width, height, msaaSamples);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007BF0 File Offset: 0x00005DF0
		private void Swap()
		{
			foreach (KeyValuePair<int, RTHandle[]> keyValuePair in this.m_RTHandles)
			{
				if (keyValuePair.Value.Length > 1)
				{
					RTHandle rthandle = keyValuePair.Value[keyValuePair.Value.Length - 1];
					int i = 0;
					int num = keyValuePair.Value.Length - 1;
					while (i < num)
					{
						keyValuePair.Value[i + 1] = keyValuePair.Value[i];
						i++;
					}
					keyValuePair.Value[0] = rthandle;
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[0], RTHandleSystem.ResizeMode.Auto);
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[1], RTHandleSystem.ResizeMode.OnDemand);
				}
				else
				{
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[0], RTHandleSystem.ResizeMode.Auto);
				}
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007CDC File Offset: 0x00005EDC
		private void Dispose(bool disposing)
		{
			if (!this.m_DisposedValue)
			{
				if (disposing)
				{
					this.ReleaseAll();
					this.m_RTHandleSystem.Dispose();
					this.m_RTHandleSystem = null;
				}
				this.m_DisposedValue = true;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007D08 File Offset: 0x00005F08
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007D14 File Offset: 0x00005F14
		public void ReleaseAll()
		{
			foreach (KeyValuePair<int, RTHandle[]> keyValuePair in this.m_RTHandles)
			{
				int i = 0;
				int num = keyValuePair.Value.Length;
				while (i < num)
				{
					this.m_RTHandleSystem.Release(keyValuePair.Value[i]);
					i++;
				}
			}
			this.m_RTHandles.Clear();
		}

		// Token: 0x0400012B RID: 299
		private Dictionary<int, RTHandle[]> m_RTHandles = new Dictionary<int, RTHandle[]>();

		// Token: 0x0400012C RID: 300
		private RTHandleSystem m_RTHandleSystem = new RTHandleSystem();

		// Token: 0x0400012D RID: 301
		private bool m_DisposedValue;
	}
}
