using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LarenthiosItems.Items
{
    public class EntropicPendulum : ItemDef
    {
        public Action<DamageReport> Proc;

        public EntropicPendulum()
        {
            name = "LARRY_PENDULUM_NAME";
            nameToken = "LARRY_PENDULUM_NAME";
            pickupToken = "LARRY_PENDULUM_PICKUP";
            descriptionToken = "LARRY_PENDULUM_DESC";
            loreToken = "LARRY_PENDULUM_LORE";

            _itemTierDef = Addressables.LoadAssetAsync<ItemTierDef>("RoR2/Base/Common/LunarTierDef.asset").WaitForCompletion();
            pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
            pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();

            canRemove = true;
            hidden = false;

            Proc = report =>
            {
                if (!report.victim || !report.victimBody || !report.attackerBody || report.attackerBody.inventory.GetItemCount(itemIndex) == 0)
                {
                    return;
                }

                report.damageDealt = report.victim.health > (report.victim.fullHealth / 2f) && DateTime.Now.Second / 10 % 2 == 0 ? report.damageDealt * 2 : report.damageDealt / 2;
            };
        }
    }
}