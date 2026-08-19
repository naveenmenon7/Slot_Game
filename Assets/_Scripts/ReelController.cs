using System.Collections;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    [Header("Reel References")]
    [SerializeField] private Transform _symbolContainer;

    [Header("Spin Settings")]
    [SerializeField] private float _spinSpeed = 10f;
    [SerializeField] private float _minimumSpinDuration = 2.2f;
    [SerializeField] private float _maximumSpinDuration = 3f;
    [SerializeField] private float _symbolSpacing = 1.5f;
    [SerializeField] private float _settleDuration = 0.35f;

    private bool _isSpinning;

    public bool IsSpinning => _isSpinning;

    public SlotSymbol.SymbolType CurrentResult { get; private set; }

    public void Spin()
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

        // Randomize how long the reel spins.
        float spinDuration = Random.Range(
            _minimumSpinDuration,
            _maximumSpinDuration
        );

        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            float normalizedTime =
                elapsedTime / spinDuration;

            // Gradually slow the reel down.
            float speedMultiplier =
                1f - Mathf.SmoothStep(
                    0f,
                    1f,
                    normalizedTime
                );

            MoveSymbols(
                _spinSpeed * speedMultiplier
            );

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Find whichever symbol naturally ended
        // closest to the center.
        Transform resultSymbol =
            FindClosestToCenter();

        if (resultSymbol != null)
        {
            // Smoothly move that symbol into the center.
            yield return StartCoroutine(
                SettleToCenter(resultSymbol)
            );

            SlotSymbol slotSymbol =
                resultSymbol.GetComponent<SlotSymbol>();

            if (slotSymbol != null)
            {
                CurrentResult =
                    slotSymbol.Type;
            }
        }

        _isSpinning = false;
    }

    private void MoveSymbols(float currentSpeed)
    {
        float bottomLimit = -3f;

        float loopDistance =
            _symbolSpacing *
            _symbolContainer.childCount;

        for (int i = 0;
             i < _symbolContainer.childCount;
             i++)
        {
            Transform symbol =
                _symbolContainer.GetChild(i);

            Vector3 position =
                symbol.localPosition;

            position.y -=
                currentSpeed * Time.deltaTime;

            // Recycle the symbol from the bottom
            // back to the top of the reel.
            if (position.y < bottomLimit)
            {
                position.y += loopDistance;
            }

            symbol.localPosition =
                position;
        }
    }

    private Transform FindClosestToCenter()
    {
        Transform closestSymbol = null;

        float closestDistance =
            Mathf.Infinity;

        for (int i = 0;
             i < _symbolContainer.childCount;
             i++)
        {
            Transform symbol =
                _symbolContainer.GetChild(i);

            float distance =
                Mathf.Abs(symbol.localPosition.y);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSymbol = symbol;
            }
        }

        return closestSymbol;
    }

    private IEnumerator SettleToCenter(
        Transform resultSymbol)
    {
        float offset =
            resultSymbol.localPosition.y;

        Vector3[] startPositions =
            new Vector3[
                _symbolContainer.childCount
            ];

        Vector3[] targetPositions =
            new Vector3[
                _symbolContainer.childCount
            ];

        for (int i = 0;
             i < _symbolContainer.childCount;
             i++)
        {
            Transform symbol =
                _symbolContainer.GetChild(i);

            startPositions[i] =
                symbol.localPosition;

            Vector3 targetPosition =
                symbol.localPosition;

            targetPosition.y -= offset;

            targetPositions[i] =
                targetPosition;
        }

        float elapsedTime = 0f;

        while (elapsedTime < _settleDuration)
        {
            float normalizedTime =
                elapsedTime /
                _settleDuration;

            float smoothTime =
                Mathf.SmoothStep(
                    0f,
                    1f,
                    normalizedTime
                );

            for (int i = 0;
                 i < _symbolContainer.childCount;
                 i++)
            {
                Transform symbol =
                    _symbolContainer.GetChild(i);

                symbol.localPosition =
                    Vector3.Lerp(
                        startPositions[i],
                        targetPositions[i],
                        smoothTime
                    );
            }

            elapsedTime +=
                Time.deltaTime;

            yield return null;
        }

        // Guarantee the final position.
        for (int i = 0;
             i < _symbolContainer.childCount;
             i++)
        {
            _symbolContainer
                .GetChild(i)
                .localPosition =
                targetPositions[i];
        }
    }
}