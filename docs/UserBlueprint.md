```markdown
# UserBlueprint

## 1. Introduction

This **User Blueprint** defines the high-level requirements, acceptance tests, and Master Project Plan for the upcoming **SPARC** specification phase. Our ultimate goal this cycle is to deliver a **fast**, **secure**, and **fully-automated** test framework for **wallet-framework-dotnet**, ensuring every module—from core utilities to end-to-end credential flows—meets functional, performance, and security standards.

---

## 2. Project Requirements

1. **Functional Coverage**  
   - 100% of public APIs exercised by unit tests.  
   - All protocol flows (OID4VC issuance & presentation, mDoc, SD-JWT) validated with integration and BDD tests.  
2. **Speed & Performance**  
   - Unit test suite runs in \< 30 s on GitHub Actions with parallel execution enabled.  
   - End-to-end (BrowserStack) scenarios complete in \< 3 min for a representative cross-browser matrix.  
   - Key performance benchmarks (serialization, ledger lookups) automated via performance-test harness.  
3. **Security & Compliance**  
   - Static analysis (Roslyn analyzers + OWASP .NET cheat sheet) enforced as a quality gate.  
   - Dynamic scans (OWASP ZAP) run against an in-memory deployment, with zero critical or high findings.  
   - SCA (OWASP Dependency-Check) integrated to block builds on unpatched CVEs.  
   - Property-based tests (FsCheck) to exercise boundary conditions and prevent common security pitfalls.

---

## 3. SPARC Cycle Ultimate Goal

> **By the end of this SPARC cycle**, we will have:
> - A **directory-wide** `WalletFramework.*.Tests` solution that compiles and runs out-of-the-box.  
> - **Automated pipelines** (GitHub Actions) for unit, integration, E2E, security and performance tests.  
> - **Pass/fail criteria** codified in acceptance tests that serve as living documentation for developers, reviewers, and auditors.

---

## 4. High-Level Acceptance Tests

| ID   | Category         | Description                                                                                                                                                         | Success Criteria                                |
|------|------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------|
| A-01 | Unit             | Every public method in `WalletFramework.Core`, `Oid4Vc`, `MdocLib`, `SdJwtVc` has at least one xUnit test.                                                         | Coverage ≥ 95% by Coverlet                      |
| A-02 | Integration      | All integration scenarios using `WebApplicationFactory<T>` against in-memory DB run without external dependencies.                                                   | 0 failures in CI run                            |
| A-03 | BDD / E2E        | Gherkin scenarios for “issue credential” and “present proof” pass in headless Chrome and Firefox on BrowserStack.                                                  | All scenarios green across browser matrix       |
| A-04 | Property-Based   | FsCheck generates at least 100 random inputs each for validation and parsing utilities, uncovering no failures or uncaught exceptions.                               | 0 FsCheck counter-examples                      |
| A-05 | SAST             | No Roslyn analyzer warnings at “error” level; OWASP .NET guidelines enforced in CI.                                                                                  | 0 analyzer errors                               |
| A-06 | DAST             | OWASP ZAP scan against a running test host reports zero critical or high-risk vulnerabilities.                                                                       | Report passes with “OK” status                  |
| A-07 | SCA              | Dependency-Check scan blocks build on any CVE ≥ 7.0 severity.                                                                                                       | CI fails if any CVE ≥ 7.0                       |
| A-08 | Performance      | Serialization latency ≤ 50 ms; ledger lookup loop (10 ops) completes in ≤ 500 ms under CI hardware.                                                                 | Benchmarks recorded and within thresholds       |

---

## 5. Master Project Plan

### 5.1 SPARC: Specification

- **Define** all acceptance tests (see § 4).  
- **Document** test environments, data requirements, and security baselines.  
- **Lock** coding conventions and CI templates.

### 5.2 SPARC: Preparation

- Scaffold each test project (`*.Tests.csproj`) with xUnit, Moq, Coverlet.  
- Create mock or in-memory fixtures for wallet, ledger, and HTTP clients.  
- Provision BrowserStack credentials and performance-test harness.

### 5.3 SPARC: Acceptance

- **Implement** unit tests one module at a time (core → Oid4Vc → MdocLib → SdJwtVc).  
- **Wire up** integration tests with `WebApplicationFactory<TEntryPoint>`.  
- **Author** BDD scenarios in SpecFlow and validate on BrowserStack.

### 5.4 SPARC: Run

- **Integrate** all suites into GitHub Actions with matrix builds and parallel jobs.  
- **Embed** SAST/DAST/SCA steps at appropriate pipeline stages.  
- **Collect** and publish coverage, performance, and security reports as pipeline artifacts.

### 5.5 SPARC: Close

- **Review** all acceptance test results, remediate any failures.  
- **Sign-off** on green CI runs across every branch.  
- **Archive** test artifacts and generate a final test-summary document.

---

## 6. Deep & Meaningful Tests to Include

1. **Edge-Case Functional Tests**  
   - Empty, null, oversized payloads for `JsonExtensions` and `UriExtensions`.  
   - Invalid credential configurations (e.g., missing `configurationId`).  
2. **Concurrency & Thread-Safety**  
   - Parallel wallet record operations against in-memory store.  
   - Race-condition tests on `PaymentTransactionDataSamples`.  
3. **Negative & Security-Focused**  
   - Tampered JSON-Web-Tokens and replayed HTTP requests.  
   - CSRF and XSS checks on cookie-based authentication flows.  
4. **Performance Benchmarks**  
   - Bulk serialization/deserialization of 1 000 records.  
   - High-throughput credential issuance simulation.  
5. **Compliance Scenarios**  
   - Encryption/decryption flows against FIPS-compliant RNG.  
   - SD-JWT selective disclosure edge tests with maximum nested claims.

---

## 7. Glossary & References

- **SPARC**: Specification, Preparation, Acceptance, Run, Close  
- **TDD**: Test-Driven Development (Red → Green → Refactor)  
- **SAST/DAST/SCA**: Static/Dynamic/Supply-Chain Analysis  
- **CI**: Continuous Integration (GitHub Actions)  
- **BDD**: Behavior-Driven Development (SpecFlow + Gherkin)

---

*End of UserBlueprint.md*  
```
