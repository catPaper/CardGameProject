using UnityEngine;
using System.Collections;
using UnityEngine.UI;



/// <summary>
/// カードの情報
/// </summary>
public class CardInformation : MonoBehaviour {
	

	[SerializeField][Header("カードID")]
	private string _cardID;
	[SerializeField][Header("カードネーム")]
	private string _cardName;
	[SerializeField][Header("コスト")]
	private int _cardCost;
	[SerializeField][Header("攻撃力")]
	private int _atackPoint;
	[SerializeField][Header("体力")]
	private int _hitPoint;
	[SerializeField][Header("カードイメージ")]
	private Sprite _cardImage;
	[SerializeField][Header("プレイ時イメージ")]
	private Sprite _playImage;
	[SerializeField][Header("カード説明")][Multiline]
	private string _cardExplain;

	public enum CardType{
		CREATURE,
		MAGIC,
		EQUIPMENT
	}

	public enum HeroType{
		NORMAL,
		MAGE,
		HUNTER,
		PALADIN,
		WARRIOR,
		DRUID,
		WARLOCK,
		SHAMAN,
		PRIEST,
		ROGUE,
	}

	public struct HeroColor
	{
		public Color NORMAL{ get; private set;}
		public Color MAGE{ get; private set;}
		public Color HUNTER{ get; private set;}
		public Color PALADIN{ get; private set;}
		public Color WARRIOR{ get; private set;}
		public Color DRUID{ get; private set;}
		public Color WARLOCK{ get; private set;}
		public Color SHAMAN{ get; private set;}
		public Color PRIEST{ get; private set;}

		public HeroColor(Color nrml,Color mg,Color hntr,Color pldn,Color wrrr,Color drd,Color wrlck,Color shmn,Color prst)
		{
			NORMAL = nrml;
			MAGE = mg;
			HUNTER = hntr;
			PALADIN = pldn;
			WARRIOR = wrrr;
			DRUID = drd;
			WARLOCK = wrlck;
			SHAMAN = shmn;
			PRIEST = prst;
		}
	}

	public HeroColor heroColor = new HeroColor(
		new Color(0.8f, 0.6f, 0.6f),
		new Color(0, 0, 1),
		new Color(0, 1, 0),
		new Color(1, 1, 0),
		new Color(1, 0, 0),
		new Color(0.5f, 0.25f, 0),
		new Color(1, 0, 1),
		new Color(0, 0.2f, 0.7f),
		new Color(0, 0, 0));

	[SerializeField][Header("対象クラス")]
	private HeroType _heroType;
	[SerializeField][Header("カードタイプ")]
	private CardType _cardType;

	public string CardID()
	{
		return _cardID;
	}

	public string CardName()
	{
		return _cardName;
	}

	public int CardCost()
	{
		return _cardCost;
	}

	public int AtackPoint()
	{
		return _atackPoint;
	}

	public int HitPoint()
	{
		return _hitPoint;
	}

	public Sprite CardImage()
	{
		return _cardImage;
	}

	public Sprite PlayImage()
	{
		return _playImage;
	}

	public string HeroType_String()
	{
		return _heroType.ToString();
	}

	public string CardType_String()
	{
		return _cardType.ToString();
	}

	public string CardExplain()
	{
		return _cardExplain;
	}

	public Color GetHeroColor()
	{
		string heroName = _heroType.ToString ();

		if (heroName.CompareTo (HeroType.NORMAL.ToString ()) == 0) {
			return heroColor.NORMAL;
		} else if (heroName.CompareTo (HeroType.MAGE.ToString ()) == 0) {
			return heroColor.MAGE;
		} else if (heroName.CompareTo (HeroType.HUNTER.ToString ()) == 0) {
			return heroColor.HUNTER;
		} else if (heroName.CompareTo (HeroType.PALADIN.ToString ()) == 0) {
			return heroColor.PALADIN;
		} else if (heroName.CompareTo (HeroType.WARRIOR.ToString ()) == 0) {
			return heroColor.WARRIOR;
		} else if (heroName.CompareTo (HeroType.DRUID.ToString ()) == 0) {
			return heroColor.DRUID;
		} else if (heroName.CompareTo (HeroType.WARLOCK.ToString ()) == 0) {
			return heroColor.WARLOCK;
		} else if (heroName.CompareTo (HeroType.SHAMAN.ToString ()) == 0) {
			return heroColor.SHAMAN;
		} else {	//**PRIEST**
			return heroColor.PRIEST;
		}
	}
		
}
