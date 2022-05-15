using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    /// <summary>
    /// �p�l���I�u�W�F�N�g�̊Ǘ�
    /// </summary>
    public class PanelsController : MonoBehaviour
    {
        [SerializeField] private GameObject panelPrefab;
        private List<PanelObject> panels = new List<PanelObject>();

        /// <summary>
        /// �p�l������
        /// </summary>
        /// <param name="data"></param>
        internal void Generate(PanelData data)
        {
            GameObject instance = Instantiate(panelPrefab);
            instance.transform.parent = this.transform;
            panels.Add(new PanelObject(instance, data));
        }

        /// <summary>
        /// �p�l���̔j��
        /// </summary>
        internal void Dispose()
        {
            foreach (var panel in panels)
            {
                panel.Dispose();
                panels.Remove(panel);
            }
        }
    }

    public class PanelObject
    {
        GameObject panel;
        PanelData data;

        public PanelObject(GameObject obj, PanelData data)
        {
            panel = obj;
            this.data = data;
            panel.transform.localPosition = new Vector3(data.Position.x, 0, data.Position.y);
        }

        public void SetPosition(Vector3 position)
        {

        }

        public void Dispose()
        {
            MonoBehaviour.Destroy(panel);
        }
    }
}
