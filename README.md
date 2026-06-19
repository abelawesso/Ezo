# 📐 Ezo - Évaluateur d'Expressions Mathématiques

Un projet C# .NET 8 pour évaluer des expressions mathématiques avec support des opérateurs de base et des fonctions trigonométriques/logarithmiques.

## 🎯 Fonctionnalités

### Opérateurs Supportés
- ➕ Addition (`+`)
- ➖ Soustraction (`-`)
- ✖️ Multiplication (`*`)
- ➗ Division (`/`)


### Fonctions Mathématiques
- `sqrt(x)` - Racine carrée
- `sin(x)` - Sinus
- `cos(x)` - Cosinus
- `tan(x)` - Tangente
- `log(x)` - Logarithme naturel

### Types d'Évaluation
- **Native** : Utilise `DataTable.Compute` pour une évaluation rapide
- **Integre** : Implémentation personnalisée avec support des fonctions mathématiques

## 📦 Structure du Projet

```
Ezo/
├── Core/                           # Projet principal
│   ├── Evaluateur/
│   │   ├── Evaluateur.cs          # Classe d'évaluation principale
│   │   ├── Lecteur.cs             # Lecteur d'expressions
│   │   └── Ordonnanceur.cs        # Ordonnancement (notation polonaise inverse)
│   ├── Enums/
│   │   ├── TypeElements/          # Types d'éléments (nombre, opérateur, fonction)
│   │   └── TypeEvaluations/       # Types d'évaluation (Native, Integre)
│   └── Exceptions/                # Exceptions personnalisées
│       ├── DivisionParZeroException.cs
│       └── ExpressionInvalideException.cs
├── Ezotest/                        # Projet de tests (xUnit)
│   └── EzoTest.cs                 # Tests unitaires
└── README.md                       # Ce fichier
```

## 🚀 Installation & Configuration

### Prérequis
- .NET 8 SDK ou version ultérieure
- Visual Studio 2022 ou Code Visual Studio

### Cloner le Projet
```bash
git clone https://github.com/abelawesso/Ezo.git
cd Ezo
```

### Compiler le Projet
```bash
dotnet build
```

## 📚 Utilisation

### Exemple Basique

```csharp
using Core;

// Évaluation avec type Native
decimal result1 = Evaluateur.Evaluer("2 + 3 * 4", TypeEvaluation.Native);
Console.WriteLine(result1); // Output: 14

// Évaluation avec type Integre
decimal result2 = Evaluateur.Evaluer("sqrt(16) + 2", TypeEvaluation.Integre);
Console.WriteLine(result2); // Output: 6

// Expressions complexes
decimal result3 = Evaluateur.Evaluer("(10 + 5) * 2 - 3", TypeEvaluation.Native);
Console.WriteLine(result3); // Output: 27
```

### Gestion des Erreurs

```csharp
try
{
	Evaluateur.Evaluer("1/0", TypeEvaluation.Integre);
}
catch (ExpressionInvalideException ex)
{
	Console.WriteLine($"Erreur: {ex.Message}");
}
```

## ✅ Tests

Le projet inclut une suite de tests complète utilisant **xUnit**.

### Exécuter les Tests
```bash
dotnet test
```

### Couverture des Tests
- ✅ Opérations arithmétiques de base
- ✅ Expressions complexes avec parenthèses
- ✅ Nombres décimaux
- ✅ Fonctions mathématiques (sqrt, sin, cos, tan, log)
- ✅ Gestion de la division par zéro
- ✅ Validation des expressions invalides

#### Résultats Actuels
```
18 tests - 16 réussis ✅ - 2 échoués ⚠️
```

> **Note** : Les 2 tests en échec concernent le support des parenthèses et des fonctions dans l'implémentation `Integre`. Cela sera corrigé dans une future version.

## 🏗️ Architecture

### Évaluation Native
Utilise la classe `.NET` `DataTable.Compute` pour évaluer les expressions mathématiques standards.

### Évaluation Integre
1. **Lecteur** (`Lecteur.cs`) - Analyse l'expression et identifie les tokens
2. **Ordonnanceur** (`Ordonnanceur.cs`) - Convertit en notation ordonnée
3. **Evaluateur** (`Evaluateur.cs`) - Évalue l'expression 

## 📋 Exceptions

### `ExpressionInvalideException`
Levée quand une expression est mal formée ou contient des éléments non valides.

### `DivisionParZeroException`
Levée lors d'une tentative de division par zéro dans le mode d'évaluation `Integre`.

## 🔄 Flux de Développement

Le projet fonctionne sur la branche `feat/development`. Les contributions sont bienvenues !

### Branche Actuelle
```
feat/development
```

## 📝 Exemples d'Expressions Valides

| Expression | Type | Résultat |
|-----------|------|----------|
| `2 + 3` | Native/Integre | 5 |
| `10 - 4` | Native/Integre | 6 |
| `5 * 3` | Native/Integre | 15 |
| `20 / 4` | Native/Integre | 5 |
| `2 + 2 * 5 + 5` | Native/Integre | 17 |
| `10 / 2` | Native/Integre | 5 |
| `sqrt(4)` | Native | 2 |
| `sin(0)` | Native | 0 |
| `(2 + 5) * 3` | Native | 21 |

## 🐛 Limitations Connues

- Les parenthèses ne sont pas totalement supportées en mode `Integre`
- Les fonctions mathématiques ne sont supportées qu'en mode `Native` dans certains cas
- Pas de support pour les exposants (`^`)

## 🚧 Améliorations Futures

- [ ] Support complet des parenthèses en mode `Integre`
- [ ] Support des exposants (`^`)
- [ ] Support des nombres complexes
- [ ] API REST pour évaluer les expressions
- [ ] Interface graphique (GUI)
- [ ] Amélioration de la gestion des erreurs avec messages détaillés

## 📄 Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

## 👨‍💻 Auteur

**Abel Awesso**
- GitHub: [@abelawesso](https://github.com/abelawesso)

## 📞 Support

Pour toute question ou problème, veuillez ouvrir une issue sur [GitHub](https://github.com/abelawesso/Ezo/issues).

---

**Dernière mise à jour** : 2024
**Version** : 1.0.0
**Framework** : .NET 8
