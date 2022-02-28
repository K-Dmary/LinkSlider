using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace InputSystemManager
{
    /// <summary>
    /// 「押す」、「離す」は　Button
    /// 長押しは Value
    /// </summary>
    public class DemoInputManager
    {
        private DemoInputActions demoInputActions;

        //ボタンに登録...押されたとき、押されて数秒の時、離されたとき
        public void RegistFire(UnityAction action) => demoInputActions.Player.Fire.performed += context => action();

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

        // Update is called once per frame
        private void Update()
        {
            //十字キー使用例
            var direction = GetMoveButton();

            //長押し使用例
            if (IsShotHold()) Debug.Log("Shot");
        }

        private void Load()
        {
            //DemoのinputAction
            demoInputActions = new DemoInputActions();
            demoInputActions.Enable();

            //Fireボタン登録例
            RegistFire(() => Debug.Log("Fire"));
        }
    }
}
