using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour {

	[SerializeField][Header("マナテキスト")]
	private Text _manaText;

	//現在溜まっているマナ数
	private int _myMana;
	//このターンの残りマナ数
	private int _myRestMana;

	void Awake()
	{
		_myMana = 0;
		_myRestMana = 0;
		UpdateManaText ();
	}

	/// <summary>
	/// マナを補充し一つ増やす
	/// </summary>
	public void ManaReloadProcess()
	{
		IncreaseMana (1);
		Reload ();
		UpdateManaText ();
	}

	/// <summary>
	/// ゲーム中に増えるマナ
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void IncreaseMana(int amount)
	{
		_myMana = Mathf.Min(10,_myMana +amount);
		UpdateManaText ();
	}

	/// <summary>
	/// 一時的に増えるマナ
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void IncreaseCurrentMana(int amount)
	{
		_myRestMana = Mathf.Min(10, amount);
		UpdateManaText ();
	}

	/// <summary>
	/// ゲーム中のマナを減らす
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void DecreaseMana(int amount)
	{
		_myMana = Mathf.Max (0, _myMana - amount);
		UpdateManaText ();
	}

	/// <summary>
	/// 残っているマナから必要マナ数を消費する
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void DecreaseCurrentMana(int amount)
	{
		if (isUseMana (amount)) {
			_myRestMana -= amount;
		} else {
			Debug.Log ("これ以上消費できないはずだよ" + gameObject.name);
			_myRestMana = 0;
		}
		UpdateManaText ();
	}

	/// <summary>
	/// 指定された量のマナで足りるかどうか
	/// </summary>
	/// <returns><c>true</c>, if use mana was ised, <c>false</c> otherwise.</returns>
	/// <param name="amount">Amount.</param>
	public bool isUseMana(int amount)
	{
		if (amount > _myRestMana)
			return false;
		else
			return true;
	}

	/// <summary>
	/// マナを充填する
	/// </summary>
	public void Reload()
	{
		_myRestMana = _myMana;
	}

	private void UpdateManaText()
	{
		_manaText.text = _myRestMana + "/" + _myMana;
	}
}
