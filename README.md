# 🔍 Tokero UI Automated Tests

This repository contains a collection of automated tests for the [Tokero](https://tokero.dev/en/) staging environment, written using **Playwright** with **.NET 8** and **NUnit**.

The goal of these tests is to demonstrate integration testing skills across multiple browsers, languages, and performance validation.

---

## 🚀 How to Run

### 📦 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Node.js (required for Playwright internal tools)

Install Playwright browsers:

```bash
npx playwright install
```

### ▶️ Run all tests:

```bash
dotnet test
```

To run tests in a specific browser (e.g. Firefox):

```bash
dotnet test --filter TestContactPageLoads firefox
```

---

## 🔎 What’s Tested

- **Policies Page (EN)** – navigates via footer, checks if links are present
- **Policies Page (RO)** – same flow in Romanian
- **Contact Page** – ensures the "Contact us" heading is visible
- **Homepage Performance** – validates the page loads under 5 seconds
- **Multibrowser Execution** – tests are run in Chromium, Firefox, and WebKit
- **Cookie Popup Handling** – automatically accepts if present

---

## ✅ Test Report

All tests were executed on Windows 11 using:

- `dotnet test`
- Playwright v1.44.0
- .NET 8 SDK

### ✔️ Test Summary

| Test Name                   | Description                                              | Status    |
|----------------------------|----------------------------------------------------------|-----------|
| `TestPoliciesPageLoads`     | Checks that the EN Policies page loads                  | ✅ Passed |
| `TestPoliciesInRomanian`    | Checks RO Policies page from footer                     | ✅ Passed |
| `TestContactPageLoads`      | Validates "Contact us" title is visible                 | ✅ Passed |
| `TestPerformanceHomepage`   | Ensures EN homepage loads in under 5s                   | ✅ Passed |
| Multi-browser execution     | Tested on Chromium, Firefox, WebKit                     | ✅ Passed |
| Cookie popup handling       | Cookie consent is accepted if present                   | ✅ Passed |

### 🖥 Terminal Output

```
NUnit Adapter 4.4.0.0: Test execution started
Running all tests in ...\TokeroTests\bin\Debug\net8.0\TokeroTests.dll
TokeroTests test succeeded (13.5s)
Test summary: total: 4, failed: 0, succeeded: 4, skipped: 0
```

---

## ⚙️ Stack / Technologies

- [Microsoft.Playwright (.NET)](https://playwright.dev/dotnet/)
- [NUnit](https://nunit.org/)
- [.NET 8](https://dotnet.microsoft.com/)
- Headless browser testing with Chromium, Firefox, WebKit

## 🎥 Test Execution Videos

https://www.loom.com/share/ab5188dabd9948b0b2bded6ecb40455b
