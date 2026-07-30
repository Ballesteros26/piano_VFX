using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x020003FB RID: 1019
	internal class SerialPortStream : Stream, ISerialStream, IDisposable
	{
		// Token: 0x06001EEC RID: 7916
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int open_serial(string portName);

		// Token: 0x06001EED RID: 7917 RVA: 0x0007A278 File Offset: 0x00078478
		public SerialPortStream(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, bool dtrEnable, bool rtsEnable, Handshake handshake, int readTimeout, int writeTimeout, int readBufferSize, int writeBufferSize)
		{
			this.fd = SerialPortStream.open_serial(portName);
			if (this.fd == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			this.TryBaudRate(baudRate);
			if (!SerialPortStream.set_attributes(this.fd, baudRate, parity, dataBits, stopBits, handshake))
			{
				SerialPortStream.ThrowIOException();
			}
			this.read_timeout = readTimeout;
			this.write_timeout = writeTimeout;
			this.SetSignal(SerialSignal.Dtr, dtrEnable);
			if (handshake != Handshake.RequestToSend && handshake != Handshake.RequestToSendXOnXOff)
			{
				this.SetSignal(SerialSignal.Rts, rtsEnable);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0007A2F3 File Offset: 0x000784F3
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0007A2FB File Offset: 0x000784FB
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0007A317 File Offset: 0x00078517
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0007A31F File Offset: 0x0007851F
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Flush()
		{
		}

		// Token: 0x06001EFA RID: 7930
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int read_serial(int fd, byte[] buffer, int offset, int count);

		// Token: 0x06001EFB RID: 7931
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern bool poll_serial(int fd, out int error, int timeout);

		// Token: 0x06001EFC RID: 7932 RVA: 0x0007A33C File Offset: 0x0007853C
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num;
			bool flag = SerialPortStream.poll_serial(this.fd, out num, this.read_timeout);
			if (num == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			if (!flag)
			{
				throw new TimeoutException();
			}
			int num2 = SerialPortStream.read_serial(this.fd, buffer, offset, count);
			if (num2 == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			return num2;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001EFF RID: 7935
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int write_serial(int fd, byte[] buffer, int offset, int count, int timeout);

		// Token: 0x06001F00 RID: 7936 RVA: 0x0007A3C4 File Offset: 0x000785C4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (SerialPortStream.write_serial(this.fd, buffer, offset, count, this.write_timeout) < 0)
			{
				throw new TimeoutException("The operation has timed-out");
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0007A42D File Offset: 0x0007862D
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (SerialPortStream.close_serial(this.fd) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06001F02 RID: 7938
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int close_serial(int fd);

		// Token: 0x06001F03 RID: 7939 RVA: 0x0000B395 File Offset: 0x00009595
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0007A451 File Offset: 0x00078651
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0007A460 File Offset: 0x00078660
		~SerialPortStream()
		{
			try
			{
				this.Dispose(false);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0007A49C File Offset: 0x0007869C
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001F07 RID: 7943
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern bool set_attributes(int fd, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handshake);

		// Token: 0x06001F08 RID: 7944 RVA: 0x0007A4B7 File Offset: 0x000786B7
		public void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs)
		{
			if (!SerialPortStream.set_attributes(this.fd, baud_rate, parity, data_bits, sb, hs))
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06001F09 RID: 7945
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int get_bytes_in_buffer(int fd, int input);

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x0007A4D2 File Offset: 0x000786D2
		public int BytesToRead
		{
			get
			{
				int num = SerialPortStream.get_bytes_in_buffer(this.fd, 1);
				if (num == -1)
				{
					SerialPortStream.ThrowIOException();
				}
				return num;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0007A4E9 File Offset: 0x000786E9
		public int BytesToWrite
		{
			get
			{
				int num = SerialPortStream.get_bytes_in_buffer(this.fd, 0);
				if (num == -1)
				{
					SerialPortStream.ThrowIOException();
				}
				return num;
			}
		}

		// Token: 0x06001F0C RID: 7948
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int discard_buffer(int fd, bool inputBuffer);

		// Token: 0x06001F0D RID: 7949 RVA: 0x0007A500 File Offset: 0x00078700
		public void DiscardInBuffer()
		{
			if (SerialPortStream.discard_buffer(this.fd, true) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0007A515 File Offset: 0x00078715
		public void DiscardOutBuffer()
		{
			if (SerialPortStream.discard_buffer(this.fd, false) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06001F0F RID: 7951
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern SerialSignal get_signals(int fd, out int error);

		// Token: 0x06001F10 RID: 7952 RVA: 0x0007A52C File Offset: 0x0007872C
		public SerialSignal GetSignals()
		{
			int num;
			SerialSignal serialSignal = SerialPortStream.get_signals(this.fd, out num);
			if (num == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			return serialSignal;
		}

		// Token: 0x06001F11 RID: 7953
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int set_signal(int fd, SerialSignal signal, bool value);

		// Token: 0x06001F12 RID: 7954 RVA: 0x0007A54F File Offset: 0x0007874F
		public void SetSignal(SerialSignal signal, bool value)
		{
			if (signal < SerialSignal.Cd || signal > SerialSignal.Rts || signal == SerialSignal.Cd || signal == SerialSignal.Cts || signal == SerialSignal.Dsr)
			{
				throw new Exception("Invalid internal value");
			}
			if (SerialPortStream.set_signal(this.fd, signal, value) == -1)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06001F13 RID: 7955
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int breakprop(int fd);

		// Token: 0x06001F14 RID: 7956 RVA: 0x0007A586 File Offset: 0x00078786
		public void SetBreakState(bool value)
		{
			if (value && SerialPortStream.breakprop(this.fd) == -1)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06001F15 RID: 7957
		[DllImport("libc")]
		private static extern IntPtr strerror(int errnum);

		// Token: 0x06001F16 RID: 7958 RVA: 0x0007A59E File Offset: 0x0007879E
		private static void ThrowIOException()
		{
			throw new IOException(Marshal.PtrToStringAnsi(SerialPortStream.strerror(Marshal.GetLastWin32Error())));
		}

		// Token: 0x06001F17 RID: 7959
		[DllImport("MonoPosixHelper")]
		private static extern bool is_baud_rate_legal(int baud_rate);

		// Token: 0x06001F18 RID: 7960 RVA: 0x0007A5B4 File Offset: 0x000787B4
		private void TryBaudRate(int baudRate)
		{
			if (!SerialPortStream.is_baud_rate_legal(baudRate))
			{
				throw new ArgumentOutOfRangeException("baudRate", "Given baud rate is not supported on this platform.");
			}
		}

		// Token: 0x04001B23 RID: 6947
		private int fd;

		// Token: 0x04001B24 RID: 6948
		private int read_timeout;

		// Token: 0x04001B25 RID: 6949
		private int write_timeout;

		// Token: 0x04001B26 RID: 6950
		private bool disposed;
	}
}
