# Git Workflow

## Git Commit Conventions

### Commit Message Format
- **Title**: Short, imperative mood, max 50 characters
- **Body**: Bullet points describing changes
- **No AI attribution**: Omit Co-Authored-By lines

### Rules
- Use English for all commit messages
- Start with imperative verb (Add, Fix, Update, Remove, Refactor)
- No filler words or unnecessary descriptions
- Each bullet point describes one logical change
- No periods at end of title or bullet points

### Example
```
Add Auth module

- Add AuthModule with DI registration
- Add AuthController with login/register/me endpoints
- Add User entity
- Add repository and service layers
- Add DTOs for authentication
```

### Commit Grouping
- Group related changes logically
- One commit per module/feature when adding new code
- Separate commits for unrelated changes
- Configuration changes can be bundled if related

### Failure Conditions
- Commit messages not in English are incorrect
- Non-imperative title is incorrect
- Co-Authored-By lines are incorrect
- Filler words in commit messages are incorrect

## GitHub CLI Workflow

All GitHub operations use the **GitHub CLI** (`gh`).

### Installation
GitHub CLI is installed via `winget` and located at `C:\Program Files\GitHub CLI\gh.exe`.

### Branch + PR Workflow
1. Create a feature branch from `main`:
```bash
git checkout -b feature/{feature-name}
```

2. Commit changes and push with upstream tracking:
```bash
git push -u origin feature/{feature-name}
```

3. Create a Pull Request via `gh`:
```bash
gh pr create --title "Add feature X" --body "## Summary
- Change 1
- Change 2

## Test plan
- [ ] Verification step 1
- [ ] Verification step 2"
```

4. Merge the PR:
```bash
gh pr merge {pr-number} --merge
```

5. Return to `main` and pull:
```bash
git checkout main && git pull origin main
```

### Branch Naming
- `feature/{name}` for new features
- `fix/{name}` for bug fixes
- `refactor/{name}` for refactoring
- Use kebab-case for branch names

### PR Conventions
- Title: Short, imperative mood (same as commit title rules)
- Body: `## Summary` with bullet points, `## Test plan` with checklist
- Target branch: `main`
- Merge strategy: merge commit (not squash or rebase)

### Failure Conditions
- PRs without a summary section are incorrect
- Direct pushes to `main` for feature work are incorrect
- PRs without a test plan are incorrect
