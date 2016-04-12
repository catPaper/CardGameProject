using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillInformation : MonoBehaviour {

    [Header("スキルリスト")]
    public List<SkillType> _skillList = new List<SkillType>(); 


    public enum SkillType
    {
        IMMUNE,         // 無敵
        INSPIRE,        // 激励
        WINDFURY,       // 疾風
        ENRAGE,         // 激怒
        OVERLOAD,       // オーバーロード
        COMBO,          // コンボ
        SILENCE,        // 沈黙
        SECRET,         // 秘策
        JOUST,          // ジャウスト
        STEALTH,        // 隠れ身
        SPELLDAMAGE,    // 呪文ダメージ
        CHARGE,         // 突撃
        CHOOSEONE,      // 選択
        DEATHRATTLE,    // 断末魔
        DIVINESHIELD,   // 聖なる盾
        TAUNT,          // 挑発
        TRANSFORM,      // 変身
        BATTLECRY,      // 雄叫び
        DISCOVER,       // 発見
        FATIGUE,        // 疲労
        FREEZE,         // 凍結
        MEGAWINDFURY,   // メガ・ウィンドフューリー
		//**********ここに追加*****************//
		UNKNOWN			//これが最後になるように調節
    }

    public void Immune()
    {

    }
    public void Inspire()
    {

    }
    public void Windfury()
    {

    }
    public void Enrage()
    {

    }
    public void OverLoad()
    {

    }
    public void Combo()
    {

    }
    public void Silence()
    {

    }
    public void Secret()
    {

    }
    public void Joust()
    {

    }
    public void Stealth()
    {

    }
    public void SpellDamage()
    {

    }
    public void Charge()
    {

    }
    public void ChooseOne()
    {

    }
    public void DeathRattle()
    {

    }
    public void DivineShield()
    {

    }
    public void Taunt()
    {

    }
    public void Transform()
    {

    }
    public void BattleCry()
    {

    }
    public void Discover()
    {

    }
    public void Fatigue()
    {

    }
    public void Freeze()
    {

    }
    public void MegaWindFury()
    {

    }    


}
