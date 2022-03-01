using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public interface IGridData
    {
        /// <summary>
        /// �O���b�h�̃}�X���i���j
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// �O���b�h�̃}�X���i�c�j
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// �O���b�h���쐬����B
        /// </summary>
        public void Generate();

        /// <summary>
        /// �O���b�h��worldPosition��Ԃ��B
        /// </summary>
        public Vector3 GetMassPos(Vector2 index);
    }


    [CreateAssetMenu(menuName = "Scriptables/Create GridData")]
    public class GridDataSctiptableObject : ScriptableObject, IGridData
    {
        [SerializeField] private int width;
        [SerializeField] private int height;

        //�P�}�X�̃T�C�Y
        [SerializeField] private float massSize;

        [SerializeField, HideInInspector]
        private GridMass[,] masses;

        public int Width { get => width; }
        public int Height { get => height; }

        public bool IsExist => Width + Height != 0;

        public void Generate()
        {
            masses = new GridMass[Width, Height];
            Vector3 pos = new Vector3(0, 0, 0);
            for(int w = 0; w < Width; w++)
            {
                for(int h = 0; h < Height; h++)
                {
                    pos.x = (w - Mathf.FloorToInt((Width * 0.5f) - 0.5f)) * massSize;
                    pos.z = (h - Mathf.FloorToInt((Height * 0.5f) - 0.5f)) * massSize;
                    Vector2 index = new Vector2(w, h);
                    masses[w,h] = new GridMass(index, pos);
                }
            }
        }

        public Vector3 GetMassPos(Vector2 index)
        {
            int x = (int)index.x;
            int y = (int)index.y;
            return masses[x, y].Position;
        }

        [System.Serializable]
        public class GridMass
        {
            [SerializeField, HideInInspector]
            private Vector2 indexPosition;
            [SerializeField, HideInInspector]
            private Vector3 position;

            public Vector3 Position { get => position; }

            public GridMass(Vector2 index, Vector3 pos)
            {
                indexPosition = index;
                position = pos;
            }
        }
    }
}
