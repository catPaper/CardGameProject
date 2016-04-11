using UnityEngine;
using System.Collections;

public interface CardInterface  {

	/// <summary>
	/// 自分が所有するゲームオブジェクトかどうか
	/// </summary>
	/// <returns><c>true</c> if this instance is own; otherwise, <c>false</c>.</returns>
	bool isOwn();

	/// <summary>
	/// 対象がミニオンかどうか
	/// </summary>
	/// <returns><c>true</c> if this instance is minion; otherwise, <c>false</c>.</returns>
	bool IsMinion();

}
