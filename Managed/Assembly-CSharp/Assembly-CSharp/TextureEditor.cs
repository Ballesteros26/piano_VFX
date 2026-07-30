using System;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000024 RID: 36
public class TextureEditor : MonoBehaviour
{
	// Token: 0x0600014F RID: 335 RVA: 0x00010FFE File Offset: 0x0000F1FE
	private void Start()
	{
		if (PlayerPrefs.GetString("CustomTexture").Length > 0)
		{
			this.RestoreTextureValues(PlayerPrefs.GetString("CustomTexture"));
			Debug.Log(PlayerPrefs.GetString("CustomTexture"));
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00011034 File Offset: 0x0000F234
	public void RefreshPrev()
	{
		if (PlayerPrefs.GetString("CustomTexture").Length > 0)
		{
			this.RestoreTextureValues(PlayerPrefs.GetString("CustomTexture"));
		}
		else
		{
			this.tileMaterial.SetTexture("_UserTexture", this.defaultTexture);
			this.tileMaterial.SetFloat("_TextureSize", 0f);
			this.tileMaterial.SetFloat("_NoiseScale", 0f);
			this.tileMaterial.SetFloat("_DistortionAmount", 0f);
			this.tileMaterial.SetVector("_TextureSpeed", new Vector2(0f, 0f));
			this.tileMaterial.SetVector("_DistortionSpeed", new Vector2(0f, 0f));
		}
		this.tileMaterial.SetVector("_Tiling", new Vector2(this.prevTile.transform.localScale.x + 1.7f, this.prevTile.transform.localScale.y));
		this.tileMaterial.SetVector("_Tiling2", new Vector2(this.prevTile.transform.localScale.x, this.prevTile.transform.localScale.y));
		this.tileMaterial.SetVector("_CornerTiling", new Vector2(1f, this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x));
		this.tileMaterial.SetVector("_CornerOffset", new Vector2(0f, (1f - this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x) / 2f));
		this.tileMaterial.SetFloat("_CornerHeight", this.prevTile.transform.localScale.y * 1f / this.prevTile.transform.localScale.x);
		this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00011298 File Offset: 0x0000F498
	public void OpenTextureImage()
	{
		ExtensionFilter[] array = new ExtensionFilter[]
		{
			new ExtensionFilter("Image File", new string[] { "png", "jpg", "jpeg" })
		};
		StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", array, false, delegate(string[] paths)
		{
			if (paths.Length == 0)
			{
				return;
			}
			this.textureImagePath = paths[0];
			this.pathText.text = paths[0];
			this.OpenImageFile();
		});
	}

	// Token: 0x06000152 RID: 338 RVA: 0x000112F8 File Offset: 0x0000F4F8
	public void OpenImageFile()
	{
		if (File.Exists(this.textureImagePath))
		{
			byte[] array = File.ReadAllBytes(this.textureImagePath);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.LoadImage(array);
			this.tileMaterial.SetTexture("_UserTexture", texture2D);
			this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
			return;
		}
		this.pathText.text = "File is missing!";
		this.tileMaterial.SetTexture("_UserTexture", this.defaultTexture);
		this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00011394 File Offset: 0x0000F594
	public void ChangeTextureSpeedX()
	{
		this.tileMaterial.SetVector("_TextureSpeed", new Vector2(this.TextureSpeedXSlider.value, this.TextureSpeedYSlider.value));
		this.TextureSpeedXText.text = "Texture Speed X: " + Math.Round((double)this.TextureSpeedXSlider.value, 2);
		this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00011414 File Offset: 0x0000F614
	public void ChangeTextureSpeedY()
	{
		this.tileMaterial.SetVector("_TextureSpeed", new Vector2(this.TextureSpeedXSlider.value, this.TextureSpeedYSlider.value));
		this.TextureSpeedYText.text = "Texture Speed Y: " + Math.Round((double)this.TextureSpeedYSlider.value, 2);
		this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00011494 File Offset: 0x0000F694
	public void ChangeDistortionSpeed()
	{
		this.tileMaterial.SetVector("_DistortionSpeed", new Vector2(this.DistortionSpeedSlider.value, 0f));
		this.DistortionSpeedText.text = "Distortion Speed: " + Math.Round((double)this.DistortionSpeedSlider.value, 2);
		this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00011510 File Offset: 0x0000F710
	public void ChangeDistortionAmount()
	{
		this.tileMaterial.SetFloat("_DistortionAmount", this.DistortionAmountSlider.value);
		this.DistortionAmountText.text = "Distortion Amount: " + Math.Round((double)this.DistortionAmountSlider.value, 2);
		this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0001157C File Offset: 0x0000F77C
	public void ChangeNoiseScale()
	{
		this.tileMaterial.SetFloat("_NoiseScale", this.NoiseScaleSlider.value);
		this.NoiseScaleText.text = "Noise Scale: " + Math.Round((double)this.NoiseScaleSlider.value, 2);
		this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x000115E8 File Offset: 0x0000F7E8
	public void ChangeTextureSize()
	{
		this.tileMaterial.SetFloat("_TextureSize", this.TextureSizeSlider.value);
		this.TextureSizeText.text = "Texture Size: " + Math.Round((double)this.TextureSizeSlider.value, 2);
		this.prevTile.GetComponent<Renderer>().material = this.tileMaterial;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00011654 File Offset: 0x0000F854
	public void SaveTextureValues()
	{
		string text = JsonUtility.ToJson(new TextureObject
		{
			textureImagePath = this.textureImagePath,
			textureSpeedX = this.TextureSpeedXSlider.value,
			textureSpeedY = this.TextureSpeedYSlider.value,
			distortionSpeed = this.DistortionSpeedSlider.value,
			distortionAmount = this.DistortionAmountSlider.value,
			noiseScale = this.NoiseScaleSlider.value,
			textureSize = this.TextureSizeSlider.value
		});
		PlayerPrefs.SetString("CustomTexture", text);
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000116F0 File Offset: 0x0000F8F0
	public void RestoreTextureValues(string json)
	{
		TextureObject textureObject = new TextureObject();
		textureObject = JsonUtility.FromJson<TextureObject>(json);
		this.textureImagePath = textureObject.textureImagePath;
		this.pathText.text = this.textureImagePath;
		this.OpenImageFile();
		this.TextureSpeedXSlider.value = textureObject.textureSpeedX;
		this.TextureSpeedYSlider.value = textureObject.textureSpeedY;
		this.DistortionSpeedSlider.value = textureObject.distortionSpeed;
		this.DistortionAmountSlider.value = textureObject.distortionAmount;
		this.NoiseScaleSlider.value = textureObject.noiseScale;
		this.TextureSizeSlider.value = textureObject.textureSize;
	}

	// Token: 0x04000334 RID: 820
	private string textureImagePath;

	// Token: 0x04000335 RID: 821
	public Text pathText;

	// Token: 0x04000336 RID: 822
	public Material tileMaterial;

	// Token: 0x04000337 RID: 823
	public GameObject prevTile;

	// Token: 0x04000338 RID: 824
	public Slider TextureSpeedXSlider;

	// Token: 0x04000339 RID: 825
	public Slider TextureSpeedYSlider;

	// Token: 0x0400033A RID: 826
	public Slider DistortionSpeedSlider;

	// Token: 0x0400033B RID: 827
	public Slider DistortionAmountSlider;

	// Token: 0x0400033C RID: 828
	public Slider NoiseScaleSlider;

	// Token: 0x0400033D RID: 829
	public Slider TextureSizeSlider;

	// Token: 0x0400033E RID: 830
	public Text TextureSpeedXText;

	// Token: 0x0400033F RID: 831
	public Text TextureSpeedYText;

	// Token: 0x04000340 RID: 832
	public Text DistortionSpeedText;

	// Token: 0x04000341 RID: 833
	public Text DistortionAmountText;

	// Token: 0x04000342 RID: 834
	public Text NoiseScaleText;

	// Token: 0x04000343 RID: 835
	public Text TextureSizeText;

	// Token: 0x04000344 RID: 836
	public Texture2D defaultTexture;
}
