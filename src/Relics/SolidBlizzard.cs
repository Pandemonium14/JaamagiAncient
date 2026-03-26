using BaseLib.Abstracts;
using BaseLib.Utils;
using JaamagiAncient.enchants;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;

namespace JaamagiAncient.Relics;

[Pool(typeof(EventRelicPool))]
public class SolidBlizzard : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    
    public override string PackedIconPath => "res://JaamagiAncient/RelicImages/small/SolidBlizzard.png";

    protected override string PackedIconOutlinePath => "res://JaamagiAncient/RelicImages/outline/SolidBlizzard.png";

    protected override string BigIconPath => "res://JaamagiAncient/RelicImages/large/SolidBlizzard.png";
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<Icy>();
    
    public override bool TryModifyCardRewardOptionsLate(
        Player player,
        List<CardCreationResult> cardRewards,
        CardCreationOptions options)
    {
        if (player != Owner)
            return false;
        Icy icy = ModelDb.Enchantment<Icy>();
        foreach (CardCreationResult cardReward in cardRewards)
        {
            CardModel card1 = cardReward.Card;
            if (icy.CanEnchant(card1))
            {
                CardModel card2 = Owner.RunState.CloneCard(card1);
                CardCmd.Enchant<Icy>(card2, 1M);
                cardReward.ModifyCard(card2, this);
            }
        }
        return true;
    }
    
}