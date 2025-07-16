using System;
using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ObjectParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;

        public void SetAverageColor(Sprite sprite, Color color)
        {
            try
            {
                var pixels = sprite.texture.GetPixels();
                float r = 0f, g = 0f, b = 0f, a = 0f;
                foreach (var pixel in pixels)
                {
                    r += pixel.r;
                    g += pixel.g;
                    b += pixel.b;
                    a += pixel.a;
                }

                var pixelCount = pixels.Length;
                var c = new Color(r / pixelCount * color.r, g / pixelCount * color.g, b / pixelCount * color.b, a / pixelCount * color.a);
                var main = particle.main;
                main.startColor = c;
            }
            catch (ArgumentException)
            {
                var main = particle.main;
                main.startColor = color;
            }
        }
    }
}
