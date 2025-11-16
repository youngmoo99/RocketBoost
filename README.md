# Rocket Boost

중력을 거스르며 로켓을 조종해 착륙 지점까지 도달하는 물리 기반 3D 플라이트 퍼즐 게임
A physics-based 3D rocket control game — balance thrust, rotation, and timing to reach the landing pad safely!

<p align="center"> <a href="#demo">🎮 플레이 영상</a> • <a href="#features">✨ 주요 특징</a> • <a href="#tech-stack">🧰 기술 스택</a> • <a href="#setup">⚙️ 설치/실행</a> • <a href="#screenshots">🖼️ 스크린샷</a> </p> <p> <img alt="Unity" src="https://img.shields.io/badge/Unity-6.0-black?logo=unity"/> <img alt="Platform" src="https://img.shields.io/badge/Platform-Windows%20%7C%20macOS-blue"/> </p>
## TL;DR

* **장르**: 3D Physics / Arcade Puzzle
* **엔진**: Unity 6.0
* **역할(Role)**: 기획 100%, 프로그래밍 100% 
* **플레이 루프**: 로켓 추진(Thrust) → 회전 조작 → 장애물 회피 → 착륙 성공 시 다음 레벨 이동

<h2 id="demo">🎮 플레이 영상</h2>

▶️ Gameplay Video: [플레이 영상](https://youtu.be/pnmQwe5bvPU)

스페이스바로 로켓의 엔진을 점화하고, 좌우 회전으로 균형을 잡아 착륙 지점까지 도달합니다.
충돌 시 폭발 이펙트와 사운드가 발생하며 2초 후 재시작됩니다.

<h2 id="features">✨ 주요 특징 / Features</h2>

* 🪐 **물리 기반 조종 시스템** — Rigidbody를 활용한 현실감 있는 추진력/회전 구현

* 🔥 **엔진 연출** — 추진 방향에 따라 파티클 및 사운드 동기화

* 💥 **충돌 감지 및 씬 전환** — 태그(Finish, Friendly 등)에 따라 성공/실패 처리

* 🧪 **디버그 모드 지원** — L키로 다음 씬, C키로 충돌 판정 토글

* 🚀 **씬 루프 구조** — 마지막 스테이지 도달 시 자동으로 첫 씬으로 회귀

* 🌀 **Oscillator** — 왕복 이동하는 플랫폼 구현(Mathf.PingPong 기반)

* ⎋ **ESC 종료 기능** — 빌드 실행 시 즉시 게임 종료 가능

<h2 id="tech-stack">🧰 기술 스택 / Tech Stack</h2>

**엔진**: Unity 6.0

**언어**: C#

**패키지/툴**: Input System, ParticleSystem, Rigidbody Physics, TextMeshPro, Git, VS Code

**핵심 시스템 구성**

| 시스템                     | 설명                       |
| ----------------------- | ------------------------ |
| **Movement**      | 추진(Thrust) 및 회전 처리, 파티클/사운드 연동 |
| **CollisionHandler**               | 충돌 판정, 성공/실패 시퀀스 관리 및 씬 로드                   |
| **Oscillator**          | 플랫폼 왕복 이동 (Mathf.PingPong 기반)                    |
| **QuitApplication**     | ESC 입력 시 게임 종료 처리                |
| **Audio/Particles**        | AudioSource + ParticleSystem 통한 피드백   |
| **Debug Keys (C/L)**    | 충돌 토글 / 레벨 스킵 디버깅 |

---

<h2 id="architecture">🏗️ 프로젝트 구조 / Architecture</h2>

```
Assets/
  RocketBoost/
    Player/
      Movement.cs
      CollisionHandler.cs
    Environment/
      Oscillator.cs
    System/
      QuitApplication.cs
    Audio/
      (Engine, Crash, Success)
    VFX/
      (MainEngine, Left/Right Thrusters, Explosions)
    Scenes/
      Level01.unity
      Level02.unity
      Level03.unity
```

**설계 포인트**:

* 입력 / 물리 / 씬 전환 / 연출이 명확히 분리된 구조
* 태그 기반 충돌 로직으로 스테이지 제작 편의성 확보
* Invoke() 기반 딜레이 연출로 간결한 타이밍 제어
* Input System으로 향후 패드 입력 확장 가능


---

<h2 id="setup">⚙️ 설치 및 실행 / Setup</h2>

저장소 클론

git clone https://github.com/<YOUR_ID>/RoyalRun.git


Unity Hub에서 프로젝트 열기

Packages 자동 복구 후, Assets/Scenes/Demo.unity 실행

▶️ Play

에셋 의존성이 있는 경우, Readme/팝업 안내에 따라 의존 패키지를 함께 설치하세요.

<h2 id="controls">🎮 조작법 / Controls</h2>

| 동작    | 조작      |  설명  |
| ----- | ---------- | ---------- |
| 추진 | Space	|로켓 상향 추진
| 좌회전    | A   | 반시계 방향 회전 |
| 우회전| D                     | 시계 방향 회전 |
|충돌 토글|D                     | 충돌 판정 on/off |
|레벨 스킵|C                     | 다음 레벨로 이동 |
|게임 종료|ESC       | 프로그램 종료 |

---

<h2 id="screenshots">🖼️ 스크린샷 / Screenshots</h2> <p align="center"> <img src="RocketBoost GamePlay.png" width="720" alt="Rocket Boost Gameplay"/> </p>

균형을 잡으며 왕복 플랫폼을 넘어 착륙하는 순간, 사운드와 파티클이 맞물려 짜릿한 피드백을 제공합니다.

<h2 id="roadmap">🚀 향후 계획 / Roadmap</h2>

 * [ ] 난이도별 중력 세기 조정

 * [ ] 연료 소모 시스템 (Time-Limit 방식)

 * [ ] UI HUD (연료/속도/거리 표시)

 * [ ] Checkpoint 시스템 추가

 * [ ] 모바일 터치 컨트롤 대응

<h2 id="credits">👤 제작자 / Credits</h2>

* **기획·개발**: 김영무 (Kim YoungMoo)

* **아트 리소스**: Low-poly Space Pack (Unity Asset Store)

* **사운드**: 무료 라이브러리 + 자체 믹싱

* **참고 강의**: [강의 링크](https://www.udemy.com/course/best-3d-c-unity/?kw=C%23%EA%B3%BC+UNITY%EB%A1%9C+3&src=sac&couponCode=KEEPLEARNING)


---

<h2 id="contact">📬 연락처 / Contact</h2>

* **이메일**: [rladuan612@gmail.com](mailto:rladuan612@gmail.com)

* **포트폴리오**: [포트폴리오](https://www.naver.com)
