using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    사운드 제어 스크립트
*/
public class SoundManager : MonoBehaviour
{   
    // BGM 슬라이더 등록될 변수
    public Slider bgmSoundSlider;
    // EFFECT 슬라이더 등록될 변수
    public Slider effectSoundSlider;

    // 배경음악 음소거 토글버튼
    public Toggle muteBgmToggle;
    // 효과음 음소거 토글버튼
    public Toggle muteEffectToggle;

    // 이전 사운드값 저장변수
    private float prevSoundValue;


    /*
        배경음악 음소거 이벤트
    */
    public void OnMuteBgmCheck(bool isOn) {
        // SoundToggle(음소거)이 체크되었을 때
        if(isOn) {
            prevSoundValue = bgmSoundSlider.value;
            bgmSoundSlider.value = 0;

        } else {    
            // 이전 음량값이 0이 아닐때만 음소거 취소시 이전의 저장된 값으로 변경
            if(prevSoundValue != 0) {
                bgmSoundSlider.value = prevSoundValue;
            }
        }
    }

    /*
        효과음 음소거 이벤트
    */
    public void OnMuteEffectCheck(bool isOn) {
        if (isOn) {
            prevSoundValue = effectSoundSlider.value;
            effectSoundSlider.value = 0;

        } else {
            // 이전 음량값이 0이 아닐때만 음소거 취소시 이전의 저장된 값으로 변경
            if (prevSoundValue != 0) {
                effectSoundSlider.value = prevSoundValue;
            }
        }
    }

    /*
        사운드(배경음악) 확인 후 음소거 활성화 이벤트
    */
    public void OnMuteBgm() {
        // 배경음악 음량이 0이 되고, 음소거 상태가 아닐 때 > 음소거 상태로 변경
        if(bgmSoundSlider.value == 0 && !muteBgmToggle.isOn) {
            muteBgmToggle.isOn = true;
        } 
        // 음소거 상태에서 음량 조절했을 때
        else if (bgmSoundSlider.value != 0 && muteBgmToggle.isOn) {
            muteBgmToggle.isOn = false;
        }
    }

    /*
        사운드(효과음) 확인 후 음소거 활성화 이벤트
    */
    public void OnMuteEffect() {
        // 효과음의 음량이 0이 되고, 음소거 상태가 아닐 때 > 음소거 상태로 변경
        if (effectSoundSlider.value == 0 && !muteEffectToggle.isOn) {
            muteEffectToggle.isOn = true;
        }
        // 음소거 상태에서 음량 조절했을 때
        else if (effectSoundSlider.value != 0 && muteEffectToggle.isOn) {
            muteEffectToggle.isOn = false;
        }
    }

}
