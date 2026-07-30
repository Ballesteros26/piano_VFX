using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class ProjectObject
{
	// Token: 0x06000138 RID: 312 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
	public ProjectObject(string name, string midiPath, string audioPath, string videoPath, string imagePath, string colorProfileData, string userValuesData, string editorValuesData)
	{
		this.name = name;
		this.midiPath = midiPath;
		this.audioPath = audioPath;
		this.videoPath = videoPath;
		this.imagePath = imagePath;
		this.colorProfileData = colorProfileData;
		this.userValuesData = userValuesData;
		this.editorValuesData = editorValuesData;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000C462 File Offset: 0x0000A662
	public string GetJSON()
	{
		return JsonUtility.ToJson(this);
	}

	// Token: 0x040002F0 RID: 752
	public string name;

	// Token: 0x040002F1 RID: 753
	public string midiPath;

	// Token: 0x040002F2 RID: 754
	public string audioPath;

	// Token: 0x040002F3 RID: 755
	public string videoPath;

	// Token: 0x040002F4 RID: 756
	public string imagePath;

	// Token: 0x040002F5 RID: 757
	public string colorProfileData;

	// Token: 0x040002F6 RID: 758
	public string userValuesData;

	// Token: 0x040002F7 RID: 759
	public string editorValuesData;
}
