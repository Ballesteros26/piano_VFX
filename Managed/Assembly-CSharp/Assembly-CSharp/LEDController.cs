using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000010 RID: 16
public class LEDController : MonoBehaviour
{
	// Token: 0x06000077 RID: 119 RVA: 0x00007DB0 File Offset: 0x00005FB0
	private void Update()
	{
		if (this.notesToSpawn.Count > 0)
		{
			for (int i = 0; i < this.notesToSpawn.Count; i++)
			{
				byte b = 0;
				byte b2 = this.notesToSpawn[i];
				while ((int)b2 < this.firstKeyID || (int)b2 > this.lastKeyID)
				{
					b2 -= (SevenBitNumber)12;
					b += 1;
				}
				Vector2 vector = new Vector2(this.keyXCoordinates[(int)b2 - this.firstKeyID] + (float)b * this.octaveLength, -3.15f);
				GameObject gameObject;
				if (this.IsBlack(b2))
				{
					gameObject = this.blackKey;
				}
				else
				{
					gameObject = this.whiteKey;
				}
				GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(gameObject, vector, Quaternion.identity);
				this.spawnedNotes[(int)this.notesToSpawn[i]] = gameObject2;
				gameObject2.transform.Rotate(0f, 0f, 180f);
				gameObject2.GetComponent<LivePlayNote>().play = true;
				gameObject2.GetComponent<LivePlayNote>().CopyData();
				gameObject2.GetComponent<SpawnEffect>().ApplyColor(0);
				gameObject2.transform.GetComponent<Renderer>().material.SetVector("_Tiling", gameObject2.transform.localScale);
				gameObject2.transform.GetComponent<Renderer>().material.SetFloat("_StartPoint", (float)global::UnityEngine.Random.Range(0, 100));
				this.notesToSpawn.RemoveAt(i);
			}
		}
		if (this.notesToStop.Count > 0)
		{
			for (int j = 0; j < this.notesToStop.Count; j++)
			{
				byte b3 = this.notesToStop[j];
				if (this.spawnedNotes[(int)b3] != null)
				{
					this.spawnedNotes[(int)b3] = null;
					this.notesToStop.RemoveAt(j);
				}
			}
		}
		if (this.spawnedNotes.Length != 0)
		{
			for (int k = 0; k < this.spawnedNotes.Length; k++)
			{
				if (this.spawnedNotes[k] != null)
				{
					float num = (this.spawnedNotes[k].transform.position.y - -3.15f) * 2.11f / 10.529711f;
					this.spawnedNotes[k].transform.localScale = new Vector3(this.spawnedNotes[k].transform.localScale.x, num, this.spawnedNotes[k].transform.localScale.z);
					this.spawnedNotes[k].transform.GetComponent<Renderer>().material.SetVector("_Tiling", this.spawnedNotes[k].transform.localScale);
					foreach (object obj in this.spawnedNotes[k].transform)
					{
						Transform transform = (Transform)obj;
						if (num != 0f)
						{
							transform.localScale = new Vector3(1f, 0.5f / (num / 0.133f), 1f);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x000080F0 File Offset: 0x000062F0
	public string[] GetDevices()
	{
		string[] array = new string[InputDevice.GetDevicesCount()];
		int num = 0;
		foreach (InputDevice inputDevice in InputDevice.GetAll())
		{
			array[num] = inputDevice.Name;
			num++;
		}
		return array;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00008150 File Offset: 0x00006350
	public string[] GetSerialPorts()
	{
		string[] array = new string[SerialPort.GetPortNames().Length];
		int num = 0;
		foreach (string text in SerialPort.GetPortNames())
		{
			array[num] = text;
			num++;
		}
		return array;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00008190 File Offset: 0x00006390
	public void SetInputDeviceLivePlay()
	{
		this.inputDevice = InputDevice.GetById(this.inputDevices.value);
		this.inputDeviceDesposed = false;
		this.inputDevice.EventReceived += this.OnEventReceivedLivePlay;
		this.inputDevice.StartEventsListening();
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000081DC File Offset: 0x000063DC
	public void SetInputDevice()
	{
		if (InputDevice.GetDevicesCount() == 0)
		{
			this.infoText.text = "No input devices found.";
			return;
		}
		if (SerialPort.GetPortNames().Length == 0)
		{
			this.infoText.text = "No COM ports selected.";
			return;
		}
		if (this.portOpen)
		{
			this.infoText.text = "Port already open.";
			return;
		}
		if (this.isRecordingMidi)
		{
			this.infoText.text = "MIDI recording in progress. Open port before recording.";
			return;
		}
		this.SaveLed();
		if (!this.isRecordingMidi)
		{
			this.inputDevice = InputDevice.GetById(this.inputDevices.value);
			this.inputDeviceDesposed = false;
			this.inputDevice.EventReceived += this.OnEventReceived;
			this.inputDevice.StartEventsListening();
		}
		else
		{
			this.inputDevice.EventReceived += this.OnEventReceived;
		}
		try
		{
			this.infoText.text = "Arduino is restarding.";
			this.portOpen = true;
			this.arduinoPort = new SerialPort();
			this.arduinoPort.PortName = SerialPort.GetPortNames()[this.serialPorts.value];
			this.arduinoPort.BaudRate = 9600;
			this.arduinoPort.Open();
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
			this.infoText.text = "Opening serial port failed.";
			return;
		}
		Thread.Sleep(2000);
		byte[] bytes = BitConverter.GetBytes(255);
		this.arduinoPort.Write(bytes, 0, bytes.Length);
		for (int i = 0; i < 3; i++)
		{
			Thread.Sleep(100);
			byte[] array;
			if (i != 0)
			{
				if (i != 1)
				{
					array = BitConverter.GetBytes((int)this.blueSlider.value);
				}
				else
				{
					array = BitConverter.GetBytes((int)this.greenSlider.value);
				}
			}
			else
			{
				array = BitConverter.GetBytes((int)this.redSlider.value);
			}
			this.arduinoPort.Write(array, 0, array.Length);
		}
		this.infoText.text = "Port open.";
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000083D4 File Offset: 0x000065D4
	public void ClosePort()
	{
		if (this.portOpen)
		{
			this.arduinoPort.Close();
			this.infoText.text = "Port closed.";
			this.portOpen = false;
		}
		if (this.inputDevice != null && this.inputDevice.IsListeningForEvents)
		{
			this.inputDevice.StopEventsListening();
			this.inputDevice.Dispose();
			this.inputDeviceDesposed = true;
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00008440 File Offset: 0x00006640
	private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
	{
		if (!this.portOpen)
		{
			return;
		}
		MidiDevice midiDevice = (MidiDevice)sender;
		if (e.Event.EventType.Equals(MidiEventType.NoteOn))
		{
			NoteOnEvent noteOnEvent = e.Event as NoteOnEvent;
			if (((int)noteOnEvent.NoteNumber > this.lastNote.value + 22 && (int)noteOnEvent.NoteNumber > this.firstNote.value + 22) || ((int)noteOnEvent.NoteNumber < this.lastNote.value + 22 && (int)noteOnEvent.NoteNumber < this.firstNote.value + 22))
			{
				return;
			}
			float num;
			if (this.firstNote.value < this.lastNote.value)
			{
				num = this.Remap((float)noteOnEvent.NoteNumber, (float)(this.firstNote.value + 22), (float)(this.lastNote.value + 22), 0f, (float)(this.numOfLeds.value + 60));
			}
			else
			{
				num = (float)(this.numOfLeds.value + 60) - this.Remap((float)noteOnEvent.NoteNumber, (float)(this.lastNote.value + 22), (float)(this.firstNote.value + 22), 0f, (float)(this.numOfLeds.value + 60));
			}
			byte[] bytes = BitConverter.GetBytes((short)((byte)num));
			this.arduinoPort.Write(bytes, 0, bytes.Length);
		}
		if (e.Event.EventType.Equals(MidiEventType.NoteOff))
		{
			NoteOffEvent noteOffEvent = e.Event as NoteOffEvent;
			if (((int)noteOffEvent.NoteNumber > this.lastNote.value + 22 && (int)noteOffEvent.NoteNumber > this.firstNote.value + 22) || ((int)noteOffEvent.NoteNumber < this.lastNote.value + 22 && (int)noteOffEvent.NoteNumber < this.firstNote.value + 22))
			{
				return;
			}
			float num2;
			if (this.firstNote.value < this.lastNote.value)
			{
				num2 = this.Remap((float)noteOffEvent.NoteNumber, (float)(this.firstNote.value + 22), (float)(this.lastNote.value + 22), 0f, (float)(this.numOfLeds.value + 60));
			}
			else
			{
				num2 = (float)(this.numOfLeds.value + 60) - this.Remap((float)noteOffEvent.NoteNumber, (float)(this.lastNote.value + 22), (float)(this.firstNote.value + 22), 0f, (float)(this.numOfLeds.value + 60));
			}
			byte[] bytes2 = BitConverter.GetBytes((short)((byte)num2));
			this.arduinoPort.Write(bytes2, 0, bytes2.Length);
		}
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00008740 File Offset: 0x00006940
	private void OnEventReceivedLivePlay(object sender, MidiEventReceivedEventArgs e)
	{
		MidiDevice midiDevice = (MidiDevice)sender;
		if (e.Event.EventType.Equals(MidiEventType.NoteOn))
		{
			Debug.Log("Event received from " + midiDevice.Name + ": " + e.Event.ToString());
			NoteOnEvent noteOnEvent = e.Event as NoteOnEvent;
			if (noteOnEvent.Velocity > 0)
			{
				SevenBitNumber noteNumber = noteOnEvent.NoteNumber;
				this.notesToSpawn.Add(noteNumber);
			}
			else
			{
				SevenBitNumber noteNumber2 = noteOnEvent.NoteNumber;
				this.notesToStop.Add(noteNumber2);
			}
		}
		if (e.Event.EventType.Equals(MidiEventType.NoteOff))
		{
			Debug.Log("Event received from " + midiDevice.Name + ": " + e.Event.ToString());
			SevenBitNumber noteNumber3 = (e.Event as NoteOffEvent).NoteNumber;
			this.notesToStop.Add(noteNumber3);
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00008854 File Offset: 0x00006A54
	public float Remap(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00008866 File Offset: 0x00006A66
	private void OnApplicationQuit()
	{
		if (!this.inputDeviceDesposed)
		{
			this.inputDevice.Dispose();
		}
		if (this.arduinoPort != null && this.arduinoPort.IsOpen)
		{
			this.arduinoPort.Close();
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000889C File Offset: 0x00006A9C
	public bool RecordMidi()
	{
		if (InputDevice.GetDevicesCount() == 0)
		{
			return false;
		}
		if (this.inputDeviceDesposed)
		{
			this.inputDevice = InputDevice.GetById(this.inputDevices.value);
			this.inputDeviceDesposed = false;
		}
		this.isRecordingMidi = true;
		this.recording = new Recording(TempoMap.Default, this.inputDevice);
		if (!this.inputDevice.IsListeningForEvents)
		{
			this.inputDevice.EventReceived += this.OnEventReceived;
			this.inputDevice.StartEventsListening();
		}
		this.recording.Start();
		return true;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00008930 File Offset: 0x00006B30
	public string StopMidiRecording(string midiToPath)
	{
		this.recording.Stop();
		Melanchall.DryWetMidi.Core.MidiFile midiFile = this.recording.ToFile();
		this.recording.Dispose();
		if (!this.portOpen)
		{
			this.inputDevice.StopEventsListening();
			this.inputDevice.Dispose();
			this.inputDeviceDesposed = true;
		}
		string text;
		try
		{
			midiFile.Write(midiToPath + "/Piano-VFX_" + DateTime.Now.ToString("MM_dd_yyyy_h_mm_ss") + ".mid", false, MidiFileFormat.MultiTrack, null);
			this.isRecordingMidi = false;
			text = "Recording finished. File moved to desired location.";
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
			this.isRecordingMidi = false;
			text = "Recording failed.";
		}
		return text;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000089E4 File Offset: 0x00006BE4
	public void SaveLed()
	{
		string text = JsonUtility.ToJson(new LedProfile
		{
			red = (int)this.redSlider.value,
			green = (int)this.greenSlider.value,
			blue = (int)this.blueSlider.value,
			firstLed = this.firstNote.value,
			lastLed = this.lastNote.value,
			numOfLeds = this.numOfLeds.value
		});
		PlayerPrefs.SetString("LedValues", text);
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00008A78 File Offset: 0x00006C78
	public void LoadLed(string json)
	{
		LedProfile ledProfile = new LedProfile();
		ledProfile = JsonUtility.FromJson<LedProfile>(json);
		this.redSlider.value = (float)ledProfile.red;
		this.greenSlider.value = (float)ledProfile.green;
		this.blueSlider.value = (float)ledProfile.blue;
		this.firstNote.value = ledProfile.firstLed;
		this.lastNote.value = ledProfile.lastLed;
		this.numOfLeds.value = ledProfile.numOfLeds;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00008AFB File Offset: 0x00006CFB
	private bool IsBlack(byte noteID)
	{
		return noteID == 22 || noteID == 25 || noteID == 27 || noteID == 30 || noteID == 32;
	}

	// Token: 0x0400016A RID: 362
	private InputDevice inputDevice;

	// Token: 0x0400016B RID: 363
	public Dropdown inputDevices;

	// Token: 0x0400016C RID: 364
	public Dropdown serialPorts;

	// Token: 0x0400016D RID: 365
	public Dropdown firstNote;

	// Token: 0x0400016E RID: 366
	public Dropdown lastNote;

	// Token: 0x0400016F RID: 367
	public Dropdown numOfLeds;

	// Token: 0x04000170 RID: 368
	public Text infoText;

	// Token: 0x04000171 RID: 369
	private SerialPort arduinoPort;

	// Token: 0x04000172 RID: 370
	public bool portOpen;

	// Token: 0x04000173 RID: 371
	public Slider redSlider;

	// Token: 0x04000174 RID: 372
	public Slider greenSlider;

	// Token: 0x04000175 RID: 373
	public Slider blueSlider;

	// Token: 0x04000176 RID: 374
	private bool touchSensitivity;

	// Token: 0x04000177 RID: 375
	private Recording recording;

	// Token: 0x04000178 RID: 376
	public bool isRecordingMidi;

	// Token: 0x04000179 RID: 377
	private bool inputDeviceDesposed = true;

	// Token: 0x0400017A RID: 378
	private int firstKeyID = 21;

	// Token: 0x0400017B RID: 379
	private int lastKeyID = 32;

	// Token: 0x0400017C RID: 380
	private float octaveLength = 2.3915f;

	// Token: 0x0400017D RID: 381
	private float[] keyXCoordinates = new float[]
	{
		-8.7069f, -8.4964f, -8.369f, -8.027f, -7.8754f, -7.681f, -7.4676f, -7.339f, -6.997f, -6.8493f,
		-6.659f, -6.489f
	};

	// Token: 0x0400017E RID: 382
	public GameObject whiteKey;

	// Token: 0x0400017F RID: 383
	public GameObject blackKey;

	// Token: 0x04000180 RID: 384
	private List<byte> notesToSpawn = new List<byte>();

	// Token: 0x04000181 RID: 385
	private List<byte> notesToStop = new List<byte>();

	// Token: 0x04000182 RID: 386
	private GameObject[] spawnedNotes = new GameObject[127];
}
