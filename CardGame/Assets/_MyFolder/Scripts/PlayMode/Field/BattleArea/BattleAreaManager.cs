using UnityEngine;
using System.Collections;

public class BattleAreaManager : MonoBehaviour {

	[SerializeField][Header("キャライメージプレファブ")]
	private BattleArea_CardInformation[] _myBattleArea_CardInformation = new BattleArea_CardInformation[7];

	private int activeCharaPrefabs;		//有効になっているキャラプレファブ数

	/// <summary>
	/// 自分のキャライメージをすべて非表示にする
	/// </summary>
	public void AllCharaImageDeactive()
	{
		for (int i = 0; i < 7; i++) {
			_myBattleArea_CardInformation [i].DeactiveCharaPrefab ();
		}
		activeCharaPrefabs = 0;
	}

	public void ActiveCharaPrefab(CardInformation _targetCardInformation,BattleArea_CardInformation _targetBattleArea_CardInformation)
	{
		_targetBattleArea_CardInformation.SetTheCard (_targetCardInformation);
		activeCharaPrefabs++;
	}

	private void DeactiveCharaPrefab(BattleArea_CardInformation _targetBattleArea_CardInformation)
	{
		_targetBattleArea_CardInformation.DeadProcess();
		activeCharaPrefabs--;
	}

	/// <summary>
	/// ターン終了時等、自陣のミニオンの攻撃可能状態を終える
	/// </summary>
	public void AllMinionAtackFinish()
	{
		for (int i = 0; i < 7; i++) {
			if (_myBattleArea_CardInformation [i].gameObject.transform.FindChild("CharaPrefab").gameObject.activeSelf)
				_myBattleArea_CardInformation [i].AtackFinish();
		}
	}

	/// <summary>
	/// 自陣のすべての攻撃できるミニオンを攻撃可能状態にする
	/// </summary>
	public void AllMinionAtackReflesh()
	{
		for (int i = 0; i < 7; i++) {
			if (_myBattleArea_CardInformation [i].gameObject.transform.FindChild("CharaPrefab").gameObject.activeSelf)
				_myBattleArea_CardInformation [i].AtackReflesh ();
		}
	}

	/// <summary>
	/// ミニオンが生存可能かどうかチェック
	/// </summary>
	private void AliveCheck()
	{
		for (int i = 0; i < 7; i++) {
			if (_myBattleArea_CardInformation [i].gameObject.transform.FindChild("CharaPrefab").gameObject.activeSelf) {
				if (_myBattleArea_CardInformation [i].MyHealth () <= 0)
					DeadProcess (_myBattleArea_CardInformation[i]);
					
			}
		}
	}

	/// <summary>
	/// 破壊プロセス
	/// </summary>
	private void DeadProcess(BattleArea_CardInformation _deadTargetInfo)
	{
		//TODO 効果により破壊時チェックを行う
		DeactiveCharaPrefab(_deadTargetInfo);
	}

	void Update()
	{
		AliveCheck ();
	}

	/// <summary>
	///指定したスキルを所持しているか
	/// </summary>
	/// <returns><c>true</c>, if skill was searched, <c>false</c> otherwise.</returns>
	/// <param name="_skillName">Skill name.</param>

	public bool SearchSkill(SkillInformation.SkillType _skillName)
	{
		for (int i = 0; i < 7; i++) {
			if (_myBattleArea_CardInformation [i].gameObject.transform.FindChild ("CharaPrefab").gameObject.activeSelf) {
				if (_myBattleArea_CardInformation [i].SearchSkill (_skillName))
					return true;
			}
		}

		return false;
	}
}
