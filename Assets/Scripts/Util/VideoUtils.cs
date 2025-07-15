using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace Util
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoUtils : MonoBehaviour
    {
        [SerializeField] private VideoPlayer video;
        [SerializeField] private UnityEvent onVideoFinished;
        private bool _initializer;

        private void Update()
        {
            if (video.isPlaying) _initializer = true;
            else if (_initializer)
            {
                onVideoFinished.Invoke();
                _initializer = false;
            }
        }
    }
}
