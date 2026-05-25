# 

# Проект демонстрирует сервис-ориентированную архитектуру с упором на:

# \- input abstraction

# \- controller-first UI

# \- систему окон

# \- save/load abstraction

# \- platform-aware services

# \- расширяемость под платформенные требования

# 

# Проект поддерживает runtime-переключение между keyboard/mouse и gamepad с использованием Unity New Input System.

# 

# \---

# 

# &#x20;Архитектура

# 

# Проект построен на сервисной архитектуре с использованием интерфейсов и собственного `ServiceLocator`.

# 

# Основные сервисы:

# \- `IInputService`

# \- `IWindowService`

# \- `ICursorService`

# \- `ISaveService`

# \- `IPlatformService`

# \- `IAchievementService`

# 

# Такой подход позволяет системам оставаться независимыми друг от друга и упрощает замену платформенных реализаций.

# 

# \---

# 

# &#x20;Input System

# 

# Реализован с использованием Unity New Input System.

# 

# Особенности:

# \- runtime-переключение между keyboard/mouse и gamepad

# \- отдельные Action Maps для Gameplay и UI

# \- приоритет UI Action Map над Gameplay Action Map

# \- controller-first UI navigation

# \- обработка курсора в зависимости от схемы управления

# \- input abstraction через `IInputService`

# 

# При открытии UI автоматически активируется UI Action Map, а Gameplay Action Map перестаёт получать input events.

# 

# \---

# 

# &#x20;Window System

# 

# Система окон реализована через `WindowService` с использованием stack-based подхода.

# 

# Особенности:

# \- стек окон

# \- автоматическое восстановление focus

# \- приоритет верхнего окна

# \- поддержка controller navigation

# \- корректная работа при переключении input scheme

# 

# UI окна не уничтожаются и не создаются повторно, а скрываются/показываются через WindowService.

# 

# \---

# 

# &#x20;Save System

# 

# Система сохранений построена на двух уровнях абстракции:

# 

# \- `ISaveService`

# \- `ISaveBackend`

# 

# Gameplay код не записывает данные напрямую в файл.

# 

# Текущая реализация:

# \- `JsonFileBackend`

# 

# Архитектура позволяет заменить backend на:

# \- Steam Cloud

# \- PlayStation Save Storage

# \- Xbox Storage

# \- cloud save solutions

# 

# без изменений gameplay-кода.

# 

# \---

# 

# &#x20;Platform-aware Architecture

# 

# Платформенные функции абстрагированы через `IPlatformService`.

# 

# Текущая реализация:

# \- `DummyPlatformService`

# 

# Архитектура подготовлена для расширения под:

# \- achievements

# \- presence

# \- social systems

# \- profanity filtering

# \- DLC/premium systems

# 

# без использования `if platform == ...` внутри gameplay систем.

# 

# \---

# 

# &#x20;Achievement System

# 

# Ачивки реализованы через `AchievementService`.

# 

# Особенности:

# \- session-based kill tracking

# \- сохранение открытых ачивок

# \- platform abstraction support

# \- UI integration

# 

# Текущие достижения:

# \- First Blood — уничтожить 1 врага

# \- Area Cleared — уничтожить 5 врагов

# \- War Machine — уничтожить 10 врагов

# \- Unstoppable — уничтожить 20 врагов

# 

# \---

# 

# &#x20;Дополнительные системы

# 

# Реализованы:

# \- projectile pooling

# \- enemy pooling

# \- weapon switching

# \- automatic/semi-automatic weapons

# \- reload system

# \- HUD

# \- melee AI enemies

# \- VFX pooling

# 

# \---

# 

# Использованные ассеты

# 

# \- Legacy Particle Pack  

# https://assetstore.unity.com/packages/vfx/particles/legacy-particle-pack-73777

# 

# \- SciFi Warehouse Kit

# 

# \- 3D модели оружия со Sketchfab

# 

# \---

# 

# &#x20;Управление

# 

# &#x20;Keyboard \& Mouse

# \- WASD — движение

# \- Mouse — обзор

# \- Left Mouse Button — стрельба

# \- Right Mouse Button — прицеливание

# \- R — перезарядка

# \- 1/2 — смена оружия

# \- Escape — меню/пауза

# 

# &#x20;Gamepad

# \- Left Stick — движение

# \- Right Stick — обзор

# \- RT — стрельба

# \- LT — прицеливание

# \- X/Square — перезарядка

# \- D-Pad — смена оружия

# \- Start — меню/пауза

