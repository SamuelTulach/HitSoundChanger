using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using System.Linq;

namespace HitSoundChanger.UI
{
    internal class SoundListView : BeatSaberMarkupLanguage.ViewControllers.BSMLResourceViewController
    {
        public override string ResourceName => "HitSoundChanger.UI.SoundList.bsml";

        [UIComponent("soundList")]
        public CustomListTableData customListTableData;

        [UIAction("soundSelect")]
        internal void SelectSound(TableView tableView, int row)
        {
            Plugin.currentHitSound = Plugin.hitSounds[row];
            Plugin.Settings.SetString("HitSoundChanger", "Last Selected Sound", Plugin.hitSounds[row].folderPath);
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
        }

        [UIAction("#post-parse")]
        internal void SetupSaberList()
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