using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine; 
using UnityEngine.AddressableAssets;

namespace LarenthiosItems.Items
{
    public class AirbornePathogen : ItemDef
    {
        public Action<DamageReport> Proc;

        public static int debuffs = 1;
        public static int targets = 4;
        public static float range = 240f;

        public AirbornePathogen()
        {
            name = "LARRY_PATHOGEN_NAME";
            nameToken = "LARRY_PATHOGEN_NAME";
            pickupToken = "LARRY_PATHOGEN_PICKUP";
            descriptionToken = "LARRY_PATHOGEN_DESC";
            loreToken = "LARRY_PATHOGEN_LORE";

            _itemTierDef = Addressables.LoadAssetAsync<ItemTierDef>("RoR2/Base/Common/Tier3Def.asset").WaitForCompletion();
            pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
            pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();

            canRemove = true;
            hidden = false;

            Proc = report =>
            {
                if (!report.attacker || !report.attackerBody || !report.victim || !report.victimBody)
                {
                    return;
                }
                System.Random r = new();

                BuffIndex[] activeBuffs = report.victimBody.GetFieldValue<BuffIndex[]>("activeBuffsList")
                    .Where(b => report.victimBody.HasBuff(b) && BuffCatalog.debuffBuffIndices.Contains(b))
                    .ToArray();

                List<CharacterBody.TimedBuff> activeTimedBuffs = report.victimBody.GetFieldValue<List<CharacterBody.TimedBuff>>("timedBuffs")
                    .Where(b => report.victimBody.HasBuff(b.buffIndex) && BuffCatalog.debuffBuffIndices.Contains(b.buffIndex)).ToList();

                List<Tuple<BuffIndex, float>> buffs = activeBuffs
                    .Select(ab => new Tuple<BuffIndex, float>(ab, -1f))
                    .Concat(activeTimedBuffs.Select(atb => new Tuple<BuffIndex, float>(atb.buffIndex, atb.timer)))
                    .ToList();

                CharacterBody.readOnlyInstancesList
                    .Where(chara => chara.healthComponent.alive && !chara.isPlayerControlled && Vector3.Distance(chara.corePosition, report.victimBody.corePosition) < range)
                    .OrderBy(chara => Vector3.Distance(chara.corePosition, report.victimBody.corePosition))
                    .Take(targets)
                    .ForEachTry(chara =>
                    {
                        buffs.OrderBy(_ => r.Next()).Take(debuffs).ForEachTry(b =>
                        {

                            if (b.Item2 == -1f)
                            {
                                chara.SetBuffCount(b.Item1, report.victimBody.GetBuffCount(b.Item1));
                            }
                            else
                            {
                                chara.AddTimedBuff(b.Item1, b.Item2);
                            }
                        });
                    });
            };
        }
    }
}