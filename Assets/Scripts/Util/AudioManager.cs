using System.Collections.Generic;
using Codes.Util;
using UnityEngine;

namespace Util
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBungleton<AudioManager>
    {
        [SerializeField] private AudioSource audioSource;
        private readonly Dictionary<string, AudioClip> _audioClips = new();

        public void PlaySound(SoundType type)
        {
            audioSource.PlayOneShot(GetAudioClip(SoundTypeToString(type)));
        }

        private static string SoundTypeToString(SoundType type)
        {
            return type switch
            {
                SoundType.BulletShot => "SFX/BulletShot",
                SoundType.Collect => "SFX/CollectItem",
                SoundType.Bandages => "SFX/Bandages",
                SoundType.RifleReload => "SFX/ReloadGun",
                SoundType.Warflare => "SFX/Warflare",
                SoundType.Talk => "SFX/Warflare",
                SoundType.Granade => "SFX/Granade",
                SoundType.Walk => "SFX/Walking",
                SoundType.Running => "SFX/Running",
                SoundType.ButtonHover => "SFX/UI_MouseOver",
                _ => "none"
            };
        }
        
        private AudioClip GetAudioClip(string path)
        {
            if (_audioClips.TryGetValue(path, out var clip)) return clip;
            clip = Resources.Load<AudioClip>($"Audio/{path}");
            if (!clip) return null;
            return _audioClips[path] = clip;
        }
    }

    public enum SoundType
    {
        BulletShot,
        Collect,
        Bandages,
        RifleReload,
        Warflare,
        Talk,
        Granade,
        Walk,
        Running,
        ButtonHover,
    }
}
