using UnityEngine;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

public class MMC : IUserMod
{
	public string ModName = "MMC";
	public string ModDescription = "MultiMonitor Cities. A simple mod that allows the game to use Surround / Eyefinity.";

	public string Name {
		get {
			return ModName;
		}
	}
	public string Description {
		get {
			return ModDescription;
		}
	}
	public class LoadGame : LoadingExtensionBase
	{
		public static readonly string configpath = "MMC_Config.xml";
		public Config config;
		public AppMode AppMode;
		public Texture2D MMCBufferTexture;
		public int MainToolStripScreen;//maintoolbar current screen;
		public int currentScreen;//which screen is the UI on?
		public int InfoViewsContainerHeight;//what is the InfoViewsContainer height?
		public int StatisticsButtonHeight;//what is the info menu height?
		public int StatisticsButtonScreen;//what is the statistics button width.
		public int InfoViewsContainerScreen;//what is the statistics button width.
		public int InfoPanelHeight;//what is the info panel height?
		public int MainToolStripHeight;//what is the MainToolstrip height?
		public int ThumbnailBarHeight;//what is the thumbnailbar height?
		public int TSBarHeight;//what is the TSBarHeightHeight?
		//
		public int FullScreenContainerHeight;//what is the full screen height;
		public int ScreenWidth;//what is the width of all the monitors?
		public int ScreenHeight;//what is the height of all the monitors?
		public int ActualScreenHeight;//what is the height of one monitor?
		public int ActualScreenWidth;//what is the width of one monitor?
		public float FieldOfView;//what is your FOV?
		public float currentFov;//what is the current fov?
		public bool isFullScreen;
		public override void OnLevelLoaded (LoadMode mode) {
			config = Config.Deserialize(configpath);//open it up first
			if(config == null) {//check if available otherwise make a new one
				config = new Config();
			}
			if(AppMode == AppMode.AssetEditor || AppMode == AppMode.Game || AppMode == AppMode.MapEditor) {
				StatisticsButtonScreen = config.StatisticsButtonScreen;
				InfoViewsContainerScreen = config.InfoViewsContainerScreen;
				isFullScreen = config.isFullScreen;
				MainToolStripScreen = config.MainToolStripScreen;
				currentScreen = config.currentScreen;
				FullScreenContainerHeight = config.FullScreenContainerHeight;
				ScreenWidth = config.MultiScreenWidth;
				ScreenHeight = config.MultiScreenHeight;
				ActualScreenWidth = config.SingleScreenWidth;
				ActualScreenHeight = config.SingleScreenHeight;
				//
				InfoViewsContainerHeight = config.InfoViewsContainerHeight;//what is the InfoViewsContainer height?
				StatisticsButtonHeight = config.StatisticsButtonHeight;//what is the info menu height?
				InfoPanelHeight = config.InfoPanelHeight;//what is the info panel height?
				MainToolStripHeight = config.MainToolStripHeight;//what is the MainToolstrip height?
				ThumbnailBarHeight = config.ThumbnailBarHeight;//what is the thumbnailbar height?
				TSBarHeight = config.TSBarHeight;//what is the TSBarHeightHeight?
				SaveConfig();//its' been made so save it
				ScreenSize();
				PCamController();
				//
				GameObject infomenuButton = GameObject.Find("InfoMenu");
				infomenuButton.GetComponent<UITabstrip>().absolutePosition = new Vector3(StatisticsButtonScreen, StatisticsButtonHeight, 0);

				GameObject infoviews = GameObject.Find("InfoViewsContainer");
				infoviews.GetComponent<UITabContainer>().absolutePosition = new Vector3(InfoViewsContainerScreen, InfoViewsContainerHeight, 0);

				GameObject fsc = GameObject.Find("FullScreenContainer");
				fsc.GetComponent<UIPanel>().size = new Vector2(ScreenWidth, FullScreenContainerHeight);
				fsc.GetComponent<UIPanel>().absolutePosition = new Vector3(0, 0, 0);//this has to be zero'd no matter what to fill screen.
				fsc.GetComponent<UIPanel>().area = new Vector4(0, 0, ScreenWidth, FullScreenContainerHeight);

				GameObject uiviewobj = GameObject.Find("UIView");
				uiviewobj.GetComponentInChildren<UIView>().fixedWidth = ScreenWidth;
				uiviewobj.GetComponentInChildren<UIView>().fixedHeight = ScreenHeight;

				GameObject thumbnailbar = GameObject.Find("ThumbnailBar");
				thumbnailbar.GetComponent<UISlicedSprite>().width = ActualScreenWidth;
				thumbnailbar.GetComponent<UISlicedSprite>().relativePosition = new Vector3(currentScreen, ThumbnailBarHeight, 0);

				GameObject TSBar = GameObject.Find("TSBar");
				TSBar.GetComponent<UISlicedSprite>().width = ActualScreenWidth;
				TSBar.GetComponent<UISlicedSprite>().relativePosition = new Vector3(currentScreen, TSBarHeight, 0);

				GameObject MainToolstrip = GameObject.Find("MainToolstrip");
				MainToolstrip.GetComponent<UITabstrip>().width = ActualScreenWidth;
				MainToolstrip.GetComponent<UITabstrip>().relativePosition = new Vector3(MainToolStripScreen + 505, MainToolStripHeight, 0);

				GameObject infopanel = GameObject.Find("InfoPanel");
				//thumbnailbar.GetComponent<UIPanel>().area = new Vector4(1600, 856, 1600, 44);
				infopanel.GetComponent<UIPanel>().width = ActualScreenWidth;
				infopanel.GetComponent<UIPanel>().relativePosition = new Vector4(currentScreen, InfoPanelHeight);
			}
		}
		void SaveConfig () {
			Config.Serialize(configpath, config);//close it back up
		}

		void PCamController () {
			Camera[] camobj = GameObject.FindObjectsOfType<Camera>();
			foreach(Camera cam in camobj) {
				currentFov = config.CameraFov;
				cam.fieldOfView = currentFov;
			}
		}

		void ScreenSize () {
			if(ScreenWidth == 0 && ScreenHeight == 0) {//should have a better way to detecting if the resolution has been changed to the correct size
				//don't set resolution
			} else {
				UnityEngine.Screen.SetResolution(ScreenWidth, ScreenHeight, isFullScreen);
			}
		}
	}
}