using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineController : MonoBehaviour
{
    [Header("Reel References")]
    [SerializeField] private ReelController _reel01;
    [SerializeField] private ReelController _reel02;
    [SerializeField] private ReelController _reel03;

    [Header("Payout Multipliers")]
    [SerializeField] private int _sevenMultiplier = 10;
    [SerializeField] private int _cherriesMultiplier = 5;
    [SerializeField] private int _bellMultiplier = 3;
    [SerializeField] private int _barMultiplier = 2;

    [Header("Game Settings")]
    [SerializeField] private int _startingBalance = 1000;

    [Header("UI")]
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private TMP_Text _currentBetText;
    [SerializeField] private TMP_Text _resultText;

    [Header("Bet Buttons")]
    [SerializeField] private Button _bet10Button;
    [SerializeField] private Button _bet50Button;
    [SerializeField] private Button _bet100Button;
    [SerializeField] private Button _cancelButton;

    [Header("Bet Button Colors")]
    [SerializeField] private Color _normalButtonColor =
        new Color32(24, 35, 61, 255);

    [SerializeField] private Color _selectedButtonColor =
        new Color32(223, 175, 36, 255);

    [SerializeField] private Color _disabledButtonColor =
        new Color32(89, 98, 115, 255);

    [Header("Result Colors")]
    [SerializeField] private Color _winColor =
        new Color32(244, 197, 66, 255);

    [SerializeField] private Color _noWinColor =
        new Color32(184, 192, 208, 255);

    [SerializeField] private Color _betPlacedColor =
        new Color32(108, 74, 182, 255);

    [SerializeField] private Color _warningColor =
        new Color32(217, 74, 74, 255);

    [SerializeField] private Color _defaultColor =
        new Color32(255, 248, 231, 255);

    private int _balance;
    private int _currentBet;
    private bool _isSpinning;

    public bool IsSpinning => _isSpinning;
    public bool HasBet => _currentBet > 0;

    private void Start()
    {
        _balance = _startingBalance;
        _currentBet = 0;

        SetResult(
            "PLACE YOUR BET",
            _defaultColor
        );

        UpdateUI();
        UpdateBetButtonVisuals();
    }

    public void SetBet10()
    {
        SetBet(10);
    }

    public void SetBet50()
    {
        SetBet(50);
    }

    public void SetBet100()
    {
        SetBet(100);
    }

    private void SetBet(int amount)
    {
        if (_isSpinning)
        {
            return;
        }

        if (_balance < amount)
        {
            SetResult(
                "NOT ENOUGH G",
                _warningColor
            );

            return;
        }

        _currentBet = amount;

        SetResult(
            $"BET PLACED: {amount} G",
            _betPlacedColor
        );

        UpdateUI();
        UpdateBetButtonVisuals();
    }

    public void CancelBet()
    {
        if (_isSpinning)
        {
            return;
        }

        if (!HasBet)
        {
            return;
        }

        _currentBet = 0;

        SetResult(
            "BET CANCELLED",
            _noWinColor
        );

        UpdateUI();
        UpdateBetButtonVisuals();
    }

    public void StartSpin()
    {
        if (_isSpinning)
        {
            return;
        }

        if (!HasBet)
        {
            SetResult(
                "PLACE YOUR BET",
                _defaultColor
            );

            return;
        }

        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        _isSpinning = true;

        int roundBet = _currentBet;

        // Deduct the bet when the round starts.
        _balance -= roundBet;

        SetResult(
            "GOOD LUCK!",
            _defaultColor
        );

        UpdateUI();
        UpdateBetButtonVisuals();

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

        CheckResult(roundBet);

        // The bet is consumed after the round.
        _currentBet = 0;

        _isSpinning = false;

        UpdateUI();
        UpdateBetButtonVisuals();
    }

    private void CheckResult(int roundBet)
    {
        SlotSymbol.SymbolType result01 =
            _reel01.CurrentResult;

        SlotSymbol.SymbolType result02 =
            _reel02.CurrentResult;

        SlotSymbol.SymbolType result03 =
            _reel03.CurrentResult;

        Debug.Log(
            $"Results: {result01} | {result02} | {result03}"
        );

        bool isWin =
            result01 == result02 &&
            result02 == result03;

        if (!isWin)
        {
            SetResult(
                "NO WIN",
                _noWinColor
            );

            Debug.Log("NO WIN");

            return;
        }

        int multiplier =
            GetPayoutMultiplier(result01);

        int payout =
            roundBet * multiplier;

        _balance += payout;

        SetResult(
            $"WIN! +{payout} G",
            _winColor
        );

        Debug.Log(
            $"WIN! {result01} | " +
            $"Bet: {roundBet}G | " +
            $"Multiplier: x{multiplier} | " +
            $"Payout: {payout}G"
        );
    }

    private int GetPayoutMultiplier(
        SlotSymbol.SymbolType symbol)
    {
        switch (symbol)
        {
            case SlotSymbol.SymbolType.Seven:
                return _sevenMultiplier;

            case SlotSymbol.SymbolType.Cherries:
                return _cherriesMultiplier;

            case SlotSymbol.SymbolType.Bell:
                return _bellMultiplier;

            case SlotSymbol.SymbolType.Bar:
                return _barMultiplier;

            default:
                return 0;
        }
    }

    private void UpdateBetButtonVisuals()
    {
        if (_isSpinning)
        {
            SetButtonColor(
                _bet10Button,
                _disabledButtonColor
            );

            SetButtonColor(
                _bet50Button,
                _disabledButtonColor
            );

            SetButtonColor(
                _bet100Button,
                _disabledButtonColor
            );
        }
        else
        {
            SetButtonColor(
                _bet10Button,
                _currentBet == 10
                    ? _selectedButtonColor
                    : _normalButtonColor
            );

            SetButtonColor(
                _bet50Button,
                _currentBet == 50
                    ? _selectedButtonColor
                    : _normalButtonColor
            );

            SetButtonColor(
                _bet100Button,
                _currentBet == 100
                    ? _selectedButtonColor
                    : _normalButtonColor
            );
        }

        // Cancel is only available when a bet exists
        // and the reels are not spinning.
        if (_cancelButton != null)
        {
            _cancelButton.interactable =
                !_isSpinning && HasBet;
        }
    }

    private void SetButtonColor(
        Button button,
        Color color)
    {
        if (button == null)
        {
            return;
        }

        Image buttonImage =
            button.GetComponent<Image>();

        if (buttonImage != null)
        {
            buttonImage.color = color;
        }
    }

    private void SetResult(
        string message,
        Color color)
    {
        if (_resultText == null)
        {
            return;
        }

        _resultText.text = message;
        _resultText.color = color;
    }

    private void UpdateUI()
    {
        if (_balanceText != null)
        {
            _balanceText.text =
                $"BALANCE: {_balance} G";
        }

        if (_currentBetText != null)
        {
            _currentBetText.text =
                $"BET: {_currentBet} G";
        }
    }
}