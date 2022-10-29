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

### **수정 사항**
<img src="https://user-images.githubusercontent.com/86781939/198838681-03e75d3a-0348-45af-9742-203dfd970887.PNG"  width="725" height="41" >
 - (해결) 타일 애니메이션이 종료되기 전에 씬이 전환됨 -> 씬 전환 함수를 타일 애니메이션 종료 후에 배치
<br>

## - 사용한 에셋
  - 애니메이션 - DOTween (HOTween v2): (https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676?locale=ko-KR)
  - 스프라이트 - Kenney: (https://www.kenney.nl/assets?q=2d)