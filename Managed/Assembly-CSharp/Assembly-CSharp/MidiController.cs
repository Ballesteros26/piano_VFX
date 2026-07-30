using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class MidiController : MonoBehaviour
{
	// Token: 0x0600008A RID: 138 RVA: 0x00008D3A File Offset: 0x00006F3A
	public void GenerateTiles(string mfp)
	{
		this.midiFilePath = mfp;
		this.mf = new MidiFile(this.midiFilePath);
		this.distPerSecond = this.keys.GetComponent<MoveTile>().speed;
		this.VisualizeMidi();
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00008D70 File Offset: 0x00006F70
	private void VisualizeMidi()
	{
		float num = 0f;
		float num2 = 0f;
		List<uint> list = new List<uint>();
		List<float> list2 = new List<float>();
		if (this.mf.defaultTempo)
		{
			list2.Add(this.mf.tempoHistory.Dequeue());
			list.Add(0U);
		}
		byte b = 0;
		int num3 = -1;
		for (int i = 0; i < this.mf.tracks.Count; i++)
		{
			uint num4 = 0U;
			float num5 = 7f;
			for (int j = 0; j < this.mf.tracks[i].events.Count; j++)
			{
				num4 += this.mf.tracks[i].events[j].deltaTick;
				if (this.mf.tracks[i].events[j].type.Equals(144))
				{
					uint num6 = num4;
					if (i != num3)
					{
						num3 = i;
						b += 1;
						Debug.Log("New track color index: " + b);
					}
					for (int k = j + 1; k < this.mf.tracks[i].events.Count; k++)
					{
						num6 += this.mf.tracks[i].events[k].deltaTick;
						if (this.mf.tracks[i].events[k].type.Equals(128) && this.mf.tracks[i].events[j].key == this.mf.tracks[i].events[k].key)
						{
							byte b2 = this.mf.tracks[i].events[j].key;
							byte b3 = 0;
							while ((int)b2 < this.firstKeyID || (int)b2 > this.lastKeyID)
							{
								b2 -= 12;
								b3 += 1;
							}
							GameObject gameObject;
							if (this.IsBlack(b2))
							{
								gameObject = this.tileBlack;
							}
							else
							{
								gameObject = this.tileWhite;
							}
							bool flag = true;
							for (int l = list.Count - 1; l >= 0; l--)
							{
								if (list[l] <= num4)
								{
									if (flag)
									{
										flag = false;
										if (l == 0)
										{
											num = num4 * list2[l];
										}
										else
										{
											num = (num4 - list[l]) * list2[l];
										}
										num2 = list2[l];
									}
									else if (l == 0)
									{
										num += list[l + 1] * list2[l];
									}
									else
									{
										num += (list[l + 1] - list[l]) * list2[l];
									}
								}
							}
							Vector2 vector;
							if (this.moveDown)
							{
								vector = new Vector3(this.keyXCoordinates[(int)b2 - this.firstKeyID] + (float)b3 * this.octaveLength, num5 + num * this.distPerSecond, (float)(-(float)(1 / b)));
							}
							else
							{
								vector = new Vector3(this.keyXCoordinates[(int)b2 - this.firstKeyID] + (float)b3 * this.octaveLength, -(num5 + 5.8f + num * this.distPerSecond), (float)(-(float)(1 / b)));
							}
							GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(gameObject, vector, Quaternion.identity);
							if (!this.moveDown)
							{
								gameObject2.transform.Rotate(0f, 0f, 180f);
							}
							gameObject2.GetComponent<SpawnEffect>().note = this.mf.tracks[i].events[j].key;
							gameObject2.GetComponent<SpawnEffect>().velocity = this.mf.tracks[i].events[j].velocity;
							gameObject2.GetComponent<SpawnEffect>().ApplyColor(b);
							Debug.Log(b);
							this.lastNoteTime = num5 + num;
							float num7 = (num6 - num4) * num2 * this.distPerSecond / 5f;
							if (num7 > 0.03f)
							{
								num7 -= 0.03f;
							}
							else if (num7 <= 0f)
							{
								num7 = 1E-07f;
							}
							gameObject2.transform.localScale = new Vector2(gameObject2.transform.localScale.x, num7);
							if (gameObject2.transform.localScale.y < 0.03f)
							{
								gameObject2.transform.localScale = new Vector2(gameObject2.transform.localScale.x, 0.03f);
							}
							gameObject2.transform.GetComponent<Renderer>().material.SetVector("_Tiling", gameObject2.transform.localScale);
							if (this.fileManager.GetComponent<FileManager>().animatedTextures.value == 5)
							{
								gameObject2.transform.GetComponent<Renderer>().material.SetFloat("_TextureSize", gameObject2.transform.GetComponent<Renderer>().material.GetFloat("_TextureSize") * 1.5f / gameObject2.transform.localScale.y);
								gameObject2.transform.GetComponent<Renderer>().material.SetVector("_Tiling", new Vector2(gameObject2.transform.localScale.x + gameObject2.transform.localScale.y, gameObject2.transform.localScale.y));
								gameObject2.transform.GetComponent<Renderer>().material.SetVector("_Tiling2", new Vector2(gameObject2.transform.localScale.x, gameObject2.transform.localScale.y));
							}
							gameObject2.transform.GetComponent<Renderer>().material.SetFloat("_StartPoint", (float)global::UnityEngine.Random.Range(0, 100));
							gameObject2.transform.GetComponent<Renderer>().material.SetVector("_CornerTiling", new Vector2(1f, gameObject2.transform.localScale.y * 1f / gameObject2.transform.localScale.x));
							gameObject2.transform.GetComponent<Renderer>().material.SetVector("_CornerOffset", new Vector2(0f, (1f - gameObject2.transform.localScale.y * 1f / gameObject2.transform.localScale.x) / 2f));
							gameObject2.transform.GetComponent<Renderer>().material.SetFloat("_CornerHeight", gameObject2.transform.localScale.y * 1f / gameObject2.transform.localScale.x);
							gameObject2.GetComponent<BoxCollider2D>().size = new Vector2(gameObject2.GetComponent<BoxCollider2D>().size.x, gameObject2.GetComponent<BoxCollider2D>().size.y + 2.8f / gameObject2.transform.localScale.y / 200f);
							if (this.fileLength < gameObject2.transform.localScale.y * 5f + num5 + num * this.distPerSecond)
							{
								this.fileLength = gameObject2.transform.localScale.y * 5f + num5 + num * this.distPerSecond;
							}
							gameObject2.transform.parent = this.keys.transform;
							break;
						}
					}
				}
				else if (this.mf.tracks[i].events[j].type.Equals(176))
				{
					if (this.mf.tracks[i].events[j].key == 64)
					{
						bool flag2 = true;
						for (int m = list.Count - 1; m >= 0; m--)
						{
							if (list[m] <= num4)
							{
								if (flag2)
								{
									flag2 = false;
									if (m == 0)
									{
										num = num4 * list2[m];
									}
									else
									{
										num = (num4 - list[m]) * list2[m];
									}
									num2 = list2[m];
								}
								else if (m == 0)
								{
									num += list[m + 1] * list2[m];
								}
								else
								{
									num += (list[m + 1] - list[m]) * list2[m];
								}
							}
						}
						GameObject gameObject2;
						if (this.mf.tracks[i].events[j].velocity > 0)
						{
							if (this.moveDown)
							{
								gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(this.pedalObj, new Vector2(0f, num5 + num * this.distPerSecond - 0.075f), Quaternion.identity);
							}
							else
							{
								gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(this.pedalObj, new Vector2(0f, -(num5 + 5.82f + num * this.distPerSecond) - 0.075f), Quaternion.identity);
							}
							gameObject2.GetComponent<PedalController>().pedalDown = true;
						}
						else
						{
							if (this.moveDown)
							{
								gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(this.pedalObj, new Vector2(0f, num5 + num * this.distPerSecond - 0.15f), Quaternion.identity);
							}
							else
							{
								gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(this.pedalObj, new Vector2(0f, -(num5 + 5.8f + num * this.distPerSecond) - 0.15f), Quaternion.identity);
							}
							gameObject2.GetComponent<PedalController>().pedalDown = false;
						}
						gameObject2.transform.parent = this.keys.transform;
					}
				}
				else if (this.mf.tracks[i].events[j].type.Equals(96))
				{
					list2.Add(this.mf.tempoHistory.Dequeue());
					list.Add(num4);
				}
			}
		}
		this.keys.gameObject.transform.position = new Vector3(0f, 0f, -1f);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00008AFB File Offset: 0x00006CFB
	private bool IsBlack(byte noteID)
	{
		return noteID == 22 || noteID == 25 || noteID == 27 || noteID == 30 || noteID == 32;
	}

	// Token: 0x04000189 RID: 393
	private MidiFile mf;

	// Token: 0x0400018A RID: 394
	public GameObject tileWhite;

	// Token: 0x0400018B RID: 395
	public GameObject tileBlack;

	// Token: 0x0400018C RID: 396
	public GameObject keys;

	// Token: 0x0400018D RID: 397
	public GameObject fileManager;

	// Token: 0x0400018E RID: 398
	public float tileWidth = 0.05f;

	// Token: 0x0400018F RID: 399
	public float xStart;

	// Token: 0x04000190 RID: 400
	private float[] keyXCoordinates = new float[]
	{
		-8.7069f, -8.4964f, -8.369f, -8.027f, -7.8754f, -7.681f, -7.4676f, -7.339f, -6.997f, -6.8493f,
		-6.659f, -6.489f
	};

	// Token: 0x04000191 RID: 401
	private int firstKeyID = 21;

	// Token: 0x04000192 RID: 402
	private int lastKeyID = 32;

	// Token: 0x04000193 RID: 403
	private float octaveLength = 2.3915f;

	// Token: 0x04000194 RID: 404
	private float distPerSecond;

	// Token: 0x04000195 RID: 405
	public bool canPlaySound = true;

	// Token: 0x04000196 RID: 406
	public GameObject pedalObj;

	// Token: 0x04000197 RID: 407
	public string midiFilePath;

	// Token: 0x04000198 RID: 408
	public float lastNoteTime;

	// Token: 0x04000199 RID: 409
	public float fileLength;

	// Token: 0x0400019A RID: 410
	public bool moveDown;
}
