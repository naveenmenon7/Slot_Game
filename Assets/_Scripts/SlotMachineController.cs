using System.Collections;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    [Header("Reel References")]
    [SerializeField] private ReelController _reel01;
    [SerializeField] private ReelController _reel02;
    [SerializeField] private ReelController _reel03;

    [Header("Payouts")]
    [SerializeField] private int _sevenPayout = 100;
    [SerializeField] private int _cherriesPayout = 50;
    [SerializeField] private int _bellPayout = 30;
    [SerializeField] private int _barPayout = 20;

    private bool _isSpinning;

    public bool IsSpinning => _isSpinning;

    private void Update()
    {
        // Keep Space as a temporary testing option.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSpin();
        }
    }

    public void StartSpin()
    {
        if (_isSpinning)
        {
            return;
        }

        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        _isSpinning = true;

        _reel01.Spin();
        _reel02.Spin();
        _reel03.Spin();

        // Wait until all reels have stopped.
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

        if (!isWin)
        {
            Debug.Log("NO WIN");
            return;
        }

        int payout = GetPayout(result01);

        Debug.Log($"WIN! Payout: {payout}");
    }

    private int GetPayout(
        SlotSymbol.SymbolType symbol)
    {
        switch (symbol)
        {
            case SlotSymbol.SymbolType.Seven:
                return _sevenPayout;

            case SlotSymbol.SymbolType.Cherries:
                return _cherriesPayout;

            case SlotSymbol.SymbolType.Bell:
                return _bellPayout;

            case SlotSymbol.SymbolType.Bar:
                return _barPayout;

            default:
                return 0;
        }
    }
}