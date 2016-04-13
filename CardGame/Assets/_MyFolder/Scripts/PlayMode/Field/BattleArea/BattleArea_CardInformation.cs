using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BattleArea_CardInformation : MonoBehaviour,CardInterface {

	[SerializeField][Header("キャラプレファブ")]
	private GameObject _charaPrefab;
	[SerializeField][Header("キャライメージ")]
	private Image _charaImage;
	[SerializeField][Header("攻撃力")]
	private Text _atackPointText;
	[SerializeField][Header("体力")]
	private Text _hitPointText;
	[SerializeField][Header("選択可能かどうか")]
	private GameObject ActiveImage;
	[Header("スキルイメージ")]
	[SerializeField]
	private Image TauntImage;
	[SerializeField]
	private Image DivineSheildImage;
	[SerializeField]
	private GameObject WindFurryImage;
	[SerializeField][Header("どちらの物か")]
	private GameManager.Who _who;

	private CardInformation _myCardInformation;

	private int _atackCounter;		//攻撃回数をカウント

	//キャラを囲うフレーム
	private Image _charaFrame;


	public enum ActiveCharacter
	{
		ACTIVE,		//アクティブ状態
		STAY		//非アクティブ状態
	}

	private ActiveCharacter _activeCharacter = new ActiveCharacter();

	/// <summary>
	/// 自分の所有するものか
	/// </summary>
	/// <returns><c>true</c>, if own was ised, <c>false</c> otherwise.</returns>
	public bool isOwn()
	{
		return (_who == GameManager.Who.MY);
	}

		

	/// <summary>
	/// カードインフォーメーションをもとにカードをセットする
	/// </summary>
	/// <param name="_targetCardInformation">Target card information.</param>
	public void SetTheCard(CardInformation _targetCardInformation)
	{
		_charaFrame.enabled = false;

		_myCardInformation = CardInformation.Instantiate(_targetCardInformation);

		if(SearchSkill(SkillInformation.SkillType.CHARGE))
			AtackReflesh();
		else
			AtackFinish();

		UpdateCardInfo();
		_charaPrefab.SetActive (true);

	}

	/// <summary>
	/// カード情報を更新する
	/// </summary>
	public void UpdateCardInfo()
	{
		if(_activeCharacter == ActiveCharacter.ACTIVE){
			ActiveImage.SetActive(true);
		}else{
			ActiveImage.SetActive(false);
		}
		_atackPointText.text = _myCardInformation.AtackPoint ().ToString();
		_hitPointText.text = _myCardInformation.HitPoint ().ToString();
		_charaImage.sprite = _myCardInformation.PlayImage ();
		Deactive_AllSkillImage ();
		Set_SkillImage ();
	}

	/// <summary>
	/// すべてのスキルイメージを非表示にする
	/// </summary>
	private void Deactive_AllSkillImage()
	{
		TauntImage.enabled = false;
		DivineSheildImage.enabled = false;
		WindFurryImage.SetActive(false);
		//ここにどんどん追加
	}

	/// <summary>
	/// スキルイメージをカードインフォーメーションをもとに表示する
	/// </summary>
	private void Set_SkillImage()
	{
		//挑発イメージをセット
		if (SearchSkill (SkillInformation.SkillType.TAUNT))
			TauntImage.enabled = true;
		if (SearchSkill (SkillInformation.SkillType.DIVINESHIELD))
			DivineSheildImage.enabled = true;
		if(SearchSkill(SkillInformation.SkillType.MEGAWINDFURY)){
			if(_atackCounter < 3)
				WindFurryImage.SetActive(true);
		}
		if(SearchSkill(SkillInformation.SkillType.WINDFURY)){
			if(_atackCounter < 2)
				WindFurryImage.SetActive(true);
		}
		//ここにどんどん追加
	}

	/// <summary>
	/// ダメージを受ける
	/// </summary>
	public void Damage(int amount)
	{
		_myCardInformation.Damage (amount);
		UpdateCardInfo();
	}


	/// <summary>
	/// 現段階では場に出ているカードはミニオン
	/// </summary>
	/// <returns><c>true</c> if this instance is minion; otherwise, <c>false</c>.</returns>
	public bool IsMinion()
	{
		return true;
	}

	/// <summary>
	/// ミニオンの破壊処理
	/// </summary>
	public void DeadProcess()
	{
		Destroy (_myCardInformation.gameObject);
		DeactiveCharaPrefab ();
	}

	/// <summary>
	/// キャラプレファブを非表示にする
	/// </summary>
	public void DeactiveCharaPrefab()
	{
		Deactive_AllSkillImage ();
		_activeCharacter = ActiveCharacter.STAY;
		ActiveImage.SetActive(false);
		_charaPrefab.SetActive (false);
		_charaFrame.enabled =true;
	}

	void Awake()
	{
		_charaFrame = GetComponent<Image> ();
	}

	/// <summary>
	/// 攻撃をできるようにする
	/// </summary>
	public void AtackReflesh()
	{
		_atackCounter = 0;
		_activeCharacter = ActiveCharacter.ACTIVE;

		UpdateCardInfo();
	}

	/// <summary>
	/// 一回攻撃する
	/// </summary>
	public void OneAtack()
	{
		_atackCounter++;

		if(!CanAtack())
			_activeCharacter = ActiveCharacter.STAY;

		UpdateCardInfo();
	}

	/// <summary>
	/// 攻撃を完了させる
	/// </summary>
	public void AtackFinish()
	{
		//メガウィンドフューリー
		if(_myCardInformation.SearchSkill(SkillInformation.SkillType.MEGAWINDFURY)){
			_atackCounter = 3;
			return;
		}
		//疾風
		if(_myCardInformation.SearchSkill(SkillInformation.SkillType.WINDFURY)){
			_atackCounter = 2;
			return;
		}
		//それいがい
		_atackCounter = 1;

		_activeCharacter = ActiveCharacter.STAY;

		UpdateCardInfo();
	}

	/// <summary>
	/// このミニオンが攻撃可能かどうかチェック
	/// </summary>
	/// <returns><c>true</c>, if check was atacked, <c>false</c> otherwise.</returns>
	public bool AtackCheck()
	{
		if (_myCardInformation.AtackPoint() <= 0)
			return false;

		//TODO 効果等で攻撃できない場合参照して記述

		return CanAtack();
	}

	/// <summary>
	/// 指定したスキルを所持しているか
	/// </summary>
	/// <returns><c>true</c>, if skill was searched, <c>false</c> otherwise.</returns>
	/// <param name="_skillName">Skill name.</param>
	public bool SearchSkill(SkillInformation.SkillType _skillName)
	{
		return _myCardInformation.SearchSkill (_skillName);
	}

	/// <summary>
	/// 攻撃力
	/// </summary>
	/// <returns>The atack power.</returns>
	public int MyAtackPower()
	{
		return _myCardInformation.AtackPoint();
	}

	/// <summary>
	/// 体力
	/// </summary>
	/// <returns>The health.</returns>
	public int MyHealth()
	{
		return _myCardInformation.HitPoint ();
	}


	/// <summary>
	/// 攻撃可能回数かどうか
	/// </summary>
	/// <returns><c>true</c> if this instance can atack; otherwise, <c>false</c>.</returns>
	private bool CanAtack()
	{
		if(_atackCounter == 0){
			return true;
		}else if(_atackCounter == 1){
			if(_myCardInformation.SearchSkill(SkillInformation.SkillType.WINDFURY)||_myCardInformation.SearchSkill(SkillInformation.SkillType.MEGAWINDFURY))
				return true;
			else
				return false;
		}else{
			return false;
		}
	}
}
