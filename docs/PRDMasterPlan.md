# Master Project Plan

## Overall Project Goal

By the end of this SPARC cycle, we will have a comprehensive, directory-wide `*.Tests` solution covering all test projects (WalletFramework.\*, Hyperledger.Aries.Tests, WalletFramework.Integration.Tests, WalletFramework.MdocLib.Tests, WalletFramework.MdocVc.Tests, WalletFramework.Oid4Vc.Tests, WalletFramework.SdJwtVc.Tests, etc.) achieving **100% project-wide code coverage metrics**, with all tests compiling and running out-of-the-box. Our automated pipelines (GitHub Actions) will enforce unit, integration, E2E, security, and performance tests, generate coverage dashboards, and implement pass/fail criteria codified in acceptance tests—providing comprehensive visibility for developers, reviewers, and auditors.

## 1. SPARC: Specification

**Phase AI Verifiable End Goal:** Master Acceptance Test Plan and all High-Level Acceptance Tests defined and documented; Initial Strategic Research and High-Level Test Strategy Research Reports created; PRDMasterPlan.md created.

### Micro Tasks

1. **Define High-Level Acceptance Tests:**

   * **Description:** Define comprehensive high-level end-to-end acceptance tests based on the User Blueprint and High-Level Test Strategy Research Report.
   * **AI Verifiable Deliverable:** High-level acceptance test files created in `test/HighLevelTests/EndToEnd/` directory (e.g., `CredentialIssuanceFlow.feature`, `CredentialPresentationFlow.feature`, etc.), each with clearly defined AI Verifiable Completion Criteria.
2. **Create Master Acceptance Test Plan:**

   * **Description:** Create a Master Acceptance Test Plan document outlining the high-level testing strategy, phases, and scenarios with AI verifiable criteria.
   * **AI Verifiable Deliverable:** Markdown file `docs/master_acceptance_test_plan.md` created, containing the test plan with AI verifiable steps and criteria.
3. **Create Initial Strategic Research Report:**

   * **Description:** Conduct initial strategic research to inform the SPARC specification.
   * **AI Verifiable Deliverable:** Markdown file `./docs/initial_strategic_research_report.md` created, containing the research findings.
4. **Create High-Level Test Strategy Research Report:**

   * **Description:** Conduct specialized research to define the optimal strategy for high-level acceptance tests.
   * **AI Verifiable Deliverable:** Markdown file `docs/research/high_level_test_strategy_report.md` created, outlining the high-level testing strategy.
5. **Create PRDMasterPlan.md:**

   * **Description:** Create the Master Project Plan document outlining all SPARC phases and micro tasks with AI verifiable end results.
   * **AI Verifiable Deliverable:** Markdown file `docs/PRDMasterPlan.md` created, containing the detailed project plan with AI verifiable tasks and phases.

## 2. SPARC: Preparation

**Phase AI Verifiable End Goal:** Test projects scaffolded with necessary dependencies and configurations; Mock fixtures created; BrowserStack credentials and performance-test harness provisioned.

### Micro Tasks

1. **Scaffold Test Projects:**

   * **Description:** Create or update `*.Tests.csproj` files for **all** test projects including:

     * Core & Domain: `WalletFramework.Core.Tests`, `WalletFramework.CredentialManagement.Tests`, `WalletFramework.NewModule.Tests`, `WalletFramework.SecureStorage.Tests`
     * Service Integrations: `WalletFramework.Integration.Tests`, `Hyperledger.Aries.Tests`
     * Protocol Layers: `WalletFramework.MdocLib.Tests`, `WalletFramework.MdocVc.Tests`, `WalletFramework.Oid4Vc.Tests`, `WalletFramework.Oid4Vp.Tests`, `WalletFramework.SdJwtVc.Tests`
     * Quality & Performance: `WalletFramework.BDDE2E.Tests`, `WalletFramework.Performance.Tests`, `WalletFramework.PropertyBased.Tests`
     * Main solution: `wallet-framework-dotnet.Tests.sln`
       **Dependencies:** xUnit, Moq, Coverlet, FsCheck, SpecFlow, BenchmarkDotNet
   * **AI Verifiable Deliverable:** `.csproj` files and solution file exist for **every** test project, each referencing the correct package dependencies and project under test.
2. **Create Mock/In-Memory Fixtures:**

   * **Description:** Develop mock or in-memory implementations for external dependencies like wallet storage, ledger interactions, and HTTP clients to enable isolated integration tests.
   * **AI Verifiable Deliverable:** Relevant mock or in-memory fixture classes/files created within the test projects (e.g., `MockWalletService.cs`, `InMemoryLedgerClient.cs`).
3. **Provision BrowserStack Credentials and Performance Harness:**

   * **Description:** Set up access to BrowserStack for cross-browser E2E testing and configure a performance-test harness (e.g., BenchmarkDotNet) for key performance benchmarks.
   * **AI Verifiable Deliverable:** Configuration files or environment variables for BrowserStack and the performance harness are set up (details to be specified in a separate configuration document).

## 3. SPARC: Acceptance

**Phase AI Verifiable End Goal:** All tests across **every** test project implemented and passing.

### Micro Tasks

1. **Implement Unit Tests:**

   * **Description:** Write unit tests for public methods in each core module (`WalletFramework.Core`, `CredentialManagement`, `NewModule`, `SecureStorage`, etc.) following London School TDD principles.
   * **AI Verifiable Deliverable:** Test files exist and pass in `WalletFramework.*.Tests` for core modules, verified by test runner output.
2. **Implement Integration Tests:**

   * **Description:** Write integration tests using `WebApplicationFactory<T>` (or equivalent) to verify interactions between components in `WalletFramework.Integration.Tests` and `Hyperledger.Aries.Tests` without external dependencies.
   * **AI Verifiable Deliverable:** Integration test files exist and pass, confirmed by CI test results.
3. **Implement BDD/E2E Tests:**

   * **Description:** Write SpecFlow Gherkin scenarios and step definitions in `WalletFramework.BDDE2E.Tests` to cover end-to-end flows (credential issuance, presentation) running on BrowserStack.
   * **AI Verifiable Deliverable:** `.feature` and step definition files exist and pass across the defined browser matrix.
4. **Implement Protocol & Domain Tests:**

   * **Description:** Ensure test coverage in protocol modules: `MdocLib`, `MdocVc`, `Oid4Vc`, `Oid4Vp`, `SdJwtVc` via their respective `*.Tests` projects.
   * **AI Verifiable Deliverable:** All tests in `WalletFramework.MdocLib.Tests`, `WalletFramework.MdocVc.Tests`, `WalletFramework.Oid4Vc.Tests`, `WalletFramework.Oid4Vp.Tests`, `WalletFramework.SdJwtVc.Tests` pass.
5. **Implement Performance Benchmarks:**

   * **Description:** Write performance tests in `WalletFramework.Performance.Tests` using BenchmarkDotNet for serialization, ledger lookup loops, and cryptographic operations.
   * **AI Verifiable Deliverable:** Benchmark projects run with results within defined thresholds.
6. **Implement Property-Based Tests:**

   * **Description:** Use FsCheck in `WalletFramework.PropertyBased.Tests` to exercise boundary and random-input scenarios for parsing, validation, and encoding utilities.
   * **AI Verifiable Deliverable:** Property-based tests execute without counterexamples.
7. **Implement Secure Storage Tests:**

   * **Description:** Write unit and integration tests for secure storage modules (`WalletFramework.SecureStorage.Tests`) ensuring encryption, key management, and data isolation.
   * **AI Verifiable Deliverable:** Secure storage test suite passes with expected security assertions.

## 4. SPARC: Run SPARC: Acceptance

**Phase AI Verifiable End Goal:** All unit, integration, and BDD/E2E tests implemented and passing.

### Micro Tasks

1. **Implement Unit Tests:**

   * **Description:** Write unit tests for public methods in each module (`WalletFramework.Core`, `Oid4Vc`, `MdocLib`, `SdJwtVc`) following London School TDD principles.
   * **AI Verifiable Deliverable:** Test files created in the respective test projects (e.g., `WalletFramework.Core.Tests/UtilsTests.cs`), and test runner output shows all implemented unit tests passing.
2. **Implement Integration Tests:**

   * **Description:** Write integration tests using `WebApplicationFactory<T>` to verify interactions between components without external dependencies.
   * **AI Verifiable Deliverable:** Integration test files created (e.g., `WalletFramework.Integration.Tests/CredentialFlowsTests.cs`), and test runner output shows all implemented integration tests passing.
3. **Implement BDD/E2E Tests:**

   * **Description:** Write SpecFlow step definitions and implement the logic to execute the Gherkin scenarios defined in the high-level acceptance tests on BrowserStack.
   * **AI Verifiable Deliverable:** Step definition files created (e.g., `WalletFramework.BDDE2E.Tests/StepDefinitions/CredentialSteps.cs`), and BrowserStack test run report shows all BDD scenarios passing across the specified browser matrix.

## 4. SPARC: Run

**Phase AI Verifiable End Goal:** Automated CI pipelines configured and executing all test suites and security scans successfully.

### Micro Tasks

1. **Integrate Test Suites into GitHub Actions:**

   * **Description:** Configure GitHub Actions workflows to build the project, run all unit, integration, and BDD/E2E test suites with matrix builds and parallel jobs.
   * **AI Verifiable Deliverable:** `.github/workflows/ci.yml` file created or updated, and a GitHub Actions run shows successful execution of all test suites.
2. **Embed Security Scans in CI:**

   * **Description:** Add steps to the GitHub Actions workflow to run Roslyn analyzers, OWASP ZAP against an in-memory host, and OWASP Dependency-Check with gating on high-severity CVEs.
   * **AI Verifiable Deliverable:** `.github/workflows/ci.yml` file updated, and a GitHub Actions run includes successful execution of all security scans with reported results meeting the defined criteria (0 analyzer errors, ZAP report OK, no CVEs ≥ 7.0).
3. **Collect and Publish Reports:**

   * **Description:** Configure the CI pipeline to collect and publish test coverage reports (Coverlet), performance benchmarks (BenchmarkDotNet), and security scan reports as pipeline artifacts.
   * **AI Verifiable Deliverable:** `.github/workflows/ci.yml` file updated, and a GitHub Actions run successfully publishes the specified reports as artifacts.

## 5. SPARC: Close

**Phase AI Verifiable End Goal:** All acceptance tests pass; Security reports show no critical/high vulnerabilities; Performance benchmarks are within thresholds; Project is signed off and test artifacts are archived.

### Micro Tasks

1. **Review and Remediate Failures:**

   * **Description:** Analyze any test failures or security/performance issues reported in the CI pipeline and implement necessary code changes or configuration updates to address them.
   * **AI Verifiable Deliverable:** Subsequent CI pipeline runs show all tests passing and security/performance criteria met.
2. **Sign-off on Green CI Runs:**

   * **Description:** Ensure that the CI pipeline runs successfully on all relevant branches (e.g., main, release branches) with all checks passing.
   * **AI Verifiable Deliverable:** Latest CI runs on designated branches show a "success" status.
3. **Archive Test Artifacts and Generate Summary:**

   * **Description:** Archive the collected test reports and artifacts and generate a final test-summary document.
   * **AI Verifiable Deliverable:** Test reports and artifacts are archived (details to be specified in a separate archiving procedure document), and a final test-summary document is created (e.g., `docs/test_summary_report.md`).
