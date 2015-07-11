using UnityEngine;
using System.Collections;
using Assets.Scripts.GUI;
using UnityEngine.Events;

public class TimerEvent : MonoBehaviour {
    [SerializeField]
    private Menu _menu;
    [SerializeField]
    private float _timeBeforAnimation = 1f;
    private Coroutine _timerCoroutine;
    private bool _isRunningCoroutine = false;

    public UnityEvent ActionEvent;

    public void StartTimer() {
        _menu.IsOpen = true;
        _timerCoroutine = StartCoroutine(WantExit());
    }

    public void StartTimer(float timeBeforAnimation) {
        _timeBeforAnimation = timeBeforAnimation;
        _menu.IsOpen = true;
        _timerCoroutine = StartCoroutine(WantExit());
    }

    public void StopTimer() {
        Debug.Log("StopTimer");
        _isRunningCoroutine = false;
        StopCoroutine(_timerCoroutine);
    }

    public void RestartTimer() {
        Debug.Log("RestartTimer");
        if (_isRunningCoroutine) {
            _isRunningCoroutine = !_isRunningCoroutine;
            StopCoroutine(_timerCoroutine);
        }

        if (!_menu.IsOpen) {
            Debug.Log("ok");
            _menu.IsOpen = true;
            _menu.IsOpen = false;
            _timerCoroutine = StartCoroutine(Exit());
        } else {
            _timerCoroutine = StartCoroutine(WantExit());
        }
    }

    IEnumerator WantExit() {
        _isRunningCoroutine = true;
        Debug.Log("WantExit");
        yield return new WaitForSeconds(_timeBeforAnimation);
        _menu.IsOpen = false;
        _timerCoroutine = StartCoroutine(Exit());
        _isRunningCoroutine = false;
    }

    IEnumerator Exit() {
        _isRunningCoroutine = true;
        Debug.Log("Want destroy");
        yield return new WaitForSeconds(5f); // TODO Amau : Get Animation Time
        ActionEvent.Invoke();
        Destroy(gameObject);
        Debug.Log("destroy");
        _isRunningCoroutine = false;        ;
    }
}
