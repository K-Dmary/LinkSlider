using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
    public class DemoMapManager : MonoBehaviour
    {
        [SerializeField] private GridDataSctiptableObject gridData; 
        // Start is called before the first frame update
        public void LoadStart()
        {
            gridData.Generate();
        }
    }
}
