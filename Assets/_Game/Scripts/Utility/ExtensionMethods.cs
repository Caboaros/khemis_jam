using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Random = System.Random;

namespace Utility
{
    public enum AnchorPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public static class ExtensionMethods
    {
        /// <summary>
        /// Creates and returns a clone of any given scriptable object.
        /// </summary>
        public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
        {
            if (scriptableObject == null)
            {
                Debug.LogError($"ScriptableObject was null. Returning default {typeof(T)} object.");
                return (T)ScriptableObject.CreateInstance(typeof(T));
            }

            T instance = UnityEngine.Object.Instantiate(scriptableObject);
            instance.name = scriptableObject.name; // remove (Clone) from name
            return instance;
        }

        public static float Map(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float MapClamped(this float value, float from1, float to1, float from2, float to2)
        {
            return Mathf.Min(((value - from1) / (to1 - from1) * (to2 - from2) + from2), to2);
        }

        /// <summary>
        /// Arredonda um valor float para o número de casas decimais desejado.
        /// </summary>
        /// <param name="value">Valor a ser convertido.</param>
        /// <param name="decimals">Número de casas decimais</param>
        /// <returns></returns>
        public static float Round(this float value, int decimals)
        {
            return (float)Math.Round(Convert.ToDouble(value), decimals);
        }

        public static double Round(this double value, int decimals)
        {
            return Math.Round(value, decimals);
        }

        public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
        {
            if (rectTransform == null) return;

            Vector2 size = rectTransform.rect.size;
            Vector2 deltaPivot = rectTransform.pivot - pivot;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
            rectTransform.pivot = pivot;
            rectTransform.localPosition -= deltaPosition;
        }

        public static void SetPivot(this RectTransform rectTransform, float x, float y)
        {
            if (rectTransform == null) return;

            Vector2 pivot = new Vector2(x, y);
            Vector2 size = rectTransform.rect.size;
            Vector2 deltaPivot = rectTransform.pivot - pivot;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
            rectTransform.pivot = pivot;
            rectTransform.localPosition -= deltaPosition;
        }

        public static void SetAnchor(this RectTransform rectTransform, AnchorPosition anchor)
        {
            var anchorPos = GetAnchorPos(anchor);

            rectTransform.anchorMax = anchorPos;
            rectTransform.anchorMin = anchorPos;
        }

        public static void SetPivot(this RectTransform rectTransform, AnchorPosition anchor)
        {
            var pivotPos = GetAnchorPos(anchor);
            rectTransform.pivot = pivotPos;
        }

        private static Vector2 GetAnchorPos(AnchorPosition anchor)
        {
            Vector2 anchorPos = Vector2.zero;
            switch (anchor)
            {
                case AnchorPosition.TopLeft:
                    anchorPos = new Vector2(0, 1);
                    break;
                case AnchorPosition.TopCenter:
                    anchorPos = new Vector2(0.5f, 1);
                    break;
                case AnchorPosition.TopRight:
                    anchorPos = new Vector2(1, 1);
                    break;
                case AnchorPosition.MiddleLeft:
                    anchorPos = new Vector2(0, .5f);
                    break;
                case AnchorPosition.MiddleCenter:
                    anchorPos = new Vector2(.5f, .5f);
                    break;
                case AnchorPosition.MiddleRight:
                    anchorPos = new Vector2(1, .5f);
                    break;
                case AnchorPosition.BottomLeft:
                    anchorPos = new Vector2(0, 0);
                    break;
                case AnchorPosition.BottomCenter:
                    anchorPos = new Vector2(.5f, 0);
                    break;
                case AnchorPosition.BottomRight:
                    anchorPos = new Vector2(1, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
            }

            return anchorPos;
        }

        public static void SetAnchorAndPosition(this RectTransform rectTransform, AnchorPosition anchor,
            Vector2 position)
        {
            SetAnchor(rectTransform, anchor);
            rectTransform.anchoredPosition = position;
        }

        public static void SetAnchorAndPosition(this RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax,
            Vector2 position)
        {
            rectTransform.anchorMax = anchorMax;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchoredPosition = position;
        }

        public static bool Overlaps(this RectTransform a, RectTransform b)
        {
            return a.WorldRect().Overlaps(b.WorldRect());
        }

        public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse)
        {
            return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
        }

        public static Rect WorldRect(this RectTransform rectTransform)
        {
            Vector2 sizeDelta = rectTransform.sizeDelta;
            float rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
            float rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;

            Vector3 position = rectTransform.position;
            return new Rect(position.x - rectTransformWidth / 2f, position.y - rectTransformHeight / 2f,
                rectTransformWidth, rectTransformHeight);
        }

        public static Color Transparent(this Color color)
        {
            return new Color(1, 1, 1, 0);
        }

        /// <summary>
        /// Changes the layer of an object and all its children, grandchildren and son on...
        /// </summary>
        /// <param name="root"></param>
        /// <param name="layer"></param>
        public static void SetLayer(this Transform root, int layer)
        {
            root.gameObject.layer = layer;
            foreach (Transform child in root)
                SetLayer(child, layer);
        }

        public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }

        /// <summary>
        /// Returns the amount of digits of a number.
        /// </summary>
        /// <param name="num">Number</param>
        /// <returns></returns>
        public static int GetDigitsAmount(this float num)
        {
            int amount = 0;
            string stringNum = num.ToString(CultureInfo.InvariantCulture);
            foreach (char c in stringNum)
            {
                if (char.IsDigit(c))
                    amount++;
            }

            return amount;
        }

        public static IEnumerator MoveOverSpeedCoroutine(this Transform transform, Vector3 end, float speed)
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            // speed should be 1 unit per second
            while (transform.position != end)
            {
                transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
                yield return wait;
            }
        }

        public static IEnumerator MoveOverSecondsCoroutine(this Transform transform, Vector3 end, float seconds,
            bool continueAfterReachPosition = false)
        {
            float elapsedTime = 0;
            Vector3 startingPos = transform.position;
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            while (elapsedTime < seconds)
            {
                transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return wait;
            }

            transform.position = end;

            if (continueAfterReachPosition)
            {
                //transform.position += 
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}