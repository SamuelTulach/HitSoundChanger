using BeatSaberMarkupLanguage.MenuButtons;
using UnityEngine;

namespace HitSoundChanger.UI
{
    internal class HitSoundChangerUI : MonoBehaviour
    {
        internal SoundListFlowCoordinator _soundListFlow;
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
            GameObject.DontDestroyOnLoad(this);
            CreateHitSoundButton();
        }

        private void CreateHitSoundButton()
        {
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