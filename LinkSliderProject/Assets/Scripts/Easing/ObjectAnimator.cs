using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace EasingSystem
{
    public class ObjectAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private List<ObjAnimNode> animNodes = new List<ObjAnimNode>();
        [SerializeField] private IntReactiveProperty nodeNumRp = new IntReactiveProperty(-1);

        [SerializeField] private FloatReactiveProperty movingTimeRp = new FloatReactiveProperty(0);
        [SerializeField] private IntReactiveProperty movingEasingRp = new IntReactiveProperty(0);


        public bool IsPlaying { get; private set; } = false;

        public ObjAnimNode Node 
        { 
            get 
            {
                return Count == 0 ? null : animNodes[NodeNum];
            }
        }
        public int Count { get => animNodes.Count; }
        public int NodeNum
        {
            get => nodeNumRp.Value;
            set
            {
                if (Count == 0)
                {
                    nodeNumRp.Value = -1;
                }
                if (value < 0 || Count <= value)
                {
                    return;
                }
                nodeNumRp.Value = value;
            }
        }

        public int PlayingNum { get; private set; }

        public float MovingTime { get => movingTimeRp.Value; set => movingTimeRp.Value = value; }
        public EasingType MovingEasing { get => (EasingType)movingEasingRp.Value; set => movingEasingRp.Value = (int)value; }

        public void LoadAwake()
        {
            if(Count != 0) { return; }
            nodeNumRp.Subscribe(_ =>
            {
                if (Count == 0) return;
                target.transform.position = Node.transform.position;
                target.transform.rotation = Node.transform.rotation;
                target.transform.localScale = Node.transform.localScale;

                MovingTime = Node.Time;
                MovingEasing = Node.Easing;
            }).AddTo(target);
            movingTimeRp.Subscribe(value =>
            {
                if (Node == null) return;
                Node.Time = value; 
            }).AddTo(target);
            movingEasingRp.Subscribe(value =>
            {
                if (Node == null) return;
                Node.Easing = (EasingType)value;
            }).AddTo(target);

            IsPlaying = false;
        }

        public void Generate()
        {
            int index = Count;
            GameObject obj = new GameObject("AnimNode" + index);
            obj.transform.parent = this.transform;
            CopyTransform(target.transform, ref obj);
            Debug.Log(obj.transform.position);
            if (Count == 0) animNodes.Add(new ObjAnimNode(target, obj.transform));
            else animNodes.Add(new ObjAnimNode(target, obj.transform, animNodes[Count - 1].transform));
            NodeNum++;
        }

        public static void CopyTransform(Transform from, ref GameObject to)
        {
            to.transform.position = from.transform.position;
            to.transform.rotation = from.transform.rotation;
            to.transform.localScale = from.transform.localScale;
        }

        public void Remove()
        {
            if (Count == 0) return;
            Stop();
            Node.Dispose();
            animNodes.Remove(animNodes[NodeNum]);
            if (NodeNum == 0) return;
            if (Count <= NodeNum) NodeNum = Count - 1;
            else Node.SetBeforeData(animNodes[NodeNum - 1].transform);
        }

        public async UniTaskVoid PlayAll()
        {

            if (IsPlaying) return;

            IsPlaying = true;
            PlayingNum = 0;

            while (PlayingNum < Count)
            {
                Debug.Log(PlayingNum);
                await animNodes[PlayingNum].Play();
                PlayingNum++;
            }
            IsPlaying = false;
        }

        public void Stop()
        {
            if (IsPlaying && Count != 0) animNodes[PlayingNum].Stop();
            IsPlaying = false;
        }

        private void Start()
        {
            LoadAwake();
        }

        [System.Serializable]
        public class ObjAnimNode
        {
            [SerializeField] private Transform targetData;

            [SerializeField] private float time;
            [SerializeField] private EasingType easing;

            [SerializeField]
            private Transform beforeData;
            [SerializeField]
            private LerpObjectbyEasing animSystem;

            public Transform transform => targetData;

            public float Time { get => time; set => time = value; }
            public EasingType Easing { get => easing; set => easing = (EasingType)value; }

            public void SetBeforeData(Transform before) => beforeData = before;
            //Targetの場所を保存
            public ObjAnimNode(GameObject obj, Transform target, Transform before = null ,float t = 0, EasingType eas = EasingType.SineInOut)
            {
                targetData = target.transform;
                beforeData = before ? before : null;

                time = t;
                easing = eas;
                animSystem = new LerpObjectbyEasing(obj);
            }

            public async UniTask Play()
            {
                if (beforeData == null) return;
                await animSystem.PlayTransform(beforeData, targetData, time, easing);
            }

            public void Stop() => animSystem.Stop();

            public void Dispose()
            {
                DestroyImmediate(targetData.gameObject);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ObjectAnimator))]
    public class ObjectAnimatorEditor : UnityEditor.Editor
    {
        private ObjectAnimator anim;
        private bool isOpenSingle;

        private void Awake()
        {
            anim = target as ObjectAnimator;
            anim.LoadAwake();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            isOpenSingle = EditorGUILayout.BeginFoldoutHeaderGroup(isOpenSingle, "SingleAnim");
            if (isOpenSingle)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Generate"))
                {
                    anim.Generate();
                }

                if (GUILayout.Button("Remove"))
                {
                    anim.Remove();
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Play"))
                {
                    _ = anim.PlayAll();
                }

                if (GUILayout.Button("Stop"))
                {
                    anim.Stop();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
#endif
}
