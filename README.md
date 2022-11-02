# 3Match Puzzle Game 2

### **1. 게임 진행**
  - 이웃한 타일끼리만 위치 변경 가능
  - 동일한 타일 3개 이상 연결 시 연결된 타일을 없애고 보스에게 데미지
  - 모든 이동 횟수 사용 시 게임 종료 (메인 씬으로 이동)
<br>

### **2. 상점**
  - 스테이지 종료 시 골드 획득
  - 획득한 골드로 아이템 구매 가능
  - 아이템 구매 시 효과 적용
<br>

---------------------------------

### **# 개발 일지**
<img src="https://user-images.githubusercontent.com/86781939/198838681-03e75d3a-0348-45af-9742-203dfd970887.PNG"  width="725" height="41" >

 - ~~애니메이션이 종료되기 전에 씬이 전환됨 -> 씬 전환 메소드를 타일 애니메이션 메소드 이후에 배치~~ (재수정)
 - **2022.10.31** 상점 Scene 및 Coin 변수 추가 (Stage 클리어 시 Coin 획득), UI Canvas 해상도 조정
 - **2022.11.01** Stage 종료 후 보상 UI 추가, 오류 수정
    - 타일 Swap, Pop 도중 타일 선택 불가능하게 수정
    - 애니메이션이 종료되기 전에 씬 전환시 오류 발생 -> Invoke 이용해 애니메이션 도중이면 1초 후 함수 다시 호출
    - 이동 가능 횟수 소진 이후 타일 이동 가능 -> 이동 횟수 소진 시 Tile Select 비활성화
 - **2022.11.02** Shop UI 추가, 아이템 추가
    - 아이템 구매 시 이동 가능 횟수 +1
 - **2022.11.03** 체력 UI 추가, 아이템 추가
    - fillAmount 이용해 남은 체력 이미지로 표시
    - 아이템 구매 시 데미지 증가


<br>

## - 사용한 에셋
  - 애니메이션 - DOTween (HOTween v2): (https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676?locale=ko-KR)
  - 스프라이트 - Kenney: (https://www.kenney.nl/assets?q=2d)