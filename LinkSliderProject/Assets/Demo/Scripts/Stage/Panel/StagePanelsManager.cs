using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class StagePanelsManager
    {
        private PanelsData panelsData;

        public StagePanelsManager(PanelsData data)
        {
            panelsData = data;
            Debug.Log($"��݂��߂��� : {panelsData.A}");
        }
    }
}
