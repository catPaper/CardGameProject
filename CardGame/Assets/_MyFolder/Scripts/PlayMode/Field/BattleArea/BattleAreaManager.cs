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
}
