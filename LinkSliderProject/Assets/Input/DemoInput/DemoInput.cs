using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �u�����v�A�u�����v�́@Button
/// �������� Value
/// </summary>
public class DemoInput : MonoBehaviour
{
    public Text text;
    DemoInputActions inputActionsDemo;

    //�{�^���ɓo�^...�����ꂽ�Ƃ��A������Đ��b�̎��A�����ꂽ�Ƃ�
    public void RegistFire(UnityAction action) => inputActionsDemo.Player.Fire.performed += context => action();

    //�{�^����������Ă��邩�̎擾
    public bool IsShotHold() => inputActionsDemo.Player.ShotHold.ReadValue<float>() > 0;

    /// <summary>
    /// �\���L�[�̎擾
    /// 
    /// �E => x : 1
    /// �� => x : -1
    /// 
    /// �� => y : 1
    /// �� => y : -1
    /// </summary>
    public Vector2 GetMoveButton() => inputActionsDemo.Player.Move.ReadValue<Vector2>();
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
        text.text = direction.ToString();

        //�������g�p��
        if (IsShotHold()) Debug.Log("Shot");
    }

    private void Load()
    {
        //Demo��inputAction
        inputActionsDemo = new DemoInputActions();
        inputActionsDemo.Enable();

        //Fire�{�^���o�^��
        RegistFire(() => Debug.Log("Fire"));
    }
}
