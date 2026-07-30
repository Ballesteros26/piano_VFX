using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NatSuite.Recorders.Internal;
using UnityEngine;

namespace NatSuite.Recorders
{
	// Token: 0x02000044 RID: 68
	[Doc("JPGRecorder")]
	public sealed class JPGRecorder : IMediaRecorder
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00013ED3 File Offset: 0x000120D3
		[TupleElementNames(new string[] { "width", "height" })]
		[Doc("FrameSize")]
		public ValueTuple<int, int> frameSize
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get
			{
				return new ValueTuple<int, int>(this.framebuffer.width, this.framebuffer.height);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00013EF0 File Offset: 0x000120F0
		[Doc("JPGRecorderCtor")]
		public JPGRecorder(int imageWidth, int imageHeight)
		{
			this.framebuffer = new Texture2D(imageWidth, imageHeight, TextureFormat.RGBA32, false, false);
			this.writeQueue = new Queue<byte[]>();
			string recordingPath = Utility.GetPath(string.Empty);
			Directory.CreateDirectory(recordingPath);
			this.recordingTask = Task.Run<string>(delegate
			{
				int num = 0;
				for (;;)
				{
					object syncRoot = ((ICollection)this.writeQueue).SyncRoot;
					byte[] array;
					lock (syncRoot)
					{
						if (this.writeQueue.Count <= 0)
						{
							continue;
						}
						array = this.writeQueue.Dequeue();
					}
					if (array == null)
					{
						break;
					}
					File.WriteAllBytes(Path.Combine(recordingPath, ++num + ".jpg"), array);
				}
				return recordingPath;
			});
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00013F60 File Offset: 0x00012160
		[Doc("CommitFrame")]
		public void CommitFrame<T>(T[] pixelBuffer, long timestamp) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(pixelBuffer, GCHandleType.Pinned);
			this.CommitFrame(gchandle.AddrOfPinnedObject(), timestamp);
			gchandle.Free();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00013F8C File Offset: 0x0001218C
		[Doc("CommitFrame")]
		public void CommitFrame(IntPtr nativeBuffer, long timestamp)
		{
			this.framebuffer.LoadRawTextureData(nativeBuffer, this.framebuffer.width * this.framebuffer.height * 4);
			byte[] array = this.framebuffer.EncodeToJPG();
			object syncRoot = ((ICollection)this.writeQueue).SyncRoot;
			lock (syncRoot)
			{
				this.writeQueue.Enqueue(array);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00013E17 File Offset: 0x00012017
		[Doc("CommitSamplesNotSupported")]
		public void CommitSamples(float[] sampleBuffer, long timestamp)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00014008 File Offset: 0x00012208
		[Doc("FinishWriting", "FinishWritingDiscussion")]
		public Task<string> FinishWriting()
		{
			object syncRoot = ((ICollection)this.writeQueue).SyncRoot;
			lock (syncRoot)
			{
				this.writeQueue.Enqueue(null);
			}
			global::UnityEngine.Object.Destroy(this.framebuffer);
			return this.recordingTask;
		}

		// Token: 0x040003D8 RID: 984
		private readonly Texture2D framebuffer;

		// Token: 0x040003D9 RID: 985
		private readonly Queue<byte[]> writeQueue;

		// Token: 0x040003DA RID: 986
		private readonly Task<string> recordingTask;
	}
}
