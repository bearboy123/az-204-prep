Push this repo to GitHub

If you already created a GitHub repository, run:

```powershell
Set-Location "E:\Keerthi\projects\azure-204\MyTimerFunctionApp"
# replace <remote-url> with your repository HTTPS URL (e.g. https://github.com/youruser/MyTimerFunctionApp.git)
git remote add origin <remote-url>
git branch -M main
git push -u origin main
```

If you want to create a new repo using the GitHub website:
1. Go to https://github.com/new and create a new repository named `MyTimerFunctionApp`.
2. Follow the instructions on the page and run the `git remote add` and `git push` commands above.

If you have GitHub CLI installed later, you can run:

```powershell
# create and push using gh
gh repo create youruser/MyTimerFunctionApp --public --source=. --remote=origin --push
```

Notes:
- `local.settings.json` is excluded from git because it contains secrets for local development.
- Consider creating a repo README and CI (e.g., GitHub Actions) for automatic deployment.
