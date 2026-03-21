using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace JaamagiAncient.Relics;

[Pool(typeof(EventRelicPool))]
public class FrostedFlakes : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new MaxHpVar(40M)];

    public override async Task AfterObtained()
    {
        await CreatureCmd.GainMaxHp(Owner.Creature, DynamicVars.MaxHp.BaseValue);
        await CreatureCmd.Heal(Owner.Creature, Owner.Creature.MaxHp * 2);
    }

    public override decimal ModifyHealAmount(Creature creature, decimal amount)
    {
        return amount/2M;
    }
}