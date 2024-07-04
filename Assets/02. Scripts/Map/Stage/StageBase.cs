using System;
using UnityEngine;

public abstract class StageBase : MonoBehaviour
{
    [Serializable]
    struct StageComposition
    {
        public GameObject stageParent;
        public Transform startPosition;
        public Transform disappearTileParent;
    }

    [SerializeField] StageComposition cleanStage;
    [SerializeField] StageComposition drugStage;

    protected bool isClean = true;
    private StageComposition currStage;

    public int StageIndex { get; protected set; }

    private void Awake()
    {
        cleanStage.stageParent.SetActive(false);
        drugStage.stageParent.SetActive(false);
    }

    public virtual void SetStage(bool _isClean)
    {
        isClean = _isClean;
        currStage = _isClean ? cleanStage : drugStage;

        currStage.stageParent.SetActive(true);
    }

    public Vector3 GetStartPosition() => isClean switch
    {
        true => cleanStage.startPosition.position,
        false => drugStage.startPosition.position
    };

    public virtual void ResetStage()
    {
        if (currStage.disappearTileParent != null)
        {
            var disappearTiles = currStage.disappearTileParent.GetComponentsInChildren<SpriteRenderer>(true);
            var fullAlpha = new Color(0f, 0f, 0f, 1f);

            foreach (var tile in disappearTiles)
            {
                tile.gameObject.SetActive(true);
                tile.color += fullAlpha;
            }
        }
    }

    public abstract void UseDrug();
}