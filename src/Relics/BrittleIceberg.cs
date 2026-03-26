using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace JaamagiAncient.Relics;

[Pool(typeof(EventRelicPool))]
public class BrittleIceberg : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;


    public override string PackedIconPath => "res://JaamagiAncient/RelicImages/small/BrittleIceberg.png";

    protected override string PackedIconOutlinePath => "res://JaamagiAncient/RelicImages/outline/BrittleIceberg.png";

    protected override string BigIconPath => "res://JaamagiAncient/RelicImages/large/BrittleIceberg.png";

    public override decimal ModifyHpLostAfterOstyLate(Creature target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        if (target != Owner.Creature) return amount;
        
        return amount * 2M;
    }
    
    public override Task AfterModifyingHpLostAfterOsty()
    {
        Flash();
        return Task.CompletedTask;
    }

    public override bool ShouldClearBlock(Creature creature)
    {
        return creature != Owner.Creature;
    }
}