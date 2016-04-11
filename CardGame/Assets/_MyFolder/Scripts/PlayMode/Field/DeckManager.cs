using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 利用するデッキの一括管理
/// </summary>
public class DeckManager : MonoBehaviour {

	[SerializeField]
	private Text _myDeckRestCardsText;

	private CardInformation[] _myDeck = new CardInformation[30];
	private int topOfCard;	//デッキの一番上の配列番号

	/// <summary>
	/// デッキリストから使うデッキの中身をコピーする
	/// </summary>
	public void Set_UseDeck(CardInformation[] cardInformation)
	{
		for (int i = 0; i < 30; i++) {
			_myDeck [i] = cardInformation [i];
		}
		SetUp ();
	}

	/// <summary>
	/// デッキの中身をシャッフルする
	/// </summary>
	private void Shuffle()
	{
		CardInformation  tmp;
		int tmpBoxNumber;
		int tmpBoxNumber2;
		for (int i = 0; i < 200; i++) {
			tmpBoxNumber = Random.Range (topOfCard, 30);
			tmpBoxNumber2 = Random.Range (topOfCard, 30);
			tmp = _myDeck [tmpBoxNumber];
			_myDeck [tmpBoxNumber] = _myDeck [tmpBoxNumber2];
			_myDeck [tmpBoxNumber2] = tmp;
		}
	}

	/// <summary>
	/// ゲームを始める準備をする
	/// </summary>
	private void SetUp()
	{
		topOfCard = 0;
		UpdateDeckRestText ();
		Shuffle ();
	}

	/// <summary>
	/// カードをドローし、カードインフォメーションを渡す
	/// </summary>
	/// <returns>カードインフォメーション</returns>
	public CardInformation DrawCard()
	{
		topOfCard++;
		if (topOfCard <= 30) {
			UpdateDeckRestText();
			return _myDeck [topOfCard - 1];
		} else {
			Debug.Log ("これ以上カードが引けません");
			return null;
		}
	}

	/// <summary>
	/// デッキの残り枚数を更新する
	/// </summary>
	private void UpdateDeckRestText()
	{
		_myDeckRestCardsText.text = "残り枚数:" + (30 - topOfCard).ToString ();
	}
		
		
}
