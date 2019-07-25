using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLerpMove : MonoBehaviour
{
    public GameObject marker;    //마커
    public float moveSpeed = 3f; //이동속도

    private float elapsedTime = 0f;//타이머용 변수
    private float arriveTime = 0f; //도착시간
    private Vector3 startPos;      //이동시작위치
    private Vector3 endPos;        //이동목표위치

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 위치에서 Ray정보 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //충돌정보 저장
            RaycastHit hit;

            //레이캐스트 발사
            if(Physics.Raycast(ray,out hit))
            {
                //Ray와 부딪힌 위치를 마커위치로 설정
                Vector3 markerPos = hit.point;
                //높이는 마커의 높이 값으로 보정
                markerPos.y = marker.transform.position.y;
                //마커위치 설정
                marker.transform.position = markerPos;
                //마커 켜기
                marker.SetActive(true);

                //위치보정
                markerPos.y = transform.position.y;

                //도착시간 구하기(초단위)
                //Distance(x,y) = 두지점사이 거리(벡터 길이) 구해줌(y-x)
                arriveTime = Vector3.Distance(transform.position , markerPos)/moveSpeed;//시간 = 거리/속력

                //시작위치 저장
                startPos = transform.position;

                //목표위치 저장
                endPos = markerPos;
                //타이머 초기화
                elapsedTime = 0f;
            }
        }

        if (marker.activeSelf)
        {
            Move(arriveTime);
        }



    }


    void Move(float arriveTime)
    {
        elapsedTime += Time.deltaTime;
        transform.position =Vector3.Lerp(startPos, endPos, elapsedTime / arriveTime);

        //도착확인
        if (Vector3.Distance(transform.position, endPos) == 0f)
        {
            marker.SetActive(false);
        }
    }
}
