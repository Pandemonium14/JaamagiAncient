using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using JaamagiAncient.relicadds;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace JaamagiAncient.Relics;



[Pool(typeof(EventRelicPool))]
public class CrystalScale : CustomRelicModel
{
    public override string PackedIconPath => "res://JaamagiAncient/RelicImages/small/CrystalScale.png";

    protected override string PackedIconOutlinePath => "res://JaamagiAncient/RelicImages/outline/CrystalScale.png";

    protected override string BigIconPath => "res://JaamagiAncient/RelicImages/large/CrystalScale.png";
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<PolarShell>()];

    public override async Task AfterObtained()
    {
        CardModel card = Owner.RunState.CreateCard(ModelDb.Card<PolarShell>(),Owner);
        CardPileAddResult result = await CardPileCmd.Add(card, PileType.Deck);
        CardCmd.PreviewCardPileAdd(result);
    }
}