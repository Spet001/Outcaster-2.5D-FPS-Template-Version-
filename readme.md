# Outcaster - A 2.5D FPS Template for Unity 6

![Outcaster Banner](MDimages/2dot5D%20FPS%20template.png)

**A 2.5D boomer shooter template combining retro FPS mechanics with modern character action elements.**

[![Unity Version](https://img.shields.io/badge/Unity-6.0-blue.svg)](https://unity.com/)
[![URP](https://img.shields.io/badge/Render%20Pipeline-URP-green.svg)](https://unity.com/srp/universal-render-pipeline)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Itch.io](https://img.shields.io/badge/itch.io-Download-red.svg)](https://spet01.itch.io/outcaster-retro-fps-template)

🎮 **[Download on itch.io](https://spet01.itch.io/outcaster-retro-fps-template)**

---

## 📋 Table of Contents
- [English Documentation](#english-documentation)
- [Documentação em Português](#documentação-em-português)

---

# English Documentation

## 🎮 Overview

**Outcaster** (Template Version) is a  FPS template for Unity 6 that brings together classic boomer shooter mechanics with a unique style-based combat system inspired by character action games like Devil May Cry and Yakuza 0. Perfect for developers looking to create retro-styled shooters with modern gameplay depth.

### Key Features

✅ **2.5D Visual Style** - Combine 3D environments with billboard sprites for that classic DOOM aesthetic  
✅ **Modular Weapon System** - Extensible weapon base class supporting both hitscan and projectile weapons  
✅ **Vertical Auto-Aim** - Classic DOOM-style aiming for streamlined combat  
✅ **Combat Style System** - Treat weapons as fighting styles with unique behaviors  
✅ **Health & Armor System** - Complete damage management with HUD integration  
✅ **Enemy AI** - Sentry turret system with line-of-sight detection  
✅ **Input System Integration** - Built with Unity's new Input System  
✅ **URP Ready** - Optimized for Universal Render Pipeline

![Gameplay Screenshot](MDimages/1.png)

## 🛠️ Technical Specifications

### Requirements
- **Unity Version**: 6.0 or higher
- **Render Pipeline**: Universal Render Pipeline (URP)
- **Input System**: New Input System (1.14.2+)
- **Scripting Backend**: Mono or IL2CPP
- **Platform**: PC (Windows/Mac/Linux)

### Key Dependencies
```json
{
  "Unity Input System": "1.14.2",
  "TextMeshPro": "Included",
  "Cinemachine": "2.10.4",
  "Universal RP": "17.2.0",
  "Unity Collections": "2.5.7",
  "Burst Compiler": "1.8.24"
}
```

## 📂 Project Structure

```
Assets/
├── Scripts/
│   ├── WeaponBase.cs              # Core weapon system
│   ├── PlayerWeaponController.cs  # Weapon switching and firing
│   ├── PlayerHP.cs                # Player health/armor management
│   ├── HUDManager.cs              # UI management
│   ├── BillboardManager.cs        # Sprite billboarding
│   ├── SentryScript.cs            # Enemy AI
│   ├── RocketProjectile.cs        # Projectile physics
│   ├── IDamageable.cs             # Damage interface
│   └── EnemyHealth.cs             # Enemy health system
├── Prefabs/                       # Weapon, enemy, and effect prefabs
├── Scenes/                        # Demo scene
├── Materials/                     # PBR and unlit materials
├── Sprites/                       # Billboard sprite assets
└── Audio/                         # Sound effects and music
```

## 🎯 Core Systems

### 1. Weapon System Architecture

The template uses a flexible, inheritance-based weapon system:

```csharp
public class WeaponBase : MonoBehaviour
{
    // Weapon Properties
    public string weaponName;
    public float fireRate;
    
    // Combat Modes
    public bool isHitscan;              // Instant raycast
    public float hitscanRange;          // Range for hitscan
    public int hitscanDamage;           // Damage value
    
    public GameObject projectilePrefab; // For projectile weapons
    public int projectileDamage;        // Projectile damage
    
    // Auto-aim System
    public bool enableVerticalAutoAim;
    public float autoAimRange;
    public float autoAimVerticalTolerance;
    public float autoAimHorizontalAngle;
}
```

**Supported Weapon Types:**
- **Hitscan Weapons** (Shotgun) - Instant raycast damage
- **Projectile Weapons** (Rocket Launcher) - Physics-based projectiles
- **Melee Weapons** (Katana) - Close-range combat [Planned]

### 2. Auto-Aim System

Classic DOOM-style vertical auto-aim that automatically adjusts shots to hit enemies in vertical range:

```csharp
// Automatically targets enemies within:
- Horizontal angle: 15° cone
- Vertical tolerance: 5 units
- Max range: 30 units
```

### 3. Health & Armor System

Damage absorption prioritizes armor before health:

```csharp
public void TakeDamage(int damage)
{
    if (armor > 0)
    {
        int armorAbsorb = Mathf.Min(damage, armor);
        armor -= armorAbsorb;
        damage -= armorAbsorb;
    }
    health -= damage;
}
```

### 4. Billboard System

Sprites automatically face the camera for 2.5D effect:

```csharp
public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(transform.position + 
            mainCamera.transform.rotation * Vector3.forward);
    }
}
```

### 5. Enemy AI (Sentry)

Turrets with line-of-sight detection and projectile firing:

```csharp
- Raycast-based vision
- Automatic target tracking
- Configurable fire rate
- Projectile spawning at fire point
```

## 🎨 Combat Style System

Outcaster treats weapons as **combat styles** rather than just different guns:

| Style | Type | Behavior | Special Ability (Planned) |
|-------|------|----------|---------------------------|
| **Rocket** | Heavy | Area damage projectiles | Rocket jump / Melee bash |
| **Shotgun** | Chaos | High damage hitscan | "Too Many Shotguns" multi-shot |
| **Katana** | Technical | Melee combos | Dash slash / Parry |
| **Chaingun** | Builder | Rapid-fire combo system | Combo multipliers |

## 🎮 Getting Started

### Quick Setup

1. **Open in Unity 6**
   - Clone/download the repository
   - Open the project in Unity 6.0 or higher

2. **Configure Input System**
   - Input actions are pre-configured in `InputSystem_Actions.inputactions`
   - Controls: 
     - Mouse/LMB: Fire
     - Keys 1-4: Switch weapons
     - WASD: Movement (via StarterAssets)

3. **Scene Setup**
   - Open `Assets/Scenes/Scene1.unity`
   - Press Play to test

4. **Customize Weapons**
   - Navigate to `Assets/Prefabs/Weapons`
   - Duplicate existing weapon prefabs
   - Modify `WeaponBase` component values
   - Add to `PlayerWeaponController.weaponPrefabs[]`

### Creating Custom Weapons

```csharp
// 1. Create new weapon prefab
// 2. Add WeaponBase component
// 3. Configure properties:

[Header("Basic Settings")]
weaponName = "My Weapon";
fireRate = 0.5f;

[Header("Combat Mode")]
isHitscan = true;  // or false for projectile
hitscanDamage = 50;
hitscanRange = 100f;

[Header("Auto-Aim")]
enableVerticalAutoAim = true;
autoAimRange = 30f;

// 4. Add to player weapon array
```

### Creating Custom Enemies

Implement the `IDamageable` interface:

```csharp
public class MyEnemy : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        // Custom damage logic
    }
}
```

## 🎨 HUD System

The template includes a complete HUD system with:

- **Health Bar** - Visual bar + numeric display
- **Armor Bar** - Visual bar + numeric display  
- **Weapon Name** - Current equipped weapon
- **Player Portrait** - Character face (updates with damage) [Planned]

Customize via `HUDManager.cs`:

```csharp
public void UpdateWeaponNameDisplay(string weaponName);
public void UpdateHealthDisplay(int current, int max);
public void UpdateArmorDisplay(int current, int max);
```

## 🚀 Roadmap & Planned Features

### Phase 1 - Core Combat ✅
- [x] Weapon system (hitscan + projectile)
- [x] Player health/armor
- [x] Basic enemy AI
- [x] Auto-aim system
- [x] HUD implementation

### Phase 2 - Style System 🚧
- [ ] Weapon special abilities
- [ ] Combo system
- [ ] Style meter/ranking
- [ ] Inter-weapon combo chains
- [ ] Melee weapons (Katana)

### Phase 3 - Content 📋
- [ ] Multiple enemy types
- [ ] Level progression system
- [ ] Upgrade/ability trees
- [ ] Roguelike elements option
- [ ] Boss encounters

### Phase 4 - Polish 📋
- [ ] Dynamic music system
- [ ] Enhanced VFX
- [ ] Screen shake & camera effects
- [ ] Sound design integration
- [ ] Performance optimization

## 💡 Use Cases

This template is perfect for:

- **Indie Developers** - Quick-start your boomer shooter project
- **Game Jams** - Pre-built systems for rapid prototyping
- **Learning Projects** - Study well-structured Unity FPS code
- **Commercial Projects** - Production-ready foundation for your game
- **Portfolio Pieces** - Showcase your modifications and improvements

## 📜 License

This template is available under the MIT License. You are free to use it for commercial or personal projects.

## 🤝 Contributing

Contributions are welcome! Feel free to submit pull requests or open issues.

## 📧 Support

For questions, issues, or feature requests, please open an issue on the repository or contact the developer.

---

# Documentação em Português

## 🎮 Visão Geral

**Outcaster** é um template FPS pronto para produção no Unity 6 que combina mecânicas clássicas de boomer shooter com um sistema único de combate baseado em estilos, inspirado em jogos de character action como *Devil May Cry* e *Yakuza 0*. Perfeito para desenvolvedores que desejam criar shooters retrô com profundidade de gameplay moderna.

### Recursos Principais

✅ **Estilo Visual 2.5D** - Combine ambientes 3D com sprites billboard para aquela estética clássica de DOOM  
✅ **Sistema Modular de Armas** - Classe base de arma extensível suportando armas hitscan e projéteis  
✅ **Mira Automática Vertical** - Mira estilo DOOM clássico para combate simplificado  
✅ **Sistema de Estilos de Combate** - Trate armas como estilos de luta com comportamentos únicos  
✅ **Sistema de Vida & Armadura** - Gerenciamento completo de dano com integração de HUD  
✅ **IA de Inimigos** - Sistema de torreta sentinela com detecção de linha de visão  
✅ **Integração com Input System** - Construído com o novo Input System da Unity  
✅ **Pronto para URP** - Otimizado para Universal Render Pipeline

![Screenshot Gameplay](MDimages/1.png)

## 🛠️ Especificações Técnicas

### Requisitos
- **Versão do Unity**: 6.0 ou superior
- **Render Pipeline**: Universal Render Pipeline (URP)
- **Input System**: New Input System (1.14.2+)
- **Scripting Backend**: Mono ou IL2CPP
- **Plataforma**: PC (Windows/Mac/Linux)

### Dependências Principais
```json
{
  "Unity Input System": "1.14.2",
  "TextMeshPro": "Incluído",
  "Cinemachine": "2.10.4",
  "Universal RP": "17.2.0",
  "Unity Collections": "2.5.7",
  "Burst Compiler": "1.8.24"
}
```

## 📂 Estrutura do Projeto

```
Assets/
├── Scripts/
│   ├── WeaponBase.cs              # Sistema central de armas
│   ├── PlayerWeaponController.cs  # Troca e disparo de armas
│   ├── PlayerHP.cs                # Gerenciamento de vida/armadura
│   ├── HUDManager.cs              # Gerenciamento de UI
│   ├── BillboardManager.cs        # Sistema de billboard de sprites
│   ├── SentryScript.cs            # IA inimiga
│   ├── RocketProjectile.cs        # Física de projéteis
│   ├── IDamageable.cs             # Interface de dano
│   └── EnemyHealth.cs             # Sistema de vida de inimigos
├── Prefabs/                       # Prefabs de armas, inimigos e efeitos
├── Scenes/                        # Cena de demonstração
├── Materials/                     # Materiais PBR e unlit
├── Sprites/                       # Assets de sprites billboard
└── Audio/                         # Efeitos sonoros e música
```

## 🎯 Sistemas Principais

### 1. Arquitetura do Sistema de Armas

O template usa um sistema flexível de armas baseado em herança:

```csharp
public class WeaponBase : MonoBehaviour
{
    // Propriedades da Arma
    public string weaponName;
    public float fireRate;
    
    // Modos de Combate
    public bool isHitscan;              // Raycast instantâneo
    public float hitscanRange;          // Alcance para hitscan
    public int hitscanDamage;           // Valor de dano
    
    public GameObject projectilePrefab; // Para armas de projétil
    public int projectileDamage;        // Dano do projétil
    
    // Sistema de Auto-Mira
    public bool enableVerticalAutoAim;
    public float autoAimRange;
    public float autoAimVerticalTolerance;
    public float autoAimHorizontalAngle;
}
```

**Tipos de Armas Suportados:**
- **Armas Hitscan** (Shotgun) - Dano instantâneo por raycast
- **Armas de Projétil** (Lançador de Foguetes) - Projéteis baseados em física
- **Armas Corpo a Corpo** (Katana) - Combate de curto alcance [Planejado]

### 2. Sistema de Auto-Mira

Mira automática vertical estilo DOOM clássico que ajusta automaticamente os tiros para acertar inimigos no alcance vertical:

```csharp
// Mira automaticamente em inimigos dentro de:
- Ângulo horizontal: cone de 15°
- Tolerância vertical: 5 unidades
- Alcance máximo: 30 unidades
```

### 3. Sistema de Vida & Armadura

Absorção de dano prioriza armadura antes da vida:

```csharp
public void TakeDamage(int damage)
{
    if (armor > 0)
    {
        int armorAbsorb = Mathf.Min(damage, armor);
        armor -= armorAbsorb;
        damage -= armorAbsorb;
    }
    health -= damage;
}
```

### 4. Sistema de Billboard

Sprites automaticamente viram para a câmera para efeito 2.5D:

```csharp
public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(transform.position + 
            mainCamera.transform.rotation * Vector3.forward);
    }
}
```

### 5. IA de Inimigos (Sentinela)

Torretas com detecção de linha de visão e disparo de projéteis:

```csharp
- Visão baseada em raycast
- Rastreamento automático de alvos
- Taxa de disparo configurável
- Spawn de projéteis no ponto de tiro
```

## 🎨 Sistema de Estilos de Combate

Outcaster trata armas como **estilos de combate** ao invés de apenas armas diferentes:

| Estilo | Tipo | Comportamento | Habilidade Especial (Planejada) |
|--------|------|---------------|----------------------------------|
| **Rocket** | Pesado | Projéteis com dano em área | Rocket jump / Golpe corpo a corpo |
| **Shotgun** | Caos | Hitscan de alto dano | "Muitas Shotguns" disparo múltiplo |
| **Katana** | Técnico | Combos corpo a corpo | Dash cortante / Parry |
| **Chaingun** | Builder | Sistema de combo rápido | Multiplicadores de combo |

## 🎮 Começando

### Configuração Rápida

1. **Abrir no Unity 6**
   - Clone/baixe o repositório
   - Abra o projeto no Unity 6.0 ou superior

2. **Configurar Input System**
   - Ações de input pré-configuradas em `InputSystem_Actions.inputactions`
   - Controles: 
     - Mouse/Botão Esquerdo: Atirar
     - Teclas 1-4: Trocar armas
     - WASD: Movimento (via StarterAssets)

3. **Configuração da Cena**
   - Abra `Assets/Scenes/Scene1.unity`
   - Pressione Play para testar

4. **Personalizar Armas**
   - Navegue até `Assets/Prefabs/Weapons`
   - Duplique prefabs de armas existentes
   - Modifique valores do componente `WeaponBase`
   - Adicione ao array `PlayerWeaponController.weaponPrefabs[]`

### Criando Armas Personalizadas

```csharp
// 1. Crie novo prefab de arma
// 2. Adicione componente WeaponBase
// 3. Configure propriedades:

[Header("Configurações Básicas")]
weaponName = "Minha Arma";
fireRate = 0.5f;

[Header("Modo de Combate")]
isHitscan = true;  // ou false para projétil
hitscanDamage = 50;
hitscanRange = 100f;

[Header("Auto-Mira")]
enableVerticalAutoAim = true;
autoAimRange = 30f;

// 4. Adicione ao array de armas do jogador
```

### Criando Inimigos Personalizados

Implemente a interface `IDamageable`:

```csharp
public class MeuInimigo : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        // Lógica customizada de dano
    }
}
```

## 🎨 Sistema de HUD

O template inclui um sistema completo de HUD com:

- **Barra de Vida** - Barra visual + display numérico
- **Barra de Armadura** - Barra visual + display numérico  
- **Nome da Arma** - Arma equipada atualmente
- **Retrato do Jogador** - Rosto do personagem (atualiza com dano) [Planejado]

Personalize via `HUDManager.cs`:

```csharp
public void UpdateWeaponNameDisplay(string weaponName);
public void UpdateHealthDisplay(int current, int max);
public void UpdateArmorDisplay(int current, int max);
```

## 🚀 Roadmap & Recursos Planejados

### Fase 1 - Combate Central ✅
- [x] Sistema de armas (hitscan + projétil)
- [x] Vida/armadura do jogador
- [x] IA básica de inimigos
- [x] Sistema de auto-mira
- [x] Implementação de HUD

### Fase 2 - Sistema de Estilos 🚧
- [ ] Habilidades especiais de armas
- [ ] Sistema de combos
- [ ] Medidor/ranking de estilo
- [ ] Correntes de combo entre armas
- [ ] Armas corpo a corpo (Katana)

### Fase 3 - Conteúdo 📋
- [ ] Múltiplos tipos de inimigos
- [ ] Sistema de progressão de níveis
- [ ] Árvores de upgrade/habilidades
- [ ] Opção de elementos roguelike
- [ ] Encontros de chefes

### Fase 4 - Polimento 📋
- [ ] Sistema de música dinâmica
- [ ] VFX aprimorados
- [ ] Screen shake & efeitos de câmera
- [ ] Integração de sound design
- [ ] Otimização de performance

## 💡 Casos de Uso

Este template é perfeito para:

- **Desenvolvedores Indie** - Início rápido para seu projeto de boomer shooter
- **Game Jams** - Sistemas pré-construídos para prototipagem rápida
- **Projetos de Aprendizado** - Estude código FPS bem estruturado no Unity
- **Projetos Comerciais** - Base pronta para produção do seu jogo
- **Peças de Portfólio** - Mostre suas modificações e melhorias

## 📜 Licença

Este template está disponível sob a licença MIT. Você é livre para usá-lo em projetos comerciais ou pessoais.

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para enviar pull requests ou abrir issues.

## 📧 Suporte

Para dúvidas, problemas ou solicitações de recursos, por favor abra uma issue no repositório ou contate o desenvolvedor.

---


