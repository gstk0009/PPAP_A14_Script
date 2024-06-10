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
