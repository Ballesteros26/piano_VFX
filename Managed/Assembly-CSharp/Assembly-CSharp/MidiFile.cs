using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class MidiFile
{
	// Token: 0x0600008F RID: 143 RVA: 0x000098B5 File Offset: 0x00007AB5
	public MidiFile(string fileName)
	{
		this.ParseFile(fileName);
	}

	// Token: 0x06000090 RID: 144 RVA: 0x000098E8 File Offset: 0x00007AE8
	public void ParseFile(string filePath)
	{
		try
		{
			this.reader = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));
		}
		catch (Exception)
		{
			return;
		}
		uint num = this.reader.ReadUInt32();
		this.Swap32(num);
		num = this.reader.ReadUInt32();
		this.Swap32(num);
		ushort num2 = this.reader.ReadUInt16();
		this.Swap16(num2);
		num2 = this.reader.ReadUInt16();
		ushort num3 = this.Swap16(num2);
		num2 = this.reader.ReadUInt16();
		ushort num4 = this.Swap16(num2);
		this.div = num4;
		ushort num5 = 1;
		for (ushort num6 = 0; num6 < num3; num6 += 1)
		{
			MonoBehaviour.print("=== NEW TRACK");
			num = this.reader.ReadUInt32();
			this.Swap32(num);
			num = this.reader.ReadUInt32();
			this.Swap32(num);
			bool flag = false;
			byte b = 0;
			this.tracks.Add(new MidiTrack());
			while (!flag)
			{
				uint num7 = this.ReadValue();
				byte b2 = this.reader.ReadByte();
				if (b2 < 128)
				{
					b2 = b;
					this.reader.BaseStream.Seek(-1L, SeekOrigin.Current);
				}
				if (128.Equals(b2 & 240))
				{
					b = b2;
					byte b3 = this.reader.ReadByte();
					byte b4 = this.reader.ReadByte();
					MonoBehaviour.print("Delta Time: " + num7);
					MonoBehaviour.print("NOTE OFF: " + b3);
					this.tracks[(int)num6].events.Add(new MidiEvent(b2 & 240, b3, b4, num7));
				}
				else if (144.Equals(b2 & 240))
				{
					b = b2;
					byte b5 = this.reader.ReadByte();
					byte b6 = this.reader.ReadByte();
					if (b6 == 0)
					{
						MonoBehaviour.print("Delta Time: " + num7);
						MonoBehaviour.print("NOTE OFF: " + b5);
						this.tracks[(int)num6].events.Add(new MidiEvent(128, b5, b6, num7));
					}
					else
					{
						MonoBehaviour.print("Delta Time: " + num7);
						MonoBehaviour.print("NOTE ON: " + b5);
						this.tracks[(int)num6].events.Add(new MidiEvent(b2 & 240, b5, b6, num7));
						if (num5 != num6)
						{
							num5 = num6;
							this.numberOfTracks += 1;
						}
					}
				}
				else if (160.Equals(b2 & 240))
				{
					b = b2;
					this.reader.ReadByte();
					this.reader.ReadByte();
					this.tracks[(int)num6].events.Add(new MidiEvent(b2 & 240, 0, 0, num7));
				}
				else if (176.Equals(b2 & 240))
				{
					b = b2;
					byte b7 = this.reader.ReadByte();
					byte b8 = this.reader.ReadByte();
					MonoBehaviour.print("Delta Time: " + num7);
					MonoBehaviour.print(string.Concat(new object[] { "CONTROL CHANGE - ControlID: ", b7, ", ControlValue: ", b8 }));
					this.tracks[(int)num6].events.Add(new MidiEvent(b2 & 240, b7, b8, num7));
				}
				else if (192.Equals(b2 & 240))
				{
					b = b2;
					byte b9 = b2 & 15;
					byte b10 = this.reader.ReadByte();
					MonoBehaviour.print(string.Concat(new object[] { "PROGRAM CHANGE - Channel: ", b9, ", RrogramID: ", b10 }));
					this.tracks[(int)num6].events.Add(new MidiEvent(b2 & 240, 0, 0, num7));
				}
				else if (208.Equals(b2 & 240))
				{
					b = b2;
					this.reader.ReadByte();
					this.tracks[(int)num6].events.Add(new MidiEvent(b2 & 240, 0, 0, num7));
				}
				else if (224.Equals(b2 & 240))
				{
					b = b2;
					this.reader.ReadByte();
					this.reader.ReadByte();
					this.tracks[(int)num6].events.Add(new MidiEvent(b2 & 240, 0, 0, num7));
				}
				else if (240.Equals(b2 & 240))
				{
					bool flag2 = false;
					if (b2 == 240)
					{
						MonoBehaviour.print("System Exclusive Begin: " + this.ReadString(this.ReadValue()));
					}
					if (b2 == 247)
					{
						MonoBehaviour.print("System Exclusive End: " + this.ReadString(this.ReadValue()));
					}
					if (b2 == 255)
					{
						byte b11 = this.reader.ReadByte();
						uint num8 = this.ReadValue();
						if (b11 != 47)
						{
							if (b11 != 81)
							{
								if (b11 != 88)
								{
									int num9 = 0;
									while ((long)num9 < (long)((ulong)num8))
									{
										this.reader.ReadByte();
										num9++;
									}
								}
								else
								{
									this.reader.ReadByte();
									this.reader.ReadByte();
									this.numberOfTicksInMetronomeClick = this.reader.ReadByte();
									MonoBehaviour.print("Delta Time: " + num7);
									MonoBehaviour.print("Number of ticks in metronome click: " + this.numberOfTicksInMetronomeClick);
									this.reader.ReadByte();
								}
							}
							else
							{
								this.tempo = 0U;
								this.tempo |= (uint)((uint)this.reader.ReadByte() << 16);
								this.tempo |= (uint)((uint)this.reader.ReadByte() << 8);
								this.tempo |= (uint)this.reader.ReadByte();
								this.BPM = 60000000U / this.tempo;
								this.tempoHistory.Enqueue(this.tempo / (uint)num4 / 1000000f);
								MonoBehaviour.print("Delta Time: " + num7);
								MonoBehaviour.print("BPM: " + this.BPM);
								flag2 = true;
							}
						}
						else
						{
							flag = true;
							MonoBehaviour.print("endTrack");
						}
					}
					if (flag2)
					{
						this.tracks[(int)num6].events.Add(new MidiEvent(96, 0, 0, num7));
					}
					else
					{
						this.tracks[(int)num6].events.Add(new MidiEvent(b2 & 240, 0, 0, num7));
					}
				}
				else
				{
					MonoBehaviour.print("Unrecognised status byte: " + b2.ToString("X2"));
				}
			}
		}
		if (this.tempoHistory.Count == 0)
		{
			this.tempoHistory.Enqueue(this.tempo / (uint)num4 / 1000000f);
			this.defaultTempo = true;
		}
		this.reader.Close();
	}

	// Token: 0x06000091 RID: 145 RVA: 0x0000A0DC File Offset: 0x000082DC
	private uint Swap32(uint n)
	{
		return ((n >> 24) & 255U) | ((n << 8) & 16711680U) | ((n >> 8) & 65280U) | ((n << 24) & 4278190080U);
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0000A107 File Offset: 0x00008307
	private ushort Swap16(ushort n)
	{
		return (ushort)((n >> 8) | ((int)n << 8));
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000A114 File Offset: 0x00008314
	private string ReadString(uint length)
	{
		string text = "";
		for (uint num = 0U; num < length; num += 1U)
		{
			text += this.reader.ReadByte().ToString("X1");
		}
		return text;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000A154 File Offset: 0x00008354
	private uint ReadValue()
	{
		uint num = (uint)this.reader.ReadByte();
		if (num >> 7 == 1U)
		{
			num &= 127U;
			byte b;
			do
			{
				b = this.reader.ReadByte();
				num = (uint)((ulong)((ulong)num << 7) | (ulong)((long)(b & 127)));
			}
			while (b >> 7 == 1);
		}
		return num;
	}

	// Token: 0x040001A0 RID: 416
	private BinaryReader reader;

	// Token: 0x040001A1 RID: 417
	public List<MidiTrack> tracks = new List<MidiTrack>();

	// Token: 0x040001A2 RID: 418
	public Queue<float> tempoHistory = new Queue<float>();

	// Token: 0x040001A3 RID: 419
	private uint tempo = 500000U;

	// Token: 0x040001A4 RID: 420
	public bool defaultTempo;

	// Token: 0x040001A5 RID: 421
	public ushort div;

	// Token: 0x040001A6 RID: 422
	public ushort numberOfTracks;

	// Token: 0x040001A7 RID: 423
	private uint BPM;

	// Token: 0x040001A8 RID: 424
	private byte numberOfTicksInMetronomeClick;
}
