using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace HitSoundChanger
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static PluginSettings CurrentSettings;

        public static List<HitSoundCollection> hitSounds = new List<HitSoundCollection>();
        public static HitSoundCollection currentHitSound { get; internal set; }

        public static List<AudioClip> originalShortSounds;
        public static List<AudioClip> originalLongSounds;
        public static List<AudioClip> originalBadSounds;

        [Init]
        public Plugin(Config config, IPALogger logger)
        {
            Instance = this;
            Log = logger;
            CurrentSettings = config.Generated<PluginSettings>();
        }

        [OnStart]
        public void OnApplicationStart()
        {
            var harmony = new Harmony("com.otiosum.BeatSaber.HitSoundChanger");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
            HitSoundChanger.UI.HitSoundChangerUI.OnLoad();
            SharedCoroutineStarter.instance.StartCoroutine(LoadAudio());
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            /**/
        }

        public IEnumerator LoadAudio()
        {
            Log.Info("Attempting to load Audio files");
            var folderPath = Environment.CurrentDirectory + "/UserData/HitSoundChanger";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var directories = Directory.GetDirectories(folderPath);
            hitSounds.Add(new HitSoundCollection { name = "Default", folderPath = "Default" });

            foreach (var folder in directories)
            {
                var newSounds = new HitSoundCollection(folder);
                yield return newSounds.LoadSounds();
                hitSounds.Add(newSounds);
            }

            var lastSound = CurrentSettings.LastSelected;
            var lastSounds = hitSounds.FirstOrDefault(x => x.folderPath == lastSound) ?? hitSounds[0];
            currentHitSound = lastSounds;
        }
    }
}