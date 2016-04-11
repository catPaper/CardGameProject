using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardAreaManager : MonoBehaviour {

	[SerializeField]
	private GameObject[] _handCardFolder = new GameObject[10];

	//所持しているカード数
	private int activeHandCards;

	/// <summary>
	/// すべての手札を非表示/破棄する
	/// </summary>
	private void AllDisCard()
	{
		for (int i = 0; i < 10; i++) {
			_handCardFolder [i].SetActive(false);
		}
		activeHandCards = 0;
	}

	/// <summary>
	/// 手札のカードを有効にする
	/// </summary>
	private void ActiveCard()
	{
		_handCardFolder [activeHandCards - 1].SetActive (true);
	}

	/// <summary>
	/// カード情報にもとづいてカードを手札に追加する
	/// </summary>
	public void GetCard(CardInformation cardInfo)
	{
		if (!IsGetCard ()) {
			Debug.Log ("手札がいっぱいです");
		} else {
			activeHandCards++;
			ActiveCard ();
			_handCardFolder [activeHandCards - 1].GetComponentInChildren<CardArea_CardInformation> ().SetTheCard (cardInfo);

		}
	}

	/// <summary>
	/// カードをプレイしたためソートを行う
	/// </summary>
	/// <returns><c>true</c> if this instance is sort; otherwise, <c>false</c>.</returns>
	public void IsSort()
	{
		activeHandCards--;
		GameObject tmpObject;
		if (activeHandCards == 0)
			return;
		for (int i = 0; i < activeHandCards; i++) {
			if (!_handCardFolder [i].activeSelf) {
				tmpObject = _handCardFolder [i];
				_handCardFolder [i] = _handCardFolder [i + 1];
				_handCardFolder [i + 1] = tmpObject;
			}
		}
	}

	public bool IsGetCard()
	{
		if (activeHandCards < 10)
			return true;
		else
			return false;
	}

	void Awake()
	{
		AllDisCard ();
	}
}
