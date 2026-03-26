using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;

namespace JaamagiAncient.Relics.Relics;

[Pool(typeof(EventRelicPool))]
public class MeltingPermafrost : CustomRelicModel
{
    
    public override RelicRarity Rarity => RelicRarity.Ancient;
    
    public override string PackedIconPath => "res://JaamagiAncient/RelicImages/small/MeltingPermafrost.png";

    protected override string PackedIconOutlinePath => "res://JaamagiAncient/RelicImages/outline/MeltingPermafrost.png";

    protected override string BigIconPath => "res://JaamagiAncient/RelicImages/large/MeltingPermafrost.png";

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PlatingPower>("PlatingPower",3)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<PlatingPower>()];

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (!CombatManager.Instance.IsInProgress || cardPlay.Card.Owner != Owner || cardPlay.Card.Type != CardType.Power)
            return;
        Flash();
        await PowerCmd.Apply<PlatingPower>(Owner.Creature, DynamicVars["PlatingPower"].BaseValue, Owner.Creature, null);
    }
}