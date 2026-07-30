using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200024E RID: 590
	internal class SNIPacket : IDisposable, IEquatable<SNIPacket>
	{
		// Token: 0x06001A1C RID: 6684 RVA: 0x00084498 File Offset: 0x00082698
		public SNIPacket(SNIHandle handle)
		{
			this._offset = 0;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x000844A7 File Offset: 0x000826A7
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x000844AF File Offset: 0x000826AF
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000844B8 File Offset: 0x000826B8
		public int DataLeft
		{
			get
			{
				return this._length - this._offset;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000844C7 File Offset: 0x000826C7
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000844CF File Offset: 0x000826CF
		public bool IsInvalid
		{
			get
			{
				return this._data == null;
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000844DA File Offset: 0x000826DA
		public void Dispose()
		{
			this._data = null;
			this._length = 0;
			this._capacity = 0;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000844F1 File Offset: 0x000826F1
		public void SetCompletionCallback(SNIAsyncCallback completionCallback)
		{
			this._completionCallback = completionCallback;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000844FA File Offset: 0x000826FA
		public void InvokeCompletionCallback(uint sniErrorCode)
		{
			this._completionCallback(this, sniErrorCode);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00084509 File Offset: 0x00082709
		public void Allocate(int capacity)
		{
			this._capacity = capacity;
			this._data = new byte[capacity];
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00084520 File Offset: 0x00082720
		public SNIPacket Clone()
		{
			SNIPacket snipacket = new SNIPacket(null);
			snipacket._data = new byte[this._length];
			Buffer.BlockCopy(this._data, 0, snipacket._data, 0, this._length);
			snipacket._length = this._length;
			return snipacket;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0008456B File Offset: 0x0008276B
		public void GetData(byte[] buffer, ref int dataSize)
		{
			Buffer.BlockCopy(this._data, 0, buffer, 0, this._length);
			dataSize = this._length;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00084589 File Offset: 0x00082789
		public void SetData(byte[] data, int length)
		{
			this._data = data;
			this._length = length;
			this._capacity = length;
			this._offset = 0;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000845A8 File Offset: 0x000827A8
		public int TakeData(SNIPacket packet, int size)
		{
			int num = this.TakeData(packet._data, packet._length, size);
			packet._length += num;
			return num;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000845D8 File Offset: 0x000827D8
		public void AppendData(byte[] data, int size)
		{
			Buffer.BlockCopy(data, 0, this._data, this._length, size);
			this._length += size;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000845FC File Offset: 0x000827FC
		public void AppendPacket(SNIPacket packet)
		{
			Buffer.BlockCopy(packet._data, 0, this._data, this._length, packet._length);
			this._length += packet._length;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00084630 File Offset: 0x00082830
		public int TakeData(byte[] buffer, int dataOffset, int size)
		{
			if (this._offset >= this._length)
			{
				return 0;
			}
			if (this._offset + size > this._length)
			{
				size = this._length - this._offset;
			}
			Buffer.BlockCopy(this._data, this._offset, buffer, dataOffset, size);
			this._offset += size;
			return size;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0008468F File Offset: 0x0008288F
		public void Release()
		{
			this._length = 0;
			this._capacity = 0;
			this._data = null;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000846A6 File Offset: 0x000828A6
		public void Reset()
		{
			this._length = 0;
			this._data = new byte[this._capacity];
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000846C0 File Offset: 0x000828C0
		public void ReadFromStreamAsync(Stream stream, SNIAsyncCallback callback)
		{
			bool error = false;
			stream.ReadAsync(this._data, 0, this._capacity).ContinueWith(delegate(Task<int> t)
			{
				Exception ex = ((t.Exception != null) ? t.Exception.InnerException : null);
				if (ex != null)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 35U, ex);
					error = true;
				}
				else
				{
					this._length = t.Result;
					if (this._length == 0)
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 0U, 2U, string.Empty);
						error = true;
					}
				}
				if (error)
				{
					this.Release();
				}
				callback(this, error ? 1U : 0U);
			}, CancellationToken.None, TaskContinuationOptions.LongRunning | TaskContinuationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00084719 File Offset: 0x00082919
		public void ReadFromStream(Stream stream)
		{
			this._length = stream.Read(this._data, 0, this._capacity);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00084734 File Offset: 0x00082934
		public void WriteToStream(Stream stream)
		{
			stream.Write(this._data, 0, this._length);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0003515E File Offset: 0x0003335E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0008474C File Offset: 0x0008294C
		public override bool Equals(object obj)
		{
			SNIPacket snipacket = obj as SNIPacket;
			return snipacket != null && this.Equals(snipacket);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0008476C File Offset: 0x0008296C
		public bool Equals(SNIPacket packet)
		{
			return packet != null && packet == this;
		}

		// Token: 0x040012C8 RID: 4808
		private byte[] _data;

		// Token: 0x040012C9 RID: 4809
		private int _length;

		// Token: 0x040012CA RID: 4810
		private int _capacity;

		// Token: 0x040012CB RID: 4811
		private int _offset;

		// Token: 0x040012CC RID: 4812
		private string _description;

		// Token: 0x040012CD RID: 4813
		private SNIAsyncCallback _completionCallback;
	}
}
