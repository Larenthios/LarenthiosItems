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

        public void Awake()
        {
            Log.Init(Logger);

            var displayRules = new ItemDisplayRuleDict(null);
            
            airbornePathogen = ScriptableObject.CreateInstance<AirbornePathogen>();
            sunsetter = ScriptableObject.CreateInstance<Sunsetter>();


            ItemAPI.Add(new CustomItem(airbornePathogen, displayRules));
            ItemAPI.Add(new CustomItem(sunsetter, displayRules));

            GlobalEventManager.onCharacterDeathGlobal += airbornePathogen.Proc;
            GlobalEventManager.onServerDamageDealt += sunsetter.Proc;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(sunsetter.itemIndex);
            }
        }
    }
}
