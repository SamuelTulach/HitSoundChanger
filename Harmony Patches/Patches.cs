using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace HitSoundChanger.Harmony_Patches
{
    [HarmonyPatch(typeof(NoteCutSoundEffect))]
    [HarmonyPatch("Awake", MethodType.Normal)]
    internal class BadCutSoundPatch
    {
        public static void Prefix(ref AudioClip[] ____badCutSoundEffectAudioClips)
        {
            if (Plugin.originalBadSounds == null)
            {
                Plugin.originalBadSounds = new List<AudioClip>();
                Plugin.originalBadSounds.AddRange(____badCutSoundEffectAudioClips);
            }

            if (Plugin.currentHitSound.badHitSoundEffects == null)
            {
                ____badCutSoundEffectAudioClips = Plugin.originalBadSounds.ToArray();
            }
            else
            {
                ____badCutSoundEffectAudioClips = Plugin.currentHitSound.badHitSoundEffects;
            }
        }
    }

    [HarmonyPatch(typeof(NoteCutSoundEffectManager))]
    [HarmonyPatch("Start", MethodType.Normal)]
    internal class HitSoundsPatch
    {
        public static void Prefix(ref AudioClip[] ____longCutEffectsAudioClips, ref AudioClip[] ____shortCutEffectsAudioClips)
        {
            if (Plugin.originalLongSounds == null)
            {
                Plugin.originalLongSounds = new List<AudioClip>();
                Plugin.originalLongSounds.AddRange(____longCutEffectsAudioClips);
            }

            if (Plugin.originalShortSounds == null)
            {
                Plugin.originalShortSounds = new List<AudioClip>();
                Plugin.originalShortSounds.AddRange(____shortCutEffectsAudioClips);
            }

            if (Plugin.currentHitSound.longHitSoundEffects == null || Plugin.currentHitSound.shortHitSoundEffects == null)
            {
                ____longCutEffectsAudioClips = Plugin.originalLongSounds.ToArray();
                ____shortCutEffectsAudioClips = Plugin.originalShortSounds.ToArray();
            }
            else
            {
                ____longCutEffectsAudioClips = Plugin.currentHitSound.longHitSoundEffects;
                ____shortCutEffectsAudioClips = Plugin.currentHitSound.shortHitSoundEffects;
            }
        }
    }
}