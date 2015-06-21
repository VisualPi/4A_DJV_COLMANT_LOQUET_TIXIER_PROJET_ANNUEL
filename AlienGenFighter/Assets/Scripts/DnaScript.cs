using System.Linq;
using UnityEngine;

public enum ECharateristic : byte
{
	NbLamb = 0, NbArm, Nbhead, Skincolor, Wings, Height, Sociability, Bravery, Adventurous, Aggressiveness,
	Tolerance, Activity, Charism, Timidity, Influence, Attitude, Curiosity, Perfectionism, Sporty, Ambition,
	Patience, Negociation, Authority, Attention, Kindness, Domination, Suggestibility, Honesty, Subjectivity,
	Instinct, Loyalty, Memory, HeatResistance, DeseaseResistance
};

public class DnaScript
{
	public const string LANG_CODE = "FR"; //TODO : doit etre a part

	private byte[] _genes;
	private const byte Size = 34;

	public DnaScript(byte nbLamb, byte nbArm, byte nbHead, byte skinColor, byte wings, byte height, byte sociability, byte bravery, byte adventurous, byte aggressiveness, byte tolerance, byte activity, byte charism, byte timidity, byte influence, byte attitude, byte curiosity, byte perfectionism, byte sporty, byte ambition, byte patience, byte negociation, byte authority, byte attention, byte kindness, byte domination, byte suggestibility, byte honesty, byte subjectivity, byte instinct, byte loyalty, byte memory, byte heatResistance, byte deseaseResistance)
	{
		_genes = new byte[Size];
		_genes[(int)ECharateristic.NbLamb]				= nbLamb;
		_genes[(int)ECharateristic.NbArm]				= nbArm;
		_genes[(int)ECharateristic.Nbhead]				= nbHead;
		_genes[(int)ECharateristic.Skincolor]			= skinColor;
		_genes[(int)ECharateristic.Wings]				= wings;
		_genes[(int)ECharateristic.Height]				= height;
		_genes[(int)ECharateristic.Sociability]		    = sociability;
		_genes[(int)ECharateristic.Bravery]			    = bravery;
		_genes[(int)ECharateristic.Adventurous]		    = adventurous;
		_genes[(int)ECharateristic.Aggressiveness]		= aggressiveness;
		_genes[(int)ECharateristic.Tolerance]			= tolerance;
		_genes[(int)ECharateristic.Activity]			= activity;
		_genes[(int)ECharateristic.Charism]			    = charism;
		_genes[(int)ECharateristic.Timidity]			= timidity;
		_genes[(int)ECharateristic.Influence]			= influence;
		_genes[(int)ECharateristic.Attitude]			= attitude;
		_genes[(int)ECharateristic.Curiosity]			= curiosity;
		_genes[(int)ECharateristic.Perfectionism]		= perfectionism;
		_genes[(int)ECharateristic.Sporty]				= sporty;
		_genes[(int)ECharateristic.Ambition]			= ambition;
		_genes[(int)ECharateristic.Patience]			= patience;
		_genes[(int)ECharateristic.Negociation]		    = negociation;
		_genes[(int)ECharateristic.Authority]			= authority;
		_genes[(int)ECharateristic.Attention]			= attention;
		_genes[(int)ECharateristic.Kindness]			= kindness;
		_genes[(int)ECharateristic.Domination]			= domination;
		_genes[(int)ECharateristic.Suggestibility]		= suggestibility;
		_genes[(int)ECharateristic.Honesty]			    = honesty;
		_genes[(int)ECharateristic.Subjectivity]		= subjectivity;
		_genes[(int)ECharateristic.Instinct]			= instinct;
		_genes[(int)ECharateristic.Loyalty]			    = loyalty;
        _genes[(int)ECharateristic.Memory]              = memory;
        _genes[(int)ECharateristic.HeatResistance]		= heatResistance;
		_genes[(int)ECharateristic.DeseaseResistance]	= deseaseResistance;
	}
	public DnaScript(DnaScript defaultDna)
	{
		_genes = defaultDna._genes;
	}
	public DnaScript(byte[] genes)
	{
		_genes = genes;
	}
	public DnaScript()
	{
		_genes = new byte[Size]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	}
	public byte[] GetGenotype()
	{
		return _genes;
	}
	public byte GetGeneAt(ECharateristic cara)
	{
		return _genes[(byte) cara];
	}
	public byte GetGeneAt(byte index)
	{
		return _genes[index];
	}
	public void SetGeneAt(ECharateristic cara, byte value)
	{
		_genes[(byte) cara] = value;
	}
	public void SetGeneAt(byte index, byte value)
	{
		_genes[index] = value;
	}
	public static DnaScript operator *(DnaScript gene, DnaScript gene2)
	{
		DnaScript dnaRet = new DnaScript();
		for (byte i = 0; i < Size; ++i)
			dnaRet.SetGeneAt(i, (byte)Random.Range(gene.GetGeneAt(i), gene2.GetGeneAt(i)));

		return dnaRet;
	}
	public string ToCode()
	{
		return _genes.Aggregate("", (current, g) => current + g);
	}
	public override string ToString() //la fonction servira probablement jamais !
	{
		string sRet;
		if (LANG_CODE.Equals("FR")) //TODO : a voir si on le gere ou pas
			sRet = string.Format("Nombre de jambes : {0} Nombre de bras : {1} Nombre de tete : {2} Couleur de Peau : {3} " +
			                     "Ailes : {4} Taille : {5} Sociabilité : {6} Bravoure : {7} Aventure : {8} Aggressivié : {9} " +
			                     "Tolérance : {10} Activité : {11} Charisme : {12} Timidité : {13} Influence : {14} " +
			                     "Attitude : {15} Curiosité : {16} Perfectionnisme : {17} Sportif : {18} Ambition : {19} " +
			                     "Patience : {20} Negociation : {21} Autorité : {22} Attention : {23} Gentillesse : {24} " +
			                     "Domination : {25} Influencabilité : {26} Honneteté : {27} Partialité : {28} Instinct : {29} " +
								 "Loyauté : {30} Memoire : {31} Resistance au chaud : {32} Resistance a la maladie : {33}"
								 , _genes[0], _genes[1], _genes[2], _genes[3], _genes[4], _genes[5], _genes[6], _genes[7]
								 , _genes[8], _genes[9], _genes[10], _genes[11], _genes[12], _genes[13], _genes[14]
								 , _genes[15], _genes[16], _genes[17], _genes[18], _genes[19], _genes[20], _genes[21]
								 , _genes[22], _genes[23], _genes[24], _genes[25], _genes[26], _genes[27], _genes[28]
								 , _genes[29], _genes[30], _genes[31], _genes[32], _genes[33]);
		else
			sRet = string.Format("Number of lambs : {0} Number of arms : {1} Number of head : {2} Skin color : {3} " +
									 "Wings : {4} Height : {5} Sociability : {6} Bravery : {7} Adventure : {8} Aggressiveness : {9} " +
									 "Tolerance : {10} Activity : {11} Charism : {12} Timidity : {13} Influence : {14} " +
									 "Attitude : {15} Curiosity : {16} Perfectionism : {17} Sporty : {18} Ambition : {19} " +
									 "Patience : {20} Negociation : {21} Authority : {22} Attention : {23} Kindness : {24} " +
									 "Domination : {25} Suggestibility : {26} Honesty : {27} Subjectivity : {28} Instinct : {29} " +
									 "Loyalty : {30} Memory : {31} Heat resistance : {32} Desease resistance : {33}"
								 , _genes[0], _genes[1], _genes[2], _genes[3], _genes[4], _genes[5], _genes[6], _genes[7]
								 , _genes[8], _genes[9], _genes[10], _genes[11], _genes[12], _genes[13], _genes[14]
								 , _genes[15], _genes[16], _genes[17], _genes[18], _genes[19], _genes[20], _genes[21]
								 , _genes[22], _genes[23], _genes[24], _genes[25], _genes[26], _genes[27], _genes[28]
								 , _genes[29], _genes[30], _genes[31], _genes[32], _genes[33]);
		return sRet;
	}
}
