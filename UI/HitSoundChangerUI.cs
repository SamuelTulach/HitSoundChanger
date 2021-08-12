using BeatSaberMarkupLanguage.MenuButtons;
using System;
using UnityEngine;

namespace HitSoundChanger.UI
{
    internal class HitSoundChangerUI : MonoBehaviour
    {
        internal SoundListView _soundListViewController;
        internal HitSoundChanger.UI.SoundListFlowCoordinator _soundListFlow;
        public static HitSoundChangerUI _instance;

        internal static void OnLoad()
        {
            if (_instance != null)
            {
                return;
            }

            new GameObject("HitSoundChangerUI").AddComponent<HitSoundChangerUI>();
        }

        private void Awake()
        {
            _instance = this;
            try
            {
                //_buttonInstance = Resources.FindObjectsOfTypeAll<Button>().First(x => (x.name == "QuitButton"));
                //_backButtonInstance = Resources.FindObjectsOfTypeAll<Button>().First(x => (x.name == "BackArrowButton"));
                //_mainMenuViewController = Resources.FindObjectsOfTypeAll<MainMenuViewController>().First();
                //_mainMenuRectTransform = _buttonInstance.transform.parent as RectTransform;
            }
            catch (Exception ex)
            {
                Utilities.Logging.Log.Error($"{ex.Message}\n{ex.StackTrace}");
            }
            GameObject.DontDestroyOnLoad(this);
            CreateHitSoundButton();
        }

        private void CreateHitSoundButton()
        {
            Utilities.Logging.Log.Debug("Adding HitSounds button");
            MenuButtons.instance.RegisterButton(new MenuButton("HitSounds", "Change HitSounds Here!", HitSoundButtonPressed, true));
        }

        internal void ShowSoundListFlow()
        {
            if (_soundListFlow == null)
                _soundListFlow = BeatSaberMarkupLanguage.BeatSaberUI.CreateFlowCoordinator<SoundListFlowCoordinator>();
            BeatSaberMarkupLanguage.BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinatorOrAskForTutorial(_soundListFlow);
        }

        private void HitSoundButtonPressed()
        {
            ShowSoundListFlow();
        }
    }
}