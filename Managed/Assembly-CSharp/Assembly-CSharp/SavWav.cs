using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000021 RID: 33
public static class SavWav
{
	// Token: 0x0600013B RID: 315 RVA: 0x0000FB28 File Offset: 0x0000DD28
	public static string Save(string filename, AudioClip clip)
	{
		if (!filename.ToLower().EndsWith(".wav"))
		{
			filename += ".wav";
		}
		string text = Path.Combine(Application.persistentDataPath, filename);
		Debug.Log(text);
		Directory.CreateDirectory(Path.GetDirectoryName(text));
		using (FileStream fileStream = SavWav.CreateEmpty(text))
		{
			SavWav.ConvertAndWrite(fileStream, clip);
			SavWav.WriteHeader(fileStream, clip);
		}
		return text;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
	public static AudioClip TrimSilence(AudioClip clip, float min)
	{
		float[] array = new float[clip.samples];
		clip.GetData(array, 0);
		return SavWav.TrimSilence(new List<float>(array), min, clip.channels, clip.frequency);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000FBDE File Offset: 0x0000DDDE
	public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz)
	{
		return SavWav.TrimSilence(samples, min, channels, hz, false, false);
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000FBEC File Offset: 0x0000DDEC
	public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz, bool _3D, bool stream)
	{
		int num = 0;
		while (num < samples.Count && Mathf.Abs(samples[num]) <= min)
		{
			num++;
		}
		samples.RemoveRange(0, num);
		num = samples.Count - 1;
		while (num > 0 && Mathf.Abs(samples[num]) <= min)
		{
			num--;
		}
		samples.RemoveRange(num, samples.Count - num);
		AudioClip audioClip = AudioClip.Create("TempClip", samples.Count, channels, hz, _3D, stream);
		audioClip.SetData(samples.ToArray(), 0);
		return audioClip;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000FC78 File Offset: 0x0000DE78
	private static FileStream CreateEmpty(string filepath)
	{
		FileStream fileStream = new FileStream(filepath, FileMode.Create);
		byte b = 0;
		for (int i = 0; i < 44; i++)
		{
			fileStream.WriteByte(b);
		}
		return fileStream;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
	private static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
	{
		float[] array = new float[clip.samples];
		clip.GetData(array, 0);
		short[] array2 = new short[array.Length];
		byte[] array3 = new byte[array.Length * 2];
		int num = 32767;
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = (short)(array[i] * (float)num);
			new byte[2];
			BitConverter.GetBytes(array2[i]).CopyTo(array3, i * 2);
		}
		fileStream.Write(array3, 0, array3.Length);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000FD24 File Offset: 0x0000DF24
	private static void WriteHeader(FileStream fileStream, AudioClip clip)
	{
		int frequency = clip.frequency;
		int channels = clip.channels;
		int samples = clip.samples;
		fileStream.Seek(0L, SeekOrigin.Begin);
		byte[] bytes = Encoding.UTF8.GetBytes("RIFF");
		fileStream.Write(bytes, 0, 4);
		byte[] bytes2 = BitConverter.GetBytes(fileStream.Length - 8L);
		fileStream.Write(bytes2, 0, 4);
		byte[] bytes3 = Encoding.UTF8.GetBytes("WAVE");
		fileStream.Write(bytes3, 0, 4);
		byte[] bytes4 = Encoding.UTF8.GetBytes("fmt ");
		fileStream.Write(bytes4, 0, 4);
		byte[] bytes5 = BitConverter.GetBytes(16);
		fileStream.Write(bytes5, 0, 4);
		byte[] bytes6 = BitConverter.GetBytes(1);
		fileStream.Write(bytes6, 0, 2);
		byte[] bytes7 = BitConverter.GetBytes(channels);
		fileStream.Write(bytes7, 0, 2);
		byte[] bytes8 = BitConverter.GetBytes(frequency);
		fileStream.Write(bytes8, 0, 4);
		byte[] bytes9 = BitConverter.GetBytes(frequency * channels * 2);
		fileStream.Write(bytes9, 0, 4);
		ushort num = (ushort)(channels * 2);
		fileStream.Write(BitConverter.GetBytes(num), 0, 2);
		byte[] bytes10 = BitConverter.GetBytes(16);
		fileStream.Write(bytes10, 0, 2);
		byte[] bytes11 = Encoding.UTF8.GetBytes("data");
		fileStream.Write(bytes11, 0, 4);
		byte[] bytes12 = BitConverter.GetBytes(samples * channels * 2);
		fileStream.Write(bytes12, 0, 4);
	}

	// Token: 0x04000303 RID: 771
	private const int HEADER_SIZE = 44;
}
