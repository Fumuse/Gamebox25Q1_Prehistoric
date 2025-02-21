using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementsLibrary", menuName = "Gameplay/ElementsLibrary", order = 1100)]
public class ElementsLibrary : ScriptableObject
{
    [field: SerializeField] public List<ElementCompound> Elements { get; private set; } = new();

    [CanBeNull]
    public ElementCompound this[ElementType type]
    {
        get
        {
            return Elements.FirstOrDefault((element) => element.ElementType.Equals(type));
        }
    }

    private void OnValidate()
    {
        SortElementsByTotalAmount();
    }

    [CanBeNull]
    public ElementCompound GetElementToTransform(Dictionary<ElementType, int> availableParticles)
    {
        ElementCompound element = null;
        foreach (ElementCompound elementCompound in Elements)
        {
            if (elementCompound.CanBeTransform(availableParticles))
            {
                element = elementCompound;
                break;
            }
        }

        return element;
    }

    private void SortElementsByTotalAmount()
    {
        Elements = Elements
            .OrderByDescending(e => e.Recipe.Sum(pv => pv.Amount))
            .ToList();
    }
}