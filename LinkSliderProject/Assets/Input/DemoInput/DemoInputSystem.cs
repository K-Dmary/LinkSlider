using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace InputSystemManager
{
    /// <summary>
    /// �u�����v�A�u�����v�́@Button
    /// �������� Value
    /// </summary>
    public class DemoInputManager
    {
        private DemoInputActions demoInputActions;

        //�{�^���ɓo�^...�����ꂽ�Ƃ��A������Đ��b�̎��A�����ꂽ�Ƃ�
        public void RegistFire(UnityAction action) => demoInputActions.Player.Fire.performed += context => action();

        //�{�^����������Ă��邩�̎擾
        public bool IsShotHold() => demoInputActions.Player.ShotHold.ReadValue<float>() > 0;

        /// <summary>
        /// �\���L�[�̎擾
        /// 
        /// �E => x : 1
        /// �� => x : -1
        /// 
        /// �� => y : 1
        /// �� => y : -1
        /// </summary>
        public Vector2 GetMoveButton() => demoInputActions.Player.Move.ReadValue<Vector2>();
        // Start is called before the first frame update
        private void Start()
        {
            Load();
        }

        // Update is called once per frame
        private void Update()
        {
            //�\���L�[�g�p��
            var direction = GetMoveButton();

            //�������g�p��
            if (IsShotHold()) Debug.Log("Shot");
        }

        private void Load()
        {
            //Demo��inputAction
            demoInputActions = new DemoInputActions();
            demoInputActions.Enable();

            //Fire�{�^���o�^��
            RegistFire(() => Debug.Log("Fire"));
        }
    }
}
