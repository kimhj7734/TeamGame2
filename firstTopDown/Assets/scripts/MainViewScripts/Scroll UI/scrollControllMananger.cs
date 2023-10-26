using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollControllMananger : MonoBehaviour
{
    public RectTransform List;
    public int count;
    
    /* X, Y 값 담을 변수 */
    private float pos;
    private float posY;
    // private float limitPos;

    /* 오른쪽/왼쪽 이동 시 위치 담을 변수 */
    private float movePos;
    private float movePosY;

    private float moveLimitPos = 1376.25f;
    
    private bool isScroll = false;

    /* X, Y 값 화면 표시하기 위한 값 계산 */
    void Start() {
        pos = List.localPosition.x;
        posY = List.localPosition.y;
        movePos = List.rect.xMax - List.rect.xMax / count;
        movePosY = List.rect.yMax - List.rect.yMax / count;
    }

    // 오른쪽 이동 ( X 축은 위치변함없이 잘 되는데, Y 축이 자꾸 변해서, Y 축 계산도 넣었음.. )
    public void Right() {
        // List.rect.xMax + List.rect.xMax --> List.rect.width
        if (List.rect.xMax + List.rect.xMax / count == movePos) {
            isScroll = false;
        } else {
            isScroll = true;
            movePos = pos - List.rect.width / count;
            pos = movePos;

            if (pos < moveLimitPos) {
            // pos 의 값이 특정 값을 넘어갔을 때, 버튼 비활성화

            }

            movePosY = posY - List.rect.yMax / count;
            posY = movePosY;
            StartCoroutine(Scroll());   // 코루틴 실행
        }
    }

    // 왼쪽 이동 ( X 축은 위치변함없이 잘 되는데, Y 축이 자꾸 변해서, Y 축 계산도 넣었음.. )
    public void Left() {
        if (List.rect.xMax + List.rect.xMax / count == movePos) {
            isScroll = false;
        } else {
            // Debug.Log(List.rect.xMax + List.rect.xMax); // 2195
            isScroll = true;
            movePos = pos + List.rect.width / count;
            pos = movePos;
            
            // if (pos >= moveLimitPos) {
            //     isScroll = false;
            // }

            movePosY = posY - List.rect.yMax / count;
            posY = movePosY;
            StartCoroutine(Scroll());
        }
    }

    // 거리 계산 ( Y 축이 자꾸 변해서 고정하려고 넣었음(movePosY) )
    IEnumerator Scroll() {
        while (isScroll) {
            List.localPosition = 
            Vector2.Lerp(List.localPosition, new Vector2(movePos, movePosY), Time.deltaTime * 5);
            if (Vector2.Distance(List.localPosition, new Vector2(movePos, movePosY)) < 0.1f) {
                isScroll = false;
            }
            yield return null;
        }
    }
}
