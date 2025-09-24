using System.Collections;
using UnityEngine;

public class PlanetShadowAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Coroutine fadeCoroutine;

    /// <summary>
    /// Fades the sprite over time.
    /// </summary>
    /// <param name="duration">How long the fade should take (seconds).</param>


    public IEnumerator FadeOutCoroutine(float duration)
    {
        if (spriteRenderer == null) yield break;

        Color originalColor = spriteRenderer.color;
        float startAlpha = originalColor.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(startAlpha, 0f, t));
            yield return null;
        }

        // Ensure it's fully transparent at the end
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
