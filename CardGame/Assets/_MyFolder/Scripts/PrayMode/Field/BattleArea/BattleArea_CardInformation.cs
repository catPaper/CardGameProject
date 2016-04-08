using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BattleArea_CardInformation : MonoBehaviour {

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
		_myCardInformation = _targetCardInformation;

		_atackPointText.text = _myCardInformation.AtackPoint ().ToString();
		_hitPointText.text = _myCardInformation.HitPoint ().ToString();
		_charaImage.sprite = _myCardInformation.PlayImage ();
		_charaPrefab.SetActive (true);

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
}
