using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// ヒーロの体力等の管理
/// </summary>
public class HeroManager : MonoBehaviour,CardInterface {

	[SerializeField]
	private Text _myToughnessText;
	[SerializeField]
	private Image _myArmorImage;
	[SerializeField][Header("どちらの物か")]
	private GameManager.Who _who;

	private int _myToughness;

	public bool isOwn()
	{
		if (_who == GameManager.Who.MY)
			return true;
		else
			return false;
	}

	public bool IsMinion()
	{
		return false;
	}

	/// <summary>
	/// 体力の初期化
	/// </summary>
	private void InitToughness()
	{
		_myToughness = 30;
		_myToughnessText.text = _myToughness.ToString();
	}

	/// <summary>
	/// 体力を1減らす
	/// </summary>
	public void DecreaseToughness()
	{
		_myToughness = Mathf.Max (_myToughness - 1, 0);
		_myToughnessText.text = _myToughness.ToString();
		if (_myToughness == 0) {
			DeadProcess ();
		}
	}

	/// <summary>
	/// 体力を1増やす
	/// </summary>
	public void IncreaseToughness()
	{
		_myToughness++;
		_myToughnessText.text = _myToughness.ToString();
	}

	/// <summary>
	/// アーマーイメージを非表示にする
	/// </summary>
	private void HideArmorImage()
	{
		_myArmorImage.gameObject.SetActive(false);
	}

	private void SetUp()
	{
		InitToughness ();
		HideArmorImage ();
	}

	private void DeadProcess()
	{
		Debug.Log ("あなたは死にました。");
	}

	void Awake()
	{
		SetUp ();
	}

}
