using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using System.Linq;

namespace HitSoundChanger.UI
{
    internal class SoundList : BSMLResourceViewController
    {
        public override string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);

        [UIComponent("soundList")]
        public CustomListTableData customListTableData = new CustomListTableData();

        [UIAction("soundSelect")]
        internal void SelectSound(TableView tableView, int row)
        {
            Plugin.currentHitSound = Plugin.hitSounds[row];
            //Plugin.Settings.SetString("HitSoundChanger", "Last Selected Sound", Plugin.hitSounds[row].folderPath);
            Plugin.CurrentSettings.LastSelected = Plugin.hitSounds[row].folderPath;
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
        }

        [UIAction("#post-parse")]
        internal void SetupSoundList()
        {
            customListTableData.data.Clear();
            foreach (var hitsound in Plugin.hitSounds)
            {
                customListTableData.data.Add(new CustomListTableData.CustomCellInfo(hitsound.name, hitsound.containedSounds));
            }

            customListTableData.tableView.ReloadData();

            var selectedIndex = Plugin.hitSounds.IndexOf(
                Plugin.hitSounds.First(x => x.folderPath == Plugin.currentHitSound.folderPath));
            customListTableData.tableView.ScrollToCellWithIdx(selectedIndex, HMUI.TableView.ScrollPositionType.Center, false);
            customListTableData.tableView.SelectCellWithIdx(selectedIndex);
        }
    }
}