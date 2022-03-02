using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace EasingSystem
{
    public enum EasingType
    {
        None,

        SineIn,
        SineOut,
        SineInOut,

        QuadIn,
        QuadOut,
        QuadInOut,

        CubicIn,
        CubicOut,
        CubicInOut,

        QuartIn,
        QuartOut,
        QuartInOut,

        QuintIn,
        QuintOut,
        QuintInOut,

        ExpoIn,
        ExpoOut,
        ExpoInOut,

        CircIn,
        CircOut,
        CircInOut,

        BackIn,
        BackOut,
        BackInOut,

        ElasticIn,
        ElasticOut,
        ElasticInOut,

        BounceIn,
        BounceOut,
        BounceInOut,
    }

    public class EasingSheet
    {
        //https://qiita.com/pixelflag/items/e5ddf0160781170b671b
        public static float GetEasing(EasingType type, float time, float totaltime, float min = 0, float max = 1)
        {
            switch (type)
            {
                case EasingType.None:
                    return None(time, totaltime, min, max);

                case EasingType.SineIn:
                    return SineIn(time, totaltime, min, max);
                case EasingType.SineOut:
                    return SineOut(time, totaltime, min, max);
                case EasingType.SineInOut:
                    return SineInOut(time, totaltime, min, max);

                case EasingType.QuadIn:
                    return QuadIn(time, totaltime, min, max);
                case EasingType.QuadOut:
                    return QuadOut(time, totaltime, min, max);
                case EasingType.QuadInOut:
                    return QuadInOut(time, totaltime, min, max);

                case EasingType.CubicIn:
                    return CubicIn(time, totaltime, min, max);
                case EasingType.CubicOut:
                    return CubicOut(time, totaltime, min, max);
                case EasingType.CubicInOut:
                    return CubicInOut(time, totaltime, min, max);

                case EasingType.QuartIn:
                    return QuartIn(time, totaltime, min, max);
                case EasingType.QuartOut:
                    return QuartOut(time, totaltime, min, max);
                case EasingType.QuartInOut:
                    return QuartInOut(time, totaltime, min, max);

                case EasingType.QuintIn:
                    return QuintIn(time, totaltime, min, max);
                case EasingType.QuintOut:
                    return QuintOut(time, totaltime, min, max);
                case EasingType.QuintInOut:
                    return QuintInOut(time, totaltime, min, max);

                case EasingType.ExpoIn:
                    return ExpoIn(time, totaltime, min, max);
                case EasingType.ExpoOut:
                    return ExpoOut(time, totaltime, min, max);
                case EasingType.ExpoInOut:
                    return ExpoInOut(time, totaltime, min, max);

                case EasingType.CircIn:
                    return CircIn(time, totaltime, min, max);
                case EasingType.CircOut:
                    return CircOut(time, totaltime, min, max);
                case EasingType.CircInOut:
                    return CircInOut(time, totaltime, min, max);

                case EasingType.BackIn:
                    return BackIn(time, totaltime, min, max);
                case EasingType.BackOut:
                    return BackOut(time, totaltime, min, max);
                case EasingType.BackInOut:
                    return BackInOut(time, totaltime, min, max);

                case EasingType.ElasticIn:
                    return ElasticIn(time, totaltime, min, max);
                case EasingType.ElasticOut:
                    return ElasticOut(time, totaltime, min, max);
                case EasingType.ElasticInOut:
                    return ElasticInOut(time, totaltime, min, max);

                case EasingType.BounceIn:
                    return BounceIn(time, totaltime, min, max);
                case EasingType.BounceOut:
                    return BounceOut(time, totaltime, min, max);
                case EasingType.BounceInOut:
                    return BounceInOut(time, totaltime, min, max);
            }

            return 0;
        }

        public static float None(float time, float totaltime, float min = 0, float max = 1)
        {
            max -= min; //1

            float formula = time / totaltime;

            return (max * formula) + min;
        }

        public static float SineIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            float formula = 1 - Mathf.Cos((x * Mathf.PI) / 2);
            max -= min;
            return (max * formula) + min;
        }

        public static float SineOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            float formula = Mathf.Sin((x * Mathf.PI) / 2);
            max -= min;
            return (max * formula) + min;
        }

        /// <summary>
        /// Easing.SineInOut
        /// </summary>
        /// <param name="time">動き始めて何秒かを表します。</param>
        /// <param name="totaltime">動き始めてから終わるまでの時間です。</param>
        /// <param name="min">返す値の最大</param>
        /// <param name="max">返す値の最小</param>
        /// <returns>time の時点で 0 から 1 のなかでどの値かを返します。</returns>
        public static float SineInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = (1 - Mathf.Cos(x * Mathf.PI)) / 2;
            return (max * formula) + min;
        }

        public static float QuadIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x * x;
            return (max * formula) + min;
        }

        public static float QuadOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = 1 - ((1 - x) * (1 - x));
            return (max * formula) + min;
        }

        public static float QuadInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x < 0.5 ? 2 * x * x : 1 - (Mathf.Pow((-2 * x) + 2, 2) / 2);
            return (max * formula) + min;
        }

        public static float CubicIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x * x * x;
            return (max * formula) + min;
        }

        public static float CubicOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = 1 - Mathf.Pow(1 - x, 3);
            return (max * formula) + min;
        }

        public static float CubicInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x < 0.5 ? 4 * x * x * x : 1 - (Mathf.Pow((-2 * x) + 2, 3) / 2);
            return (max * formula) + min;
        }

        public static float QuartIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x * x * x * x;
            return (max * formula) + min;
        }

        public static float QuartOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = 1 - Mathf.Pow(1 - x, 4);
            return (max * formula) + min;
        }

        public static float QuartInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x < 0.5 ? 8 * x * x * x * x : 1 - (Mathf.Pow((-2 * x) + 2, 4) / 2);
            return (max * formula) + min;
        }

        public static float QuintIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x * x * x * x * x;
            return (max * formula) + min;
        }

        public static float QuintOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = 1 - Mathf.Pow(1 - x, 5);
            return (max * formula) + min;
        }

        public static float QuintInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x < 0.5 ? 16 * x * x * x * x * x : 1 - (Mathf.Pow((-2 * x) + 2, 5) / 2);
            return (max * formula) + min;
        }

        public static float ExpoIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x == 0 ? 0 : Mathf.Pow(2, (10 * x) - 10);
            return (max * formula) + min;
        }

        public static float ExpoOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
            return (max * formula) + min;
        }

        public static float ExpoInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula =
                x == 0 ?
                0 :
                x == 1 ?
                1 :
                x < 0.5 ?
                Mathf.Pow(2, (20 * x) - 10) / 2 :
                (2 - Mathf.Pow(2, (-20 * x) + 10)) / 2;
            return (max * formula) + min;
        }

        public static float CircIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
            return (max * formula) + min;
        }

        public static float CircOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
            return (max * formula) + min;
        }

        public static float CircInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula =
                x < 0.5 ?
                (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2 :
                (Mathf.Sqrt(1 - Mathf.Pow((-2 * x) + 2, 2)) + 1) / 2;
            return (max * formula) + min;
        }

        public static float BackIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float c1 = 1.70158f;
            float c3 = c1 + 1;
            float formula = (c3 * x * x * x) - (c1 * x * x);
            return (max * formula) + min;
        }

        public static float BackOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float c1 = 1.70158f;
            float c3 = c1 + 1;
            float formula = 1 + (c3 * Mathf.Pow(x - 1, 3)) + (c1 * Mathf.Pow(x - 1, 2));
            return (max * formula) + min;
        }

        public static float BackInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float c1 = 1.70158f;
            float c2 = c1 * 1.525f;
            float formula =
                x < 0.5 ?
                (Mathf.Pow(2 * x, 2) * (((c2 + 1) * 2 * x) - c2)) / 2 :
                ((Mathf.Pow((2 * x) - 2, 2) * (((c2 + 1) * ((x * 2) - 2)) + c2)) + 2) / 2;
            return (max * formula) + min;
        }

        public static float ElasticIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float c4 = (2 * Mathf.PI) / 3;
            float formula =
                x == 0 ?
                0 :
                x == 1 ?
                1 :
                -Mathf.Pow(2, (10 * x) - 10) * Mathf.Sin(((x * 10) - 10.75f) * c4);
            return (max * formula) + min;
        }

        public static float ElasticOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float c4 = (2 * Mathf.PI) / 3;
            float formula = x == 0 ?
                0 :
                x == 1 ?
                1 :
                (Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4)) + 1;
            return (max * formula) + min;
        }

        public static float ElasticInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float c5 = (2 * Mathf.PI) / 4.5f;
            float formula =
                x == 0 ?
                0 :
                x == 1 ?
                1 :
                x < 0.5 ?
                -(Mathf.Pow(2, (20 * x) - 10) * Mathf.Sin(((20 * x) - 11.125f) * c5)) / 2 :
                (Mathf.Pow(2, (-20 * x) + 10) * Mathf.Sin(((20 * x) - 11.125f) * c5) / 2) + 1;
            return (max * formula) + min;
        }

        public static float BounceIn(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = 1 - BounceCalc(1 - x);
            return (max * formula) + min;
        }

        public static float BounceOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula = BounceCalc(x);

            return (max * formula) + min;
        }

        public static float BounceInOut(float time, float totaltime, float min = 0, float max = 1)
        {
            float x = time / totaltime;
            max -= min; //1
            float formula =
                x < 0.5 ?
                (1 - BounceOut(1 - (2 * x), totaltime, min, max)) / 2 :
                (1 + BounceOut((2 * x) - 1, totaltime, min, max)) / 2;
            return (max * formula) + min;
        }

        private static float BounceCalc(float x)
        {
            float n1 = 7.5625f;
            float d1 = 2.75f;
            float formula = 0;
            if (x < 1 / d1)
            {
                formula = n1 * x * x;
            }
            else if (x < 2 / d1)
            {
                formula = (n1 * (x -= 1.5f / d1) * x) + 0.75f;
            }
            else if (x < 2.5 / d1)
            {
                formula = (n1 * (x -= 2.25f / d1) * x) + 0.9375f;
            }
            else
            {
                formula = (n1 * (x -= 2.625f / d1) * x) + 0.984375f;
            }

            return formula;
        }
    }
    
    //このクラスに任意のオブジェクトを指定して生成する
    //
    //スタート位置
    //エンド位置
    //移動にかかる時間
    //Easing
    //
    //を指定して移動。
    public class LerpObjectbyEasing
    {
        private CancellationTokenSource cts; //cancel用
        private GameObject target;
        private bool isPlaying;

        public LerpObjectbyEasing(GameObject obj)
        {
            target = obj;
            cts = new CancellationTokenSource();
            isPlaying = false;
        }

        public void Stop()
        {
            cts.Cancel();
            isPlaying = false;
        }

        public async UniTask PlayTransform(Transform start, Transform end, float time, EasingType easing = EasingType.SineInOut)
        {
            Debug.Log(isPlaying);
            if (isPlaying)
            {
                return;
            }

            isPlaying = true;
            cts = new CancellationTokenSource();
            CancellationToken targetToken = target.GetCancellationTokenOnDestroy();

            float elapsedTime = 0;
            float rate;

            while (elapsedTime < time)
            {
                if (cts.Token.IsCancellationRequested)
                {
                    return;
                }

                elapsedTime += Time.deltaTime;
                rate = EasingSheet.GetEasing(easing, elapsedTime, time);

                Vector3 posdiff = start.position - end.position;
                target.transform.position = start.position + (posdiff * -rate);
                target.transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, rate);
                target.transform.localScale = Vector3.Lerp(start.localScale, end.localScale, rate);

                await UniTask.Yield(PlayerLoopTiming.Update, targetToken);
            }

            target.transform.position = end.position;
            target.transform.rotation = end.rotation;
            target.transform.localScale = end.localScale;
            isPlaying = false;
        }
    }
}