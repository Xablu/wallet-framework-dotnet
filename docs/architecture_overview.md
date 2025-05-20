# WalletFramework.NET High-Level Architecture Overview

## 1. Introduction

This document outlines the high-level architecture of the WalletFramework.NET project, with a specific focus on the testing framework being developed during this SPARC cycle. The architecture is designed to support the project's overall goal of creating a fast, secure, and fully-automated test framework, as defined in the [Master Project Plan](docs/Master Project Plan.md). It directly aligns with the high-level acceptance criteria detailed in the [Master Acceptance Test Plan](docs/MasterAcceptanceTestPlan.md), ensuring that the system can be verified against broad, user-centric outcomes.

## 2. Overall Architectural Style

The WalletFramework.NET project follows a modular architecture. The core functionalities are encapsulated within distinct .NET libraries, allowing for clear separation of concerns and improved testability. The testing framework mirrors this modularity, with dedicated test projects for each core component.

## 3. Core Components

The primary functional areas of the WalletFramework.NET are organized into the following core library projects:

-   **WalletFramework.Core:** Contains fundamental utilities, extensions, and shared functionalities used across the framework.
-   **WalletFramework.Oid4Vc:** Implements the OpenID for Verifiable Credentials (OID4VC) protocols, including issuance and presentation flows.
-   **WalletFramework.MdocLib:** Provides support for ISO 18013-5 Mobile Driving Licence (mDL) and other mdoc-based credentials.
-   **WalletFramework.SdJwtVc:** Handles Self-Described JSON Web Tokens (SD-JWT) and Verifiable Credentials based on SD-JWT.

These components are designed with dependency injection principles in mind to facilitate testing by allowing dependencies to be easily mocked or replaced with test-specific implementations.

## 4. Testing Framework Architecture

The testing framework is a critical part of the WalletFramework.NET architecture for this SPARC cycle. Its structure is designed to enable comprehensive and automated testing across various dimensions.

### 4.1. Test Projects

Corresponding to the core modules, dedicated test projects are established:

-   `test/WalletFramework.Core.Tests/`: Houses unit and property-based tests for `WalletFramework.Core`.
-   `test/WalletFramework.Oid4Vc.Tests/`: Houses unit and property-based tests for `WalletFramework.Oid4Vc`.
-   `test/WalletFramework.MdocLib.Tests/`: Houses unit and property-based tests for `WalletFramework.MdocLib`.
-   `test/WalletFramework.SdJwtVc.Tests/`: Houses unit and property-based tests for `WalletFramework.SdJwtVc`.
-   `test/WalletFramework.Integration.Tests/`: Contains integration tests that verify interactions between core modules and simulated external dependencies.
-   `test/WalletFramework.BDDE2E.Tests/` (Proposed): A dedicated project for BDD/E2E scenarios, potentially utilizing SpecFlow and interacting with the framework through a test host or application.
-   `test/WalletFramework.Performance.Tests/` (Proposed): A project for performance benchmarks using BenchmarkDotNet.

This structure directly supports the AI verifiable task 2.1 (Scaffold test projects) and the implementation tasks in Phase 3 of the Master Project Plan.

### 4.2. Test Infrastructure and Utilities

-   **Testing Frameworks:** xUnit is used as the primary test runner. Moq is utilized for creating mock objects in unit tests. FsCheck is integrated for property-based testing. SpecFlow is planned for BDD/E2E tests. BenchmarkDotNet is planned for performance tests.
-   **Mocking and Fixtures:** In-memory implementations and mock objects for external dependencies (e.g., wallet storage, ledger interactions, HTTP clients) are provided to ensure integration tests can run without requiring actual external services (Task 2.3).
-   **Integration Test Host:** The integration test project leverages `WebApplicationFactory<T>` to host relevant parts of the framework in a test environment, enabling realistic interaction testing (Task 3.5).

### 4.3. CI/CD Pipeline Integration

The automated testing is orchestrated by a GitHub Actions workflow defined in `.github/workflows/ci.yml`. This pipeline is a central component of the testing architecture, ensuring that all tests are run automatically on code changes.

The pipeline includes steps for:

-   Building the solution.
-   Running unit tests (Task 4.1, A-01).
-   Running property-based tests (Task 4.1, A-04).
-   Running integration tests (Task 4.1, A-02).
-   Running SAST checks using Roslyn analyzers (Task 4.3, A-05).
-   Running DAST scans against a test host (Task 4.4, A-06).
-   Running SCA checks using OWASP Dependency-Check (Task 4.5, A-07).
-   Running performance tests and benchmarks (Task 4.6, A-08).
-   Running BDD/E2E tests, potentially integrated with BrowserStack for cross-browser testing (Task 4.2, A-03).
-   Collecting and publishing test reports, code coverage reports (using Coverlet), security scan results, and performance benchmarks as artifacts (Task 4.7).

This pipeline directly supports all tasks in Phase 4 of the Master Project Plan and provides the mechanism for verifying the AI verifiable success criteria of the high-level acceptance tests (A-01 to A-08).

## 5. Data Flow and Interactions

Within the testing framework, test projects interact with the core modules by calling their public APIs. Mock objects and in-memory fixtures intercept calls to external dependencies, providing controlled responses for testing. The CI pipeline orchestrates the execution flow, running tests sequentially or in parallel as configured, and feeding results into reporting tools.

## 6. Alignment with SPARC and AI Verifiable Outcomes

This architecture is fundamentally aligned with the SPARC framework:

-   **Specification:** The architecture is derived from and supports the goals and tests defined in the Specification phase documents ([Master Project Plan](docs/Master Project Plan.md), [Master Acceptance Test Plan](docs/MasterAcceptanceTestPlan.md)).
-   **Preparation:** The modular design and emphasis on testability directly enable the scaffolding and setup tasks in the Preparation phase.
-   **Acceptance:** The architecture provides the structure and tools necessary to implement the various test categories and achieve the initial passing results defined in the Acceptance phase.
-   **Run:** The integrated CI pipeline is the core of the Run phase, automating test execution and reporting.
-   **Completion:** The comprehensive testing framework and automated reporting facilitate the final verification and sign-off in the Completion phase.

The architecture directly supports the AI verifiable outcomes by providing the necessary structure and integrating tools that produce verifiable outputs (e.g., test reports, coverage reports, scan results) that can be checked automatically.

## 7. Identified Needs and Future Considerations

Based on this high-level architecture, the immediate needs for the next phases include:

-   **Scaffolding:** Creation of the proposed `WalletFramework.BDDE2E.Tests` and `WalletFramework.Performance.Tests` projects, if not already present.
-   **Implementation:** Writing the actual test code within the test projects for all categories and modules, guided by the high-level acceptance tests and any future granular test plans.
-   **Configuration:** Detailed configuration of the CI pipeline, including setting up test execution, reporting, and artifact publishing.
-   **Fixture Development:** Further development and refinement of mock objects and in-memory fixtures to cover all necessary dependencies.
-   **Addressing Knowledge Gaps:** As noted in the Master Acceptance Test Plan, further detailed design and implementation will be needed in future cycles to address specific knowledge gaps and refine testing strategies for complex scenarios.

## 8. Conclusion

The defined high-level architecture provides a solid foundation for building the automated testing framework for WalletFramework.NET. Its modularity, focus on testability, and integration with automated pipelines directly support the project's goals and the AI verifiable outcomes outlined in the Master Project Plan and Master Acceptance Test Plan. This document serves as a guide for human programmers to understand the design, implement the testing framework, and ensure alignment with the project's objectives.