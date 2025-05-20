# Master Project Plan

## Overall Project Goal

By the end of this SPARC cycle, the project will have a **fast, secure, and fully-automated test framework for wallet-framework-dotnet**. This framework will include a **directory-wide `WalletFramework.*.Tests` solution that compiles and runs out-of-the-box**, **automated pipelines (GitHub Actions) for unit, integration, E2E, security, and performance tests**, and **pass/fail criteria codified in acceptance tests** that serve as living documentation.

**AI Verifiable End Goal:**
- Existence of a compilable test solution file (e.g., `WalletFramework.Tests.sln`).
- Existence and successful execution of GitHub Actions workflow files for unit, integration, E2E, security, and performance tests.
- Existence of high-level acceptance test files in `test/HighLevelTests/` with defined AI verifiable success criteria.

## Phases

### Phase 1: SPARC: Specification

**Phase AI Verifiable End Goal:** All foundational specification documents, including the Master Acceptance Test Plan, high-level acceptance tests, and the Master Project Plan, are created and registered.

| Task ID | Description                                                                 | AI Verifiable Deliverable / Completion Criteria                                                                 | Relevant Acceptance Tests / Blueprint Sections |
| :------ | :-------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------- | :--------------------------------------------- |
| 1.1     | Define and document high-level acceptance tests.                            | Existence of markdown files in `test/HighLevelTests/` for each high-level test (A-01 to A-08).                  | A-01 to A-08, Blueprint §4                     |
| 1.2     | Create the Master Acceptance Test Plan.                                     | Existence of `docs/MasterAcceptanceTestPlan.md`.                                                                | Blueprint §4                                   |
| 1.3     | Document test environments, data requirements, and security baselines.      | Existence of documentation files (e.g., markdown) detailing these aspects within the `docs/` or `test/` directories. | Blueprint §5.1                                 |
| 1.4     | Lock coding conventions and CI templates.                                   | Existence of configuration files for linters, formatters, and initial CI workflow templates (e.g., `.github/workflows/ci.yml`). | Blueprint §5.1                                 |
| 1.5     | Create the Master Project Plan document.                                    | Existence of `docs/Master Project Plan.md`.                                                                     | Blueprint §5                                   |

### Phase 2: SPARC: Preparation

**Phase AI Verifiable End Goal:** Test projects are scaffolded, necessary dependencies and tools are installed, and test environments/fixtures are provisioned.

| Task ID | Description                                                                 | AI Verifiable Deliverable / Completion Criteria                                                                                                | Relevant Acceptance Tests / Blueprint Sections |
| :------ | :-------------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------- | :--------------------------------------------- |
| 2.1     | Scaffold test projects (`*.Tests.csproj`).                                  | Existence of test project files (e.g., `test/WalletFramework.Core.Tests/WalletFramework.Core.Tests.csproj`) with necessary test runner references (xUnit). | Blueprint §5.2                                 |
| 2.2     | Add necessary testing framework dependencies (xUnit, Moq, Coverlet).        | Verification of test project file content to include references to xUnit, Moq, and Coverlet NuGet packages.                                  | Blueprint §5.2                                 |
| 2.3     | Create mock or in-memory fixtures for wallet, ledger, and HTTP clients.     | Existence of code files for mock/in-memory implementations within the test projects.                                                           | Blueprint §5.2                                 |
| 2.4     | Provision BrowserStack credentials and performance-test harness.            | Existence of configuration files or environment variables for BrowserStack and performance test harness setup.                                 | Blueprint §5.2                                 |

### Phase 3: SPARC: Acceptance

**Phase AI Verifiable End Goal:** Unit, integration, and BDD tests are implemented and demonstrate initial passing results.

| Task ID | Description                                                                 | AI Verifiable Deliverable / Completion Criteria                                                                                                | Relevant Acceptance Tests / Blueprint Sections |
| :------ | :-------------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------- | :--------------------------------------------- |
| 3.1     | Implement unit tests for `WalletFramework.Core`.                            | Code coverage report for `WalletFramework.Core` module shows increasing coverage, aiming for ≥ 95%.                                          | A-01, Blueprint §5.3                           |
| 3.2     | Implement unit tests for `WalletFramework.Oid4Vc`.                          | Code coverage report for `WalletFramework.Oid4Vc` module shows increasing coverage, aiming for ≥ 95%.                                        | A-01, Blueprint §5.3                           |
| 3.3     | Implement unit tests for `WalletFramework.MdocLib`.                         | Code coverage report for `WalletFramework.MdocLib` module shows increasing coverage, aiming for ≥ 95%.                                       | A-01, Blueprint §5.3                           |
| 3.4     | Implement unit tests for `WalletFramework.SdJwtVc`.                         | Code coverage report for `WalletFramework.SdJwtVc` module shows increasing coverage, aiming for ≥ 95%.                                       | A-01, Blueprint §5.3                           |
| 3.5     | Implement integration tests using `WebApplicationFactory<T>`.               | Successful execution of integration tests with 0 failures in a CI environment.                                                                 | A-02, Blueprint §5.3                           |
| 3.6     | Author BDD scenarios in SpecFlow for "issue credential" and "present proof". | Existence of `.feature` files defining BDD scenarios.                                                                                          | A-03, Blueprint §5.3                           |
| 3.7     | Implement step definitions for BDD scenarios.                               | Existence of code files containing SpecFlow step definitions linked to `.feature` files.                                                       | A-03, Blueprint §5.3                           |
| 3.8     | Implement property-based tests using FsCheck.                               | Successful execution of FsCheck tests with 0 counter-examples found for validation and parsing utilities.                                      | A-04, Blueprint §5.3                           |

### Phase 4: SPARC: Run

**Phase AI Verifiable End Goal:** All test suites are integrated into automated CI pipelines, and reporting mechanisms are configured.

| Task ID | Description                                                                 | AI Verifiable Deliverable / Completion Criteria                                                                                                | Relevant Acceptance Tests / Blueprint Sections |
| :------ | :-------------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------- | :--------------------------------------------- |
| 4.1     | Integrate unit, integration, and property-based tests into GitHub Actions.  | Successful execution of unit, integration, and property-based test jobs within the CI pipeline (`.github/workflows/ci.yml`).                   | A-01, A-02, A-04, Blueprint §5.4               |
| 4.2     | Integrate BDD/E2E tests with BrowserStack in GitHub Actions.                | Successful execution of BDD/E2E test jobs on BrowserStack via the CI pipeline, with all scenarios passing.                                     | A-03, Blueprint §5.4                           |
| 4.3     | Embed SAST checks (Roslyn analyzers) in the CI pipeline.                    | CI pipeline fails if Roslyn analyzer warnings at "error" level are detected.                                                                   | A-05, Blueprint §5.4                           |
| 4.4     | Configure DAST scans (OWASP ZAP) against a running test host in CI.         | CI pipeline includes a step to run OWASP ZAP scan, and the scan report indicates zero critical or high-risk vulnerabilities.                   | A-06, Blueprint §5.4                           |
| 4.5     | Integrate SCA checks (OWASP Dependency-Check) in the CI pipeline.           | CI pipeline fails if OWASP Dependency-Check identifies any CVE with a severity score ≥ 7.0.                                                    | A-07, Blueprint §5.4                           |
| 4.6     | Integrate performance tests and benchmarking into CI.                       | CI pipeline includes a performance test job that records benchmarks and verifies they are within defined thresholds.                           | A-08, Blueprint §5.4                           |
| 4.7     | Collect and publish coverage, performance, and security reports as artifacts. | CI pipeline successfully generates and publishes artifacts containing code coverage reports, performance benchmarks, and security scan results. | Blueprint §5.4                                 |

### Phase 5: SPARC: Close

**Phase AI Verifiable End Goal:** All acceptance tests pass, and final documentation and artifacts are generated and archived.

| Task ID | Description                                                                 | AI Verifiable Deliverable / Completion Criteria                                                                                                | Relevant Acceptance Tests / Blueprint Sections |
| :------ | :-------------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------- | :--------------------------------------------- |
| 5.1     | Review and remediate any test failures.                                     | All jobs in the main CI pipeline (`.github/workflows/ci.yml`) report a "success" status.                                                       | A-01 to A-08, Blueprint §5.5                   |
| 5.2     | Sign-off on green CI runs across relevant branches.                         | The main branch and any designated release branches show recent successful CI runs.                                                            | Blueprint §5.5                                 |
| 5.3     | Archive test artifacts.                                                     | Confirmation of test artifacts (reports, logs) being stored in a designated archive location (e.g., linked from CI run details).             | Blueprint §5.5                                 |
| 5.4     | Generate a final test-summary document.                                     | Existence of a comprehensive test summary document (e.g., markdown or PDF) in the `docs/reports/` directory, summarizing all test outcomes. | Blueprint §5.5                                 |