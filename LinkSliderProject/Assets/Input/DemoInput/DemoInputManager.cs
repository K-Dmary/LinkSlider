using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace InputSystemManager
{
    /// <summary>
    /// 「押す」、「離す」は　Button
    /// 長押しは Value
    /// </summary>
    public class DemoInputManager
    {
        private DemoInputActions demoInputActions;

        public bool Up => 0 < GetMoveButton().y;

        public bool Down => GetMoveButton().y < 0;

        public bool Right => 0 < GetMoveButton().x;

        public bool Left => GetMoveButton().x < 0;

        //ボタンに登録...押されたとき、押されて数秒の時、離されたとき
        public void RegistFire(UnityAction action) => demoInputActions.Player.Fire.performed += context => action();

        public void RegistUp(UnityAction action) => demoInputActions.Player.Up.performed += context => action();
        public void RegistDown(UnityAction action) => demoInputActions.Player.Down.performed += context => action();
        public void RegistRight(UnityAction action) => demoInputActions.Player.Right.performed += context => action();
        public void RegistLeft(UnityAction action) => demoInputActions.Player.Left.performed += context => action();

        //ボタンが押されているかの取得
        public bool IsShotHold() => demoInputActions.Player.ShotHold.ReadValue<float>() > 0;

        /// <summary>
        /// 十字キーの取得
        /// 
        /// 右 => x : 1
        /// 左 => x : -1
        /// 
        /// 上 => y : 1
        /// 下 => y : -1
        /// </summary>
        public Vector2 GetMoveButton() => demoInputActions.Player.Move.ReadValue<Vector2>();
        // Start is called before the first frame update
        private void Start()
        {
            Load();
        }


        public void Load()
        {
            //DemoのinputAction
            demoInputActions = new DemoInputActions();
            demoInputActions.Enable();
        }
    }
}
