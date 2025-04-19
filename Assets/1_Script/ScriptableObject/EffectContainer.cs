using System.Collections.Generic;
using UnityEngine;

namespace Class
{
    [CreateAssetMenu (fileName = "EffectContainer", menuName = "ScriptableObject/EffectContainer", order = 2)]
    public class EffectContainer : ScriptableObject
    {
        public List<HorrorEffect> HorrorEffects;

        public enum Stages
        {
            none = 0,
            stage1 = 1,
            stage2 = 2,
            stage3 = 3,
            stage4 = 4,
            stage5 = 5,
            stage6 = 6,
            stage7 = 7,
        }

        public HorrorEffect GetHorrorEffect(Stages stage = Stages.none)
        {
            var rand = 0;
            if (stage == Stages.none)
            {
                do
                {
                     rand = UnityEngine.Random.Range(0, HorrorEffects.Count);
                    
                }while(HorrorEffects[rand].stageInfo == (int)stage);
            }
            
            return HorrorEffects[rand];
        }
        
    }
}