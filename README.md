# PPAP_A14_Script

 게임 플레이시 종료 : Alt + F4
 
각 Script 별 기능

Controller
- AnimSpeedController : 애니메이션 속도 조절
- ButtonController : StartScene or EndPanel 버튼 누를 시 Scene 전환
- CinemachineController : Player 생존 유무에 따른 vritual camera들을 관리
- PlayerAnimationController : Player Animation 관리
- TextColorCycler : Text 무지개색으로 변환
- PlasyerController : Player 이동, 마우스, 점프 등 관리
  
ItemData
- ItemData : 아이템 데이터 속성
- ItemObject : 아이템 충돌 처리 (아이템 스텟 플레이어에 적용)
- ItemSpawner : ObjectPool에서 생성해둔 Object들 위치 변경

Manager
- CarObjectPool : Car Object 생성
- EndingSceneManager : EndingScene 실행 후 StartScene으로 이동
- GameManager : score, 게임 over, 게임 reset 관리
- LoadSceneManager : Scene 전환 시 Load용
- NPCUIManager : Npc에게 Pizza Object를 들고있는 상태에서 충돌 했을 때 나오는 UI
- ObjectPool : Item 및 점수 Obejct 생성
- SoundManager : Sound 관리
- UIManager : Score, Item Gauge 표시
- CharacterManager : Player 관리

Player
- interaction : 튜토리얼 영상 상호작용 관리
- Player : CharacterManager 관리용

Spawn&MiniMap
- FindOther : Fake Npc를 찾았을 때
- FindWs : Real Npc를 찾았을 때
- GetPizza : Pizza Object 생성
- Map : World Map [M 키]
- SpawnPizza : Spawn Pizza Object 생성

Vehicle
- Vehicle : Car 이동, 충돌 처리 관리
- VehicleEffect : Sound 관리

## 👨‍🏫 프로젝트소개
<h4>Run pizza Run 게임에서 영감을 받아 제작한 러닝 액션 게임입니다. 
<br>

## ⌛ 제작 기간
<h4>2024.06.03 ~ 204.06.11

## 👨‍👨‍👦 멤버구성
- #### 팀장 : 김민우 - 장애물(자동차) 생성 및 이동 로직(플레이어 추격, 직선 이동 등등) 구현 
- #### 팀원 : 문주원 - 플레이어 이동 및 애니메이션, 카메라 제어 구현
- #### 팀원 : 이경현 - NPC 및 피자 스폰 위치, 미니맵 구현, 게임 종료 씬 구현
- #### 팀원 : 최세은 - 아이템(+ 튜토리얼용 아이템) 구현, 점수 관리 및 게임 시작 · 로드 씬 구현
---
## 🎮 게임소개
- #### 🍍 게임이름 
   <b2> `PPAP : Please PineApple Pizza`
 
- #### 📎 게임설명
  파인애플 피자를 싫어하는 사람들의 위협을 피해, 안전하게 피자를 배달하는 게임입니다.

  플레이어는 맵 전역에 있는 자동차들을 피하며 피자 스폰 영역에서 피자를 획득하고, 획득한 피자를 가짜 우석 매니저님들 사이에 있는 진짜 우석 매니저님에게 전달하는 것이 목적입니다. 

- #### 🕹️ 조작법
  - <h4> WASD : 플레이어를 상하좌우 방향으로 이동시킵니다.
  - <h4> Space : 일정한 높이로 점프를 할 수 있습니다. 
  - <h4> Shift + WASD : 플레이어는 shift 키를 누른 상태로 이동하여 질주할 수 있습니다. 
  - <h4> M : M 버튼을 누르면 미니맵을 펼칠 수 있습니다. 미니맵에서는 피자의 스폰 위치와 피자를 전달해야 하는 가짜 · 진짜 매니저님들의 위치를 확인할 수 있습니다.
  - <h4> Mouse : 마우스를 회전하여 카메라를 통해 다양한 각도로 시점을 회전할 수 있습니다.
  - <h4> E : 튜토리얼 아이템이 감지된 상태에서 E 버튼을 누르면, 해당 아이템에 대응하는 튜토리얼 영상을 확인할 수 있습니다. 

## 🎬 플레이 화면
<details>
<summary><h4>🏃게임 시작 화면</summary>
<div markdown="1">

  ![ezgif-5-b7b792133d](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/e47cd94e-b859-474d-908d-80188dd8c02b)

</div>
</details>

 <details>
<summary><h4>🐤튜토리얼 기능 </summary>
<div markdown="1">

  ![tutorial](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/ce8d581a-6fbf-4654-be0c-f0158774ee1c)

  - 튜토리얼 아이템을 감지한 상태에서 E 버튼을 누르면 해당 아이템에 대한 튜토리얼 영상을 확인할 수 있습니다. 
</div>
</details>

 <details>
<summary><h4>🍕피자 스폰 포인트 </summary>
<div markdown="1">

 ![pizzaspawn](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/9b8cd0f5-33c6-466b-aa59-53e57ebe0125)

  - 플레이어가 보라색 영역에 들어가면 게임에 필요한 피자를 획득할 수 있습니다. 
</div>
</details>

<details>
<summary><h4>🙅‍♂️가짜 우석 매니저님 </summary>
<div markdown="1">

 ![fake](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/3848320b-e5b1-476f-bafe-3df8785b142c)

  - 플레이어는 피자를 획득한 상태에서 맵에 존재하는 우석 매니저님에게 피자를 전달해야 합니다. 그러나 맵에는 우석 매니저님과 똑같은 외관을 한 Fake 매니저님이 세 명 추가로 존재하는 상태입니다.
  
  - 만일 플레이어가 진짜 우석 매니저님이 아닌 Fake 매니저님께 피자를 전달한다면 가지고 있던 피자는 그대로 강탈당하게 됩니다.
  
  - 플레이어는 피자를 획득하기 위해 다시 보라색 피자 스폰 포인트를 찾아가야 하며, 진짜 우석 매니저님에게 전달할 때까지 이 과정을 반복하는 것이 본 게임의 주된 목적입니다.
</div>
</details>

<details>
<summary><h4>🟥이속 증가 아이템 </summary>
<div markdown="1">

![sprint](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/096b33a7-7a24-4fc2-960c-00fca5e59682)

  - 분홍색 보석은 플레이어의 속도에 +5f 연산을 해주는 이속 증가 아이템입니다. 남은 시간에 대한 정보는 게임 하단에 게이지로부터 확인할 수 있습니다. 
</div>
</details>

<details>
<summary><h4>🔷무적 아이템 </summary>
<div markdown="1">

![shield](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/ababfe9b-5082-4caf-b76d-e724899e4405)

  - 푸른색 보석은 플레이어를 일시적으로 무적 상태로 만들어주는 아이템입니다. 무적 상태 동안에는 자동차와 부딪혀도 플레이어는 사망하지 않으며, 남은 시간에 대한 정보는 게임 하단에 게이지로부터 확인할 수 있습니다. 
</div>
</details>

<details>
<summary><h4>🗺️미니맵1 </summary>
<div markdown="1">

![minimap1](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/46277a11-0e43-48c6-a469-d3eb9e3b44ef)

  - 게임 씬의 좌측 하단에는 플레이어를 기준으로 일정 범위에 해당하는 미니맵이 화면에 표시되며, M 버튼을 누르면 전체 맵을 볼 수 있습니다. 보라색 영역에서 피자를 획득하기 전까지는 피자를 획득할 수 있는 위치가 맵에 표시됩니다. 
</div>
</details>

<details>
<summary><h4>🗺️미니맵2 </summary>
<div markdown="1">

![minimap2](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/6d6210b4-5da1-45cf-b8b9-1b0dced1207d)

  - 보라색 영역에서 피자를 얻은 후에는 4명의 우석 매니저님의 위치를 맵에서 확인할 수 있습니다. 
</div>
</details>

<details>
<summary><h4>🚗장애물 충돌 + 💀플레이어 사망</summary>
<div markdown="1">

![playerDie](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/611c947c-c8f7-469b-bbc6-f587331d9f94)

  - 직선, 랜덤 방향, 플레이어 방향으로 나아가는 자동차 장애물과 플레이어가 충돌하게 되면 플레이어는 사망하게 되며 게임은 그대로 종료됩니다.
    
   ![image](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/6efcc020-38ed-4c4a-8704-866b9a07093c)

  - 플레이어가 사망하면서 래그돌 애니메이션이 실행되고, 화면에는 현재 점수와 최고 점수 및 기타 버튼들이 나타납니다. 
</div>
</details>

<details>
<summary><h4>🎊게임 클리어 </summary>
<div markdown="1">

![둥](https://github.com/S014RMoonJuWon/PPAP_A14/assets/103297048/287d2141-3e9c-45d5-8614-efe5fc8ac530)

  - 장애물들을 피하며 아이템의 보조를 받아 우석 매니저님께 파인애플 피자를 무사히 건네주게 되면 추가 점수 100점을 얻고 게임은 종료됩니다. 
</div>
</details>


