using BeatSaberMarkupLanguage;
using HMUI;
using System;

namespace HitSoundChanger.UI
{
    internal class SoundListFlowCoordinator : FlowCoordinator
    {
        private SoundListView _soundListView;

        public void Awake()
        {
            if (_soundListView == null)
                _soundListView = BeatSaberUI.CreateViewController<SoundListView>();
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try
            {
                if (firstActivation)
                {
                    SetTitle("Hit Sounds");
                    showBackButton = true;
                    ProvideInitialViewControllers(_soundListView);
                }
            }
            catch (Exception ex)
            {
                Utilities.Logging.Log.Error(ex);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            var mainFlow = BeatSaberMarkupLanguage.BeatSaberUI.MainFlowCoordinator;
            mainFlow.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal);
        }
    }
}