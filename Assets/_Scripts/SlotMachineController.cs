using System.Collections;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    [Header("Reel References")]
    [SerializeField] private ReelController _reel01;
    [SerializeField] private ReelController _reel02;
    [SerializeField] private ReelController _reel03;

    private bool _isSpinning;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isSpinning)
        {
            StartCoroutine(SpinRoutine());
        }
    }

    private IEnumerator SpinRoutine()
    {
        _isSpinning = true;

        _reel01.Spin();
        _reel02.Spin();
        _reel03.Spin();

        // Wait until all three reels finish.
        while (_reel01.IsSpinning ||
               _reel02.IsSpinning ||
               _reel03.IsSpinning)
        {
            yield return null;
        }

        CheckResult();

        _isSpinning = false;
    }

    private void CheckResult()
    {
        SlotSymbol.SymbolType result01 =
            _reel01.CurrentResult;

        SlotSymbol.SymbolType result02 =
            _reel02.CurrentResult;

        SlotSymbol.SymbolType result03 =
            _reel03.CurrentResult;

        bool isWin =
            result01 == result02 &&
            result02 == result03;

        Debug.Log(
            $"Results: {result01} | {result02} | {result03}"
        );

        if (isWin)
        {
            Debug.Log("WIN!");
        }
        else
        {
            Debug.Log("NO WIN");
        }
    }
}