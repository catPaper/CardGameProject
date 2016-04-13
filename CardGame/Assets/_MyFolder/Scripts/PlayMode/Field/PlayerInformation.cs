using UnityEngine;
using System.Collections;

/// <summary>
/// 各プレイヤーの情報
/// </summary>
public class PlayerInformation : MonoBehaviour {

	[SerializeField]
	private CardAreaManager _myCardAreaManager;
	[SerializeField]
	private HeroManager _myHeroManager;
	[SerializeField]
	private DeckManager _myDeckManager;
	[SerializeField]
	private ManaManager _myManaManager;
	[SerializeField]
	private BattleAreaManager _myBattleAreaManager;

	/// <summary>
	/// フィールド上のキャライメージを非表示にする
	/// </summary>
	public void StartCleanField(){
		_myBattleAreaManager.AllCharaImageDeactive ();
	}

	public void StartDrawProcess(bool isFisrt)
	{
		for (int i = 0; i < 3; i++) {
			DrawProcess ();
		}

		if (!isFisrt) {
			DrawProcess ();
		}
	}

	public void DeckSetting(CardInformation[] cardInformations)
	{
		_myDeckManager.Set_UseDeck (cardInformations);
	}

	/// <summary>
	/// ターンはじめのマナを補充するPhase
	/// </summary>
	public void ManaReloadProcess(){
		_myManaManager.ManaReloadProcess ();
	}

	/// <summary>
	/// カードを一枚引く処理
	/// </summary>
	public void DrawProcess(){
		_myCardAreaManager.GetCard (_myDeckManager.DrawCard ());
	}

	/// <summary>
	/// 手札のクリーチャーカードを場に出す
	/// </summary>
	/// <param name="_handCardInfo">Hand card info.</param>
	/// <param name="_fieldCardInfo">Field card info.</param>
	public void PlayCharacterCard(CardArea_CardInformation _handCardInfo,BattleArea_CardInformation _fieldCardInfo)
	{
		CardInformation _playCardInformation = _handCardInfo.MyCardInformation();
		if (_myManaManager.isUseMana (_playCardInformation.CardCost ())) {
			//マナの消費
			_myManaManager.DecreaseCurrentMana(_playCardInformation.CardCost());
			//手札からカードを出す処理
			_handCardInfo.PlayCard ();
			_myCardAreaManager.IsSort ();
			//フィールドにプレイ
			_myBattleAreaManager.ActiveCharaPrefab (_playCardInformation, _fieldCardInfo);
		}

	}



	public bool SearchSkill(SkillInformation.SkillType _skillName)
	{
		return _myBattleAreaManager.SearchSkill (_skillName);
	}

	/// <summary>
	/// 自陣のミニオンをすべて攻撃終了状態にする
	/// </summary>
	public void AllMinionAtackFinish()
	{
		_myBattleAreaManager.AllMinionAtackFinish();
	}

	/// <summary>
	/// 自陣の攻撃できるミニオンを攻撃可能にする
	/// </summary>
	public void AllMinionAtackReflesh()
	{
		_myBattleAreaManager.AllMinionAtackReflesh ();
	}

	public void GetCoin(CardInformation coinInformation)
	{
		_myCardAreaManager.GetCard (coinInformation);
	}
}
