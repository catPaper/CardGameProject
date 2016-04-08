using UnityEngine;
using System.Collections;

public class DeckInformation : MonoBehaviour {

	[SerializeField]
	private CardInformation[] _cards = new CardInformation[30];

	public CardInformation[] Cards()
	{
		return _cards;
	}
}
