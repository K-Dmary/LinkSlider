using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace InputSystemManager
{
    /// <summary>
    /// �u�����v�A�u�����v�́@Button
    /// �������� Value
    /// </summary>
    public class DemoInputManager
    {
        private DemoInputActions demoInputActions;

        public bool Up => 0 < GetMoveButton().y;

        public bool Down => GetMoveButton().y < 0;

        public bool Right => 0 < GetMoveButton().x;

        public bool Left => GetMoveButton().x < 0;

        //�{�^���ɓo�^...�����ꂽ�Ƃ��A������Đ��b�̎��A�����ꂽ�Ƃ�
        public void RegistFire(UnityAction action) => demoInputActions.Player.Fire.performed += context => action();

        public void RegistUp(UnityAction action) => demoInputActions.Player.Up.performed += context => action();
        public void RegistDown(UnityAction action) => demoInputActions.Player.Down.performed += context => action();
        public void RegistRight(UnityAction action) => demoInputActions.Player.Right.performed += context => action();
        public void RegistLeft(UnityAction action) => demoInputActions.Player.Left.performed += context => action();

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


        public void Load()
        {
            //Demo��inputAction
            demoInputActions = new DemoInputActions();
            demoInputActions.Enable();
        }
    }
}
