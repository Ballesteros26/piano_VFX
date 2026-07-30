using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x020000AB RID: 171
	internal class HttpResponseStream : Stream
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x00016080 File Offset: 0x00014280
		public HttpResponseStream(HttpResponse response)
		{
			this.response = response;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001609C File Offset: 0x0001429C
		internal bool HaveFilter
		{
			get
			{
				return this.filter != null;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000160A7 File Offset: 0x000142A7
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x000160C3 File Offset: 0x000142C3
		public Stream Filter
		{
			get
			{
				if (this.filter == null)
				{
					this.filter = new OutputFilterStream(this);
				}
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000160CC File Offset: 0x000142CC
		private void AppendBucket(HttpResponseStream.Bucket b)
		{
			if (this.first_bucket == null)
			{
				this.first_bucket = b;
				this.cur_bucket = b;
				return;
			}
			this.cur_bucket.Next = b;
			this.cur_bucket = b;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void Flush()
		{
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00016108 File Offset: 0x00014308
		private void SendChunkSize(long l, bool last)
		{
			if (l == 0L && !last)
			{
				return;
			}
			int i = 0;
			if (l >= 0L)
			{
				string text = l.ToString("x");
				while (i < text.Length)
				{
					this.chunk_buffer[i] = (byte)text[i];
					i++;
				}
			}
			this.chunk_buffer[i++] = 13;
			this.chunk_buffer[i++] = 10;
			if (last)
			{
				this.chunk_buffer[i++] = 13;
				this.chunk_buffer[i++] = 10;
			}
			this.response.WorkerRequest.SendResponseFromMemory(this.chunk_buffer, i);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000161A4 File Offset: 0x000143A4
		internal void Flush(HttpWorkerRequest wr, bool final_flush)
		{
			if (this.total == 0L && !final_flush)
			{
				return;
			}
			if (this.response.use_chunked)
			{
				this.SendChunkSize(this.total, false);
			}
			for (HttpResponseStream.Bucket next = this.first_bucket; next != null; next = next.Next)
			{
				next.Send(wr);
			}
			if (this.response.use_chunked)
			{
				this.SendChunkSize(-1L, false);
				if (final_flush)
				{
					this.SendChunkSize(0L, true);
				}
			}
			wr.FlushResponse(final_flush);
			this.Clear();
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00016220 File Offset: 0x00014420
		internal int GetTotalLength()
		{
			int num = 0;
			for (HttpResponseStream.Bucket next = this.first_bucket; next != null; next = next.Next)
			{
				num += next.Length;
			}
			return num;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001624C File Offset: 0x0001444C
		internal MemoryStream GetData()
		{
			MemoryStream memoryStream = new MemoryStream();
			for (HttpResponseStream.Bucket next = this.first_bucket; next != null; next = next.Next)
			{
				next.Send(memoryStream);
			}
			return memoryStream;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001627C File Offset: 0x0001447C
		public void WriteFile(string f, long offset, long length)
		{
			if (length == 0L)
			{
				return;
			}
			HttpResponseStream.ByteBucket byteBucket = this.cur_bucket as HttpResponseStream.ByteBucket;
			if (byteBucket != null)
			{
				byteBucket.Expandable = false;
				byteBucket = new HttpResponseStream.ByteBucket(byteBucket.blocks);
			}
			this.total += length;
			this.AppendBucket(new HttpResponseStream.BufferedFileBucket(f, offset, length));
			if (byteBucket != null)
			{
				this.AppendBucket(byteBucket);
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000162D8 File Offset: 0x000144D8
		internal void ApplyFilter(bool close)
		{
			if (this.filter == null)
			{
				return;
			}
			this.filtering = true;
			HttpResponseStream.Bucket bucket = this.first_bucket;
			this.first_bucket = null;
			this.cur_bucket = null;
			this.total = 0L;
			for (HttpResponseStream.Bucket bucket2 = bucket; bucket2 != null; bucket2 = bucket2.Next)
			{
				bucket2.Send(this.filter);
			}
			for (HttpResponseStream.Bucket bucket3 = bucket; bucket3 != null; bucket3 = bucket3.Next)
			{
				bucket3.Dispose();
			}
			if (close)
			{
				this.filter.Flush();
				this.filter.Close();
				this.filter = null;
			}
			else
			{
				this.filter.Flush();
			}
			this.filtering = false;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00016374 File Offset: 0x00014574
		public void WritePtr(IntPtr ptr, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (this.response.BufferOutput)
			{
				this.AppendBuffer(ptr, length);
				return;
			}
			if (this.filter == null || this.filtering)
			{
				this.response.WriteHeaders(false);
				HttpWorkerRequest workerRequest = this.response.WorkerRequest;
				workerRequest.SendResponseFromMemory(ptr, length);
				workerRequest.FlushResponse(false);
				return;
			}
			this.filtering = true;
			try
			{
				byte[] array = new byte[length];
				Marshal.Copy(ptr, array, 0, length);
				this.filter.Write(array, 0, length);
			}
			finally
			{
				this.filtering = false;
			}
			this.Flush(this.response.WorkerRequest, false);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00016424 File Offset: 0x00014624
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool bufferOutput = this.response.BufferOutput;
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length - offset;
			if (offset < 0 || num <= 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > num)
			{
				count = num;
			}
			if (bufferOutput)
			{
				this.AppendBuffer(buffer, offset, count);
				return;
			}
			if (this.filter == null || this.filtering)
			{
				this.response.WriteHeaders(false);
				HttpWorkerRequest workerRequest = this.response.WorkerRequest;
				if (offset == 0)
				{
					workerRequest.SendResponseFromMemory(buffer, count);
				}
				else
				{
					this.UnsafeWrite(workerRequest, buffer, offset, count);
				}
				workerRequest.FlushResponse(false);
				return;
			}
			this.filtering = true;
			try
			{
				this.filter.Write(buffer, offset, count);
			}
			finally
			{
				this.filtering = false;
			}
			this.Flush(this.response.WorkerRequest, false);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00016510 File Offset: 0x00014710
		private unsafe void UnsafeWrite(HttpWorkerRequest wr, byte[] buffer, int offset, int count)
		{
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				wr.SendResponseFromMemory((IntPtr)((void*)(ptr + offset)), count);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00016547 File Offset: 0x00014747
		private void AppendBuffer(byte[] buffer, int offset, int count)
		{
			if (!(this.cur_bucket is HttpResponseStream.ByteBucket))
			{
				this.AppendBucket(new HttpResponseStream.ByteBucket());
			}
			this.total += (long)count;
			((HttpResponseStream.ByteBucket)this.cur_bucket).Write(buffer, offset, count);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00016584 File Offset: 0x00014784
		private void AppendBuffer(IntPtr ptr, int count)
		{
			if (!(this.cur_bucket is HttpResponseStream.ByteBucket))
			{
				this.AppendBucket(new HttpResponseStream.ByteBucket());
			}
			this.total += (long)count;
			((HttpResponseStream.ByteBucket)this.cur_bucket).Write(ptr, count);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000165C0 File Offset: 0x000147C0
		internal void ReleaseResources(bool close_filter)
		{
			if (close_filter && this.filter != null)
			{
				this.filter.Close();
				this.filter = null;
			}
			for (HttpResponseStream.Bucket next = this.first_bucket; next != null; next = next.Next)
			{
				next.Dispose();
			}
			this.first_bucket = null;
			this.cur_bucket = null;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00016611 File Offset: 0x00014811
		public void Clear()
		{
			this.ReleaseResources(false);
			this.total = 0L;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00016624 File Offset: 0x00014824
		private unsafe static void memcpy4(byte* dest, byte* src, int size)
		{
			while (size >= 16)
			{
				*(int*)dest = *(int*)src;
				*(int*)(dest + 4) = *(int*)(src + 4);
				*(int*)(dest + (IntPtr)2 * 4) = *(int*)(src + (IntPtr)2 * 4);
				*(int*)(dest + (IntPtr)3 * 4) = *(int*)(src + (IntPtr)3 * 4);
				dest += 16;
				src += 16;
				size -= 16;
			}
			while (size >= 4)
			{
				*(int*)dest = *(int*)src;
				dest += 4;
				src += 4;
				size -= 4;
			}
			while (size > 0)
			{
				*dest = *src;
				dest++;
				src++;
				size--;
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000166A4 File Offset: 0x000148A4
		private unsafe static void memcpy2(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*(short*)dest = *(short*)src;
				*(short*)(dest + 2) = *(short*)(src + 2);
				*(short*)(dest + (IntPtr)2 * 2) = *(short*)(src + (IntPtr)2 * 2);
				*(short*)(dest + (IntPtr)3 * 2) = *(short*)(src + (IntPtr)3 * 2);
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*(short*)dest = *(short*)src;
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00016710 File Offset: 0x00014910
		private unsafe static void memcpy1(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*dest = *src;
				dest[1] = src[1];
				dest[2] = src[2];
				dest[3] = src[3];
				dest[4] = src[4];
				dest[5] = src[5];
				dest[6] = src[6];
				dest[7] = src[7];
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*dest = *src;
				dest[1] = src[1];
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00016798 File Offset: 0x00014998
		private unsafe static void memcpy(byte* dest, byte* src, int size)
		{
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 1) != 0 && (src & 1) != 0 && size >= 1)
				{
					*dest = *src;
					dest++;
					src++;
					size--;
				}
				if ((dest & 2) != 0 && (src & 2) != 0 && size >= 2)
				{
					*(short*)dest = *(short*)src;
					dest += 2;
					src += 2;
					size -= 2;
				}
				if (((dest | src) & 1) != 0)
				{
					HttpResponseStream.memcpy1(dest, src, size);
					return;
				}
				if (((dest | src) & 2) != 0)
				{
					HttpResponseStream.memcpy2(dest, src, size);
					return;
				}
			}
			HttpResponseStream.memcpy4(dest, src, size);
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00016820 File Offset: 0x00014A20
		public override long Length
		{
			get
			{
				throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00016820 File Offset: 0x00014A20
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x00016820 File Offset: 0x00014A20
		public override long Position
		{
			get
			{
				throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
			}
			set
			{
				throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00016820 File Offset: 0x00014A20
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00016820 File Offset: 0x00014A20
		public override void SetLength(long value)
		{
			throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00016820 File Offset: 0x00014A20
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
		}

		// Token: 0x04000FEE RID: 4078
		private HttpResponseStream.Bucket first_bucket;

		// Token: 0x04000FEF RID: 4079
		private HttpResponseStream.Bucket cur_bucket;

		// Token: 0x04000FF0 RID: 4080
		private HttpResponse response;

		// Token: 0x04000FF1 RID: 4081
		internal long total;

		// Token: 0x04000FF2 RID: 4082
		private Stream filter;

		// Token: 0x04000FF3 RID: 4083
		private byte[] chunk_buffer = new byte[24];

		// Token: 0x04000FF4 RID: 4084
		private bool filtering;

		// Token: 0x04000FF5 RID: 4085
		private const string notsupported = "HttpResponseStream is a forward, write-only stream";

		// Token: 0x020000AC RID: 172
		private sealed class BlockManager
		{
			// Token: 0x1700037F RID: 895
			// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001682C File Offset: 0x00014A2C
			public int Position
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x0600091F RID: 2335 RVA: 0x00016834 File Offset: 0x00014A34
			private unsafe void EnsureCapacity(int capacity)
			{
				if (this.block_size >= capacity)
				{
					return;
				}
				capacity += 131072;
				capacity = capacity / 131072 * 131072;
				this.data = (byte*)((this.data == null) ? ((void*)Marshal.AllocHGlobal(capacity)) : ((void*)Marshal.ReAllocHGlobal((IntPtr)((void*)this.data), (IntPtr)capacity)));
				this.block_size = capacity;
			}

			// Token: 0x06000920 RID: 2336 RVA: 0x000168A2 File Offset: 0x00014AA2
			public unsafe void Write(byte[] buffer, int offset, int count)
			{
				if (count == 0)
				{
					return;
				}
				this.EnsureCapacity(this.position + count);
				Marshal.Copy(buffer, offset, (IntPtr)((void*)(this.data + this.position)), count);
				this.position += count;
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x000168E0 File Offset: 0x00014AE0
			public unsafe void Write(IntPtr ptr, int count)
			{
				if (count == 0)
				{
					return;
				}
				this.EnsureCapacity(this.position + count);
				byte* ptr2 = (byte*)ptr.ToPointer();
				if (count < 32)
				{
					byte* ptr3 = this.data + this.position;
					for (int i = 0; i < count; i++)
					{
						*(ptr3++) = *(ptr2++);
					}
				}
				else
				{
					HttpResponseStream.memcpy(this.data + this.position, ptr2, count);
				}
				this.position += count;
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x00016956 File Offset: 0x00014B56
			public unsafe void Send(HttpWorkerRequest wr, int start, int end)
			{
				if (end - start <= 0)
				{
					return;
				}
				wr.SendResponseFromMemory((IntPtr)((void*)(this.data + start)), end - start);
			}

			// Token: 0x06000923 RID: 2339 RVA: 0x00016978 File Offset: 0x00014B78
			public unsafe void Send(Stream stream, int start, int end)
			{
				int i = end - start;
				if (i <= 0)
				{
					return;
				}
				byte[] array = new byte[Math.Min(i, 32768)];
				int num = array.Length;
				while (i > 0)
				{
					Marshal.Copy((IntPtr)((void*)(this.data + start)), array, 0, num);
					stream.Write(array, 0, num);
					start += num;
					i -= num;
					if (i > 0 && i < num)
					{
						num = i;
					}
				}
			}

			// Token: 0x06000924 RID: 2340 RVA: 0x000169DA File Offset: 0x00014BDA
			public unsafe void Dispose()
			{
				if ((IntPtr)((void*)this.data) != IntPtr.Zero)
				{
					Marshal.FreeHGlobal((IntPtr)((void*)this.data));
					this.data = (byte*)(void*)IntPtr.Zero;
				}
			}

			// Token: 0x04000FF6 RID: 4086
			private const int PreferredLength = 131072;

			// Token: 0x04000FF7 RID: 4087
			private unsafe byte* data;

			// Token: 0x04000FF8 RID: 4088
			private int position;

			// Token: 0x04000FF9 RID: 4089
			private int block_size;
		}

		// Token: 0x020000AD RID: 173
		private abstract class Bucket
		{
			// Token: 0x06000925 RID: 2341 RVA: 0x0000393A File Offset: 0x00001B3A
			public virtual void Dispose()
			{
			}

			// Token: 0x06000926 RID: 2342
			public abstract void Send(HttpWorkerRequest wr);

			// Token: 0x06000927 RID: 2343
			public abstract void Send(Stream stream);

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06000928 RID: 2344
			public abstract int Length { get; }

			// Token: 0x04000FFA RID: 4090
			public HttpResponseStream.Bucket Next;
		}

		// Token: 0x020000AE RID: 174
		private class ByteBucket : HttpResponseStream.Bucket
		{
			// Token: 0x0600092A RID: 2346 RVA: 0x00016A13 File Offset: 0x00014C13
			public ByteBucket()
				: this(null)
			{
			}

			// Token: 0x0600092B RID: 2347 RVA: 0x00016A1C File Offset: 0x00014C1C
			public ByteBucket(HttpResponseStream.BlockManager blocks)
			{
				if (blocks == null)
				{
					blocks = new HttpResponseStream.BlockManager();
				}
				this.blocks = blocks;
				this.start = blocks.Position;
			}

			// Token: 0x17000381 RID: 897
			// (get) Token: 0x0600092C RID: 2348 RVA: 0x00016A48 File Offset: 0x00014C48
			public override int Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x0600092D RID: 2349 RVA: 0x00016A50 File Offset: 0x00014C50
			public unsafe int Write(byte[] buf, int offset, int count)
			{
				if (!this.Expandable)
				{
					throw new Exception("This should not happen.");
				}
				fixed (byte* ptr = &buf[0])
				{
					byte* ptr2 = ptr;
					IntPtr intPtr = new IntPtr((void*)(ptr2 + offset));
					this.blocks.Write(intPtr, count);
				}
				this.length += count;
				return count;
			}

			// Token: 0x0600092E RID: 2350 RVA: 0x00016AA4 File Offset: 0x00014CA4
			public int Write(IntPtr ptr, int count)
			{
				if (!this.Expandable)
				{
					throw new Exception("This should not happen.");
				}
				this.blocks.Write(ptr, count);
				this.length += count;
				return count;
			}

			// Token: 0x0600092F RID: 2351 RVA: 0x00016AD5 File Offset: 0x00014CD5
			public override void Dispose()
			{
				this.blocks.Dispose();
			}

			// Token: 0x06000930 RID: 2352 RVA: 0x00016AE2 File Offset: 0x00014CE2
			public override void Send(HttpWorkerRequest wr)
			{
				if (this.length == 0)
				{
					return;
				}
				this.blocks.Send(wr, this.start, this.length);
			}

			// Token: 0x06000931 RID: 2353 RVA: 0x00016B05 File Offset: 0x00014D05
			public override void Send(Stream stream)
			{
				if (this.length == 0)
				{
					return;
				}
				this.blocks.Send(stream, this.start, this.length);
			}

			// Token: 0x04000FFB RID: 4091
			private int start;

			// Token: 0x04000FFC RID: 4092
			private int length;

			// Token: 0x04000FFD RID: 4093
			public HttpResponseStream.BlockManager blocks;

			// Token: 0x04000FFE RID: 4094
			public bool Expandable = true;
		}

		// Token: 0x020000AF RID: 175
		private class BufferedFileBucket : HttpResponseStream.Bucket
		{
			// Token: 0x06000932 RID: 2354 RVA: 0x00016B28 File Offset: 0x00014D28
			public BufferedFileBucket(string f, long off, long len)
			{
				this.file = f;
				this.offset = off;
				this.length = len;
			}

			// Token: 0x17000382 RID: 898
			// (get) Token: 0x06000933 RID: 2355 RVA: 0x00016B45 File Offset: 0x00014D45
			public override int Length
			{
				get
				{
					return (int)this.length;
				}
			}

			// Token: 0x06000934 RID: 2356 RVA: 0x00016B4E File Offset: 0x00014D4E
			public override void Send(HttpWorkerRequest wr)
			{
				wr.SendResponseFromFile(this.file, this.offset, this.length);
			}

			// Token: 0x06000935 RID: 2357 RVA: 0x00016B68 File Offset: 0x00014D68
			public override void Send(Stream stream)
			{
				using (FileStream fileStream = File.OpenRead(this.file))
				{
					byte[] array = new byte[Math.Min(fileStream.Length, 32768L)];
					long num = fileStream.Length;
					int num2;
					while (num > 0L && (num2 = fileStream.Read(array, 0, (int)Math.Min(num, 32768L))) != 0)
					{
						num -= (long)num2;
						stream.Write(array, 0, num2);
					}
				}
			}

			// Token: 0x06000936 RID: 2358 RVA: 0x00016BEC File Offset: 0x00014DEC
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"file ",
					this.file,
					" ",
					this.length.ToString(),
					" bytes from position ",
					this.offset.ToString()
				});
			}

			// Token: 0x04000FFF RID: 4095
			private string file;

			// Token: 0x04001000 RID: 4096
			private long offset;

			// Token: 0x04001001 RID: 4097
			private long length;
		}
	}
}
