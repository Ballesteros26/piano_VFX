using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000177 RID: 375
	internal static class HDBakingUtilities
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x00053274 File Offset: 0x00051474
		public static string HDProbeAssetPattern(ProbeSettings.ProbeType type)
		{
			return string.Format("{0}-*.exr", type);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00053288 File Offset: 0x00051488
		public static string GetBakedTextureDirectory(Scene scene)
		{
			string path = scene.path;
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			return Path.Combine(Path.GetDirectoryName(path), fileNameWithoutExtension);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000532BE File Offset: 0x000514BE
		public static string GetBakedTextureFilePath(HDProbe probe)
		{
			return HDBakingUtilities.GetBakedTextureFilePath(probe.settings.type, SceneObjectIDMap.GetOrCreateSceneObjectID<HDBakingUtilities.SceneObjectCategory>(probe.gameObject, HDBakingUtilities.SceneObjectCategory.ReflectionProbe), probe.gameObject.scene);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000532E8 File Offset: 0x000514E8
		public static bool TryParseBakedProbeAssetFileName(string filename, out ProbeSettings.ProbeType type, out int index)
		{
			Match match = HDBakingUtilities.k_HDProbeAssetRegex.Match(filename);
			if (!match.Success)
			{
				type = ProbeSettings.ProbeType.ReflectionProbe;
				index = 0;
				return false;
			}
			type = (ProbeSettings.ProbeType)Enum.Parse(typeof(ProbeSettings.ProbeType), match.Groups["type"].Value);
			index = int.Parse(match.Groups["index"].Value);
			return true;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00053359 File Offset: 0x00051559
		public static string GetBakedTextureFilePath(ProbeSettings.ProbeType probeType, int index, Scene scene)
		{
			return Path.Combine(HDBakingUtilities.GetBakedTextureDirectory(scene), string.Format("{0}-{1}.exr", probeType, index));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0005337C File Offset: 0x0005157C
		public static void CreateParentDirectoryIfMissing(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			if (!fileInfo.Directory.Exists)
			{
				fileInfo.Directory.Create();
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000533A8 File Offset: 0x000515A8
		public static bool TrySerializeToDisk<T>(T renderData, string filePath)
		{
			HDBakingUtilities.CreateParentDirectoryIfMissing(filePath);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filePath, FileMode.Create);
				xmlSerializer.Serialize(fileStream, renderData);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				return false;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
			}
			return true;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00053418 File Offset: 0x00051618
		public static bool TryDeserializeFromDisk<T>(string filePath, out T renderData)
		{
			if (!File.Exists(filePath))
			{
				renderData = default(T);
				return false;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			bool flag;
			try
			{
				FileStream fileStream = new FileStream(filePath, FileMode.Open);
				renderData = (T)((object)xmlSerializer.Deserialize(fileStream));
				flag = true;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				renderData = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400103C RID: 4156
		private const string k_HDProbeAssetFormat = "{0}-{1}.exr";

		// Token: 0x0400103D RID: 4157
		private static readonly Regex k_HDProbeAssetRegex = new Regex("(?<type>ReflectionProbe|PlanarProbe)-(?<index>\\d+)\\.exr");

		// Token: 0x0200029A RID: 666
		public enum SceneObjectCategory
		{
			// Token: 0x04001708 RID: 5896
			ReflectionProbe
		}
	}
}
