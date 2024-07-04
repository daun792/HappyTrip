using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapManager : Manager
{
    [SerializeField] Transform cameraTrans;
    [SerializeField] Transform playerTrans;
    [SerializeField] Transform[] stageStartPosition;
    [SerializeField] int timeLimit = 20;

    private int stageIndex = 1;
    private bool isUseDrug = false;

    private void Start()
    {
        cameraTrans.position = new Vector3(0f, 10f, -10f);
        cameraTrans.DOMoveY(0f, 1f).SetEase(Ease.OutCubic);
        StartCoroutine(CheckTime());
    }

    public void LoadNextStage()
    {
        if (stageIndex > 3) //TODO:
        {
            StartCoroutine(EndGame());

            return;
        }

        Base.Manager.UI.FadeInOut(InitStage);
    }

    public void ReloadStage()
    {
        if (stageIndex > 3) //TODO:
        {
            StartCoroutine(EndGame());
            return;
        }

        Base.Manager.UI.FadeInOut(ResetStage);
    }

    private void InitStage()
    {
        MoveCamera();
        MovePlayer();

        stageIndex++;
        StopCoroutine("CheckTime");
        StartCoroutine(CheckTime());
    }

    private void ResetStage()
    {
        stageIndex--;
        InitStage();
    }

    private void MoveCamera()
    {
        var xPos = 30 * stageIndex;
        cameraTrans.position = new Vector3(xPos, 0f, -10f);
    }

    private void MovePlayer()
    {
        playerTrans.position = stageStartPosition[stageIndex].position;
    }

    private IEnumerator EndGame()
    {
        Base.Manager.UI.FadeIn();

        yield return new WaitForSeconds(1f);

        Base.LoadScene(SceneName.Title);
    }

    #region Time
    private IEnumerator CheckTime()
    {
        float time = timeLimit;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            Base.Manager.UI.SetTime(time / timeLimit);
            yield return null;
        }

        Base.Manager.UI.FadeInOut(ResetStage);
    }
    #endregion
}
