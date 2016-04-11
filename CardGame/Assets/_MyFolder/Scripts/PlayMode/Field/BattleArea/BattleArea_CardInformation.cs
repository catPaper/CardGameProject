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
	[SerializeField][Header("どちらの物か")]
	private GameManager.Who _who;

	private CardInformation _myCardInformation;

	private bool _canAtack;	//攻撃可能状態かどうか

	//キャラを囲うフレーム
	private Image _charaFrame;


	/// <summary>
	/// 自分の所有するものか
	/// </summary>
	/// <returns><c>true</c>, if own was ised, <c>false</c> otherwise.</returns>
	public bool isOwn()
	{
		if (_who == GameManager.Who.MY)
			return true;
		else 
			return false;
	}

		

	/// <summary>
	/// カードインフォーメーションをもとにカードをセットする
	/// </summary>
	/// <param name="_targetCardInformation">Target card information.</param>
	public void SetTheCard(CardInformation _targetCardInformation)
	{
	//;
		_myCardInformation = CardInformation.Instantiate(_targetCardInformation);

		UpdateCardInfo ();
		_charaPrefab.SetActive (true);
		_canAtack = false;

	}

	/// <summary>
	/// カード情報を更新する
	/// </summary>
	public void UpdateCardInfo()
	{
		_atackPointText.text = _myCardInformation.AtackPoint ().ToString();
		_hitPointText.text = _myCardInformation.HitPoint ().ToString();
		_charaImage.sprite = _myCardInformation.PlayImage ();
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
		_charaPrefab.SetActive (false);
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
		_canAtack = true;
	}

	/// <summary>
	/// 攻撃を完了させる
	/// </summary>
	public void AtackFinish()
	{
		_canAtack = false;
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

		if (_canAtack)
			return true;
		else
			return false;
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
}
