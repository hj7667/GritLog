# GritLog

운동 습관을 매일 체크하고 연속 기록(스트릭)을 추적하는 습관 트래커입니다.
ASP.NET Core MVC + EF Core + Ajax 실무 패턴 학습을 목적으로 만들었습니다.

## 개요

| 항목 | 내용 |
|---|---|
| 프로젝트명 | GritLog (운동 습관 트래커) |
| 목적 | ASP.NET MVC + DB + Ajax 실무 패턴 학습 |
| 대상 | 개인용 (로그인 없이 단일 사용자 가정, 추후 확장 가능) |
| 기술 스택 | ASP.NET Core MVC, EF Core, MS SQL, jQuery/Ajax, Chart.js |

## 주요 기능

### 습관 관리 (CRUD)
- 운동 습관 등록 (예: "런닝 30분", "스쿼트 50개", "플랭크 3분")
- 습관 목록 조회 / 수정 / 삭제 (삭제 시 연관 기록은 함께 삭제 또는 비활성화 처리)

### 오늘 체크 (Ajax)
- 습관 목록 옆 체크박스로 완료 여부 체크/해제
- 페이지 새로고침 없이 서버에 즉시 반영, 화면(연속일수·잔디밭 등) 즉시 갱신

### 연속일수 (스트릭)
- 습관별 현재 연속일수 계산, 하루라도 빠지면 리셋
- 최장 연속기록 별도 저장/표시

### 잔디밭 (GitHub 스타일 히트맵)
- 최근 1년(또는 6개월) 요일×주차 격자
- 완료 습관 개수에 따라 색상 진하기 4단계로 표시
- 칸에 마우스 오버 시 날짜/완료 목록 툴팁 (선택 기능)

### 통계 대시보드
- 오늘 완료/전체 습관 개수, 이번달 달성률(%)
- 습관별 달성률 비교 차트 (Chart.js)

## 데이터 모델

```
Habit (습관)
├── Id (PK, int)
├── Name (string)
├── CreatedDate (DateTime)
├── IsActive (bool)

HabitLog (체크 기록)
├── Id (PK, int)
├── HabitId (FK → Habit.Id)
├── Date (DateOnly/DateTime)
├── IsCompleted (bool)

관계: Habit 1 : N HabitLog
```

## 화면 구성

| 화면 | 경로 | 설명 |
|---|---|---|
| 대시보드 | `/` , `/Habit/Index` | 오늘의 체크리스트 + 잔디밭 + 통계 카드 |
| 습관 등록/수정 | `/Habit/Create`, `/Habit/Edit/{id}` | 모달 형태 폼 |
| 습관 목록 관리 | `/Habit/Manage` | 전체 목록, 수정/삭제 |
| 통계 상세 | `/Habit/Stats` | 습관별 달성률 차트 |

## API / Controller

| 메서드 | 경로 | 기능 | 응답 |
|---|---|---|---|
| GET | `/Habit/Index` | 대시보드 화면 | View |
| POST | `/Habit/Create` | 습관 등록 | Redirect |
| POST | `/Habit/Delete/{id}` | 습관 삭제 | Redirect |
| POST | `/Habit/ToggleCheck` | 오늘 체크 토글 (Ajax) | JSON `{ success, streak }` |
| GET | `/Habit/GetHeatmapData` | 잔디밭 데이터 (Ajax) | JSON `[{ date, count }]` |
| GET | `/Habit/GetStats` | 통계 데이터 (Ajax) | JSON `{ habitName, rate }[]` |

## 개발 로드맵

- [x] **Phase 1: 기본 골격** — 프로젝트 생성, EF Core 구성, 모델 정의, Migration
- [ ] **Phase 2: CRUD** — 습관 등록/목록/수정/삭제, 기본 레이아웃
- [ ] **Phase 3: Ajax 체크 기능** — 체크 토글 API, 연속일수 계산 로직
- [ ] **Phase 4: 잔디밭 + 통계** — 날짜별 집계 API, 히트맵 View, Chart.js 연동
- [ ] **Phase 5: 다듬기** — 유효성 검사, UI 스타일링, 예외 처리

## 향후 확장

- 로그인/사용자별 데이터 분리
- 습관별 목표 설정 (주 3회 등)
- 알림/리마인더

## 시작하기

```bash
dotnet restore
dotnet ef database update
dotnet run
```