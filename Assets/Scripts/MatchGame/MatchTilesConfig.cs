using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchTilesConfig", menuName = "Configs/MatchTilesConfig")]
public class MatchTilesConfig : ScriptableObject
{
    [SerializeField]
    private List<MatchTileData> _matchTiles;

    public List<MatchTileData> Collectables { get { return _matchTiles; } }
}

[Serializable]
public class MatchTileData
{
    [SerializeField]
    private int _id;
    public int Id => _id;

    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;
}
