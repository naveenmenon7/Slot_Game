using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SlotMachineController _slotMachineController;

    private void OnMouseDown()
    {
        if (_slotMachineController.IsSpinning)
        {
            return;
        }

        if (!_slotMachineController.HasBet)
        {
            return;
        }

        _animator.Play("LeverPull", 0, 0f);

        _slotMachineController.StartSpin();
    }
}