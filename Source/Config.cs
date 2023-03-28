using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class Config
{
	public int MainToolStripScreen;//maintoolbar current screen;
	public int currentScreen;//which screen is the UI on?
	public int InfoViewsContainerHeight;//what is the InfoViewsContainer height?
	public int InfoViewsContainerScreen;//what is the statistics button width.
	public int StatisticsButtonHeight;//what is the info menu height?
	public int StatisticsButtonScreen;//what is the statistics button width.
	public int InfoPanelHeight;//what is the info panel height?
	public int MainToolStripHeight;//what is the MainToolstrip height?
	public int ThumbnailBarHeight;//what is the thumbnailbar height?
	public int TSBarHeight;//what is the TSBarHeightHeight?
	//
	public int FullScreenContainerHeight;
	public int MultiScreenWidth = 0;
	public int MultiScreenHeight = 0;
	public int SingleScreenWidth = 0;
	public int SingleScreenHeight = 0;
	public float CameraFov = 30;
	public bool isFullScreen = true;

	public void OnPreSerialize () {
	}

	public void OnPostDeserialize () {
	}

	public static void Serialize (string filename, Config config) {
		XmlSerializer serializer = new XmlSerializer(typeof(Config));
		using(StreamWriter writer = new StreamWriter(filename)) {
			config.OnPreSerialize();
			serializer.Serialize(writer, config);
		}
	}

	public static Config Deserialize (string filename) {
		XmlSerializer serializer = new XmlSerializer(typeof(Config));
		try {
			using(StreamReader reader = new StreamReader(filename)) {
				var config = (Config)serializer.Deserialize(reader);
				config.OnPostDeserialize();
				return config;
			}
		} catch {
		}
		return null;
	}
}