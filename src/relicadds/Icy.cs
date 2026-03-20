using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace JaamagiAncient.enchants;

public class Icy : EnchantmentModel
{
    private const Decimal _blockBonus = 4M;

    public override bool HasExtraCardText => true;

    public override bool CanEnchant(CardModel card)
    {
        bool canEnchant = base.CanEnchant(card);
        if (!canEnchant)  return false;
        if (!card.GainsBlock) return false;
        DynamicVar blockVar = card.DynamicVars.Block;
        return blockVar.BaseValue > 0;
    }

    public override decimal EnchantBlockAdditive(decimal originalBlock, ValueProp props)
    {
        return _blockBonus;
    }

}