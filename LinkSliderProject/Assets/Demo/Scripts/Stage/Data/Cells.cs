using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo
{
    [System.Serializable]
    public class Cells
    {
        [SerializeField]
        private List<Cell> cells;

        public Cells()
        {
            cells = new List<Cell>();
        }

        public int Count => cells.Count;

        public bool TryGetCell(Vector2Int index, out Cell cell)
        {
            cell = cells.Single(n => n.Index == index);
            if (cell == null) return false;
            else return true;
        }

        public void Add(Cell cell)
        {
            cells.Add(cell);
        }

        public void Remove(Cell cell)
        {
            cells.Remove(cell);
        }

        public void RemoveAll()
        {
            cells.RemoveAll(n => n.GetType() == typeof(Cell));
        }
    }

    [System.Serializable]
    public class Cell
    {
        [SerializeField]
        private Vector2Int index;
        [SerializeField]
        private bool isMovable;

        public Cell(Vector2Int idx, bool able)
        {
            index = idx;
            isMovable = able;
        }

        public Vector2Int Index => index;

        private bool IsMovable => isMovable;
    }
}
