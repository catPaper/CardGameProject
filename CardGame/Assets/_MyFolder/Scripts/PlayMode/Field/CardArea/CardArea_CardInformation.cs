using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// カードAreaに属するカード情報を管理する
/// </summary>
public class CardArea_CardInformation : MonoBehaviour,CardInterface {

	[SerializeField][Header("カード裏面")]
	private GameObject _cardBack;
	[SerializeField][Header("カード背景色")]
	private Image _cardBackGroundColor;
	[SerializeField][Header("カードイメージ")]
	private Image _cardImage;
	[SerializeField][Header("カードネーム")]
	private Text _cardName;
	[SerializeField][Header("カードコスト")]
	private Text _cardCost;
	[SerializeField][Header("カード説明欄")]
	private Text _cardExplain;
	[SerializeField][Header("攻撃力格納")]
	private GameObject _atackImageObject;
	[SerializeField][Header("体力格納")]
	private GameObject _hitImageObject;
	[SerializeField][Header("どちらの物か")]
	private GameManager.Who _who;

	//アタッチされたカード内で取り扱うカードインフォメーション
	[SerializeField][Header("Debug用")]
	private CardInformation _myCardInformation;

	public bool isOwn()
	{
		if (_who == GameManager.Who.MY)
			return true;
		else
			return false;
	}

	public void SetTheCard(CardInformation setCardInfo)
	{
		//自身にカードインフォメーションを反映
		_myCardInformation = setCardInfo;

		//TODO 相手のカードか自分のカードか識別
		_cardBack.SetActive(false);

		_cardImage.sprite = _myCardInformation.CardImage ();
		_cardName.text = _myCardInformation.CardName ();
		_cardExplain.text = _myCardInformation.CardExplain ();
		_cardCost.text = _myCardInformation.CardCost ().ToString();
		Setting_FromHeroType ();
		Setting_FromCardType ();

	}

	/// <summary>
	/// ヒーロータイプを識別し色の設定を行う
	/// </summary>
	private void Setting_FromHeroType()
	{
		_cardBackGroundColor.color =  _myCardInformation.GetHeroColor ();
	}

	/// <summary>
	/// カードタイプを識別しそれぞれ必要、不必要なオブジェクトを表示、非表示する
	/// </summary>
	private void Setting_FromCardType()
	{
		if (_myCardInformation.GetCardType () == CardInformation.CardType.MAGIC) {
			_atackImageObject.SetActive (false);
			_hitImageObject.SetActive (false);
		} else {
			_atackImageObject.GetComponentInChildren<Text> ().text = _myCardInformation.AtackPoint ().ToString ();
			_atackImageObject.SetActive (true);
			_hitImageObject.GetComponentInChildren<Text> ().text = _myCardInformation.HitPoint ().ToString ();
			_hitImageObject.SetActive (true);
		}
	}

	/// <summary>
	/// 対象がミニオンかどうか
	/// </summary>
	/// <returns><c>true</c> if this instance is minion; otherwise, <c>false</c>.</returns>
	public bool IsMinion()
	{
		if (_myCardInformation.GetCardType () == CardInformation.CardType.CREATURE)
			return true;
		else
			return false;
	}

	/// <summary>
	/// カードをプレイするため非表示にする
	/// </summary>
	public void PlayCard()
	{
		gameObject.transform.parent.gameObject.SetActive (false);
	}

	/// <summary>
	/// 自分のCardInformationを送る
	/// </summary>
	/// <returns>The card information.</returns>
	public CardInformation MyCardInformation()
	{
		return _myCardInformation;
	}
}
