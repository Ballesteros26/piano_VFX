using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class AudioVisualizer : MonoBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002A44 File Offset: 0x00000C44
	public void SpawnRing()
	{
		this.audioSource = this.fileManagerObj.GetComponent<FileManager>().audioSource;
		this.sampleRate = (float)AudioSettings.outputSampleRate;
		this.samples = new float[this.bufferSampleSize];
		this.spectrum = new float[this.bufferSampleSize];
		VisualizationMode visualizationMode = this.visualizationMode;
		if (visualizationMode != VisualizationMode.Ring)
		{
			if (visualizationMode == VisualizationMode.Line)
			{
				this.InitiateLine();
			}
		}
		else
		{
			this.InitiateRing();
		}
		this.audioReactorOn = true;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002ABC File Offset: 0x00000CBC
	private void InitiateRing()
	{
		FileManager component = this.fileManagerObj.GetComponent<FileManager>();
		this.extendLengths = new float[this.amountOfSegments + 1];
		this.lineRenderers = new LineRenderer[this.extendLengths.Length];
		for (int i = 0; i < this.lineRenderers.Length; i++)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.lineRendererPrefab);
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.parent = base.transform;
			LineRenderer component2 = gameObject.GetComponent<LineRenderer>();
			this.lineRendererMaterial.SetVector("_BarColor", new Color(component.reactorRedSlider.value, component.reactorGreenSlider.value, component.reactorBlueSlider.value) * component.reactorGlowSlider.value * 130f);
			component2.sharedMaterial = this.lineRendererMaterial;
			component2.positionCount = 2;
			component2.useWorldSpace = false;
			this.lineRenderers[i] = component2;
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002BC0 File Offset: 0x00000DC0
	private void InitiateLine()
	{
		FileManager component = this.fileManagerObj.GetComponent<FileManager>();
		this.extendLengths = new float[this.amountOfSegments + 1];
		this.lineRenderers = new LineRenderer[this.extendLengths.Length];
		for (int i = 0; i < this.lineRenderers.Length; i++)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.lineRendererPrefab);
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.parent = base.transform;
			LineRenderer component2 = gameObject.GetComponent<LineRenderer>();
			this.lineRendererMaterial.SetVector("_BarColor", new Color(component.reactorRedSlider.value, component.reactorGreenSlider.value, component.reactorBlueSlider.value) * component.reactorGlowSlider.value * 130f);
			component2.sharedMaterial = this.lineRendererMaterial;
			component2.positionCount = 2;
			component2.useWorldSpace = false;
			this.lineRenderers[i] = component2;
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002CC4 File Offset: 0x00000EC4
	private void Update()
	{
		if (!this.audioReactorOn)
		{
			return;
		}
		this.audioSource.GetSpectrumData(this.spectrum, 0, FFTWindow.BlackmanHarris);
		this.UpdateExtends();
		if (this.visualizationMode == VisualizationMode.Ring)
		{
			this.UpdateRing();
			return;
		}
		if (this.visualizationMode == VisualizationMode.Line)
		{
			this.UpdateLine();
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002D14 File Offset: 0x00000F14
	public void ManualUpdate()
	{
		this.audioSource.Play();
		this.audioSource.GetSpectrumData(this.spectrum, 0, FFTWindow.BlackmanHarris);
		this.audioSource.Pause();
		this.UpdateExtends();
		if (this.visualizationMode == VisualizationMode.Ring)
		{
			this.UpdateRing();
			return;
		}
		if (this.visualizationMode == VisualizationMode.Line)
		{
			this.UpdateLine();
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002D70 File Offset: 0x00000F70
	private void UpdateExtends()
	{
		int i = 0;
		int num = 0;
		int num2 = (int)(Mathf.Abs((float)this.samples.Length * this.samplePercentage) / (float)this.amountOfSegments);
		if (num2 < 1)
		{
			num2 = 1;
		}
		while (i < this.amountOfSegments)
		{
			int j = 0;
			float num3 = 0f;
			while (j < num2)
			{
				num3 += this.spectrum[num];
				num++;
				j++;
			}
			float num4 = num3 / (float)num2 * this.emphasisMultiplier;
			this.extendLengths[i] -= this.retractionSpeed * Time.deltaTime;
			if (this.extendLengths[i] < num4)
			{
				this.extendLengths[i] = num4;
			}
			if (this.extendLengths[i] > this.maximumExtendLenght)
			{
				this.extendLengths[i] = this.maximumExtendLenght + (this.extendLengths[i] - this.maximumExtendLenght) / (this.extendLengths[i] % (this.maximumExtendLenght * 100f));
			}
			i++;
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002E68 File Offset: 0x00001068
	private void UpdateRing()
	{
		for (int i = 0; i < this.lineRenderers.Length; i++)
		{
			float num = (float)i / ((float)this.lineRenderers.Length - 2f) * 3.1415927f * 2f;
			Vector2 vector = new Vector2(Mathf.Cos(num), Mathf.Sin(num));
			float num2 = this.radius + this.bufferSizeArea + this.extendLengths[i];
			this.lineRenderers[i].SetPosition(0, vector * this.radius / this.sizeIndex);
			this.lineRenderers[i].SetPosition(1, vector * num2 / this.sizeIndex);
			this.lineRenderers[i].startWidth = this.Spacing(this.radius / this.sizeIndex);
			this.lineRenderers[i].endWidth = this.Spacing(num2 / this.sizeIndex);
			this.lineRenderers[i].startColor = this.colorGradientA.Evaluate(0f);
			this.lineRenderers[i].endColor = this.colorGradientA.Evaluate((this.extendLengths[i] - 1f) / (this.maximumExtendLenght - 1f));
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002FB4 File Offset: 0x000011B4
	private void UpdateLine()
	{
		for (int i = 0; i < this.lineRenderers.Length - 1; i++)
		{
			int num = -1;
			if (i % 2 == 0)
			{
				num = 1;
			}
			this.lineRenderers[i].SetPosition(0, new Vector3(((float)num * ((float)i + 0.5f) / 4f + this.lineRenderers[i].transform.parent.position.x) / this.sizeIndex, (0f + this.lineRenderers[i].transform.parent.position.y) / this.sizeIndex, 0f));
			this.lineRenderers[i].SetPosition(1, new Vector3(((float)num * ((float)i + 0.5f) / 4f + this.lineRenderers[i].transform.parent.position.x) / this.sizeIndex, (this.bufferSizeArea / 10f + this.extendLengths[i] + this.lineRenderers[i].transform.parent.position.y) / this.sizeIndex, 0f));
			this.lineRenderers[i].startWidth = this.Spacing(2f / this.sizeIndex * this.lineWidth);
			this.lineRenderers[i].endWidth = this.Spacing(2f / this.sizeIndex * this.lineWidth);
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00003130 File Offset: 0x00001330
	private float Spacing(float radius)
	{
		float num = 6.2831855f * radius;
		float num2 = (float)this.lineRenderers.Length;
		return num / num2;
	}

	// Token: 0x04000037 RID: 55
	public bool audioReactorOn;

	// Token: 0x04000038 RID: 56
	public bool isRendering;

	// Token: 0x04000039 RID: 57
	public int bufferSampleSize;

	// Token: 0x0400003A RID: 58
	public float samplePercentage;

	// Token: 0x0400003B RID: 59
	public float emphasisMultiplier;

	// Token: 0x0400003C RID: 60
	public float retractionSpeed;

	// Token: 0x0400003D RID: 61
	public int amountOfSegments;

	// Token: 0x0400003E RID: 62
	public float radius;

	// Token: 0x0400003F RID: 63
	public float bufferSizeArea;

	// Token: 0x04000040 RID: 64
	public float maximumExtendLenght;

	// Token: 0x04000041 RID: 65
	public GameObject lineRendererPrefab;

	// Token: 0x04000042 RID: 66
	public GameObject fileManagerObj;

	// Token: 0x04000043 RID: 67
	public Material lineRendererMaterial;

	// Token: 0x04000044 RID: 68
	public VisualizationMode visualizationMode;

	// Token: 0x04000045 RID: 69
	public Gradient colorGradientA = new Gradient();

	// Token: 0x04000046 RID: 70
	public Gradient colorGradientB = new Gradient();

	// Token: 0x04000047 RID: 71
	private Gradient currentColor = new Gradient();

	// Token: 0x04000048 RID: 72
	private float sampleRate;

	// Token: 0x04000049 RID: 73
	private float[] samples;

	// Token: 0x0400004A RID: 74
	private float[] spectrum;

	// Token: 0x0400004B RID: 75
	private float[] extendLengths;

	// Token: 0x0400004C RID: 76
	private LineRenderer[] lineRenderers;

	// Token: 0x0400004D RID: 77
	private AudioSource audioSource;

	// Token: 0x0400004E RID: 78
	public float sizeIndex = 1f;

	// Token: 0x0400004F RID: 79
	public float lineWidth = 1f;
}
