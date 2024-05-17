using BepInEx;
using LarenthiosItems.Items;
using R2API;
using RoR2;
using UnityEngine;

namespace LarenthiosItems
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Plugin : BaseUnityPlugin
    {
        public const string PluginAuthor = "Larenthios";
        public const string PluginName = "LarenthiosItems";
        public const string PluginVersion = "1.0.0";
        public const string PluginGUID = PluginAuthor + "." + PluginName;

        private static AirbornePathogen airbornePathogen;
        private static Sunsetter sunsetter;
        private static EntropicPendulum entropicPendulum;

        public void Awake()
        {
            Log.Init(Logger);

            var displayRules = new ItemDisplayRuleDict(null);
            
            airbornePathogen = ScriptableObject.CreateInstance<AirbornePathogen>();
            sunsetter = ScriptableObject.CreateInstance<Sunsetter>();
            entropicPendulum = ScriptableObject.CreateInstance<EntropicPendulum>();


            ItemAPI.Add(new CustomItem(airbornePathogen, displayRules));
            ItemAPI.Add(new CustomItem(sunsetter, displayRules));
            ItemAPI.Add(new CustomItem(entropicPendulum, displayRules));

            GlobalEventManager.onCharacterDeathGlobal += airbornePathogen.Proc;
            GlobalEventManager.onServerDamageDealt += sunsetter.Proc;
            GlobalEventManager.onServerDamageDealt += entropicPendulum.Proc;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(sunsetter.itemIndex);
                //PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(entropicPendulum.itemIndex);
            }
        }
    }
}
