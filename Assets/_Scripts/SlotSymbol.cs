using UnityEngine;

public class SlotSymbol : MonoBehaviour
{
    public enum SymbolType
    {
        Seven,
        Cherries,
        Bell,
        Bar
    }

    [SerializeField] private SymbolType _symbolType;

    public SymbolType Type => _symbolType;
}