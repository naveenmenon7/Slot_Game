using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    [Header("Reel References")]
    [SerializeField] private ReelController _reel01;
    [SerializeField] private ReelController _reel02;
    [SerializeField] private ReelController _reel03;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spin();
        }
    }

    private void Spin()
    {
        if (_reel01.IsSpinning ||
            _reel02.IsSpinning ||
            _reel03.IsSpinning)
        {
            return;
        }

        _reel01.Spin();
        _reel02.Spin();
        _reel03.Spin();
    }
}