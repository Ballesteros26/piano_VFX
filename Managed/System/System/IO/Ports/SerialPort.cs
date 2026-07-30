using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Win32;

namespace System.IO.Ports
{
	/// <summary>Represents a serial port resource.</summary>
	// Token: 0x020003F7 RID: 1015
	[MonitoringDescription("")]
	public class SerialPort : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class.</summary>
		// Token: 0x06001E90 RID: 7824 RVA: 0x00079553 File Offset: 0x00077753
		public SerialPort()
			: this(SerialPort.GetDefaultPortName(), 9600, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified <see cref="T:System.ComponentModel.IContainer" /> object.</summary>
		/// <param name="container">An interface to a container. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001E91 RID: 7825 RVA: 0x00079568 File Offset: 0x00077768
		public SerialPort(IContainer container)
			: this()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001E92 RID: 7826 RVA: 0x00079570 File Offset: 0x00077770
		public SerialPort(string portName)
			: this(portName, 9600, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name and baud rate.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001E93 RID: 7827 RVA: 0x00079581 File Offset: 0x00077781
		public SerialPort(string portName, int baudRate)
			: this(portName, baudRate, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, and parity bit.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001E94 RID: 7828 RVA: 0x0007958E File Offset: 0x0007778E
		public SerialPort(string portName, int baudRate, Parity parity)
			: this(portName, baudRate, parity, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, parity bit, and data bits.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <param name="dataBits">The data bits value. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001E95 RID: 7829 RVA: 0x0007959B File Offset: 0x0007779B
		public SerialPort(string portName, int baudRate, Parity parity, int dataBits)
			: this(portName, baudRate, parity, dataBits, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, parity bit, data bits, and stop bit.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <param name="dataBits">The data bits value. </param>
		/// <param name="stopBits">One of the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> values. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001E96 RID: 7830 RVA: 0x000795AC File Offset: 0x000777AC
		public SerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
		{
			this.port_name = portName;
			this.baud_rate = baudRate;
			this.data_bits = dataBits;
			this.stop_bits = stopBits;
			this.parity = parity;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00079640 File Offset: 0x00077840
		private static string GetDefaultPortName()
		{
			string[] portNames = SerialPort.GetPortNames();
			if (portNames.Length != 0)
			{
				return portNames[0];
			}
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				return "ttyS0";
			}
			return "COM1";
		}

		/// <summary>Gets the underlying <see cref="T:System.IO.Stream" /> object for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream is in a .NET Compact Framework application and one of the following methods was called:<see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /><see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /><see cref="M:System.IO.Stream.EndRead(System.IAsyncResult)" /><see cref="M:System.IO.Stream.EndWrite(System.IAsyncResult)" />The .NET Compact Framework does not support the asynchronous model with base streams.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00079681 File Offset: 0x00077881
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Stream BaseStream
		{
			get
			{
				this.CheckOpen();
				return (Stream)this.stream;
			}
		}

		/// <summary>Gets or sets the serial baud rate.</summary>
		/// <returns>The baud rate.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The baud rate specified is less than or equal to zero, or is greater than the maximum allowable baud rate for the device. </exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state. - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00079694 File Offset: 0x00077894
		// (set) Token: 0x06001E9A RID: 7834 RVA: 0x0007969C File Offset: 0x0007789C
		[MonitoringDescription("")]
		[Browsable(true)]
		[DefaultValue(9600)]
		public int BaudRate
		{
			get
			{
				return this.baud_rate;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(value, this.parity, this.data_bits, this.stop_bits, this.handshake);
				}
				this.baud_rate = value;
			}
		}

		/// <summary>Gets or sets the break signal state.</summary>
		/// <returns>true if the port is in a break state; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x000796EB File Offset: 0x000778EB
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x000796F3 File Offset: 0x000778F3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool BreakState
		{
			get
			{
				return this.break_state;
			}
			set
			{
				this.CheckOpen();
				if (value == this.break_state)
				{
					return;
				}
				this.stream.SetBreakState(value);
				this.break_state = value;
			}
		}

		/// <summary>Gets the number of bytes of data in the receive buffer.</summary>
		/// <returns>The number of bytes of data in the receive buffer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The port is not open.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00079718 File Offset: 0x00077918
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int BytesToRead
		{
			get
			{
				this.CheckOpen();
				return this.stream.BytesToRead;
			}
		}

		/// <summary>Gets the number of bytes of data in the send buffer.</summary>
		/// <returns>The number of bytes of data in the send buffer.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0007972B File Offset: 0x0007792B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int BytesToWrite
		{
			get
			{
				this.CheckOpen();
				return this.stream.BytesToWrite;
			}
		}

		/// <summary>Gets the state of the Carrier Detect line for the port.</summary>
		/// <returns>true if the carrier is detected; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0007973E File Offset: 0x0007793E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool CDHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Cd) > SerialSignal.None;
			}
		}

		/// <summary>Gets the state of the Clear-to-Send line.</summary>
		/// <returns>true if the Clear-to-Send line is detected; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x00079756 File Offset: 0x00077956
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CtsHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Cts) > SerialSignal.None;
			}
		}

		/// <summary>Gets or sets the standard length of data bits per byte.</summary>
		/// <returns>The data bits length.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The data bits value is less than 5 or more than 8. </exception>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0007976E File Offset: 0x0007796E
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x00079778 File Offset: 0x00077978
		[DefaultValue(8)]
		[Browsable(true)]
		[MonitoringDescription("")]
		public int DataBits
		{
			get
			{
				return this.data_bits;
			}
			set
			{
				if (value < 5 || value > 8)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, value, this.stop_bits, this.handshake);
				}
				this.data_bits = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether null bytes are ignored when transmitted between the port and the receive buffer.</summary>
		/// <returns>true if null bytes are ignored; otherwise false. The default is false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO("Not implemented")]
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue(false)]
		public bool DiscardNull
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the state of the Data Set Ready (DSR) signal.</summary>
		/// <returns>true if a Data Set Ready signal has been sent to the port; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x000797CB File Offset: 0x000779CB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DsrHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Dsr) > SerialSignal.None;
			}
		}

		/// <summary>Gets or sets a value that enables the Data Terminal Ready (DTR) signal during serial communication.</summary>
		/// <returns>true to enable Data Terminal Ready (DTR); otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x000797E3 File Offset: 0x000779E3
		// (set) Token: 0x06001EA7 RID: 7847 RVA: 0x000797EB File Offset: 0x000779EB
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue(false)]
		public bool DtrEnable
		{
			get
			{
				return this.dtr_enable;
			}
			set
			{
				if (value == this.dtr_enable)
				{
					return;
				}
				if (this.is_open)
				{
					this.stream.SetSignal(SerialSignal.Dtr, value);
				}
				this.dtr_enable = value;
			}
		}

		/// <summary>Gets or sets the byte encoding for pre- and post-transmission conversion of text.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object. The default is <see cref="T:System.Text.ASCIIEncoding" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.Encoding" /> property was set to null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.Encoding" /> property was set to an encoding that is not <see cref="T:System.Text.ASCIIEncoding" />, <see cref="T:System.Text.UTF8Encoding" />, <see cref="T:System.Text.UTF32Encoding" />, <see cref="T:System.Text.UnicodeEncoding" />, one of the Windows single byte encodings, or one of the Windows double byte encodings.</exception>
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00079813 File Offset: 0x00077A13
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0007981B File Offset: 0x00077A1B
		[MonitoringDescription("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the handshaking protocol for serial port transmission of data.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.Handshake" /> values. The default is None.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value passed is not a valid value in the <see cref="T:System.IO.Ports.Handshake" /> enumeration.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x00079832 File Offset: 0x00077A32
		// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0007983C File Offset: 0x00077A3C
		[MonitoringDescription("")]
		[Browsable(true)]
		[DefaultValue(Handshake.None)]
		public Handshake Handshake
		{
			get
			{
				return this.handshake;
			}
			set
			{
				if (value < Handshake.None || value > Handshake.RequestToSendXOnXOff)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, this.data_bits, this.stop_bits, value);
				}
				this.handshake = value;
			}
		}

		/// <summary>Gets a value indicating the open or closed status of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>true if the serial port is open; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> value passed is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> value passed is an empty string ("").</exception>
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0007988F File Offset: 0x00077A8F
		[Browsable(false)]
		public bool IsOpen
		{
			get
			{
				return this.is_open;
			}
		}

		/// <summary>Gets or sets the value used to interpret the end of a call to the <see cref="M:System.IO.Ports.SerialPort.ReadLine" /> and <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)" /> methods.</summary>
		/// <returns>A value that represents the end of a line. The default is a line feed, (<see cref="P:System.Environment.NewLine" />).</returns>
		/// <exception cref="T:System.ArgumentException">The property value is empty.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property value is null.</exception>
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x00079897 File Offset: 0x00077A97
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x0007989F File Offset: 0x00077A9F
		[DefaultValue("\n")]
		[Browsable(false)]
		[MonitoringDescription("")]
		public string NewLine
		{
			get
			{
				return this.new_line;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("NewLine cannot be null or empty.", "value");
				}
				this.new_line = value;
			}
		}

		/// <summary>Gets or sets the parity-checking protocol.</summary>
		/// <returns>One of the enumeration values that represents the parity-checking protocol. The default is <see cref="F:System.IO.Ports.Parity.None" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.Parity" /> value passed is not a valid value in the <see cref="T:System.IO.Ports.Parity" /> enumeration.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x000798CE File Offset: 0x00077ACE
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x000798D8 File Offset: 0x00077AD8
		[DefaultValue(Parity.None)]
		[Browsable(true)]
		[MonitoringDescription("")]
		public Parity Parity
		{
			get
			{
				return this.parity;
			}
			set
			{
				if (value < Parity.None || value > Parity.Space)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, value, this.data_bits, this.stop_bits, this.handshake);
				}
				this.parity = value;
			}
		}

		/// <summary>Gets or sets the byte that replaces invalid bytes in a data stream when a parity error occurs.</summary>
		/// <returns>A byte that replaces invalid bytes.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x00004239 File Offset: 0x00002439
		[DefaultValue(63)]
		[MonitoringDescription("")]
		[MonoTODO("Not implemented")]
		[Browsable(true)]
		public byte ParityReplace
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the port for communications, including but not limited to all available COM ports.</summary>
		/// <returns>The communications port. The default is COM1.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to a value with a length of zero.-or-The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to a value that starts with "\\".-or-The port name was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is open. </exception>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x0007992B File Offset: 0x00077B2B
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x00079934 File Offset: 0x00077B34
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue("COM1")]
		public string PortName
		{
			get
			{
				return this.port_name;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException("Port name cannot be set while port is open.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0 || value.StartsWith("\\\\"))
				{
					throw new ArgumentException("value");
				}
				this.port_name = value;
			}
		}

		/// <summary>Gets or sets the size of the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The buffer size, in bytes. The default value is 4096.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> value set is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> property was set while the stream was open.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> property was set to an odd integer value. </exception>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x00079989 File Offset: 0x00077B89
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x00079991 File Offset: 0x00077B91
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue(4096)]
		public int ReadBufferSize
		{
			get
			{
				return this.readBufferSize;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value <= 4096)
				{
					return;
				}
				this.readBufferSize = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish.</summary>
		/// <returns>The number of milliseconds before a time-out occurs when a read operation does not finish.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The read time-out value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000799C0 File Offset: 0x00077BC0
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x000799C8 File Offset: 0x00077BC8
		[DefaultValue(-1)]
		[Browsable(true)]
		[MonitoringDescription("")]
		public int ReadTimeout
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
				if (this.is_open)
				{
					this.stream.ReadTimeout = value;
				}
				this.read_timeout = value;
			}
		}

		/// <summary>Gets or sets the number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event occurs.</summary>
		/// <returns>The number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event is fired. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReceivedBytesThreshold" /> value is less than or equal to zero. </exception>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x06001EBA RID: 7866 RVA: 0x000799F8 File Offset: 0x00077BF8
		[MonoTODO("Not implemented")]
		[DefaultValue(1)]
		[Browsable(true)]
		[MonitoringDescription("")]
		public int ReceivedBytesThreshold
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the Request to Send (RTS) signal is enabled during serial communication.</summary>
		/// <returns>true to enable Request to Transmit (RTS); otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.IO.Ports.SerialPort.RtsEnable" /> property was set or retrieved while the <see cref="P:System.IO.Ports.SerialPort.Handshake" /> property is set to the <see cref="F:System.IO.Ports.Handshake.RequestToSend" /> value or the <see cref="F:System.IO.Ports.Handshake.RequestToSendXOnXOff" /> value.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00079A0E File Offset: 0x00077C0E
		// (set) Token: 0x06001EBC RID: 7868 RVA: 0x00079A16 File Offset: 0x00077C16
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue(false)]
		public bool RtsEnable
		{
			get
			{
				return this.rts_enable;
			}
			set
			{
				if (value == this.rts_enable)
				{
					return;
				}
				if (this.is_open)
				{
					this.stream.SetSignal(SerialSignal.Rts, value);
				}
				this.rts_enable = value;
			}
		}

		/// <summary>Gets or sets the standard number of stopbits per byte.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.StopBits" /> values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.StopBits" /> value is  <see cref="F:System.IO.Ports.StopBits.None" />.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x00079A3F File Offset: 0x00077C3F
		// (set) Token: 0x06001EBE RID: 7870 RVA: 0x00079A48 File Offset: 0x00077C48
		[DefaultValue(StopBits.One)]
		[Browsable(true)]
		[MonitoringDescription("")]
		public StopBits StopBits
		{
			get
			{
				return this.stop_bits;
			}
			set
			{
				if (value < StopBits.One || value > StopBits.OnePointFive)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, this.data_bits, value, this.handshake);
				}
				this.stop_bits = value;
			}
		}

		/// <summary>Gets or sets the size of the serial port output buffer. </summary>
		/// <returns>The size of the output buffer. The default is 2048.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> value is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> property was set while the stream was open.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> property was set to an odd integer value. </exception>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x00079A9B File Offset: 0x00077C9B
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x00079AA3 File Offset: 0x00077CA3
		[DefaultValue(2048)]
		[Browsable(true)]
		[MonitoringDescription("")]
		public int WriteBufferSize
		{
			get
			{
				return this.writeBufferSize;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value <= 2048)
				{
					return;
				}
				this.writeBufferSize = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish.</summary>
		/// <returns>The number of milliseconds before a time-out occurs. The default is <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteTimeout" /> value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00079AD2 File Offset: 0x00077CD2
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x00079ADA File Offset: 0x00077CDA
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue(-1)]
		public int WriteTimeout
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
				if (this.is_open)
				{
					this.stream.WriteTimeout = value;
				}
				this.write_timeout = value;
			}
		}

		/// <summary>Closes the port connection, sets the <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> property to false, and disposes of the internal <see cref="T:System.IO.Stream" /> object.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.- or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001EC3 RID: 7875 RVA: 0x00079B0A File Offset: 0x00077D0A
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Ports.SerialPort" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x06001EC4 RID: 7876 RVA: 0x00079B13 File Offset: 0x00077D13
		protected override void Dispose(bool disposing)
		{
			if (!this.is_open)
			{
				return;
			}
			this.is_open = false;
			if (disposing)
			{
				this.stream.Close();
			}
			this.stream = null;
		}

		/// <summary>Discards data from the serial driver's receive buffer.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001EC5 RID: 7877 RVA: 0x00079B3A File Offset: 0x00077D3A
		public void DiscardInBuffer()
		{
			this.CheckOpen();
			this.stream.DiscardInBuffer();
		}

		/// <summary>Discards data from the serial driver's transmit buffer.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001EC6 RID: 7878 RVA: 0x00079B4D File Offset: 0x00077D4D
		public void DiscardOutBuffer()
		{
			this.CheckOpen();
			this.stream.DiscardOutBuffer();
		}

		/// <summary>Gets an array of serial port names for the current computer.</summary>
		/// <returns>An array of serial port names for the current computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The serial port names could not be queried.</exception>
		// Token: 0x06001EC7 RID: 7879 RVA: 0x00079B60 File Offset: 0x00077D60
		public static string[] GetPortNames()
		{
			int platform = (int)Environment.OSVersion.Platform;
			List<string> list = new List<string>();
			if (platform == 4 || platform == 128 || platform == 6)
			{
				string[] files = Directory.GetFiles("/dev/", "tty*");
				bool flag = false;
				foreach (string text in files)
				{
					if (text.StartsWith("/dev/ttyS") || text.StartsWith("/dev/ttyUSB") || text.StartsWith("/dev/ttyACM"))
					{
						flag = true;
						break;
					}
				}
				foreach (string text2 in files)
				{
					if (flag)
					{
						if (text2.StartsWith("/dev/ttyS") || text2.StartsWith("/dev/ttyUSB") || text2.StartsWith("/dev/ttyACM"))
						{
							list.Add(text2);
						}
					}
					else if (text2 != "/dev/tty" && text2.StartsWith("/dev/tty") && !text2.StartsWith("/dev/ttyC"))
					{
						list.Add(text2);
					}
				}
			}
			else
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM"))
				{
					if (registryKey != null)
					{
						foreach (string text3 in registryKey.GetValueNames())
						{
							string text4 = registryKey.GetValue(text3, "").ToString();
							if (text4 != "")
							{
								list.Add(text4);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x00079D00 File Offset: 0x00077F00
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT;
			}
		}

		/// <summary>Opens a new serial port connection.</summary>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied to the port.- or -The current process, or another process on the system, already has the specified COM port open either by a <see cref="T:System.IO.Ports.SerialPort" /> instance or in unmanaged code.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the properties for this instance are invalid. For example, the <see cref="P:System.IO.Ports.SerialPort.Parity" />, <see cref="P:System.IO.Ports.SerialPort.DataBits" />, or <see cref="P:System.IO.Ports.SerialPort.Handshake" /> properties are not valid values; the <see cref="P:System.IO.Ports.SerialPort.BaudRate" /> is less than or equal to zero; the <see cref="P:System.IO.Ports.SerialPort.ReadTimeout" /> or <see cref="P:System.IO.Ports.SerialPort.WriteTimeout" /> property is less than zero and is not <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		/// <exception cref="T:System.ArgumentException">The port name does not begin with "COM". - or -The file type of the port is not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port on the current instance of the <see cref="T:System.IO.Ports.SerialPort" /> is already open.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001EC9 RID: 7881 RVA: 0x00079D24 File Offset: 0x00077F24
		public void Open()
		{
			if (this.is_open)
			{
				throw new InvalidOperationException("Port is already open");
			}
			if (SerialPort.IsWindows)
			{
				this.stream = new WinSerialStream(this.port_name, this.baud_rate, this.data_bits, this.parity, this.stop_bits, this.dtr_enable, this.rts_enable, this.handshake, this.read_timeout, this.write_timeout, this.readBufferSize, this.writeBufferSize);
			}
			else
			{
				this.stream = new SerialPortStream(this.port_name, this.baud_rate, this.data_bits, this.parity, this.stop_bits, this.dtr_enable, this.rts_enable, this.handshake, this.read_timeout, this.write_timeout, this.readBufferSize, this.writeBufferSize);
			}
			this.is_open = true;
		}

		/// <summary>Reads a number of bytes from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer and writes those bytes into a byte array at the specified offset.</summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="buffer">The byte array to write the input to. </param>
		/// <param name="offset">The offset in the buffer array to begin writing. </param>
		/// <param name="count">The number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">The buffer passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.TimeoutException">No bytes were available to read.</exception>
		// Token: 0x06001ECA RID: 7882 RVA: 0x00079DFC File Offset: 0x00077FFC
		public int Read(byte[] buffer, int offset, int count)
		{
			this.CheckOpen();
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
			return this.stream.Read(buffer, offset, count);
		}

		/// <summary>Reads a number of characters from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer and writes them into an array of characters at a given offset.</summary>
		/// <returns>The number of characters read.</returns>
		/// <param name="buffer">The character array to write the input to. </param>
		/// <param name="offset">The offset in the buffer array to begin writing. </param>
		/// <param name="count">The number of characters to read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the buffer.- or -<paramref name="count" /> is 1 and there is a surrogate character in the buffer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">No characters were available to read.</exception>
		// Token: 0x06001ECB RID: 7883 RVA: 0x00079E58 File Offset: 0x00078058
		public int Read(char[] buffer, int offset, int count)
		{
			this.CheckOpen();
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
			int num = 0;
			int num2;
			while (num < count && (num2 = this.ReadChar()) != -1)
			{
				buffer[offset + num] = (char)num2;
				num++;
			}
			return num;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00079EC4 File Offset: 0x000780C4
		internal int read_byte()
		{
			byte[] array = new byte[1];
			if (this.stream.Read(array, 0, 1) > 0)
			{
				return (int)array[0];
			}
			return -1;
		}

		/// <summary>Synchronously reads one byte from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The byte, cast to an <see cref="T:System.Int32" />, or -1 if the end of the stream has been read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.- or -No byte was read.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001ECD RID: 7885 RVA: 0x00079EEE File Offset: 0x000780EE
		public int ReadByte()
		{
			this.CheckOpen();
			return this.read_byte();
		}

		/// <summary>Synchronously reads one character from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The character that was read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.- or -No character was available in the allotted time-out period.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001ECE RID: 7886 RVA: 0x00079EFC File Offset: 0x000780FC
		public int ReadChar()
		{
			this.CheckOpen();
			byte[] array = new byte[16];
			int num = 0;
			char[] chars;
			for (;;)
			{
				int num2 = this.read_byte();
				if (num2 == -1)
				{
					break;
				}
				array[num++] = (byte)num2;
				chars = this.encoding.GetChars(array, 0, 1);
				if (chars.Length != 0)
				{
					goto Block_2;
				}
				if (num >= array.Length)
				{
					return -1;
				}
			}
			return -1;
			Block_2:
			return (int)chars[0];
		}

		/// <summary>Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>The contents of the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001ECF RID: 7887 RVA: 0x00079F50 File Offset: 0x00078150
		public string ReadExisting()
		{
			this.CheckOpen();
			int bytesToRead = this.BytesToRead;
			byte[] array = new byte[bytesToRead];
			int num = this.stream.Read(array, 0, bytesToRead);
			return new string(this.encoding.GetChars(array, 0, num));
		}

		/// <summary>Reads up to the <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value in the input buffer.</summary>
		/// <returns>The contents of the input buffer up to the first occurrence of a <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended.- or -No bytes were read.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001ED0 RID: 7888 RVA: 0x00079F93 File Offset: 0x00078193
		public string ReadLine()
		{
			return this.ReadTo(this.new_line);
		}

		/// <summary>Reads a string up to the specified <paramref name="value" /> in the input buffer.</summary>
		/// <returns>The contents of the input buffer up to the specified <paramref name="value" />.</returns>
		/// <param name="value">A value that indicates where the read operation stops. </param>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="value" /> parameter is 0.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x06001ED1 RID: 7889 RVA: 0x00079FA4 File Offset: 0x000781A4
		public string ReadTo(string value)
		{
			this.CheckOpen();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("value");
			}
			byte[] bytes = this.encoding.GetBytes(value);
			int num = 0;
			List<byte> list = new List<byte>();
			for (;;)
			{
				int num2 = this.read_byte();
				if (num2 == -1)
				{
					goto IL_0089;
				}
				list.Add((byte)num2);
				if (num2 == (int)bytes[num])
				{
					num++;
					if (num == bytes.Length)
					{
						break;
					}
				}
				else
				{
					num = (((int)bytes[0] == num2) ? 1 : 0);
				}
			}
			return this.encoding.GetString(list.ToArray(), 0, list.Count - bytes.Length);
			IL_0089:
			return this.encoding.GetString(list.ToArray());
		}

		/// <summary>Writes the specified string to the serial port.</summary>
		/// <param name="text">The string for output. </param>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x06001ED2 RID: 7890 RVA: 0x0007A04C File Offset: 0x0007824C
		public void Write(string text)
		{
			this.CheckOpen();
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			byte[] bytes = this.encoding.GetBytes(text);
			this.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes a specified number of bytes to the serial port using data from a buffer.</summary>
		/// <param name="buffer">The byte array that contains the data to write to the port. </param>
		/// <param name="offset">The zero-based byte offset in the <paramref name="buffer" /> parameter at which to begin copying bytes to the port. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x06001ED3 RID: 7891 RVA: 0x0007A088 File Offset: 0x00078288
		public void Write(byte[] buffer, int offset, int count)
		{
			this.CheckOpen();
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
			this.stream.Write(buffer, offset, count);
		}

		/// <summary>Writes a specified number of characters to the serial port using data from a buffer.</summary>
		/// <param name="buffer">The character array that contains the data to write to the port. </param>
		/// <param name="offset">The zero-based byte offset in the <paramref name="buffer" /> parameter at which to begin copying bytes to the port. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x06001ED4 RID: 7892 RVA: 0x0007A0E0 File Offset: 0x000782E0
		public void Write(char[] buffer, int offset, int count)
		{
			this.CheckOpen();
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
			byte[] bytes = this.encoding.GetBytes(buffer, offset, count);
			this.stream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes the specified string and the <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value to the output buffer.</summary>
		/// <param name="text">The string to write to the output buffer. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)" /> method could not write to the stream.  </exception>
		// Token: 0x06001ED5 RID: 7893 RVA: 0x0007A146 File Offset: 0x00078346
		public void WriteLine(string text)
		{
			this.Write(text + this.new_line);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0007A15A File Offset: 0x0007835A
		private void CheckOpen()
		{
			if (!this.is_open)
			{
				throw new InvalidOperationException("Specified port is not open.");
			}
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0007A170 File Offset: 0x00078370
		internal void OnErrorReceived(SerialErrorReceivedEventArgs args)
		{
			SerialErrorReceivedEventHandler serialErrorReceivedEventHandler = (SerialErrorReceivedEventHandler)base.Events[this.error_received];
			if (serialErrorReceivedEventHandler != null)
			{
				serialErrorReceivedEventHandler(this, args);
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0007A1A0 File Offset: 0x000783A0
		internal void OnDataReceived(SerialDataReceivedEventArgs args)
		{
			SerialDataReceivedEventHandler serialDataReceivedEventHandler = (SerialDataReceivedEventHandler)base.Events[this.data_received];
			if (serialDataReceivedEventHandler != null)
			{
				serialDataReceivedEventHandler(this, args);
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0007A1D0 File Offset: 0x000783D0
		internal void OnDataReceived(SerialPinChangedEventArgs args)
		{
			SerialPinChangedEventHandler serialPinChangedEventHandler = (SerialPinChangedEventHandler)base.Events[this.pin_changed];
			if (serialPinChangedEventHandler != null)
			{
				serialPinChangedEventHandler(this, args);
			}
		}

		/// <summary>Represents the method that handles the error event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001EDA RID: 7898 RVA: 0x0007A1FF File Offset: 0x000783FF
		// (remove) Token: 0x06001EDB RID: 7899 RVA: 0x0007A213 File Offset: 0x00078413
		[MonitoringDescription("")]
		public event SerialErrorReceivedEventHandler ErrorReceived
		{
			add
			{
				base.Events.AddHandler(this.error_received, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.error_received, value);
			}
		}

		/// <summary>Represents the method that will handle the serial pin changed event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001EDC RID: 7900 RVA: 0x0007A227 File Offset: 0x00078427
		// (remove) Token: 0x06001EDD RID: 7901 RVA: 0x0007A23B File Offset: 0x0007843B
		[MonitoringDescription("")]
		public event SerialPinChangedEventHandler PinChanged
		{
			add
			{
				base.Events.AddHandler(this.pin_changed, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.pin_changed, value);
			}
		}

		/// <summary>Represents the method that will handle the data received event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06001EDE RID: 7902 RVA: 0x0007A24F File Offset: 0x0007844F
		// (remove) Token: 0x06001EDF RID: 7903 RVA: 0x0007A263 File Offset: 0x00078463
		[MonitoringDescription("")]
		public event SerialDataReceivedEventHandler DataReceived
		{
			add
			{
				base.Events.AddHandler(this.data_received, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.data_received, value);
			}
		}

		/// <summary>Indicates that no time-out should occur.</summary>
		// Token: 0x04001B08 RID: 6920
		public const int InfiniteTimeout = -1;

		// Token: 0x04001B09 RID: 6921
		private const int DefaultReadBufferSize = 4096;

		// Token: 0x04001B0A RID: 6922
		private const int DefaultWriteBufferSize = 2048;

		// Token: 0x04001B0B RID: 6923
		private const int DefaultBaudRate = 9600;

		// Token: 0x04001B0C RID: 6924
		private const int DefaultDataBits = 8;

		// Token: 0x04001B0D RID: 6925
		private const Parity DefaultParity = Parity.None;

		// Token: 0x04001B0E RID: 6926
		private const StopBits DefaultStopBits = StopBits.One;

		// Token: 0x04001B0F RID: 6927
		private bool is_open;

		// Token: 0x04001B10 RID: 6928
		private int baud_rate;

		// Token: 0x04001B11 RID: 6929
		private Parity parity;

		// Token: 0x04001B12 RID: 6930
		private StopBits stop_bits;

		// Token: 0x04001B13 RID: 6931
		private Handshake handshake;

		// Token: 0x04001B14 RID: 6932
		private int data_bits;

		// Token: 0x04001B15 RID: 6933
		private bool break_state;

		// Token: 0x04001B16 RID: 6934
		private bool dtr_enable;

		// Token: 0x04001B17 RID: 6935
		private bool rts_enable;

		// Token: 0x04001B18 RID: 6936
		private ISerialStream stream;

		// Token: 0x04001B19 RID: 6937
		private Encoding encoding = Encoding.ASCII;

		// Token: 0x04001B1A RID: 6938
		private string new_line = Environment.NewLine;

		// Token: 0x04001B1B RID: 6939
		private string port_name;

		// Token: 0x04001B1C RID: 6940
		private int read_timeout = -1;

		// Token: 0x04001B1D RID: 6941
		private int write_timeout = -1;

		// Token: 0x04001B1E RID: 6942
		private int readBufferSize = 4096;

		// Token: 0x04001B1F RID: 6943
		private int writeBufferSize = 2048;

		// Token: 0x04001B20 RID: 6944
		private object error_received = new object();

		// Token: 0x04001B21 RID: 6945
		private object data_received = new object();

		// Token: 0x04001B22 RID: 6946
		private object pin_changed = new object();
	}
}
