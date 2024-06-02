# Unity_3D_SpartaDungeonExploration
스파르타코딩클럽 - [스파르타 던전 탐험] 3D 프로젝트
# 클래스 설계
![image](https://github.com/ss-zun/Unity_3D_SpartaDungeonExploration/assets/66246253/b0d4a368-eacb-4538-bc47-7b57dc23eaa0)


# 시연 영상
https://youtu.be/4n8typ7aLtU

# 구현된 사항들
## 필수요구사항
    1. 기본 이동 및 점프 `Input System`, `Rigidbody ForceMode`
        - 플레이어의 이동(WASD), 점프(Space) 등을 설정
    2. 체력바 UI `UI`
        - UI 캔버스에 체력바를 추가하고 플레이어의 체력을 나타내도록 설정. 플레이어의 체력이 변할 때마다 UI 갱신.
    3. 동적 환경 조사 `Raycast` `UI`
        - Raycast를 통해 플레이어가 조사하는 오브젝트의 정보를 UI에 표시.
        - 예) 플레이어가 바라보는 오브젝트의 이름, 설명 등을 화면에 표시.
    4. 점프대 `Rigidbody ForceMode`
        - 캐릭터가 밟을 때 위로 높이 튀어 오르는 점프대 구현
        - OnCollisionEnter 트리거를 사용해 캐릭터가 점프대에 닿았을 때 ForceMode.Impulse를 사용해 순간적인 힘을 가함.
    5. 아이템 데이터 `ScriptableObject`
        - 다양한 아이템 데이터를 `ScriptableObject`로 정의. 각 아이템의 이름, 설명, 속성 등을 `ScriptableObject`로 관리
    6. 아이템 사용 `Coroutine`
        - 특정 아이템 사용 후 효과가 일정 시간 동안 지속되는 시스템 구현
        - 아이템 사용 후 일정 시간 동안 스피드 부스트.

        
## 선택요구사항
    1. 추가 UI
        - 점프나 대쉬 등 특정 행동 시 소모되는 스태미나를 표시하는 바 구현
    2. 3인칭 시점
        - 3인칭 카메라 시점을 설정하고 플레이어를 따라다니도록 설정
    3. 움직이는 플랫폼 구현
        - 시간에 따라 정해진 구역을 움직이는 발판 구현
        - 플레이어가 발판 위에서 이동할 때 자연스럽게 따라가도록 설정
    4. 벽 타기 및 매달리기
        - 캐릭터가 벽에 붙어 타고 오르거나 매달릴 수 있는 시스템 구현.
    5. 다양한 아이템 구현
        - 스피드 부스트(Speed Boost): 플레이어의 이동 속도를 일정 시간 동안 증가시킴.

