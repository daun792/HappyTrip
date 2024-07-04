using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapManager : Manager
{
    [SerializeField] Transform cameraTrans;
    [SerializeField] Transform playerTrans;
    [SerializeField] Transform[] stageStartPosition;
    [SerializeField] int timeLimit = 30;

    private int stageIndex = 1;
    private bool isUseDrug = false;
    private IEnumerator timeCheckRoutine;
    private CharacterBase player;

    private void Start()
    {
        cameraTrans.position = new Vector3(0f, 10f, -10f);
        cameraTrans.DOMoveY(0f, 1f).SetEase(Ease.OutCubic);
        player = playerTrans.GetComponent<CharacterBase>();
        timeCheckRoutine = CheckTime();
        StartCoroutine(timeCheckRoutine);
    }

    public void LoadNextStage()
    {
        if (stageIndex > 6) //TODO:
        {
            StartCoroutine(EndGame());

            return;
        }

        Base.Manager.UI.FadeInOut(InitStage);
    }

    public void ReloadStage()
    {
        if (stageIndex > 6) //TODO:
        {
            StartCoroutine(EndGame());
            return;
        }

        Base.Manager.UI.FadeInOut(ResetStage);
    }

    public void UseDrug()
    {
        isUseDrug = true;
    }

    private void InitStage()
    {
        if (isUseDrug)
        {
            SetDrugEffect();
        }

        MoveCamera();
        MovePlayer();

        stageIndex++;
        StopCoroutine(timeCheckRoutine);
        timeCheckRoutine = CheckTime();
        StartCoroutine(timeCheckRoutine);
        isUseDrug = false;
    }

    private void ResetStage()
    {
        stageIndex--;
        isUseDrug = false;
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

    private void SetDrugEffect()
    {
        switch (stageIndex)
        {
            case 1:
                Base.Manager.PostProcessing.SetSaturation(-30f);
                break;
            case 2:
                ModifyPlayerSpeed(0.8f);
                break;
            case 3:
                timeLimit = 18;
                break;
            case 4:
                Base.Manager.PostProcessing.SetLensDistortion();
                break;
            case 6:
                Base.Manager.PostProcessing.SetFlashBack();
                break;
        }
    }

    private void ModifyPlayerSpeed(float _value)
    {
        float inv = 1f / _value;
        player.MovementSpeed = _value;
        player.JumpVelocity = inv;
        player.GravityScale = inv * inv;
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
