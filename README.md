# Workshop: GitHub Intermedio

Hola

**Automatización, políticas y releases con GitHub Actions**

Nivel: intermedio en Git y GitHub, principiante absoluto en Actions
Duración: 2 horas
Formato: laboratorio guiado, autocontenido, de principio a fin

---

## Qué es esto

Un laboratorio de dos horas para gente que ya trabaja con Git y GitHub todos los
días (ramas, commits, pull requests) pero que nunca ha escrito un workflow de
Actions.

Al terminar vas a tener un repositorio con:

- Un pipeline que compila y prueba en cada push y en cada pull request
- Reportes descargables y un resumen legible de cada ejecución
- La rama `main` protegida, donde no se puede hacer merge con el pipeline en rojo
- Un tag con versionamiento semántico y un release publicado automáticamente

Nada de eso es teórico. Lo construyes tú, paso por paso, y cada ejercicio tiene
una verificación concreta para saber si funcionó.

## Qué no es

No es un curso de YAML ni un tour por el catálogo de features de GitHub. Los
temas avanzados (matrix, reusable workflows, caché, GitHub Packages, commits
firmados) están en [`laboratorio/07-extras.md`](laboratorio/07-extras.md), fuera
de las dos horas, porque meterlos en la sesión principal produce gente que copia
YAML sin entenderlo.

## Requisitos

Conocimiento previo:

- Clonar, ramas, commits, push
- Abrir un pull request
- No necesitas saber C#. El código es aritmética y formateo de texto

Herramientas: usa **GitHub Codespaces** y no instalas nada. Si prefieres tu
máquina, necesitas el SDK de .NET 9, Git y
[GitHub CLI](https://cli.github.com). Los detalles están en el ejercicio 00.

## Empieza aquí

**[laboratorio/00-preparacion.md](laboratorio/00-preparacion.md)**

Antes de eso, un aviso que importa: crea tu repositorio **público**. Los
rulesets no están disponibles en repositorios privados con GitHub Free, y el
ejercicio 05 depende de ellos. La justificación completa está en el ejercicio 00.

## El recorrido

| # | Ejercicio | Tiempo | Qué construyes |
|---|-----------|--------|----------------|
| 00 | [Preparación](laboratorio/00-preparacion.md) | 10 min | Tu copia del repo, funcionando |
| 01 | [Tu primer workflow](laboratorio/01-primer-workflow.md) | 15 min | Un workflow manual. Cuatro conceptos, nada más |
| 02 | [CI de verdad](laboratorio/02-ci-basico.md) | 20 min | Compilar y probar en cada push. Verlo fallar |
| 03 | [Artefactos y resumen](laboratorio/03-artefactos-y-resumen.md) | 15 min | Sacar reportes del runner antes de que se destruya |
| 04 | [Dos jobs y diagnóstico](laboratorio/04-dos-jobs-y-diagnostico.md) | 20 min | `needs`, `outputs`, y 4 errores en un YAML roto |
| 05 | [Proteger main](laboratorio/05-proteger-main.md) | 20 min | Ruleset, CODEOWNERS, un PR bloqueado por CI |
| 06 | [Tags y releases](laboratorio/06-tags-y-releases.md) | 15 min | SemVer y publicación automática |
| 07 | [Extras](laboratorio/07-extras.md) | Opcional | Packages, matrix, caché, seguridad |

## El proyecto base

`FinancialUtils`, una librería de .NET con cálculos financieros: interés
compuesto, amortización y formateo de moneda. Existe para que el pipeline tenga
algo real que compilar y probar. Suficientemente concreto para que los tiempos y
los reportes signifiquen algo, suficientemente simple para no distraer.

```
.
├── .devcontainer/           Configuración de Codespaces
├── .github/
│   ├── workflows/
│   │   ├── 00-hola-actions.yml   Ejercicio 01, manual
│   │   └── 01-ci.yml             Ejercicio 02 en adelante, lo vas modificando
│   ├── CODEOWNERS
│   ├── dependabot.yml
│   ├── ISSUE_TEMPLATE/
│   └── pull_request_template.md
├── laboratorio/             Los ejercicios, en orden
│   └── recursos/            El workflow roto del ejercicio 04
├── soluciones/              Estado final de cada archivo, y las respuestas
├── instructor/              Guía y checklist para quien imparte
├── scripts/
│   └── verificar-entorno.sh
├── src/FinancialUtils/
└── tests/FinancialUtils.Tests/
```

## Sobre las versiones

El proyecto apunta a `net9.0`. .NET 9 llega a fin de soporte el **10 de
noviembre de 2026**, el mismo día que .NET 8
([anuncio de Microsoft](https://devblogs.microsoft.com/dotnet/dotnet-8-9-end-of-support/)).
Para el laboratorio no cambia nada, pero si vas a impartir esto después de esa
fecha, el destino es .NET 10 LTS.

Las versiones de las acciones (`actions/checkout`, `actions/setup-dotnet`,
`actions/upload-artifact`, `actions/download-artifact`) suben de major con
frecuencia. El repositorio incluye `.github/dependabot.yml` para que las
actualizaciones lleguen como PR. Si vas a impartir el workshop, verifica las
versiones antes de la sesión. Está en el
[checklist del instructor](instructor/checklist-previo.md).

## Si vas a impartirlo

Lee [`instructor/guia-del-instructor.md`](instructor/guia-del-instructor.md).
Incluye agenda minuto a minuto, los tres puntos donde se atora la gente, qué
recortar si vas tarde, y las decisiones de diseño detrás del material.

## Reusar este material

El contenido es reutilizable. Si lo adaptas:

1. Reemplaza `@TU_USUARIO` en `.github/CODEOWNERS`
2. Ajusta el ejercicio 00 según el plan de GitHub de tu audiencia
3. Corre el laboratorio completo en un repositorio limpio antes de impartirlo

## Referencias

- [Documentación de GitHub Actions](https://docs.github.com/en/actions)
- [Sintaxis de workflows](https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-syntax)
- [Rulesets](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-rulesets/about-rulesets)
- [Facturación de Actions](https://docs.github.com/en/actions/concepts/billing-and-usage)
- [Endurecimiento de seguridad](https://docs.github.com/en/actions/reference/security/secure-use)
- [Manual de GitHub CLI](https://cli.github.com/manual)

---

Desarrollado por [Armando Blanco](https://github.com/armandoblanco).
